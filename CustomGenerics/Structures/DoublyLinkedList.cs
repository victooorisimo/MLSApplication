using CustomGenerics.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomGenerics.Structures {
    public class DoublyLinkedList<T> : ILinearDataStructure<T>, IEnumerable<T> {

        private Node<T> firstNode;
        private Node<T> lastNode;

        //Is empty method.
        public bool isEmpty(){
            return firstNode == null;
        }

        //Push in list
        public void pushInList(T value){
            Insert(value);
        }

        //Insert method.
        protected override void Insert(T value){
            Node<T> newObject = new Node<T>();
            newObject.value = value;
            if (isEmpty()){
                lastNode = newObject;
                firstNode = newObject;
                lastNode.nextNode = firstNode;
                firstNode.previousNode = lastNode;
            }else{
                lastNode.nextNode = newObject;
                newObject.previousNode = lastNode;
                lastNode = newObject;
                lastNode.nextNode = firstNode;
                firstNode.previousNode = lastNode;
            }

        }

        //Get list size
        public int getSizeList(){
            Node<T> cantValues = new Node<T>();
            cantValues = firstNode;
            int cant = 1;
            while (cantValues.nextNode != firstNode){
                cant++;
                cantValues = cantValues.nextNode;
            }
            return cant;
        }

        //Find object in list
        public int findObjectList(T value){
            Node<T> findNode = new Node<T>();
            findNode = firstNode;
            int position = 0;
            for (int i = 0; i < getSizeList(); i++){
                if (value.Equals(findNode.value)){
                    position = i;
                }
                findNode = findNode.nextNode;
            }
            return position;
        }

        //Pop in list
        public void popInList(T value){
            Delete(findObjectList(value));
        }

        //Delete object in list
        protected override void Delete(int value){
            Node<T> deleteNode = new Node<T>();
            deleteNode = firstNode;
            for (int i = 1; i < value; i++){
                deleteNode = deleteNode.nextNode;
            }
            (deleteNode.nextNode).previousNode = deleteNode.previousNode;
            (deleteNode.previousNode).nextNode = deleteNode.nextNode;
            }

        public void getObject(){
            Get();
        }

        //Get objetct of list
        protected override T Get(){
            Node<T> currentLink = firstNode;
            currentLink = firstNode;
            int iterations = getSizeList();
            for (int i = 0; i < iterations; i++){
                return currentLink.value;
                currentLink = currentLink.nextNode;
            }
            return currentLink.value;
        }

        //Inumerable Get enumerator
        IEnumerator IEnumerable.GetEnumerator(){
            return GetEnumerator();
        }

        //GetEnumerator
        public IEnumerator<T> GetEnumerator() {
            Node<T> currentLink = firstNode;
            int iteration = 0;
            int sizeList = getSizeList();
            while (iteration < sizeList) {
                yield return currentLink.value;
                currentLink = currentLink.nextNode;
                iteration++;
            }
        }

    }
}
