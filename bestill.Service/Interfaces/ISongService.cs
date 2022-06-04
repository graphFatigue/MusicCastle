using bestill.Domain.Entity;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Service.Interfaces
{
    public interface ISongService
    {
        IBaseResponse<List<Song>> GetSongs(int authorId);

        Task<IBaseResponse<SongViewModel>> GetSong(int id);

        Task<IBaseResponse<Song>> Create(SongViewModel model);//, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteSong(int id);
        Task<IBaseResponse<Song>> AddToFavorite(int id);
        Task<IBaseResponse<Song>> DeleteFromFavorite(int id);
        Task<IBaseResponse<Song>> Edit(int id, SongViewModel model);
        IBaseResponse<List<SongViewModel>> GetFavoriteSongs();
        IBaseResponse<List<SongViewModel>> Search(SongViewModel model);
        IBaseResponse<List<Album>> AlbumsIncludeSong(SongViewModel model);
    }
}
