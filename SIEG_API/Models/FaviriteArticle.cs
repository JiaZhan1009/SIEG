﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SIEG_API.Models
{
    public partial class FaviriteArticle
    {
        public int FaviriteAritcleId { get; set; }
        public int MemberId { get; set; }
        public bool? ValIdity { get; set; }
        public int ForumArticleId { get; set; }

        public virtual ForumArticle ForumArticle { get; set; }
    }
}