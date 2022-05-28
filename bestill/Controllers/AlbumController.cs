﻿using bestill.Domain.Entity;
using bestill.Domain.ViewModels.Album;
using bestill.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bestill.Controllers
{
    public class AlbumController: Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAlbums(int authorId)
        {
            var response = _albumService.GetAlbums(authorId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK&&response.Description == "Found 0 elements")
            {
                Album album = new Album( ){ AuthorId = authorId };
                List<Album> albums = new List<Album> { album };
                return View(albums);
            }
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            
            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var response = await _albumService.GetAlbum(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, int aId)
        {
            var response = await _albumService.DeleteAlbum(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetAlbums", "Album", new { authorId = aId });
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id, int authorId)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _albumService.GetAlbum(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]

        public async Task<IActionResult> Save(AlbumViewModel model, int authorId)
        {
            //ModelState.Remove("DateCreate");
            model.AuthorId = authorId;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    
                    await _albumService.Create(model);//, imageData);
                }
                else
                {
                    await _albumService.Edit(model.Id, model);
                }
                return RedirectToAction("GetAlbums", "Album", new { authorId = model.AuthorId });
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(AlbumViewModel model)
        {
            var response = _albumService.Search(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("SearchResult", response.Data);
            }
            return View("Error", $"{response.Description}");
        }
    }
}