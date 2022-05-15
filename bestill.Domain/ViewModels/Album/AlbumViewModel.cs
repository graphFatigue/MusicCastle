using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.ViewModels.Album
{
        public class AlbumViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int YearRelease { get; set; }
            public int AuthorId { get; set; }
            public string Avatar { get; set; }
        //public string Avatar { get; set; }


        //public IFormFile Avatar { get; set; }
        //public byte[]? Image { get; set; }
        //public List<Album> Albums { get; set; }
        //public List<Song> Songs { get; set; }

        ////public List<Skill> Skills { get; set; }

        ////public List<Material> Materials { get; set; }
        //public bool IsAvailable => Albums?.Count > 0;
    }
    }
