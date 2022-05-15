using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using bestill.Domain.Enum;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Album;
using bestill.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Service.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly IBaseRepository<Album> _albumRepository;

        public AlbumService(IBaseRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public IBaseResponse<List<Album>> GetAlbums(int authorId)
        {
            try
            {
                var albums = _albumRepository.GetAll().ToList();//.Where(x => x.AuthorId == authorId);//AllAsync();//;
                albums = albums.Where(x => x.AuthorId == authorId).ToList();
                if (!albums.Any())
                {
                    return new BaseResponse<List<Album>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Album>>()
                {
                    Data = (List<Album>)albums,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<Album>>()
                {
                    Description = $"[GetAlbums] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<AlbumViewModel>> GetAlbum(int id)
        {
            try
            {
                var album = await _albumRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (album == null)
                {
                    return new BaseResponse<AlbumViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new AlbumViewModel()
                {
                    Title = album.Title,
                    YearRelease = album.YearRelease,
                    Avatar = album.Avatar,
                };

                return new BaseResponse<AlbumViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AlbumViewModel>()
                {
                    Description = $"[GetAlbum] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Album>> Create(AlbumViewModel model)//, byte[] imageData)
        {
            try
            {
                var album = new Album()
                {
                    Title = model.Title,
                    YearRelease = model.YearRelease,
                    Avatar = model.Avatar
                };
                await _albumRepository.Create(album);

                return new BaseResponse<Album>()
                {
                    StatusCode = StatusCode.OK,
                    Data = album
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Album>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteAlbum(int id)
        {
            try
            {
                var album = await _albumRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (album == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _albumRepository.Delete(album);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<Album>> Edit(int id, AlbumViewModel model)
        {
            try
            {
                var album = await _albumRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (album == null)
                {
                    return new BaseResponse<Album>()
                    {
                        Description = "Album not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                album.Title = model.Title;
                album.YearRelease = model.YearRelease;

                await _albumRepository.Update(album);


                return new BaseResponse<Album>()
                {
                    Data = album,
                    StatusCode = StatusCode.OK,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Album>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }






    }
}