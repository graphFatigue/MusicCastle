using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.Entity
{
    public class Song : BaseEntity
    {
        public string Title { get; set; }
        public string Length { get; set; }//TimeSpan
        public int AuthorId { get; set; }
        public int AlbumId { get; set; }
        public bool IsFavorite { get; set; }
    }
}
