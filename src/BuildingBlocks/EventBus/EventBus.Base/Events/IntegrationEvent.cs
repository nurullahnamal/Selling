﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {

        [JsonProperty]

        public Guid Id { get; private set; }
        [JsonProperty]
        public DateTime CreatedDate { get; private set; }


        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        [System.Text.Json.Serialization.JsonConstructor]

        public IntegrationEvent(Guid ıd, DateTime createdDate)
        {
            Id = ıd;
            CreatedDate = createdDate;
        }
    }
}
