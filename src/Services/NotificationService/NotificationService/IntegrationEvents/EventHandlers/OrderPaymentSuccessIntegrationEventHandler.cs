﻿using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationService.Api.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.IntegrationEvents.EventHandlers
{
	 class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
	{

		private readonly ILogger<OrderPaymentSuccessIntegrationEventHandler> logger;

		public OrderPaymentSuccessIntegrationEventHandler(ILogger<OrderPaymentSuccessIntegrationEventHandler> logger)
		{
			this.logger = logger;
		}

		public Task Handle(OrderPaymentSuccessIntegrationEvent @event)
		{

			logger.LogInformation($"Payment Success with order  Id {@event.OrderId}");
			return Task.CompletedTask;
		}
	}
}
