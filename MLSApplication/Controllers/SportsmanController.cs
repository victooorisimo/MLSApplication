using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CustomGenerics.Structures;
using MLSApplication.Models;
using MLSApplication.Services;

namespace MLSApplication.Controllers
{
    /*
     * @author: Aylinne Recinos
     * @version: 1.0.1
     * @description: controller for the C# list and Doubly Linked List. 
     */

    public class SportsmanController : Controller {
        Stopwatch watch = new Stopwatch();
        OperationLog newOperation;

        public ActionResult SelectionPage() {
            return View();
        }

        [HttpPost]
        public ActionResult SelectionPage(Sportsman sportsman, string C_List, string DoublyList){
            try {
                var sportsmanS = Storage.Instance.listSportman;
                if (!string.IsNullOrEmpty(C_List)){
                    Storage.Instance.selectionList = true;
                    return RedirectToAction("Index");
                }
                else if (!string.IsNullOrEmpty(DoublyList)){
                    return RedirectToAction("Index");
                }
                else{
                    return RedirectToAction("Index");
                }
            }catch (Exception){
                return View();
            }
        }
       

        // GET: Sportsman
        public ViewResult Index(Sportsman model, string search, string sortOrder,string buttonSearch)
        {

            //Searching Method
            try
            {
                var sportsman = Storage.Instance.listSportman;
                var sportsmanS = from s in Storage.Instance.listSportman
                                 select s;
               

                if (!string.IsNullOrEmpty(buttonSearch))
                {
                    sportsmanS = sportsmanS.Where(s => s.name == search);
                    sportsmanS = sportsmanS.Where(s => s.lastname == search);
                    sportsmanS = sportsmanS.Where(s => s.position == search);
                    sportsmanS = sportsmanS.Where(s => s.salary == int.Parse(search));
                }
                else
                {
                    return View(sportsman.ToList());
                }
                return View();
    

                //ViewBag.nameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                //ViewBag.lastNameSortParam = sortOrder == "Lastname" ? "lastname_desc" : "Lastname";
                //var sportsman = Storage.Instance.listSportman;
                //var sportsmanDoubly = Storage.Instance.doublylistSportman;

                //switch (sortOrder)
                //{
                //    case "name_desc":
                //        if (Storage.Instance.selectionList)
                //        {
                //            sportsman = Storage.Instance.listSportman.OrderByDescending(X => X.name).ToList();
                //        }
                //        else
                //        {
                //            //Doublylinked list
                //        }
                //        break;
                //    case "Lastname":
                //        if (Storage.Instance.selectionList)
                //        {
                //            sportsman = Storage.Instance.listSportman.OrderBy(X => X.lastname).ToList();
                //        }
                //        else
                //        {
                //            //Doublylinked list
                //        }
                //        break;
                //    case "lastname_desc":
                //        if (Storage.Instance.selectionList)
                //        {
                //            sportsman = Storage.Instance.listSportman.OrderByDescending(X => X.lastname).ToList();
                //        }
                //        else
                //        {
                //            //Doublylinked list
                //        }
                //        break;
                //    default:
                //        if (Storage.Instance.selectionList)
                //        {
                //            sportsman = Storage.Instance.listSportman.OrderBy(X => X.name).ToList();
                //        }
                //        else
                //        {
                //            //Doublylinked list
                //        }
                //        break;
                //}
                //if (Storage.Instance.selectionList)
                //{
                //    return View(sportsman.ToList());
                //}
                //else
                //{
                //    return View(sportsmanDoubly.ToList());
                //}
            }
            catch (Exception)
            {
                return View();
            }

        }

        // GET: Sportsman/Details/5
        public ActionResult Details(int id){
            return View();
        }

        // GET: Sportsman/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Sportsman/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            
            try {
                var Sportman = new Sportsman {
                    name = collection["name"],
                    lastname = collection["lastname"],
                    salary = int.Parse(collection["salary"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                };
                watch.Start();
                if (Sportman.saveSportman(Storage.Instance.selectionList)) {
                    watch.Stop();
                    addOperation("New player", "A new player was added", watch.Elapsed.TotalMilliseconds.ToString());
                    return RedirectToAction("Index");
                }else{
                    return View(Sportman);
                }
            }
            catch (Exception){
                return View();
            } 
        }

        // GET: Sportsman/Edit/5
        public ActionResult Edit(int id){
            try {
                var sportman = Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                return View(sportman);
            }catch {
                return View();
            }
        }

        // POST: Sportsman/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection){
            try{
                //Storage.Instance.listSportman.RemoveAll(c => c.sportsmanId == id);
                var Sportman = new Sportsman {
                    name = collection["name"],
                    lastname = collection["lastname"],
                    salary = int.Parse(collection["salary"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                };

                var sportman = Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                var index = Storage.Instance.listSportman.IndexOf(sportman);
                Storage.Instance.listSportman[index] = Sportman;
                    return RedirectToAction("Index");
                
            }catch {
                    return RedirectToAction("Index");
            }

        }

        // GET: Sportsman/Delete/5
        public ActionResult Delete(int id){
            var sportman = new Sportsman();
            try {
                if (Storage.Instance.selectionList){
                    if (sportman.deleteSportman(id, Storage.Instance.selectionList)){
                        return View(Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault());
                    }else{
                        return View();
                    }
                }else{
                    var sportsman = returnObject(id);
                    return View(sportsman);
                }
                
                 
            }catch {
                return View(sportman);
            }
        }

        public Sportsman returnObject(int id){
            var sportsman = new Sportsman();
            DoublyLinkedList<Sportsman> sportmansCopy = Storage.Instance.doublylistSportman;
            while (id != sportsman.sportsmanId) {
                sportsman = sportmansCopy.getObject();
            }
            return sportsman;
        }

        // POST: Sportsman/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            var sportsman = new Sportsman();
            try {
                if (Storage.Instance.selectionList) {
                    if (sportsman == null) {
                        return View("NotFound");
                    }
                    Storage.Instance.listSportman.RemoveAll(c => c.sportsmanId == id);
                    return RedirectToAction("Index");
                }else {
                    sportsman = returnObject(id);
                    Storage.Instance.doublylistSportman.popInList(sportsman);
                    return RedirectToAction("Index");
                }
            }catch{
                return View(sportsman);
            }
        }

        public void addOperation(string title, string description, string time) {
            StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Logs/Log-"
            + DateTime.Now.Day+".txt");
            newOperation = new OperationLog(title, description, (time + " ms"));
            streamWriter.WriteLine(newOperation.PrintOperation());
            streamWriter.Close();
        }
    }
}
