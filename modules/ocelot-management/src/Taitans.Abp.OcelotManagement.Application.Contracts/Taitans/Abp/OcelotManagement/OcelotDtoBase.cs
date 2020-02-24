﻿using System;
using Volo.Abp.Application.Dtos;

namespace Taitans.Abp.OcelotManagement
{
    public abstract class OcelotDtoBase : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string RequestIdKey { get; set; }
    }
}