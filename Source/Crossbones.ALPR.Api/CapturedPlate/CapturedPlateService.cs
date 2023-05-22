using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models;
using Corssbones.ALPR.Business.Enums;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Sequence.Common.Interfaces;
using Corssbones.ALPR.Business.HotList.Get;
using Crossbones.ALPR.Business.HotList.Add;
using Crossbones.ALPR.Business.HotList.Change;
using Crossbones.ALPR.Business.HotList.Delete;
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
                UnitId = capturedPlate.UnitName,
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
                UnitId = capturedPlateItem.UnitName,
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

            capturedPlateItem.UnitName = capturePlatesSummaryItem.UnitId;
            capturedPlateItem.User = capturePlatesSummaryItem.UserId;
            capturedPlateItem.LoginId = capturePlatesSummaryItem.LoginId;

            return capturedPlateItem;
        }

        public async Task<PagedResponse<CapturedPlateItem>> GetAll(long userID, DateTime startDate, DateTime endDate, Pager paging, GridFilter filter, GridSort sort)
        {
            var capturePlatesSummaryItems = await this._capturePlatesSummary.GetAllWithOutPaging(filter, sort ,userID);

            var capturePlateIds = (from item in capturePlatesSummaryItems
                                   select item.CapturePlateId).ToList();

            var dataQuery = new GetCapturedPlateItem(SysSerial.Empty,
                                                                      GetQueryFilter.All,
                                                                      capturePlateIds,
                                                                      startDate,
                                                                      endDate,
                                                                      filter,
                                                                      paging,
                                                                      sort);
            
            var taskGetCapturedPlates = Inquire<PagedResponse<CapturedPlateItem>>(dataQuery);

            var capturedPlates = await taskGetCapturedPlates;

            foreach (var item in capturedPlates.Data)
            {
                var capturePlatesSummaryItem = capturePlatesSummaryItems.Find(cps => cps.CapturePlateId == item.CapturedPlateId);

                if (capturePlatesSummaryItem != null)
                {
                    item.UnitName = capturePlatesSummaryItem.UnitId;
                    item.User = capturePlatesSummaryItem.UserId;
                    item.LoginId = capturePlatesSummaryItem.LoginId;
                }
               
            }

            return capturedPlates;
        }
    }
}
