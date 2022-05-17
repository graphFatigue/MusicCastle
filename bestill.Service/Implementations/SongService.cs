using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using bestill.Domain.Enum;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Album;
using bestill.Domain.ViewModels.Song;
using bestill.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Service.Implementations
{
    public class SongService : ISongService
    {
        private readonly IBaseRepository<Song> _songRepository;

        public SongService(IBaseRepository<Song> songRepository)
        {
            _songRepository = songRepository;
        }

        public IBaseResponse<List<Song>> GetSongs(int albumId)
        {
            try
            {
                var songs = _songRepository.GetAll().ToList();//.Where(x => x.AuthorId == authorId);//AllAsync();//;
                songs = songs.Where(x => x.AlbumId == albumId).ToList();
                if (!songs.Any())
                {
                    return new BaseResponse<List<Song>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Song>>()
                {
                    Data = (List<Song>)songs,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<Song>>()
                {
                    Description = $"[GetSongs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<SongViewModel>> GetSong(int id)
        {
            try
            {
                var song = await _songRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (song == null)
                {
                    return new BaseResponse<SongViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new SongViewModel()
                {
                    Id = id,
                    AuthorId = song.AuthorId,
                    Title = song.Title,
                    AlbumId = song.AlbumId,
                    Length = song.Length,
                };

                return new BaseResponse<SongViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<SongViewModel>()
                {
                    Description = $"[GetSong] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Song>> Create(SongViewModel model)//, byte[] imageData)
        {
            try
            {
                var song = new Song()
                {
                    Title = model.Title,
                    Length = model.Length,
                    AlbumId = model.AlbumId,
                    AuthorId = model.AuthorId
                };
                await _songRepository.Create(song);

                return new BaseResponse<Song>()
                {
                    StatusCode = StatusCode.OK,
                    Data = song
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Song>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteSong(int id)
        {
            try
            {
                var song = await _songRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (song == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _songRepository.Delete(song);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteSong] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<Song>> Edit(int id, SongViewModel model)
        {
            try
            {
                var song = await _songRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (song == null)
                {
                    return new BaseResponse<Song>()
                    {
                        Description = "Album not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                song.Title = model.Title;
                song.Length = model.Length;
                song.AlbumId = model.AlbumId;
                song.AuthorId = model.AuthorId;

                await _songRepository.Update(song);


                return new BaseResponse<Song>()
                {
                    Data = song,
                    StatusCode = StatusCode.OK,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Song>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }






    }
}