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
        private readonly IBaseRepository<Artist> _artistRepository;

        public SongService(IBaseRepository<Song> songRepository, IBaseRepository<Artist> artistRepository)
        {
            _songRepository = songRepository;
            _artistRepository = artistRepository;
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
                    AuthorId = model.AuthorId,
                    IsFavorite = false,
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
                        Description = "Song not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                song.Title = model.Title;
                song.Length = model.Length;
                song.AlbumId = model.AlbumId;
                song.AuthorId = model.AuthorId;
                song.IsFavorite = model.IsFavorite;

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



        public async Task<IBaseResponse<Song>> AddToFavorite(int id)
        {
            try
            {
                var song = await _songRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (song == null)
                {
                    return new BaseResponse<Song>()
                    {
                        Description = "Song not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                song.IsFavorite = true;

                await _songRepository.Update(song);


                return new BaseResponse<Song>()
                {
                    Data = song,
                    StatusCode = StatusCode.OK,
                };

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

        public async Task<IBaseResponse<Song>> DeleteFromFavorite(int id)
        {
            try
            {
                var song = await _songRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (song == null)
                {
                    return new BaseResponse<Song>()
                    {
                        Description = "Song not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                song.IsFavorite = false;

                await _songRepository.Update(song);


                return new BaseResponse<Song>()
                {
                    Data = song,
                    StatusCode = StatusCode.OK,
                };

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


        public IBaseResponse<List<SongViewModel>> GetFavoriteSongs()
        {
            try
            {
                var songs = _songRepository.GetAll().ToList();//.Where(x => x.AuthorId == authorId);//AllAsync();//;
                songs = songs.Where(x => x.IsFavorite).ToList();//songs.Count
                List<SongViewModel> viewModel = new List<SongViewModel>();
                foreach (var song in songs)
                {
                    var artist = _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == song.AuthorId);

                    viewModel.Add(new SongViewModel()
                    {
                        Id = song.Id,
                        Title = song.Title,
                        AlbumId = song.AlbumId,
                        AuthorId = song.AuthorId,
                        IsFavorite = song.IsFavorite,
                        Length = song.Length,
                        ArtistName = artist.Result.Name
                    }) ;
                }
                if (!songs.Any())
                {
                    return new BaseResponse<List<SongViewModel>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<SongViewModel>>()
                {
                    Data = (List<SongViewModel>)viewModel,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<SongViewModel>>()
                {
                    Description = $"[GetSongs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public IBaseResponse<List<SongViewModel>> Search(SongViewModel model)
        {
            try
            {
                model.Title = model?.Title is null ? "" : model.Title;
                model.ArtistName = model?.ArtistName is null ? "" : model.ArtistName;
                var songs = _songRepository.GetAll().ToList();
                IEnumerable<Song> selectedArtists = new List<Song>();
                if (model.ArtistName != "")
                {
                var artistEntity = _artistRepository.GetAll().Where(x => x.Name.ToLower().Contains(model.ArtistName.ToLower()));
                int[] artistId = new int[artistEntity.Count()];
                int i = 0;
                foreach (var artist in artistEntity)
                {
                    artistId[i] = artist.Id;
                    i++;
                }
                if (artistId.Length > 0)
                    {
                    selectedArtists = from p in songs
                                      where p.Title.ToLower().Contains(model.Title.ToLower()) && artistId.Contains(p.AuthorId)
                                      select p;
                    }
                    
                }
                else
                {
                        selectedArtists = from p in songs
                                      where p.Title.ToLower().Contains(model.Title.ToLower())
                                      select p;
                }
                

                List<SongViewModel> viewModel = new List<SongViewModel>();
                foreach (var song in selectedArtists)
                {
                    var artist = _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == song.AuthorId);

                    viewModel.Add(new SongViewModel()
                    {
                        Id = song.Id,
                        Title = song.Title,
                        AlbumId = song.AlbumId,
                        AuthorId = song.AuthorId,
                        IsFavorite = song.IsFavorite,
                        Length = song.Length,
                        ArtistName = artist.Result.Name
                    });
                }


                if (!viewModel.Any())
                {
                    return new BaseResponse<List<SongViewModel>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<SongViewModel>>()
                {
                    Data = viewModel,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<List<SongViewModel>>()
                {
                    Description = $"[GetSongs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}

    
