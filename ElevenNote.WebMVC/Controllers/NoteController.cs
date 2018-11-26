﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class NoteController : Controller
    {
        private NoteService CreateNoteService() 
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userID);
            return service;
        }

        [Authorize]
        public ActionResult Index()
        {
            var service = CreateNoteService();
            var model = service.GetNotes();
            return View(model);
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var service = CreateNoteService();

            if (service.CreateNote(model)) 
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }
    }
}