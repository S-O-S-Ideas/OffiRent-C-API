﻿using Supermarket.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> ListAsync();
        Task AddAsync(Country country);
        Task<Country> FindById(int id);
        void Remove(Country country);
        void Update(Country country);
        
    }
}
