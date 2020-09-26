﻿using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Services.Communications
{
    public class CurrencyResponse : BaseResponse<Currency>
    {

        public CurrencyResponse(Currency resource) : base(resource)
        {
        }

        public CurrencyResponse (string message) : base(message)
        {

        }
    }
}
