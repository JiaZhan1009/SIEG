﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIEG_API.Models
{
    public partial class GetSizeAndBidResult
    {
        public string pSize { get; set; }
        public int bidPrice { get; set; }
        public int pID { get; set; }
        public int bID { get; set; }
        public int pCateID { get; set; }
        public DateTime? time { get; set; }
        public bool V { get; set; }
        public int bidID { get; set; }
        public int oID { get; set; }
    }
}
