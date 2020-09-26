﻿using Supermarket.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services.Communications
{
    public class CountryResponse : BaseResponse<Country>
    {
        public CountryResponse(Country resource) : base(resource)
        {
        }

        public CountryResponse(string message) : base(message)
        {

        }
    }
}
