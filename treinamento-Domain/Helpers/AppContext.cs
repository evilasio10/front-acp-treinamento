﻿using Microsoft.AspNetCore.Http;

namespace treinamento_Domain.Helpers
{
    public class AppContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor != null ? _httpContextAccessor.HttpContext : null;
    }
}
