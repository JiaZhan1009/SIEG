﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SIEG_API.Models
{
    public partial class BuyerBid
    {
        public int BuyerBidId { get; set; }
        public int MemberId { get; set; }
        public int Price { get; set; }
        public DateTime BidTime { get; set; }
        public bool? ValIdity { get; set; }
        public int ProductId { get; set; }
        public DateTime EffectiveTime { get; set; }
        public int Count { get; set; }

        public virtual Product Product { get; set; }
    }
}