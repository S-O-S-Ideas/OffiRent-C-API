﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffiRent.API.Domain.Models
{
    public class Account 
    {
        public int Id { get; set; }
        public bool IsPremium { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<Publication> Publications { get; set; } = new List<Publication>();
        public List<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();

    }
}