﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SIEG_API.Models
{
    public partial class SellerAddProduct
    {
        public int SellerAddProductId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int MemberId { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? SaleDate { get; set; }
        public int Price { get; set; }
        public bool? ValIdity { get; set; }

        public virtual Product Product { get; set; }
    }
}