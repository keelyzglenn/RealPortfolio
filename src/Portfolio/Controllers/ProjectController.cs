﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ProjectController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProjects()
        {
            var allProjects = Project.GetProjects();
            return View(allProjects);
        }

    }

}