using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MLSApplication.Models;
using MLSApplication.Services;

namespace MLSApplication.Controllers
{
    /*
     * @author: Aylinne Recinos
     * @version: 1.0.0
     * @description: controller for the C# list. 
     */

    public class SportsmanController : Controller
    {
        public ActionResult SelectionPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SelectionPage(Sportsman sportsman, string C_List, string DoublyList)
        {
            try
            {
                if (!string.IsNullOrEmpty(C_List)){
                    Storage.Instance.selectionList = true;
                    return RedirectToAction("Index");
                }
                else if (!string.IsNullOrEmpty(DoublyList)){
                    Storage.Instance.selectionList = false;
                    return RedirectToAction("Index");
                }
                else{
                    return View(sportsman);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Sportsman
        public ActionResult Index(string sortOrder)
        {
            ViewBag.nameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.lastNameSortParam = sortOrder == "Lastname" ? "lastname_desc" : "Lastname";
            var sportsman = Storage.Instance.listSportman;
            switch (sortOrder)
            {
                case "name_desc":
                    sportsman = Storage.Instance.listSportman.OrderByDescending(X => X.name).ToList();
                    break;
                case "Lastname":
                    sportsman = Storage.Instance.listSportman.OrderBy(X => X.lastname).ToList();
                    break;
                case "lastname_desc":
                    sportsman = Storage.Instance.listSportman.OrderByDescending(X => X.lastname).ToList();
                    break;
                default:
                    sportsman = Storage.Instance.listSportman.OrderBy(X => X.name).ToList();
                    break;
            }
            return View(sportsman.ToList());
        }

        // GET: Sportsman/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sportsman/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sportsman/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var Sportman = new Sportsman
                {
                    name = collection["name"],
                    lastname = collection["lastname"],
                    nationality = collection["nationality"],
                    salary = int.Parse(collection["salary"]),
                    height = int.Parse(collection["height"]),
                    weight = int.Parse(collection["weight"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                    dateOfBirth = collection["dateOfbirth"]
                };
                if (Sportman.saveSportman()){
                    return RedirectToAction("Index");
                }
                else{
                    return View(Sportman);
                }
            }
            catch (Exception)
            {
                return View();
            } 
        }


// GET: Sportsman/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var sportman = Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                return View(sportman);
            }
            catch
            {
                return View();
            }
        }

        // POST: Sportsman/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //Storage.Instance.listSportman.RemoveAll(c => c.sportsmanId == id);

                var Sportman = new Sportsman
                {
                    name = collection["name"],
                    lastname = collection["lastname"],
                    nationality = collection["nationality"],
                    salary = int.Parse(collection["salary"]),
                    height = int.Parse(collection["height"]),
                    weight = int.Parse(collection["weight"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                    dateOfBirth = collection["dateOfbirth"]
                };
                Storage.Instance.listSportman.Insert(Storage.Instance.listSportman.IndexOf(Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault()), Sportman);

                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: Sportsman/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var Sportman = Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                return View(Sportman);
            }
            catch
            {
                return View();
            }
        }

        // POST: Sportsman/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Sportsman = new Sportsman();
            try
            {
                if (Sportsman == null)
                    return View("NotFound");

                Storage.Instance.listSportman.RemoveAll(c => c.sportsmanId == id);
                if (Sportsman.updateSportman()){
                    return RedirectToAction("Index");
                }
                else{
                    return View(Sportsman);
                }
            }
            catch
            {
                return View(Sportsman);
            }
        }
    }
}
