using bestill.Domain.ViewModels.Album;
using bestill.Domain.ViewModels.Song;
using bestill.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using bestill.Domain.Entity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bestill.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public IActionResult GetSongs(int albumId, int authorId)
        {
            var response = _songService.GetSongs(albumId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Description == "Found 0 elements")
            {
                Song song = new Song() { AuthorId = authorId, AlbumId = albumId };
                List<Song> songs = new List<Song> { song };
                return View(songs);
            }
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public async Task<IActionResult> GetSong(int id)
        {
            var response = await _songService.GetSong(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, int alId)
        {
            var response = await _songService.DeleteSong(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetSongs", "Song", new { albumId =  alId});
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id, int albumId, int authorId)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _songService.GetSong(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]

        public async Task<IActionResult> Save(SongViewModel model, int albumId, int authorId)
        {
            model.AuthorId = authorId;
            model.AlbumId = albumId;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {

                    await _songService.Create(model);
                }
                else
                {
                    await _songService.Edit(model.Id, model);
                }
                return RedirectToAction("GetSongs", "Song", new { albumId = model.AlbumId });
            }
            return View();
        }


        public async Task<IActionResult> AddToFavorite(int id, int alId)
        {
            var response = await _songService.AddToFavorite(id);
            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetSongs", "Song", new { albumId = alId });
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> DeleteFromFavorite(int id, int alId)
        {
            var response = await _songService.DeleteFromFavorite(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetSongs", "Song", new { albumId = alId });
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public IActionResult FavoriteSongs()
        {
            var response = _songService.GetFavoriteSongs();
            if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Description == "Found 0 elements")
            {
                SongViewModel song = new SongViewModel() { };
                List<SongViewModel> songs = new List<SongViewModel> {  };
                return View(songs);
            }
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(SongViewModel model)
        {
            var response = _songService.Search(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("SearchResult", response.Data);
            }
            return View("Error", $"{response.Description}");
        }
    }
}   