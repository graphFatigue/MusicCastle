
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
        Task<IBaseResponse<IEnumerable<Artist>>> GetArtists();
        
        Task<IBaseResponse<Artist>> GetArtist(int id);

        Task<IBaseResponse<ArtistViewModel>> CreateArtist(ArtistViewModel artistViewModel);

        Task<IBaseResponse<bool>> DeleteArtist(int id);

        Task<IBaseResponse<Artist>> GetArtistByName(string name);

        Task<IBaseResponse<Artist>> Edit(int id, ArtistViewModel model);
    }
}
