﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SIEG_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SIEG_API.Models
{
    public partial interface ISIEGContextProcedures
    {
        Task<List<GetSizeAndBidResult>> GetSizeAndBidAsync(int? pCateID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetSizeAndQuoteResult>> GetSizeAndQuoteAsync(int? pCateID, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
