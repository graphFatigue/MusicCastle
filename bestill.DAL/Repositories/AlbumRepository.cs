using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.DAL.Repositories
{
    public class AlbumRepository: IBaseRepository<Album> { 

     private readonly ApplicationDbContext _db;

    public AlbumRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Create(Album entity)
    {
        await _db.Album.AddAsync(entity);
        await _db.SaveChangesAsync();
    }


        public IQueryable<Album> GetAll()
        {
            return _db.Album;
        }


        public async Task Delete(Album entity)
    {
        _db.Album.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Album> Update(Album entity)
    {
        _db.Album.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

}
}

