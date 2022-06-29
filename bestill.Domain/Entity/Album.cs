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
        public int YearRelease { get; set; }
        public int AuthorId { get; set; }
        public string Avatar { get; set; }
        public string Genre { get; set; }


    }
}

