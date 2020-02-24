﻿using System;
using Volo.Abp.Domain.Entities;

namespace Taitans.Abp.OcelotManagement
{
    public class OcelotServiceDiscoveryProvider : Entity
    {
        public virtual Guid GlobalConfigurationId { get; protected set; }
        public virtual string Host { get; set; }
        public virtual string Namespace { get; set; }
        public virtual int PollingInterval { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { GlobalConfigurationId };
        }
    }
}