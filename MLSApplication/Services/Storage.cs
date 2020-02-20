using CustomGenerics.Structures;
using MLSApplication.Models;
using System.Collections.Generic;

/*
 * @author: Victor Noe Hernández
 * @version: 1.0.0
 * @description: class for create the list of Sportmans
 */

namespace MLSApplication.Services {
    public class Storage {
        
        private static Storage _instance = null;
        
        public static Storage Instance{
            get {
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }

        public LinkedList<Sportsman> listSportman = new LinkedList<Sportsman>();
        public DoublyLinkedList<Sportsman> doublylistSportman = new DoublyLinkedList<Sportsman>();
        public bool selectionList;
    }
}