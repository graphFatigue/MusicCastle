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
        //public byte[]? Avatar { get; set; }
        public string Avatar { get; set; }
        //public List<Album> Albums { get; set; }
        //public List<Song> Songs { get; set; }

        ////public List<Skill> Skills { get; set; }

        ////public List<Material> Materials { get; set; }
        //public bool IsAvailable => Albums?.Count > 0;
    }
}

