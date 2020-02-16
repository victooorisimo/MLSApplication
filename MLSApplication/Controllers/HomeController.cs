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
     * @description: Principal view with the selection options. 
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var Sportsman = new Sportsman();
                if (Sportsman.cList())
                {
                    return View(Sportsman);
                }
                else
                {
                    //Insert the algorith for redirectioner the dates.
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}