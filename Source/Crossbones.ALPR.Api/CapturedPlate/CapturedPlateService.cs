using Corssbones.ALPR.Business.CapturedPlate.Add;
using Corssbones.ALPR.Business.CapturedPlate.Change;
using Corssbones.ALPR.Business.CapturedPlate.Delete;
using Corssbones.ALPR.Business.CapturedPlate.Get;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Api.CapturePlatesSummary;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Business;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Modules.Sequence.Common.Interfaces;
using System.Reflection;

namespace Crossbones.ALPR.Api.CapturedPlate
{
    public class CapturedPlateService : ServiceBase, ICapturedPlateService
    {
        readonly ISequenceProxy _capturedPlateSequenceProxy;
        readonly ISequenceProxy _userCapturedPlateSequenceProxy;
        ICapturePlatesSummaryService _capturePlatesSummary;

        public CapturedPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory, ICapturePlatesSummaryService capturePlatesSummary) : base(args) => (_capturedPlateSequenceProxy, _userCapturedPlateSequenceProxy, _capturePlatesSummary) = (sequenceProxyFactory.GetProxy(ALPRResources.CapturedPlate), sequenceProxyFactory.GetProxy(ALPRResources.UserCapturedPlate), capturePlatesSummary);

        public async Task<RecId> Add(CapturedPlateDTO capturedPlate)
        {
            var capturedPlateId = new RecId(await _capturedPlateSequenceProxy.Next(CancellationToken.None));

            var addCapturedPlateCommand = new AddCapturedPlateItem(capturedPlateId, capturedPlate);
            var chainCommand = new ChainCommand(addCapturedPlateCommand);

            var userCapturePlateId = new RecId(await _userCapturedPlateSequenceProxy.Next(CancellationToken.None));

            var addUserCapturedPlateCommand = new AddUserCapturedPlateItem(userCapturePlateId,
                                                                                                capturedPlate.User,
                                                                                                capturedPlateId);
            chainCommand += addUserCapturedPlateCommand;

            var addCapturePlatesSummaryCommand = new AddCapturePlatesSummaryItem(new RecId(0), new CapturePlatesSummaryDTO()
            {
                CapturePlateId = capturedPlateId,
                UserId = capturedPlate.User,
                UnitId = capturedPlate.UnitId,
                CaptureDate = capturedPlate.CapturedAt,
                LoginId = capturedPlate.LoginId,
                StationId = Convert.ToInt32(this.GetTenantId()),
                ClientId = 1,
                HasAlert = false,
                HasTicket = true
            });

            chainCommand += addCapturePlatesSummaryCommand;

            _ = await Execute(chainCommand);
            return capturedPlateId;
        }

        public async Task Change(RecId capturedPlateRecId, CapturedPlateDTO capturedPlateItem)
        {
            var changeCapturePlateCommand = new ChangeCapturedPlateItem(capturedPlateRecId, capturedPlateItem);

            var chainCommand = new ChainCommand(changeCapturePlateCommand);

            var changeCapturePlateSummaryCommand = new ChangeCapturePlatesSummaryItem(new RecId(0), new CapturePlatesSummaryDTO()
            {
                CapturePlateId = capturedPlateRecId,
                UserId = capturedPlateItem.User,
                UnitId = capturedPlateItem.UnitId,
                CaptureDate = capturedPlateItem.CapturedAt,
                LoginId = capturedPlateItem.LoginId,
                StationId = Convert.ToInt32(this.GetTenantId()),
                ClientId = 1,
                HasAlert = false,
                HasTicket = true
            });

            chainCommand += changeCapturePlateSummaryCommand;

            _ = await Execute(chainCommand);
        }

        public async Task Delete(RecId capturedPlateRecId)
        {
            var deleteCapturePlateCommand = new DeleteCapturedPlateItem(capturedPlateRecId,
                                                                                                 DeleteCommandFilter.Single);
            var chainCommand = new ChainCommand(deleteCapturePlateCommand);

            var deletedUserCapturedPlateCommand = new DeleteUserCapturedPlateItem(new RecId(0),
                                                                                                          DeleteCommandFilter.Single,
                                                                                                          0,
                                                                                                          capturedPlateRecId);
            chainCommand += deletedUserCapturedPlateCommand;

            var deleteCapturePlateSummaryCommand = new DeleteCapturePlatesSummaryItem(new RecId(0),
                                                                                                 DeleteCommandFilter.Single,
                                                                                                 capturedPlateId: capturedPlateRecId);
            chainCommand += deleteCapturePlateSummaryCommand;

            _ = await Execute(chainCommand);
        }

        public async Task DeleteAll(long userId)
        {
            var deleteCapturePlateItemCommand = new DeleteCapturedPlateItem(RecId.Empty,
                                                                                      userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                      userId);
            var chainCommand = new ChainCommand(deleteCapturePlateItemCommand);

            var deleteUserCapturePlateCommand = new DeleteUserCapturedPlateItem(new RecId(0),
                                                                                            userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                            userId);
            chainCommand += deleteUserCapturePlateCommand;

            var deleteCapturePlateSummaryCommand = new DeleteCapturePlatesSummaryItem(new RecId(0),
                                                                                                 userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                                 userId: userId);
            chainCommand += deleteCapturePlateSummaryCommand;

            _ = await Execute(chainCommand);
        }

