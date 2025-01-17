﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal? Price { get; set; }
        public int? DiscountPercent { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
