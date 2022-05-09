using bestill.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.ViewModels.Artist
{
    public class ArtistViewModel
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public IFormFile Avatar { get; set; }
            //public List<Album> Albums { get; set; }
            //public List<Song> Songs { get; set; }

            ////public List<Skill> Skills { get; set; }

            ////public List<Material> Materials { get; set; }
            //public bool IsAvailable => Albums?.Count > 0;
    }
}
