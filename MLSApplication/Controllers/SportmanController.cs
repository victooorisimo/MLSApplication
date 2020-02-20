using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MLSApplication.Models;
using MLSApplication.Services;

namespace MLSApplication.Controllers
{
    public class SportmanController : Controller
    {
        // GET: Sportman
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sportman/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sportman/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sportman/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var Sportman = new Sportsman
                {
                    name = collection["Name"],
                    lastname = collection["LastName"],
                    nationality = collection["Nationality"],
                    salary = int.Parse(collection["Salary"]),
                    height = int.Parse(collection["Height"]),
                    weight = int.Parse(collection["Weight"]),
                    position = collection["Position"],
                    futbolTeam = collection["Futbol Team"],
                    dateOfBirth = collection["Date of birth"]
                };
                if(Sportman.saveSportman())
                {
                    return RedirectToAction("Index");
                }
                else{
                    return View(Sportman);
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Sportman/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sportman/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sportman/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var Sportsman = Storage.Instance.SportsmanList.Where(c => c.sportsmanId == id).FirstOrDefault();
                return View(Sportsman);
            }
            catch
            {
                return View();
            }
        }

        // POST: Sportman/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return View();
        }
    }
}
