using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MLSApplication.Services;

/*
 * @author: Victor Noe Hernández
 * @version: 1.0.0
 * @description: class for create the players
 */

namespace MLSApplication.Models {

    public class Sportsman {
        //Class parameters
        public static int codeSportsman = 0;
        public int sportsmanId { get; set; }
        public String name { get; set; }
        public String lastname { get; set; }
        public String nationality { get; set; }
        public double salary { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public String position { get; set; }
        public String futbolTeam { get; set; }
        public String dateOfBirth { get; set; }

        public bool saveSportman()
        {
            try
            {
                codeSportsman++;
                this.sportsmanId = codeSportsman;
                Storage.Instance.listSportman.Add(this);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool deleteSportman(){
            return true;
        }
        public bool updateSportman()
        {
            return true;
        }
        
    }
}