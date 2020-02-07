using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @author: Victor Noe Hernández
 * @version: 1.0.0
 * @description: class for generate the nodes. 
 */

namespace CustomGenerics.Structures {
    
    public class Node <T> {
        //Class parameters
        public Node<T> nextNode { get; set; }
        public T value { get; set; }
    }
}
