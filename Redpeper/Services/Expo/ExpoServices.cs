using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Expo.Server.Client;
using Expo.Server.Models;
using Redpeper.Dto;

namespace Redpeper.Services.Expo
{
    public class ExpoServices : IExpoServices
    {
        public async Task SendPushNotification(List<NotificationOrderDto> orderDetails)
        {
            var expoSDKClient = new PushApiClient();
            var tickets = orderDetails.Select(x => new PushTicketRequest
            {
                PushTitle = x.Title,
                PushTo = new List<string> { x.Token},
                PushBody =$"{ x.OrderDetail.Qty} - {(x.OrderDetail.Combo != null ? x.OrderDetail.Combo.Name : x.OrderDetail.Dish.Name) }" ,
                PushPriority = "high"
            }).ToList();

            var errors  = new List<PushTicketResponse>();

            tickets.ForEach(async x =>
            {
                var result = await expoSDKClient.PushSendAsync(x);
                errors.Add(result);

            });

            errors.ForEach(x =>
            {
                if (x?.PushTicketErrors?.Count() > 0)
                {
                    foreach (var error in x.PushTicketErrors)
                    {
                        Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
                    }
                }
            });
            
        }
    }
}
