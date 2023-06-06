using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;

using Crossbones.ALPR.Models.Items;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Common.Queryables;
using Corssbones.ALPR.Business.CapturedPlate.Get;
using Crossbones.Modules.Common;
using Corssbones.ALPR.Business.CapturedPlate.Add;
using Crossbones.Modules.Business;
using Corssbones.ALPR.Business.CapturedPlate.Change;
using Corssbones.ALPR.Business.CapturedPlate.Delete;
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Api.CapturePlatesSummary;
using System.Reflection;
using E = Corssbones.ALPR.Database.Entities;
using System.Drawing;

namespace Crossbones.ALPR.Api.CapturedPlate
{
    public class CapturedPlateService : ServiceBase, ICapturedPlateService
    {
        readonly ISequenceProxy _capturedPlateSequenceProxy;
        readonly ISequenceProxy _userCapturedPlateSequenceProxy;
        ICapturePlatesSummaryService _capturePlatesSummary;

        public CapturedPlateService(ServiceArguments args, ISequenceProxyFactory sequenceProxyFactory, ICapturePlatesSummaryService capturePlatesSummary) : base(args) => (_capturedPlateSequenceProxy , _userCapturedPlateSequenceProxy, _capturePlatesSummary) = (sequenceProxyFactory.GetProxy(ALPRResources.CapturedPlate), sequenceProxyFactory.GetProxy(ALPRResources.UserCapturedPlate), capturePlatesSummary);

