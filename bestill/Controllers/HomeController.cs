using bestill.DAL.Interfaces;
using bestill.DAL.Repositories;
using bestill.Domain.Entity;
using bestill.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace bestill.Controllers
{
    public class HomeController : Controller
    {
        
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Privacy()
            {
                return View();
            }
        }
    }

