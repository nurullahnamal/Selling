using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationService.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.IntegrationEvents.EventHandlers
{
	public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
	{

		private readonly ILogger<OrderPaymentFailedIntegrationEventHandler> logger;

		public OrderPaymentFailedIntegrationEventHandler(ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
		{
			this.logger = logger;
		}

		public Task Handle(OrderPaymentFailedIntegrationEvent @event)
		{
			logger.LogInformation($"Payment failed with order Id {@event.OrderId}, errorMessage : {@event.ErrorMessage}");
			return Task.CompletedTask;	
		}
	}
}
