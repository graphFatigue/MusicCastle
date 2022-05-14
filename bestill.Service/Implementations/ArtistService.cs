using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using bestill.Domain.Enum;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Artist;
using bestill.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;

namespace bestill.Service.Implementations
{
    public class ArtistService: IArtistService
    {
        private readonly IBaseRepository<Artist> _artistRepository;

        public ArtistService(IBaseRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public IBaseResponse<List<Artist>> GetArtists()
        {
            try
            {
                var artists =  _artistRepository.GetAll().ToList();
                if (!artists.Any())
                {
                    return new BaseResponse<List<Artist>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Artist>>()
                {
                    Data = artists,
                    StatusCode = StatusCode.OK
                };
            }
            
            catch (Exception ex)
            {

                return new BaseResponse<List<Artist>>()
                {
                    Description = $"[GetArtists] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }





        public async Task<IBaseResponse<ArtistViewModel>> GetArtist(int id)
        {
            try
            {
                var artist = await _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (artist == null)
                {
                    return new BaseResponse<ArtistViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new ArtistViewModel()
                {
                    Name = artist.Name,
                    Description = artist.Description,
                    Avatar = artist.Avatar,
                };

                return new BaseResponse<ArtistViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ArtistViewModel>()
                {
                    Description = $"[GetArtist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }  
        
        public async Task<IBaseResponse<Artist>> Create(ArtistViewModel model)//, byte[] imageData)
        {
            try
            {
                var artist = new Artist()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Avatar = model.Avatar//imageData
                };
                await _artistRepository.Create(artist);

                return new BaseResponse<Artist>()
                {
                    StatusCode = StatusCode.OK,
                    Data = artist
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Artist>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteArtist(int id)
        {
            try
            {
                var artist = await _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (artist == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _artistRepository.Delete(artist);

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


        public async Task<IBaseResponse<Artist>> Edit(int id, ArtistViewModel model)
        {
            try
            {
                var car = await _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    return new BaseResponse<Artist>()
                    {
                        Description = "Artist not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                car.Description = model.Description;
                car.Name = model.Name;

                await _artistRepository.Update(car);


                return new BaseResponse<Artist>()
                {
                    Data = car,
                    StatusCode = StatusCode.OK,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Artist>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }





       
    }
}
