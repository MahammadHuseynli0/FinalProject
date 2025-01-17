﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int Counter { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Product Product { get; set; }
    }
}
