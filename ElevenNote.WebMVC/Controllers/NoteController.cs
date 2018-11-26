﻿using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers {
    public class NoteController : Controller {
        
        [Authorize]
        public ActionResult Index() {
            var model = new NoteListItem[0];
            return View(model);
        }

        public ActionResult Create() {
            return View();
        }
    }
}