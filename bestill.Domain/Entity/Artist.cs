using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.Entity
{
    public class Artist : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public bool Group { get; set; }
        public string Avatar { get; set; }
       
    }
}

