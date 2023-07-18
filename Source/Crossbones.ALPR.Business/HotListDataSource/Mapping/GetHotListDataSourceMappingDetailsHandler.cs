using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using Crossbones.Modules.Common.Exceptions;
using CsvHelper;
using CsvHelper.Configuration;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using DTO = Crossbones.ALPR.Models.DTOs;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.HotListDataSource.Mapping
{
    public class GetHotListDataSourceMappingDetailsHandler : QueryHandlerBase<GetHotListDataSourceMappingDetails>
    {
        private const string FTP_SOURCE_TAG = "FTP";
        private const string UNC_SOURCE_TAG = "UNC";

        readonly IMapper mapper;

        public GetHotListDataSourceMappingDetailsHandler(IMapper _mapper) => mapper = _mapper;

        protected override async Task<object> OnQuery(GetHotListDataSourceMappingDetails query, IQueryContext context, CancellationToken token)
        {
            if (string.IsNullOrEmpty(query.DataSourceItem.LocationPath)) { throw new CannotBeEmpty(nameof(query.DataSourceItem.LocationPath)); }
            if (string.IsNullOrEmpty(query.DataSourceItem.ConnectionType)) { throw new CannotBeEmpty(nameof(query.DataSourceItem.ConnectionType)); }
            if (string.IsNullOrEmpty(query.DataSourceItem.SchemaDefinition)) { throw new CannotBeEmpty(nameof(query.DataSourceItem.SchemaDefinition)); }

            var schemaDefinition = JsonConvert.DeserializeObject<DTO.HotListDataSourceMappingDTO>(query.DataSourceItem.SchemaDefinition!);

            var records = query.DataSourceItem.ConnectionType switch
            {
                FTP_SOURCE_TAG => await GetRecordsFromFTP(schemaDefinition!, query.DataSourceItem.LocationPath, token),
                UNC_SOURCE_TAG => await GetRecordsFromUNC(schemaDefinition!, query.DataSourceItem.LocationPath, token),
                _ => await GetRecordsFromLocal(schemaDefinition!, query.DataSourceItem.LocationPath, token)
            };

            var stateRepository = context.Get<E.State>()
                .Many(token);

            var _numberPlateListHasNoEntryInMappingRepository = context.Get<E.NumberPlate>()
                .Many(token)
                .Include(x => x.State)
                .Include(x => x.HotListNumberPlates);

            var data = records
                              .Join(stateRepository, file => file.State, state => state.StateName, (file, state) => new { FileData = file, StateInfo = state })
                              //Left Data Source
                              //Performing Group join with Right Data Source
                              .GroupJoin(_numberPlateListHasNoEntryInMappingRepository, //Right Data Source
                                    file => file.FileData.LicensePlate, //Outer Key Selector, i.e. Left Data Source Common Property
                                    db => db.LicensePlate, //Inner Key Selector, i.e. Right Data Source Common Property
                                    (file, db) => new { FileData = file.FileData, Database = db, StateInfo = file.StateInfo }) //Projecting the Result
                              .SelectMany(
                                    x => x.Database.DefaultIfEmpty(), //Performing Left Outer Join 
                                    (file, db) => new { FileData = file.FileData, Database = db, StateInfo = file.StateInfo }) //Final Result Set
                              .Where(x => x.Database == null || x.Database.HotListNumberPlates.Count == 0)
                              .ToList();

            var res = data.Select(x => new DTO.NumberPlateDTO
            {
                NeedFullInsertion = x.Database is null,
                RecId = x.Database is null ? 0 : x.Database.RecId,
                InsertType = 1,
                LicensePlate = x.FileData.LicensePlate,
                DateOfInterest = DateTime.Parse(x.FileData.DateOfInterest),
                LicenseYear = x.FileData.LicenseYear,
                LicenseType = x.FileData.LicenseType,
                AgencyId = "1077", //x.FileData.Agency, Convert Agency Name to Agency ID
                FirstName = x.FileData.FirstName,
                LastName = x.FileData.LastName,
                Alias = x.FileData.Alias,
                VehicleYear = x.FileData.VehicleYear,
                VehicleMake = x.FileData.VehicleMake,
                VehicleModel = x.FileData.VehicleModel,
                VehicleColor = x.FileData.VehicleColor,
                VehicleStyle = x.FileData.VehicleStyle,
                StateName = x.StateInfo.StateName,

                Notes = x.FileData.Notes,
                NCICNumber = x.FileData.NCICNumber,
                ImportSerialId = x.FileData.ImportSerial,
                ViolationInfo = x.FileData.ViolationInfo,
                StateId = x.StateInfo.RecId,
                Note = "Note from Handler",
                Status = 1, // Need to confirm what status is
                CreatedOn = DateTime.Now,
                LastUpdatedOn = DateTime.Now,
            });

            return res;
        }

        private async Task<IEnumerable<DTO.HotListDataSourceMappingDTO>> GetCSVRecords(
            DTO.HotListDataSourceMappingDTO schemaDefinition,
            Stream _stream,
            CancellationToken token)
        {
            using var reader = new StreamReader(_stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap(new HotListDataSourceMappingProfile(schemaDefinition));
            return await csv.GetRecordsAsync<DTO.HotListDataSourceMappingDTO>(token).ToListAsync(token);
        }

        private async Task<IEnumerable<DTO.HotListDataSourceMappingDTO>> GetRecordsFromUNC(
           DTO.HotListDataSourceMappingDTO schemaDefinition,
           string URL,
           CancellationToken token)
        {
            var request = WebRequest.Create(URL) ?? throw new InvalidValue($"Source: {UNC_SOURCE_TAG} against invalid Path: {URL}");

            try
            {
                using var response = await request.GetResponseAsync();
                using var responseStream = response.GetResponseStream();
                return await GetCSVRecords(schemaDefinition, responseStream, token);
            }
            catch (WebException ex)
            {
                throw new WebException($"Unable to connect with {UNC_SOURCE_TAG} location : {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new WebException($"Unable to connect with {UNC_SOURCE_TAG} location : {ex.Message}");
            }
        }

        private async Task<IEnumerable<DTO.HotListDataSourceMappingDTO>> GetRecordsFromLocal(
            DTO.HotListDataSourceMappingDTO schemaDefinition,
            string path,
        CancellationToken token)
        {
            if (File.Exists(path))
            {
                using var responseStream = File.OpenRead(path);
                return await GetCSVRecords(schemaDefinition, responseStream, token);
            }
            else
            {
                throw new FileNotFoundException($"File not found at the location {path}");
            }
        }

        private async Task<IEnumerable<DTO.HotListDataSourceMappingDTO>> GetRecordsFromFTP(
            DTO.HotListDataSourceMappingDTO schemaDefinition,
            string ftpPath,
            CancellationToken token,
            string UserName = "anonymous",
            string Password = "anonymous@domain.com"
            )
        {
            var request = WebRequest.Create(ftpPath) as FtpWebRequest ?? throw new InvalidValue($"Source: {FTP_SOURCE_TAG} against invalid Path: {ftpPath}");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            try
            {
                using var response = await request.GetResponseAsync() as FtpWebResponse;
                using var responseStream = response!.GetResponseStream();
                return await GetCSVRecords(schemaDefinition, responseStream, token);
            }
            catch (WebException ex)
            {
                throw new WebException($"Unable to connect with {FTP_SOURCE_TAG} location : {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new WebException($"Unable to connect with {FTP_SOURCE_TAG} location : {ex.Message}");
            }
        }
    }

    public sealed class HotListDataSourceMappingProfile : ClassMap<DTO.HotListDataSourceMappingDTO>
    {
        public HotListDataSourceMappingProfile(DTO.HotListDataSourceMappingDTO _object)
        {
            Map(m => m.ImportSerial).Name(_object.ImportSerial).Optional();
            Map(m => m.NCICNumber).Name(_object.NCICNumber).Optional();
            Map(m => m.Agency).Name(_object.Agency).Optional();
            Map(m => m.DateOfInterest).Name(_object.DateOfInterest).Optional();
            Map(m => m.LicensePlate).Name(_object.LicensePlate).Optional();
            Map(m => m.State).Name(_object.State).Optional();
            Map(m => m.LicenseYear).Name(_object.LicenseYear).Optional();
            Map(m => m.LicenseType).Name(_object.LicenseType).Optional();
            Map(m => m.VehicleYear).Name(_object.VehicleYear).Optional();
            Map(m => m.VehicleMake).Name(_object.VehicleMake).Optional();
            Map(m => m.VehicleModel).Name(_object.VehicleModel).Optional();
            Map(m => m.VehicleStyle).Name(_object.VehicleStyle).Optional();
            Map(m => m.VehicleColor).Name(_object.VehicleColor).Optional();
            Map(m => m.FirstName).Name(_object.FirstName).Optional();
            Map(m => m.LastName).Name(_object.LastName).Optional();
            Map(m => m.Alias).Name(_object.Alias).Optional();
            Map(m => m.ViolationInfo).Name(_object.ViolationInfo).Optional();
            Map(m => m.Notes).Name(_object.Notes).Optional();
        }
    }
}
