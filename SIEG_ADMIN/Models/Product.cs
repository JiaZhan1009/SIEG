﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SIEG_ADMIN.Models
{
    public partial class Product
    {
        public Product()
        {
            BuyerBid = new HashSet<BuyerBid>();
            ContactAddProduct = new HashSet<ContactAddProduct>();
            FaviriteProduct = new HashSet<FaviriteProduct>();
            Order = new HashSet<Order>();
            SellerAddProduct = new HashSet<SellerAddProduct>();
        }

        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public DateTime AddTime { get; set; }
        public bool? ValIdity { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string ImgFront { get; set; }
        public string ImgBack { get; set; }
        public string ImgSide { get; set; }
        public int? ViewsCount { get; set; }
        public string Model { get; set; }
        public string Note { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<BuyerBid> BuyerBid { get; set; }
        public virtual ICollection<ContactAddProduct> ContactAddProduct { get; set; }
        public virtual ICollection<FaviriteProduct> FaviriteProduct { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<SellerAddProduct> SellerAddProduct { get; set; }
    }
}