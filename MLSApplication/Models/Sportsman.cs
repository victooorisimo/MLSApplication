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

        public bool saveSportman(bool structureType){
            try {
                
                codeSportsman++;
                this.sportsmanId = codeSportsman;

                if (structureType){
                    Storage.Instance.listSportman.Add(this);
                }else {
                    Storage.Instance.doublylistSportman.pushInList(this);
                }
                
                return true;
            }catch (Exception e) {
                return false;
            }

        }
        public bool deleteSportman(int id, bool structureType){
            if (structureType){
                Storage.Instance.listSportman.Where(c => c.sportsmanId == id).FirstOrDefault();
                return true;
            }else{
                return false;
            }
        }
        public bool updateSportman(){
            try{
                return true;
            }catch (Exception e){
                return false;
            }
        }
        
    }
}