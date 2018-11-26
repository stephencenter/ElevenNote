using ElevenNote.Models;
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
                TempData["SaveResult"] = "Note created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create note!");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        private NoteService CreateNoteService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userID);
            return service;
        }

        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model = new NoteEdit {NoteId = detail.NoteId, Title = detail.Title, Content = detail.Content};

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Note updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to update note!");
            return View();
        }
    }
}