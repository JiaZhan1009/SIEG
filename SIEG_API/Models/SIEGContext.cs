﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SIEG_API.Models
{
    public partial class SIEGContext : DbContext
    {
        public SIEGContext()
        {
        }

        public SIEGContext(DbContextOptions<SIEGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ad { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<BuyerBid> BuyerBid { get; set; }
        public virtual DbSet<ContactAddProduct> ContactAddProduct { get; set; }
        public virtual DbSet<ContactCustomerService> ContactCustomerService { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<FaviriteArticle> FaviriteArticle { get; set; }
        public virtual DbSet<FaviriteNews> FaviriteNews { get; set; }
        public virtual DbSet<FaviriteProduct> FaviriteProduct { get; set; }
        public virtual DbSet<ForumArticle> ForumArticle { get; set; }
        public virtual DbSet<ForumReply> ForumReply { get; set; }
        public virtual DbSet<ForumReply2> ForumReply2 { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberCoupon> MemberCoupon { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCategory> NewsCategory { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<SellerAddProduct> SellerAddProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("AD");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.BankCode)
                    .HasName("PK_BankCode");

                entity.Property(e => e.BankCode).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.BannerId).HasColumnName("BannerID");

                entity.Property(e => e.Img).HasMaxLength(100);

                entity.Property(e => e.Link).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.ValIdity).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<BuyerBid>(entity =>
            {
                entity.Property(e => e.BuyerBidId).HasColumnName("BuyerBidID");

                entity.Property(e => e.BidTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Count).HasDefaultValueSql("((1))");

                entity.Property(e => e.EffectiveTime).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SaleTime).HasColumnType("datetime");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.BuyerBid)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BuyerBid__Member__7CCF64C8");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BuyerBid)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BuyerBid__Produc__7DC38901");
            });

            modelBuilder.Entity<ContactAddProduct>(entity =>
            {
                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.Category).HasMaxLength(150);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ContactAddProduct)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__ContactAd__Membe__7EB7AD3A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ContactAddProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ContactAd__Produ__7FABD173");
            });

            modelBuilder.Entity<ContactCustomerService>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__ContactC__3214EC0794049F28");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.ContactTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.InnerText)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sn)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("SN");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<FaviriteArticle>(entity =>
            {
                entity.HasKey(e => e.FaviriteAritcleId)
                    .HasName("PK__Favirite__3214EC07A39CA38B");

                entity.Property(e => e.FaviriteAritcleId).HasColumnName("FaviriteAritcleID");

                entity.Property(e => e.ForumArticleId).HasColumnName("ForumArticleID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ForumArticle)
                    .WithMany(p => p.FaviriteArticle)
                    .HasForeignKey(d => d.ForumArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FaviriteA__Forum__009FF5AC");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.FaviriteArticle)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FaviriteA__Membe__019419E5");
            });

            modelBuilder.Entity<FaviriteNews>(entity =>
            {
                entity.Property(e => e.FaviriteNewsId).HasColumnName("FaviriteNewsID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.NewsId).HasColumnName("NewsID");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.FaviriteNews)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FaviriteN__Membe__02883E1E");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.FaviriteNews)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FaviriteN__NewsI__037C6257");
            });

            modelBuilder.Entity<FaviriteProduct>(entity =>
            {
                entity.Property(e => e.FaviriteProductId).HasColumnName("FaviriteProductID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ValIdity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.FaviriteProduct)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__FaviriteP__Membe__04708690");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FaviriteProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__FaviriteP__Produ__0564AAC9");
            });

            modelBuilder.Entity<ForumArticle>(entity =>
            {
                entity.Property(e => e.ForumArticleId).HasColumnName("ForumArticleID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ArticleContent).IsRequired();

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Img).HasMaxLength(150);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ReplyCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ForumArticle)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForumArti__Membe__0658CF02");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.ForumArticle)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForumArti__Produ__074CF33B");
            });

            modelBuilder.Entity<ForumReply>(entity =>
            {
                entity.Property(e => e.ForumReplyId).HasColumnName("ForumReplyID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ForumArticleId).HasColumnName("ForumArticleID");

                entity.Property(e => e.ForumReplyContent).IsRequired();

                entity.Property(e => e.Img).HasMaxLength(150);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ForumArticle)
                    .WithMany(p => p.ForumReply)
                    .HasForeignKey(d => d.ForumArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForumRepl__Forum__08411774");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ForumReply)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForumRepl__Membe__09353BAD");
            });

            modelBuilder.Entity<ForumReply2>(entity =>
            {
                entity.Property(e => e.ForumReply2Id).HasColumnName("ForumReply2ID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ForumArticleId).HasColumnName("ForumArticleID");

                entity.Property(e => e.ForumReply2Content).IsRequired();

                entity.Property(e => e.ForumReplyId).HasColumnName("ForumReplyID");

                entity.Property(e => e.Img).HasMaxLength(150);

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ForumReply)
                    .WithMany(p => p.ForumReply2)
                    .HasForeignKey(d => d.ForumReplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForumRepl__Forum__0A295FE6");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ForumReply2)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ForumRepl__Membe__0B1D841F");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Access)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.BankAccount).HasMaxLength(150);

                entity.Property(e => e.BankCode).HasMaxLength(100);

                entity.Property(e => e.BillingAddress).HasMaxLength(150);

                entity.Property(e => e.CreditCard).HasMaxLength(150);

                entity.Property(e => e.CreditCardCcv)
                    .HasMaxLength(150)
                    .HasColumnName("CreditCardCCV");

                entity.Property(e => e.CreditCardDate).HasMaxLength(150);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.IdCardBack).HasMaxLength(100);

                entity.Property(e => e.IdCardFront).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(100);

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.BankCodeNavigation)
                    .WithMany(p => p.Member)
                    .HasForeignKey(d => d.BankCode)
                    .HasConstraintName("FK__Member__BankCode__0C11A858");
            });

            modelBuilder.Entity<MemberCoupon>(entity =>
            {
                entity.Property(e => e.MemberCouponId).HasColumnName("MemberCouponID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.MemberCoupon)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberCou__Coupo__0D05CC91");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberCoupon)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MemberCou__Membe__0DF9F0CA");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("NewsID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NewsCategoryId).HasColumnName("NewsCategoryID");

                entity.Property(e => e.NewsContent).IsRequired();

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ViewsCount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.NewsCategory)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.NewsCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__News__NewsCatego__0EEE1503");
            });

            modelBuilder.Entity<NewsCategory>(entity =>
            {
                entity.Property(e => e.NewsCategoryId).HasColumnName("NewsCategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.DoneTime).HasColumnType("datetime");

                entity.Property(e => e.Pay).HasMaxLength(150);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Receiver)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ReceivingPhone)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.Property(e => e.ShippingAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__ProductID__0FE2393C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImgBack).HasMaxLength(150);

                entity.Property(e => e.ImgFront)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ImgSide)
                    .HasMaxLength(150)
                    .HasColumnName("ImgSIde");

                entity.Property(e => e.Model).HasMaxLength(150);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.Size)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValIdity).HasDefaultValueSql("((1))");

                entity.Property(e => e.ViewsCount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__10D65D75");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Img)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SellerAddProduct>(entity =>
            {
                entity.Property(e => e.SellerAddProductId).HasColumnName("SellerAddProductID");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Count).HasDefaultValueSql("((1))");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SaleDate).HasColumnType("datetime");

                entity.Property(e => e.ValIdity)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.SellerAddProduct)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerAdd__Membe__12BEA5E7");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SellerAddProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerAdd__Produ__13B2CA20");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}