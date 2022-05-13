
using bestill.Domain.Entity;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Artist;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Service.Interfaces
{
    public interface IArtistService 
    {
        IBaseResponse<List<Artist>> GetArtists();

        Task<IBaseResponse<ArtistViewModel>> GetArtist(int id);

        Task<IBaseResponse<Artist>> Create(ArtistViewModel model, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteArtist(int id);


        Task<IBaseResponse<Artist>> Edit(int id, ArtistViewModel model);
    }
}
