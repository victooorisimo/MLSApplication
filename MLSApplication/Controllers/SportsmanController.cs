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
     * @version: 1.0.0
     * @description: controller for the C# list. 
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
                if (!string.IsNullOrEmpty(C_List)){
                    Storage.Instance.selectionList = true;
                    return RedirectToAction("Index");
                }
                else if (!string.IsNullOrEmpty(DoublyList)){
                    Storage.Instance.selectionList = false;
                    return RedirectToAction("Index");
                }else{
                    return View(sportsman);
                }
            }catch (Exception){
                return View();
            }
        }

        // GET: Sportsman
        public ViewResult Index(string searchString, string sortOrder){

            ViewBag.nameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.lastNameSortParam = sortOrder == "Lastname" ? "lastname_desc" : "Lastname";
            var sportsman = Storage.Instance.listSportman;
            var sportsmanDoubly = Storage.Instance.doublylistSportman;

            //if (structureType){
            //    var sportsman = Storage.Instance.listSportman;
            //}else{
            //    var sportsman = Storage.Instance.doublylistSportman;
            //}
            

            //if (!String.IsNullOrEmpty(searchString)) {
            //    sportsman = Storage.Instance.listSportman.Where(s => s.name.Contains(searchString)).ToList(); 
            //}

            //var sportman = new DoublyLinkedList<Sportsman>();
            //sportman.pushInList(new Sportsman { name = "Victor", lastname = "Hernández"});
            //sportman.pushInList(new Sportsman { name = "Aylinne", lastname = "Recinos" });


            switch (sortOrder) {
                case "name_desc":
                    if (Storage.Instance.selectionList){
                        sportsman = Storage.Instance.listSportman.OrderByDescending(X => X.name).ToList();
                    }else{
                        //Doublylinked list
                    }
                break;
                case "Lastname":
                    if (Storage.Instance.selectionList) {
                        sportsman = Storage.Instance.listSportman.OrderBy(X => X.lastname).ToList();
                    }else {
                        //Doublylinked list
                    }
                break;
                case "lastname_desc":
                    if (Storage.Instance.selectionList) {
                        sportsman = Storage.Instance.listSportman.OrderByDescending(X => X.lastname).ToList();
                    } else {
                        //Doublylinked list
                    }
                break;
                default:
                    if (Storage.Instance.selectionList) {
                        sportsman = Storage.Instance.listSportman.OrderBy(X => X.name).ToList();
                    }else {
                        //Doublylinked list
                    }
                break;
            }
            if (Storage.Instance.selectionList){
                return View(sportsman.ToList());
            }else {
                return View(sportsmanDoubly.ToList());
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
                    nationality = collection["nationality"],
                    salary = int.Parse(collection["salary"]),
                    height = int.Parse(collection["height"]),
                    weight = int.Parse(collection["weight"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                    dateOfBirth = collection["dateOfbirth"]
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
                    nationality = collection["nationality"],
                    salary = int.Parse(collection["salary"]),
                    height = int.Parse(collection["height"]),
                    weight = int.Parse(collection["weight"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                    dateOfBirth = collection["dateOfbirth"]
                };

                Storage.Instance.listSportman.Insert(Storage.Instance.listSportman.IndexOf(Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault()), Sportman);
                if (Sportman.updateSportman()) {
                    return RedirectToAction("Index");
                }else{
                    return View(Sportman);
                }
            }catch {
                    return RedirectToAction("Index");
            }

        }

        // GET: Sportsman/Delete/5
        public ActionResult Delete(int id){
            try {
                if (Storage.Instance.selectionList){
                    var sportman = new Sportsman();
                    if (sportman.deleteSportman(id, Storage.Instance.selectionList)){
                        return View(sportman);
                    }else{
                        return View();
                    }
                }else{
                    var sportsman = new Sportsman();
                    while (id != sportsman.sportsmanId){
                        sportsman = Storage.Instance.doublylistSportman.getObject();
                    }
                    Storage.Instance.doublylistSportman.popInList(sportsman);
                    return View(sportsman);
                }
                
                 
            }catch {
                return View();
            }
        }

        // POST: Sportsman/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            var Sportsman = new Sportsman();
            try {
                if (Storage.Instance.selectionList) {
                    if (Sportsman == null)
                        return View("NotFound");
                    Storage.Instance.listSportman.RemoveAll(c => c.sportsmanId == id);
                    if (Sportsman.updateSportman()) {
                        return RedirectToAction("Index");
                    }
                    else {
                        return View(Sportsman);
                    }
                }else {
                    return RedirectToAction("Index");
                }
            }catch{
                return View(Sportsman);
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
