using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using bestill.Domain.Enum;
using bestill.Domain.Response;
using bestill.Domain.ViewModels.Artist;
using bestill.Service.Interfaces;
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
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Artist>>> GetArtists()
        {
            var baseResponse = new BaseResponse<IEnumerable<Artist>>();
            try
            {
                var artists = await _artistRepository.Select();
                if (artists.Count==0)
                {
                    baseResponse.Description = "Found 0 elements";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = artists;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {

                return new BaseResponse<IEnumerable<Artist>>()
                {
                    Description = $"[GetArtists] : {ex.Message}"
                };
            }
        }


        public async Task<IBaseResponse<Artist>> GetArtist(int id)
        {
            var baseResponse = new BaseResponse<Artist>();
            try
            {
                var artist = await _artistRepository.Get(id);
                if (artist == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }


                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = artist;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Artist>()
                {
                    Description = $"[GetArtist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Artist>> GetArtistByName(string name)
        {
            var baseResponse = new BaseResponse<Artist>();
            try
            {
                var artist = await _artistRepository.GetByName(name);
                if (artist == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }


                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = artist;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Artist>()
                {
                    Description = $"[GetArtistByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteArtist(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var artist = await _artistRepository.Get(id);
                if (artist == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                await _artistRepository.Delete(artist);

                return baseResponse;
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

        public async Task<IBaseResponse<ArtistViewModel>> CreateArtist(ArtistViewModel artistViewModel)
        {
            var baseResponse = new BaseResponse<ArtistViewModel>();
            try
            {
                var car = new Artist()
                {
                    Name = artistViewModel.Name,
                    Description = artistViewModel.Description
                };

                await _artistRepository.Create(car);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ArtistViewModel>()
                {
                    Description = $"[CreateArtist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        //public ArtistService(IArtistRepository artistRepository)
        //{
        //    _artistRepository = artistRepository;
        //}

        //public async Task<IBaseResponse<CarViewModel>> GetCar(int id)
        //{
        //    var baseResponse = new BaseResponse<CarViewModel>();
        //    try
        //    {
        //        var car = await _artistRepository.Get(id);
        //        if (car == null)
        //        {
        //            baseResponse.Description = "User not found";
        //            baseResponse.StatusCode = StatusCode.UserNotFound;
        //            return baseResponse;
        //        }

        //        var data = new CarViewModel()
        //        {
        //            DateCreate = car.DateCreate,
        //            Description = car.Description,
        //            TypeCar = car.TypeCar.ToString(),
        //            Speed = car.Speed,
        //            Model = car.Model
        //        };

        //        baseResponse.StatusCode = StatusCode.OK;
        //        baseResponse.Data = data;
        //        return baseResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<CarViewModel>()
        //        {
        //            Description = $"[GetCar] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}

        //public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
        //{
        //    var baseResponse = new BaseResponse<CarViewModel>();
        //    try
        //    {
        //        var car = new Car()
        //        {
        //            Description = carViewModel.Description,
        //            DateCreate = DateTime.Now,
        //            Speed = carViewModel.Speed,
        //            Model = carViewModel.Model,
        //            Price = carViewModel.Price,
        //            Name = carViewModel.Name,
        //            TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)
        //        };

        //        await _artistRepository.Create(car);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<CarViewModel>()
        //        {
        //            Description = $"[CreateCar] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //    return baseResponse;
        //}

        //public async Task<IBaseResponse<bool>> DeleteCar(int id)
        //{
        //    var baseResponse = new BaseResponse<bool>()
        //    {
        //        Data = true
        //    };
        //    try
        //    {
        //        var car = await _artistRepository.Get(id);
        //        if (car == null)
        //        {
        //            baseResponse.Description = "User not found";
        //            baseResponse.StatusCode = StatusCode.UserNotFound;
        //            baseResponse.Data = false;

        //            return baseResponse;
        //        }

        //        await _artistRepository.Delete(car);

        //        return baseResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<bool>()
        //        {
        //            Description = $"[DeleteCar] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}

        //public async Task<IBaseResponse<Car>> GetCarByName(string name)
        //{
        //    var baseResponse = new BaseResponse<Car>();
        //    try
        //    {
        //        var car = await _artistRepository.GetByName(name);
        //        if (car == null)
        //        {
        //            baseResponse.Description = "User not found";
        //            baseResponse.StatusCode = StatusCode.UserNotFound;
        //            return baseResponse;
        //        }

        //        baseResponse.Data = car;
        //        return baseResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<Car>()
        //        {
        //            Description = $"[GetCarByName] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}

        public async Task<IBaseResponse<Artist>> Edit(int id, ArtistViewModel model)
        {
            var baseResponse = new BaseResponse<Artist>();
            try
            {
                var artist = await _artistRepository.Get(id);
                if (artist == null)
                {
                    baseResponse.StatusCode = StatusCode.ArtistNotFound;
                    baseResponse.Description = "Artist not found";
                    return baseResponse;
                }

                artist.Description = model.Description;
                artist.Name = model.Name;

                await _artistRepository.Update(artist);


                return baseResponse;
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

        //public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        //{
        //    var baseResponse = new BaseResponse<IEnumerable<Car>>();
        //    try
        //    {
        //        var cars = await _artistRepository.Select();
        //        if (cars.Count == 0)
        //        {
        //            baseResponse.Description = "Найдено 0 элементов";
        //            baseResponse.StatusCode = StatusCode.OK;
        //            return baseResponse;
        //        }

        //        baseResponse.Data = cars;
        //        baseResponse.StatusCode = StatusCode.OK;

        //        return baseResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<IEnumerable<Car>>()
        //        {
        //            Description = $"[GetCars] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        // }
    }
}
