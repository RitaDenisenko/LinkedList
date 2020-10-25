using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private object head;
        private LinkedList<T> tail;

        public T HeadValue
        {
            get { return (T)head; }
        }

        public bool IsEmpty
        {
            get{ return head == null; }
        }

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public LinkedList(LinkedList<T> newList)
        {
            if (newList.head == null)
                head = null;
            else
                head = newList.HeadValue;
            
            if (newList.tail == null)
                tail = null;
            else
                tail = new LinkedList<T>(newList.tail);
        }

        public LinkedList(T newHead, LinkedList<T> newTail)
        {
            head = newHead;
            if (newTail == null)
                tail = null;
            else
                tail = new LinkedList<T>(newTail);
        }

        public void AddElementToHead(T newElement)
        {
            if (!this.IsEmpty)
            {
                tail = new LinkedList<T>(this);
            }

            head = newElement;          
        }

        public void DeleteElementFromHead()
        {
            bool lengthMoreThenOne = tail != null;
            if (lengthMoreThenOne)
            {
                head = tail.head;
                tail = tail.tail;
            }
            else
                head = null;
        }

        public T GetFirstElement()
        {
            if (!this.IsEmpty)
                return HeadValue;
            else
                throw new InvalidOperationException("Нельзя вернуть первый элемент пустого списка");
        }

        public LinkedList<T> GetTail()
        {
            return tail;
        }

        public void PrintList()
        {
            foreach(T element in this)
                System.Console.Write(element + " ");

            System.Console.WriteLine();
        }

        public void Sort()
        { 
            if (tail != null)
            {
                T temporary;
                for (LinkedList<T> pointer1 = this; pointer1 != null; pointer1 = pointer1.tail)
                    for (LinkedList<T> pointer2 = pointer1.tail; pointer2 != null; pointer2 = pointer2.tail)
                    {
                        bool currentElementBiggerThenNext = (pointer1.HeadValue).CompareTo(pointer2.HeadValue) > 0;
                        if (currentElementBiggerThenNext)
                        {
                            temporary = pointer1.HeadValue;
                            pointer1.head = pointer2.HeadValue;
                            pointer2.head = temporary;
                        }
                    }  
            }
        }

        public LinkedList<T> MakeSortedListWithElement(T newElement)
        {
            LinkedList<T> result = new LinkedList<T>();

            if (this.IsEmpty)
            {
                result.AddElementToHead(newElement);
            }
            else
            {
                bool isOnlyElement = tail == null;
                if (isOnlyElement)
                {
                    bool newElementBiggerThenCurrent = newElement.CompareTo(HeadValue) >= 0;
                    if (newElementBiggerThenCurrent)
                        result.AddElementToHead(newElement);

                    result.AddElementToHead(HeadValue);
                    
                }
                else
                {
                    result = tail.MakeSortedTailWithElement(newElement);  
                    result.AddElementToHead(HeadValue);
                }

                bool newElementLessThenFirst = newElement.CompareTo(HeadValue) < 0;
                if (newElementLessThenFirst)
                {
                    result.AddElementToHead(newElement);
                }
            }
            
            return result;
        }


        private LinkedList<T> MakeSortedTailWithElement(T newElement)
        {
            LinkedList<T> result = new LinkedList<T>();

            bool isLastElement = tail == null;
            if (isLastElement)
            {
                bool newElementBiggerThenLast = newElement.CompareTo(HeadValue) >= 0;
                if (newElementBiggerThenLast)
                    result.AddElementToHead(newElement);

                result.AddElementToHead(HeadValue);
            }
            else
            {
                result = tail.MakeSortedTailWithElement(newElement);

                bool newElementBiggerThenCurrent = newElement.CompareTo(HeadValue) >= 0;
                bool newElementLessThenNext = newElement.CompareTo(tail.HeadValue) < 0;
                if (newElementBiggerThenCurrent && newElementLessThenNext)
                    result.AddElementToHead(newElement);

                result.AddElementToHead(HeadValue);
            }

            return result;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        class LinkedListEnumerator<T> : IEnumerator<T> where T : IComparable<T>
        {
            private LinkedList<T> linkedList;
            private int position = -1;

            private bool IsReseted
            {
                get { return position == -1; }
            }

            public LinkedListEnumerator(LinkedList<T> newList)
            {
                linkedList = newList;
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    if (!IsReseted)
                    {
                        LinkedList<T> pointer = linkedList;

                        for (int i = 0; (i != position) && (pointer != null); i++)
                        {
                            pointer = pointer.tail;
                        }

                        bool outOfLength = pointer == null;
                        if (!outOfLength)
                            return pointer.HeadValue;
                        else
                            throw new InvalidOperationException();
                    }
                    else
                        throw new InvalidOperationException();

                }
            }

            bool IEnumerator.MoveNext()
            {
                if (!linkedList.IsEmpty)
                {
                    LinkedList<T> pointer = linkedList;
                    for (int i = 0; (i != position) && (pointer.tail != null); i++)
                    {
                        pointer = pointer.tail;
                    }

                    bool notEndOfList = pointer.tail != null;
                    if (notEndOfList || IsReseted)
                    {
                        position++;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

            void IEnumerator.Reset()
            {
                position = -1;
            }

            void IDisposable.Dispose()
            {

            }

            object IEnumerator.Current => throw new NotImplementedException();
        }

    }
}
