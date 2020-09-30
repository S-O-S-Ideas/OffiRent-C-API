﻿using OffiRent.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Repositories
{
    public interface IOfficeRepository
    {
        Task<IEnumerable<Office>> ListAsync();
        Task<IEnumerable<Office>> ListByDistrictIdAsync(int districtId);
        Task AddAsync(Office office);
        Task<Office> FindById(int id);
        void Update(Office office);
        void Remove(Office office);
    }
}
