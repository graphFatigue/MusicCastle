using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.Entity
{
    public class Album : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public int YearRelease { get; set; }

        public string Author { get; set; }

        public int AuthorId { get; set; }

        public List<Song> Songs { get; set; }

        //public List<Material> Materials { get; set; }

        public bool IsAvailable => Songs?.Count > 0;
    }
}

