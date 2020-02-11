using CustomGenerics.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGenerics.Structures {
    class DoublyLinkedList<T> : ILinearDataStructure<T>, IEnumerable<T> {

        private Node<T> firstNode;
        private Node<T> lastNode;

        public int Size(){
            Node<T> cantValues = new Node<T>();
            cantValues = firstNode;
            int cant = 1;
            while (cantValues.nextNode != firstNode){
                cant++;
                cantValues = cantValues.nextNode;
            }
            return cant;
        }

        protected override void Insert(T value) {
            Node<T> newValue = new Node<T>();
            newValue.value = value;
            if (firstNode == null){
                firstNode = newValue;
                lastNode = newValue;
                firstNode.previousNode = lastNode;
                lastNode.nextNode = firstNode;

            }else{
                lastNode.nextNode = newValue;
                newValue.previousNode = lastNode;
                lastNode = newValue;
                lastNode.nextNode = firstNode;
                firstNode.previousNode = lastNode;
            }
        }

        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        public int Find(T value){
            Node<T> findNode = new Node<T>();
            findNode = firstNode;
            int position = 0;
            for (int i = 0; i < Size(); i++) {
                if (value.Equals(findNode.value)) {
                    position = i;
                }
                findNode = findNode.nextNode;
            }
            return position;
        }

        //public T Remove(T value){
        //    Node<T> deleteNode = new Node<T>();
        //    deleteNode = firstNode;
        //    for (int i = 1; i < Find(value); i++) {
        //        deleteNode = deleteNode.nextNode;
        //    }
        //    (deleteNode.nextNode).previousNode = deleteNode.previousNode;
        //    (deleteNode.previousNode).nextNode = deleteNode.nextNode;
        //    return deleteNode.value;
        //}
        


        //public T Remove() {
        //    //var value = Get();
        //    //Delete();
        //    //return value;
        //}

        protected override T Get(T value) {
            Node<T> findNode = new Node<T>();
            Node<T> newNode = new Node<T>();
            for (int i = 0; i < Size(); i++){
                if (value.Equals(findNode.value)){
                    newNode.value = findNode.value;
                }
                findNode = findNode.nextNode;
            }
            return newNode.value;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            //var copyList = this;
            //while (copyList.firstNode != null) {
            //    yield return copyList.Remove();
            //}
            throw new NotImplementedException();
        }
    }
}
