using bestill.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.DAL.Interfaces
{
    public interface IArtistRepository: IBaseRepository<Artist>
    {
        Task<Artist> GetByName(string name);
    }
}
