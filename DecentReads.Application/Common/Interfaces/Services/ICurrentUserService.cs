﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}
