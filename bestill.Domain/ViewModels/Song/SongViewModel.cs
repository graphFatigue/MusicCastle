using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.ViewModels.Song
{
    public class SongViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 50 символов")]
        public string Title { get; set; }
        [RegularExpression("[0-5]{1}[0-9]{1}:[0-5]{1}[0-9]{1}")]
        public string Length { get; set; }//TimeSpan
        public int AuthorId { get; set; }
        public int AlbumId { get; set; }
        public bool IsFavorite { get; set; }
        public string ArtistName { get; set; }
    }
}
