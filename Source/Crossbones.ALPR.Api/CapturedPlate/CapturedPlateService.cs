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

        public async Task<PageResponse<CapturedPlateDTO>> GetAll(long userID, DateTime startDate, DateTime endDate, Pager paging, GridFilter filter, GridSort sort, long hotListId)
        {
            var dataQuery = new GetCapturedPlateItem(RecId.Empty,
                                                     GetQueryFilter.All,
                                                     userID,
                                                     startDate,
                                                     endDate,
                                                     filter,
                                                     paging,
                                                     sort,
                                                     hotListId);

            var taskGetCapturedPlates = Inquire<PageResponse<CapturedPlateDTO>>(dataQuery);

            var capturedPlates = await taskGetCapturedPlates;
                        
            return capturedPlates;
        }
    }
}
