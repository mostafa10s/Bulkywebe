﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? CompanyId;

        [Required]
        public string? Name{get;set;}
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? state { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
