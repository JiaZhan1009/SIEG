﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SIEG_API.Models
{
    public partial class ContactCustomerService
    {
        public int ContactId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string InnerText { get; set; }
        public string State { get; set; }
        public DateTime ContactTime { get; set; }
        public string Category { get; set; }
    }
}