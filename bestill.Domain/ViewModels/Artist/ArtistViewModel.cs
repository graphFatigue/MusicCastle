using bestill.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.ViewModels.Artist
{
    public class ArtistViewModel
    {
            public int Id { get; set; }
            [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 50 символов")]
            public string Name { get; set; }
            public string Description { get; set; }
            public string Avatar { get; set; }
            public string Country { get; set; }
            public bool Group { get; set; }
        //public IFormFile Avatar { get; set; }
        //public byte[]? Image { get; set; }
        //public List<Album> Albums { get; set; }
        //public List<Song> Songs { get; set; }

        ////public List<Skill> Skills { get; set; }

        ////public List<Material> Materials { get; set; }
        //public bool IsAvailable => Albums?.Count > 0;
    }
}
