﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SIEG_API.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string IdCardFront { get; set; }
        public string IdCardBack { get; set; }
        public string Access { get; set; }
        public bool? ValIdity { get; set; }
        public DateTime AddTime { get; set; }
        public string Name { get; set; }
        public int BuyerGrade { get; set; }
        public int SellerGrade { get; set; }
        public int? BankId { get; set; }
        public int? BankAccount { get; set; }
        public string CreditCard { get; set; }
        public string CreditCardDate { get; set; }
        public int? CreditCardCcv { get; set; }
        public string BillingAddress { get; set; }
    }
}