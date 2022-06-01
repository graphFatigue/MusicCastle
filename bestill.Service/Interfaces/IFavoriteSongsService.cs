using bestill.Domain.Entity;
using bestill.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Service.Interfaces
{
    public interface IFavoriteSongsService
    {
        IBaseResponse<List<Song>> GetSongs(int authorId);

        Task<IBaseResponse<BaseEntity>> Create(BaseEntity model);

        Task<IBaseResponse<bool>> DeleteSong(int id);
    }
}
