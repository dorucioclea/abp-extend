﻿using System.Collections.Generic;

namespace Taitans.Abp.OcelotManagement
{
    public class ReRouteRateLimitRuleDto
    {
        public virtual List<string> ClientWhitelist { get; set; }
    }
}