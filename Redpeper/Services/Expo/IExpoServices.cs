using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;

namespace Redpeper.Services.Expo
{
    public interface IExpoServices
    {
        Task SendPushNotification(List<NotificationOrderDto> orderDetails);
    }
}
