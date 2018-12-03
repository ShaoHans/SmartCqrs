using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Domain.Events
{
    /// <summary>
    /// 积分任务领域事件
    /// </summary>
    public class UserPointTaskHappenedDomainEvent : INotification
    {
        public Guid UserId { get; }

        public PointTaskType PointTaskType { get; }

        public UserPointTaskHappenedDomainEvent(Guid userId, PointTaskType pointTaskType)
        {
            UserId = userId;
            PointTaskType = pointTaskType;
        }
    }
}
