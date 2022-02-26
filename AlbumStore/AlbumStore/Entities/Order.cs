using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumStore.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
        public float Quantity { get; set; }
        public float? Discount { get; set; }
    }
}