        public async Task<CapturedPlateDTO> Get(RecId capturedPlateId)
        {
            var capturePlatesSummaryItem = await this._capturePlatesSummary.Get(0, capturedPlateId);
            var query = new GetCapturedPlateItem(capturedPlateId, GetQueryFilter.Single);
            var capturedPlateItem = await Inquire<CapturedPlateDTO>(query);

            capturedPlateItem.UnitId = capturePlatesSummaryItem.UnitId;
            capturedPlateItem.User = capturePlatesSummaryItem.UserId;
            capturedPlateItem.LoginId = capturePlatesSummaryItem.LoginId;

            return capturedPlateItem;
        }

        public async Task<PageResponse<CapturedPlateDTO>> GetAll(long userID, DateTime startDate, DateTime endDate, Pager paging, GridFilter filter, GridSort sort, List<long> hotListIds)
        {
            GridFilter summaryFilters = new GridFilter()
            {
                Field = filter.Field,
                FieldType = filter.FieldType,
                Logic = filter.Logic,
                Operator = filter.Operator,
                Value = filter.Value,
                Filters = filter.Filters.Filter(f =>
                            typeof(CapturePlatesSummaryDTO).GetProperty(f.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null
                          ).ToList()
            };


            var capturePlatesSummaryItems = await this._capturePlatesSummary.GetAllWithOutPaging(summaryFilters, sort, userID);

            var capturePlateIds = (from item in capturePlatesSummaryItems
                                   select item.CapturePlateId).ToList();

            bool summarySortApplied = false;

            if (sort != null && !string.IsNullOrEmpty(sort.Field))
            {
                summarySortApplied = typeof(CapturePlatesSummaryDTO).GetProperty(sort.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
            }

            GridFilter capturedPlatesFilters = new GridFilter()
            {
                Field = filter.Field,
                FieldType = filter.FieldType,
                Logic = filter.Logic,
                Operator = filter.Operator,
                Value = filter.Value,
                Filters = filter.Filters.Filter(f =>
                            typeof(CapturedPlateDTO).GetProperty(f.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null &&
                            typeof(CapturePlatesSummaryDTO).GetProperty(f.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null
                          ).ToList()
            };

            bool latitudeFilterExist = false;
            bool longitudeFilterExist = false;


            if (capturedPlatesFilters != null && capturedPlatesFilters.Filters != null && capturedPlatesFilters.Filters.Exists(filter => filter.Field == "Latitude"))
            {
                latitudeFilterExist = true;
            }

            if (capturedPlatesFilters != null && capturedPlatesFilters.Filters != null && capturedPlatesFilters.Filters.Exists(filter => filter.Field == "Longitude"))
            {
                longitudeFilterExist = true;
            }

            var dataQuery = new GetCapturedPlateItem(RecId.Empty,
                                                     summarySortApplied || latitudeFilterExist || longitudeFilterExist ? GetQueryFilter.AllWithoutPaging : GetQueryFilter.All,
                                                     capturePlateIds,
                                                     startDate,
                                                     endDate,
                                                     capturedPlatesFilters,
                                                     paging,
                                                     sort,
                                                     hotListIds);

            PageResponse<CapturedPlateDTO> capturedPlates = null;

            if (summarySortApplied || latitudeFilterExist || longitudeFilterExist)
            {
                var taskGetCapturedPlates = Inquire<List<CapturedPlateDTO>>(dataQuery);

                List<CapturedPlateDTO> capturedPlatesItems = await taskGetCapturedPlates;

                int size = paging.Size <= 0 ? 25 : paging.Size;
                int skip = (paging.Page - 1) * paging.Size;
                int totalCount = capturedPlatesItems.Count;

                if (latitudeFilterExist || longitudeFilterExist)
                {
                    capturedPlatesItems = capturedPlatesItems.Where(cpItem => cpItem.Distance < 0.05).ToList();
                }

                if (summarySortApplied)
                {
                    capturedPlatesItems = capturedPlatesItems.OrderBy(cpItem => capturePlateIds.IndexOf(cpItem.CapturedPlateId)).ToList();
                }

                capturedPlatesItems = capturedPlatesItems.Skip(skip).Take(size).ToList();

                capturedPlates = new PageResponse<CapturedPlateDTO>(capturedPlatesItems, totalCount);
            }
            else
            {
                var taskGetCapturedPlates = Inquire<PageResponse<CapturedPlateDTO>>(dataQuery);

                capturedPlates = await taskGetCapturedPlates;
            }


            foreach (var item in capturedPlates.Items)
            {
                var capturePlatesSummaryItem = capturePlatesSummaryItems.Find(cps => cps.CapturePlateId == item.CapturedPlateId);

                if (capturePlatesSummaryItem != null)
                {
                    item.UnitId = capturePlatesSummaryItem.UnitId;
                    item.User = capturePlatesSummaryItem.UserId;
                    item.LoginId = capturePlatesSummaryItem.LoginId;
                }

            }

            return capturedPlates;
        }
    }
}
