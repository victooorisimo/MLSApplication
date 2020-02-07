using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * @author: Victor Noe Hernández
 * @version: 1.0.0
 * @description: class for create the players
 */

namespace MLSApplication.Models {

    public class Sportsman {
        //Class parameters
        public String name { get; set; }
        public String lastname { get; set; }
        public String nationality { get; set; }
        public double salary { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public String position { get; set; }
        public String futbolTeam { get; set; }
        public String dateOfBirth { get; set; }

        public bool saveSportman(){
            return true;
        }

        public bool deleteSportman(){
            return true;
        }
    }
}