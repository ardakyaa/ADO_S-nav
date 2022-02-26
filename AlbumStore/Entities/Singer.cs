using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumStore.Entities
{
    public class Singer
    {
        public int SingerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string NameAndSurname
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
