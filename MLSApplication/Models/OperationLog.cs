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

        string titleOperation { get; set; }
        string descriptionOperation { get; set; }
        DateTime datetimeOperation { get; set; }

        OperationLog(string title, string description, DateTime datetimeO){
            this.titleOperation = title;
            this.descriptionOperation = description;
            this.datetimeOperation = datetimeO;
        }
    }
}