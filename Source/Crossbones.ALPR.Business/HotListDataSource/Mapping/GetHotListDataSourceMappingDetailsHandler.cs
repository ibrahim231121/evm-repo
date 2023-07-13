using AutoMapper;
using Crossbones.Modules.Business.Contexts;
using Crossbones.Modules.Business.Handlers.Query;
using CsvHelper;
using CsvHelper.Configuration;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using DTO = Crossbones.ALPR.Models.DTOs;
using E = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.HotListDataSource.Mapping
{
    public class GetHotListDataSourceMappingDetailsHandler : QueryHandlerBase<GetHotListDataSourceMappingDetails>
    {
        readonly IMapper mapper;

        public GetHotListDataSourceMappingDetailsHandler(IMapper _mapper) => mapper = _mapper;

        private async Task<IEnumerable<DTO.HotListDataSourceMappingDTO>> GetCSVRecords(string locationPath,
            DTO.HotListDataSourceMappingDTO schemaDefinition, CancellationToken token)
        {
            using (var reader = new StreamReader(locationPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap(new HotListDataSourceMappingProfile(schemaDefinition));
                return await csv.GetRecordsAsync<DTO.HotListDataSourceMappingDTO>(token).ToListAsync(token);
            }
        }

        protected override async Task<object> OnQuery(GetHotListDataSourceMappingDetails query, IQueryContext context, CancellationToken token)
        {
            var schemaDefinition = JsonConvert.DeserializeObject<DTO.HotListDataSourceMappingDTO>(query.DataSourceItem.SchemaDefinition);
            var records = await GetCSVRecords(query.DataSourceItem.LocationPath, schemaDefinition, token);

            var _repositoryNumberPlate = context.Get<E.NumberPlate>();

            var _numberPlateListHasNoEntryInMappingTable = _repositoryNumberPlate.Many().Include(x => x.HotListNumberPlates);

            var data = records //Left Data Source
                               //Performing Group join with Right Data Source
                              .GroupJoin(_numberPlateListHasNoEntryInMappingTable, //Right Data Source
                                    file => file.LicensePlate, //Outer Key Selector, i.e. Left Data Source Common Property
                                    db => db.LicensePlate, //Inner Key Selector, i.e. Right Data Source Common Property
                                    (file, db) => new { FileData = file, Database = db }) //Projecting the Result
                              .SelectMany(
                                    x => x.Database.DefaultIfEmpty(), //Performing Left Outer Join 
                                    (file, db) => new { FileData = file.FileData, Database = db }) //Final Result Set
                              .Where(x => x.Database == null || x.Database.HotListNumberPlates.Count == 0)
                              .ToList();

            var res = data.Select(x => new DTO.NumberPlateDTO
            {
                NeedFullInsertion = x.Database is null,
                RecId = x.Database is null ? 0 : x.Database.RecId,

                LicensePlate = x.FileData.LicensePlate,
                DateOfInterest = Convert.ToDateTime(x.FileData.DateOfInterest),
                LicenseType = x.FileData.LicenseType,
                AgencyId = x.FileData.Agency,
                //StateId = x.FileData.State,
                FirstName = x.FileData.FirstName,
                LastName = x.FileData.LastName,
                Alias = x.FileData.Alias,
                VehicleYear = x.FileData.Year,
                VehicleMake = x.FileData.Make,
                VehicleModel = x.FileData.Model,
                VehicleColor = x.FileData.Color,
                VehicleStyle = x.FileData.Style,
                Notes = x.FileData.Notes,
                NCICNumber = x.FileData.NCICNumber,
                ImportSerialId = x.FileData.ImportSerial,
                ViolationInfo = x.FileData.ViolationInfo,
                CreatedOn = DateTime.Now,
                LastUpdatedOn = DateTime.Now,
            });
            return res;
        }
    }

    public sealed class HotListDataSourceMappingProfile : ClassMap<DTO.HotListDataSourceMappingDTO>
    {
        public HotListDataSourceMappingProfile(DTO.HotListDataSourceMappingDTO _object)
        {
            Map(m => m.LicensePlate).Name(_object.LicensePlate).Optional();
            Map(m => m.DateOfInterest).Name(_object.DateOfInterest).Optional();
            Map(m => m.LicenseType).Name(_object.LicenseType).Optional();
            Map(m => m.Agency).Name(_object.Agency).Optional();
            Map(m => m.State).Name(_object.State).Optional();
            Map(m => m.FirstName).Name(_object.FirstName).Optional();
            Map(m => m.LastName).Name(_object.LastName).Optional();
            Map(m => m.Alias).Name(_object.Alias).Optional();
            Map(m => m.Year).Name(_object.Year).Optional();
            Map(m => m.Make).Name(_object.Make).Optional();
            Map(m => m.Model).Name(_object.Model).Optional();
            Map(m => m.Color).Name(_object.Color).Optional();
            Map(m => m.Style).Name(_object.Style).Optional();
            Map(m => m.Notes).Name(_object.Notes).Optional();
            Map(m => m.NCICNumber).Name(_object.NCICNumber).Optional();
            Map(m => m.ImportSerial).Name(_object.ImportSerial).Optional();
            Map(m => m.ViolationInfo).Name(_object.ViolationInfo).Optional();
        }
    }
}
