﻿using Aseme.Shared.Infrastructure.Http;

namespace Hsc.Logins.Domain
{
    public class LoginFilter : PaginateFilter
    {
        public string UserName { get; set; }
    }
}