        public async Task<SysSerial> Add(CapturedPlateItem capturedPlate)
        {
            var capturedPlateId = new SysSerial(await _capturedPlateSequenceProxy.Next(CancellationToken.None));

            var addCapturedPlateCommand = new AddCapturedPlateItem(capturedPlateId, capturedPlate);
            var chainCommand = new ChainCommand(addCapturedPlateCommand);

            var userCapturePlateId = new SysSerial(await _userCapturedPlateSequenceProxy.Next(CancellationToken.None));

            var addUserCapturedPlateCommand = new AddUserCapturedPlateItem(userCapturePlateId,
                                                                                                capturedPlate.User,
                                                                                                capturedPlateId);
            chainCommand += addUserCapturedPlateCommand;

            var addCapturePlatesSummaryCommand = new AddCapturePlatesSummaryItem(new SysSerial(0), new CapturePlatesSummaryItem()
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

        public async Task Change(SysSerial capturedPlateSysSerial, CapturedPlateItem capturedPlateItem)
        {
            var changeCapturePlateCommand = new ChangeCapturedPlateItem(capturedPlateSysSerial, capturedPlateItem);

            var chainCommand = new ChainCommand(changeCapturePlateCommand);

            var changeCapturePlateSummaryCommand = new ChangeCapturePlatesSummaryItem(new SysSerial(0), new CapturePlatesSummaryItem()
            {
                CapturePlateId = capturedPlateSysSerial,
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

        public async Task Delete(SysSerial capturedPlateSysSerial)
        {
            var deleteCapturePlateCommand = new DeleteCapturedPlateItem(capturedPlateSysSerial,
                                                                                                 DeleteCommandFilter.Single);
            var chainCommand = new ChainCommand(deleteCapturePlateCommand);

            var deletedUserCapturedPlateCommand = new DeleteUserCapturedPlateItem(new SysSerial(0),
                                                                                                          DeleteCommandFilter.Single,
                                                                                                          0,
                                                                                                          capturedPlateSysSerial);
            chainCommand += deletedUserCapturedPlateCommand;

            var deleteCapturePlateSummaryCommand = new DeleteCapturePlatesSummaryItem(new SysSerial(0),
                                                                                                 DeleteCommandFilter.Single,
                                                                                                 capturedPlateId: capturedPlateSysSerial);
            chainCommand += deleteCapturePlateSummaryCommand;

            _ = await Execute(chainCommand);
        }

        public async Task DeleteAll(long userId)
        {
            var deleteCapturePlateItemCommand = new DeleteCapturedPlateItem(SysSerial.Empty,
                                                                                      userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                      userId);
            var chainCommand = new ChainCommand(deleteCapturePlateItemCommand);

            var deleteUserCapturePlateCommand = new DeleteUserCapturedPlateItem(new SysSerial(0),
                                                                                            userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                            userId);
            chainCommand += deleteUserCapturePlateCommand;

            var deleteCapturePlateSummaryCommand = new DeleteCapturePlatesSummaryItem(new SysSerial(0),
                                                                                                 userId > 0 ? DeleteCommandFilter.AllOfUser : DeleteCommandFilter.All,
                                                                                                 userId: userId);
            chainCommand += deleteCapturePlateSummaryCommand;

            _ = await Execute(chainCommand);
        }

        public async Task<CapturedPlateItem> Get(SysSerial capturedPlateId)
        {
            var capturePlatesSummaryItem = await this._capturePlatesSummary.Get(0, capturedPlateId);
            var query = new GetCapturedPlateItem(capturedPlateId, GetQueryFilter.Single);
            var capturedPlateItem = await Inquire<CapturedPlateItem>(query);

            capturedPlateItem.UnitId = capturePlatesSummaryItem.UnitId;
            capturedPlateItem.User = capturePlatesSummaryItem.UserId;
            capturedPlateItem.LoginId = capturePlatesSummaryItem.LoginId;

            return capturedPlateItem;
        }

        public async Task<PageResponse<CapturedPlateItem>> GetAll(long userID, DateTime startDate, DateTime endDate, Pager paging, GridFilter filter, GridSort sort, List<long> hotListIds)
        {
            GridFilter summaryFilters = new GridFilter()
            {
                Field = filter.Field,
                FieldType = filter.FieldType,
                Logic = filter.Logic,
                Operator = filter.Operator,
                Value = filter.Value,
                Filters = filter.Filters.Filter(f =>
                            typeof(CapturePlatesSummaryItem).GetProperty(f.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null
                          ).ToList()
            };
            

            var capturePlatesSummaryItems = await this._capturePlatesSummary.GetAllWithOutPaging(summaryFilters, sort ,userID);

            var capturePlateIds = (from item in capturePlatesSummaryItems
                                   select item.CapturePlateId).ToList();

            bool summarySortApplied = false;

            if (sort != null && !string.IsNullOrEmpty(sort.Field))
            {
                summarySortApplied = typeof(CapturePlatesSummaryItem).GetProperty(sort.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
            }

            GridFilter capturedPlatesFilters = new GridFilter()
            {
                Field = filter.Field,
                FieldType = filter.FieldType,
                Logic = filter.Logic,
                Operator = filter.Operator,
                Value = filter.Value,
                Filters = filter.Filters.Filter(f =>
                            typeof(CapturedPlateItem).GetProperty(f.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null &&
                            typeof(CapturePlatesSummaryItem).GetProperty(f.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null
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

            var dataQuery = new GetCapturedPlateItem(SysSerial.Empty,
                                                     summarySortApplied || latitudeFilterExist || longitudeFilterExist ? GetQueryFilter.AllWithoutPaging :GetQueryFilter.All,
                                                     capturePlateIds,
                                                     startDate,
                                                     endDate,
                                                     capturedPlatesFilters,
                                                     paging,
                                                     sort,
                                                     hotListIds);

            PageResponse<CapturedPlateItem> capturedPlates = null;

            if (summarySortApplied || latitudeFilterExist || longitudeFilterExist)
            {
                var taskGetCapturedPlates = Inquire<List<CapturedPlateItem>>(dataQuery);

                 List<CapturedPlateItem> capturedPlatesItems = await taskGetCapturedPlates;

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

                capturedPlates = new PageResponse<CapturedPlateItem>(capturedPlatesItems, totalCount);
            }
            else
            {
                var taskGetCapturedPlates = Inquire<PageResponse<CapturedPlateItem>>(dataQuery);

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
