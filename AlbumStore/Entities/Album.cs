using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumStore.Entities
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int GenreId { get; set; }
        public int SingerId { get; set; }
        public decimal Price { get; set; }
        public float? Discount { get; set; }
    }
}
