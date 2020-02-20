using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @author: Victor Noe Hernández
 * @version: 1.0.0
 * @description: class for ILenearDataStructure
 */

namespace CustomGenerics.Interfaces {
    public abstract class ILinearDataStructure <T> {
        //Class methods
        protected abstract void Insert(T value);
        protected abstract void Delete(int value);
        protected abstract T Get();
    }
}
