using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.Entity
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }

        public string Password { get; set; }

        public List<Song> FavoriteSongs { get; set; }

        public List<Album> FavoriteAlbums { get; set; }

        public List<Artist> FavoriteArtists { get; set; }
    }
}
