using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MLSApplication.Models {

    /*
     * @author: Victor Noe Hernández
     * @version: 1.0.0
     * @description: class for generate operation logs. 
     */

    public class OperationLog {

        public string titleOperation { get; set; }
        public string descriptionOperation { get; set; }
        public string datetimeOperation { get; set; }

        public OperationLog() { }

        public OperationLog(string title, string description, string datetimeO){
            this.titleOperation = title;
            this.descriptionOperation = description;
            this.datetimeOperation = datetimeO;
        }

        public String PrintOperation() {
            return "Title: " + this.titleOperation + " Description: " 
                + this.descriptionOperation + " Time: " + this.datetimeOperation;
        }
    }
}