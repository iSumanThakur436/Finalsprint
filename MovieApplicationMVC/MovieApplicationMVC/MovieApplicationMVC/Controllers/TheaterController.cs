using MovieApplicationMVC.Models;
using MovieApplicationMVC.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MovieApplicationMVC.Controllers
{
    public class TheaterController : Controller
    {
        private readonly TheaterRepository _repository;

        public TheaterController()
        {
            _repository = new TheaterRepository();
        }

        public ActionResult Index()
        {
            var theaters = _repository.GetAllTheaters();
            return View(theaters);
        }

        public ActionResult TheatersByLocation(string location)
        {
            var theaters = _repository.GetTheatersByLocation(location);
            return View(theaters);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["UserType"]?.ToString() != "Admin")
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Theater theater)
        {
            if (Session["UserType"]?.ToString() != "Admin")
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var isAdded = _repository.AddTheater(theater);

                if (isAdded)
                    return RedirectToAction("Index");

                ViewBag.Error = "Failed to create theater.";
            }
            return View(theater);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["UserType"]?.ToString() != "Admin")
                return RedirectToAction("Index");

            var theater = _repository.GetTheaterById(id);
            return View(theater);
        }

        [HttpPost]
        public ActionResult Edit(Theater theater)
        {
            if (Session["UserType"]?.ToString() != "Admin")
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var isUpdated = _repository.UpdateTheater(theater);

                if (isUpdated)
                    return RedirectToAction("Index");

                ViewBag.Error = "Failed to update theater.";
            }
            return View(theater);
        }

        public ActionResult Delete(string id)
        {
            if (Session["UserType"]?.ToString() != "Admin")
                return RedirectToAction("Index");

            var isDeleted = _repository.DeleteTheater(id);

            if (isDeleted)
                return RedirectToAction("Index");

            ViewBag.Error = "Failed to delete theater.";
            return RedirectToAction("Index");
        }
    }
}
