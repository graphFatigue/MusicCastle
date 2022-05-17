using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.DAL.Repositories
{
    public class SongRepository: IBaseRepository<Song>
    {
        private readonly ApplicationDbContext _db;

        public SongRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Song entity)
        {
            await _db.Song.AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public IQueryable<Song> GetAll()
        {
            return _db.Song;
        }

        public async Task Delete(Song entity)
        {
            _db.Song.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Song> Update(Song entity)
        {
            _db.Song.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}

