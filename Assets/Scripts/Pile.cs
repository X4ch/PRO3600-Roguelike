using System;
using System.Collections.Generic;

class Pile<T> {
        List<T> pile;

        public Pile() {
            List<T> pile = new List<T>();
            this.pile = pile;
        }

        public void push(T obj) {
            this.pile.Add(obj);
        }

        public T pop() {
            T result = this.pile[this.pile.Count - 1];
            this.pile.RemoveAt(this.pile.Count - 1);
            return result;
        }

        public int size() {
            return this.pile.Count;
        }

        public bool isEmpty() {
            if (this.size() == 0) {
                return true;
            }
            return false;
        }

        public T top() {
            T result = this.pile[this.pile.Count - 1];
            return result;
        }
}