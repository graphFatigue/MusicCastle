using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.ViewModels.Album
{
        public class AlbumViewModel
        {
            public int Id { get; set; }
            [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 50 символов")]
            public string Title { get; set; }
            [Range(1930, 2022, ErrorMessage = "Недопустимый год")]
            public int YearRelease { get; set; }
            public int AuthorId { get; set; }
            public string Avatar { get; set; }
            public string Genre { get; set; }
      
    }
    }
