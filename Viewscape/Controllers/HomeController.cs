using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Viewscape.Models;
using System.Net;

namespace Viewscape.Controllers
{
    public class HomeController : Controller
    {
        public ViewscapeEntities db = new ViewscapeEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Encyclopedia(string buildingLocation, string searchString)
        {
            //Search Country
            var CountryList = new List<String>();
            var CountryQuery = from d in db.ViewscapeTables
                               orderby d.Country
                               select d.Country;
            CountryList.AddRange(CountryQuery.Distinct());
            ViewBag.buildingLocation = new SelectList(CountryList);

            var buildings = from b in db.ViewscapeTables
                            select b;

            if (!String.IsNullOrEmpty(buildingLocation))
            {
                buildings = buildings.Where(x => x.Country == buildingLocation);
            }

            //Search name
            if (!String.IsNullOrEmpty(searchString))
            {
                buildings = buildings.Where(s => s.Name.Contains(searchString));
            }

            return View(buildings);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ViewscapeTable building)
        {
            if (building.Picture == null)
            {
                building.Picture = "../../Images/architecture.jpg";
            }

            if (building.Architect == null)
            {
                building.Architect = "Anon";
            }

            if (ModelState.IsValid)
            {
                db.ViewscapeTables.Add(building);
                db.SaveChanges();
                return RedirectToAction("Encyclopedia");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewscapeTable building = db.ViewscapeTables.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewscapeTable building = db.ViewscapeTables.Find(id);
            return View(building);
        }

        [HttpPost]
        public ActionResult Edit(ViewscapeTable building)
        {
            if (building.Picture == null)
            {
                building.Picture = "../../Images/architecture1.jpg";
            }

            if (building.Architect == null)
            {
                building.Architect = "Anon";
            }

            if (ModelState.IsValid)
            {
                db.Entry(building).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Encyclopedia");
            }
            return View(building);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //find the requested movie
            ViewscapeTable building = db.ViewscapeTables.Find(id);
            //return the movie to the Details View
            return View(building);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewscapeTable building = db.ViewscapeTables.Find(id);
            db.ViewscapeTables.Remove(building);
            db.SaveChanges();
            //after successful deletion, return to the Index action method
            return RedirectToAction("Encyclopedia");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nazmul Mumtahin";

            return View();
        }
    }
}