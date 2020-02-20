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
        public ViewResult Index(Sportsman model, string sortOrder, string searchString)
        {
            try
            {

                var sportsmanDoubly = Storage.Instance.doublylistSportman;
                var find = from s in Storage.Instance.listSportman
                               select s;
                watch.Start();
                if (!String.IsNullOrEmpty(searchString)) {
                    find = find.Where(s => s.name.Contains(searchString)
                                           || s.lastname.Contains(searchString)
                                           || s.salary.ToString().Contains(searchString) || s.position.Contains(searchString));
                    watch.Stop();
                    addOperation("Find object", "Find object in the LinkedList.", watch.Elapsed.TotalMilliseconds.ToString());
                }

                if (Storage.Instance.selectionList)
                {
                    return View(find.ToList());
                }
                else
                {
                    return View(sportsmanDoubly.ToList());
                }
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
                    watch.Start();
                    Storage.Instance.listSportman.Find(sportman).Value = Sportman;
                    watch.Stop();
                    addOperation("Edit an object", "Edit an object in the LinkedList", watch.Elapsed.TotalMilliseconds.ToString());
                    return RedirectToAction("Index");
                }
                else
                {
                    var sporstman = returnObject(id);
                    Storage.Instance.doublylistSportman.popInList(sporstman);
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
                    watch.Start();
                    Storage.Instance.listSportman.Remove(Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault());
                    watch.Stop();
                    addOperation("Delete an object", "Delete an object in the LinkedList", watch.Elapsed.TotalMilliseconds.ToString());
                    return RedirectToAction("Index");
                }else {
                    sportsman = returnObject(id);
                    watch.Start();
                    Storage.Instance.doublylistSportman.popInList(sportsman);
                    watch.Stop();
                    addOperation("Delete an object", "Delete an object in the Doubly List", watch.Elapsed.TotalMilliseconds.ToString());
                    return RedirectToAction("Index");
                }
            }catch{
                return View(sportsman);
            }
        }

        //Add operation to log document
        public void addOperation(string title, string description, string time) {
            StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Logs/Log-"
            + DateTime.Now.Day+".txt", true);
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
