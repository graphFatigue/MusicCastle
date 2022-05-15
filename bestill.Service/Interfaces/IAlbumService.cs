using bestill.Domain.Entity;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Service.Interfaces
{
    public interface IAlbumService
    {
        IBaseResponse<List<Album>> GetAlbums(int authorId);

        Task<IBaseResponse<AlbumViewModel>> GetAlbum(int id);

        Task<IBaseResponse<Album>> Create(AlbumViewModel model);//, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteAlbum(int id);


        Task<IBaseResponse<Album>> Edit(int id, AlbumViewModel model);
    }
}
