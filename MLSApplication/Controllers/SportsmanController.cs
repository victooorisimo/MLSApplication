using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomGenerics.Structures;
using MLSApplication.Models;
using MLSApplication.Services;

namespace MLSApplication.Controllers
{
    /*
     * @author: Aylinne Recinos and Victor Hernández
     * @version: 1.0.1
     * @description: controller for the C# list and Doubly Linked List. 
     */

    public class SportsmanController : Controller {
        //Class atributes
        Stopwatch watch = new Stopwatch();
        OperationLog newOperation;
        StreamReader streamReader;

        //Method for selection page
        public ActionResult SelectionPage() {
            return View();
        }

        //Method for selection page
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
                if (Storage.Instance.selectionList){
                    var sportman = Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                    return View(sportman);
                }else{
                    var sportsman = returnObject(id);
                    return View(sportsman);
                }
            }catch {
                return View();
            }
        }

        // POST: Sportsman/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection){
            try{
                var Sportman = new Sportsman {
                    name = collection["name"],
                    lastname = collection["lastname"],
                    salary = int.Parse(collection["salary"]),
                    position = collection["position"],
                    futbolTeam = collection["futbolTeam"],
                };
                if (Storage.Instance.selectionList)
                {
                    var sportman = Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                    var index = Storage.Instance.listSportman.IndexOf(sportman);
                    Storage.Instance.listSportman[index] = Sportman;
                    return RedirectToAction("Index");
                }else{
                    var sportsman = returnObject(id);
                    Storage.Instance.doublylistSportman.popInList(sportsman);
                    if (Sportman.saveSportman(Storage.Instance.selectionList)){
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
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

        //Method for return object by Id
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

        //Add operation to log document
        public void addOperation(string title, string description, string time) {
            StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Logs/Log-"
            + DateTime.Now.Day+".txt");
            newOperation = new OperationLog(title, description, (time + " ms"));
            streamWriter.WriteLine(newOperation.PrintOperation());
            streamWriter.Close();
        }
        
        //Method to load document .csv
        [HttpPost]
        public ActionResult LoadDocument(HttpPostedFileBase file) {
            try {
                Sportsman sportsman = new Sportsman();
                streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+ "/TestDocuments/" +file.FileName);
                int iteration = 0;
                String data;
                while (streamReader.Peek() >= 0) {
                    String lineReader = streamReader.ReadLine();
                    data = "";
                    for (int i = 0; i < lineReader.Length; i++){
                        if (lineReader.Substring(i, 1) != ";"){
                            data = data + lineReader.Substring(i,1);
                        } else {
                            if (iteration == 0){
                                if(data != "name"){
                                    sportsman.name = data;
                                }
                                iteration++;
                                data = "";
                            }else if (iteration == 1){
                                if (data != "lastname"){
                                    sportsman.lastname = data;
                                }
                                iteration++;
                                data = "";
                            }else if (iteration == 2){
                                if (data != "salary"){
                                    sportsman.salary = Convert.ToDouble(data);
                                }
                                iteration++;
                                data = "";
                            }else if (iteration == 3){
                                if (data != "position"){
                                    sportsman.position = data;
                                }
                                iteration++;
                                data = "";
                            }
                        }
                    }
                    if (data != "futbolTeam"){
                        sportsman.futbolTeam = data;
                    }
                    data = "";
                    if (sportsman.name != null) {
                        if (sportsman.saveSportman(Storage.Instance.selectionList)){
                            //return RedirectToAction("Index");
                        }else{
                            return View(sportsman);
                        }
                            
                    }
                    
                    sportsman = new Sportsman();
                    iteration = 0;
                }
                streamReader.Close();
                return RedirectToAction("Index");
            }
            catch (Exception e){
                e.ToString();
                return RedirectToAction("Index");
            }
        }
    }
}
