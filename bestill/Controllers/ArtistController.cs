using bestill.DAL.Interfaces;
using bestill.Domain.Entity;
using bestill.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using bestill.Domain.ViewModels.Artist;
using System.IO;
using System;

namespace bestill.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult GetArtists()
        {
            var response = _artistService.GetArtists();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public async Task<IActionResult> GetArtist(int id)
        {
            var response = await _artistService.GetArtist(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _artistService.DeleteArtist(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetArtists");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _artistService.GetArtist(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]

        public async Task<IActionResult> Save(ArtistViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _artistService.Create(model);
                }
                else
                {
                    await _artistService.Edit(model.Id, model);
                }
                return RedirectToAction("GetArtists");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(ArtistViewModel model)
        {
            var response = _artistService.Search(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("SearchResult", response.Data);
            }
            return View("Error", $"{response.Description}");
        }


    }
}