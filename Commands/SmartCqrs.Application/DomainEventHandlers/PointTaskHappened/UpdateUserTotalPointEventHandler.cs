using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Application.DomainEventHandlers.PointTaskHappened
{
    public class UpdateUserTotalPointEventHandler : INotificationHandler<UserPointTaskHappenedDomainEvent>
    {
        private readonly IUserAssetRepository _userAssetRepository;

        public UpdateUserTotalPointEventHandler(IUserAssetRepository userAssetRepository)
        {
            _userAssetRepository = userAssetRepository;
        }

        public async Task Handle(UserPointTaskHappenedDomainEvent notification, CancellationToken cancellationToken)
        {
            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.UserId);

            // 实际项目应该采用数据库配置任务对应的积分值
            int point = 0;
            switch (notification.PointTaskType)
            {
                case PointTaskType.Registed:
                    point = 100;
                    break;
                case PointTaskType.PublishBlog:
                    point = 15;
                    break;
                default:
                    break;
            }
            userAsset.AddPoint(point);
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
