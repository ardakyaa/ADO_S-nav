﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumStore.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int AlbumId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
