﻿using Microsoft.EntityFrameworkCore;
using OffiRent.API.Domain.Models;
using OffiRent.API.Domain.Persistence.Contexts;
using OffiRent.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;
using Castle.Core.Internal;

namespace OffiRent.API.Persistence.Repositories
{
    public class ReservationRepository: BaseRepository, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task<Reservation> FindById(int Id)
        {
            return await _context.Reservations.FindAsync(Id);
        }

        public async Task<IEnumerable<Reservation>> ListAccountReservationsAsync(int accountId, [Optional] string status)
        {
            if (!status.IsNullOrEmpty())
            {
                return await _context.Reservations
                .Where(p => p.AccountId == accountId)
                .Where(p => p.Status == status)
                .ToListAsync();
            }
            return await _context.Reservations
                .Where(p => p.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> ListAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> ListByAccountIdAsync(int accountId)
        {
            return await _context.Reservations
                .Where(p => p.AccountId == accountId)
                .ToListAsync();      
        }

        public void Remove(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
        }

        public void Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }

     
    }
}
