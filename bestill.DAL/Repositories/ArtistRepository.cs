using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.DAL.Repositories
{
    public class ArtistRepository: IBaseRepository<Artist>
    {
        private readonly ApplicationDbContext _db;

        public ArtistRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Artist entity)
        {
            await _db.Artist.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Artist> GetAll()
        {
            return _db.Artist;
        }

        public async Task<List<Artist>> Select()
        {
            return await _db.Artist.ToListAsync();
        }

        public async Task Delete(Artist entity)
        {
            _db.Artist.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Artist> Update(Artist entity)
        {
            _db.Artist.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
