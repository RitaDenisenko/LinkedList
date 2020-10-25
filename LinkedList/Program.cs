using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> intList = new LinkedList<int>();

            for(int i = 1; i <= 5; i++)
            {
                intList.AddElementToHead(10*i);
            }

            Console.WriteLine("Список целых чисел до сортировки:");
            intList.PrintList();

            intList.Sort();
            Console.WriteLine("Список целых чисел после сортировки:");
            intList.PrintList();

            LinkedList<int> newIntList = intList.MakeSortedListWithElement(5);
            Console.WriteLine("Отсортированный список целых чисел после добавления числа 5:");
            newIntList.PrintList();

            newIntList = intList.MakeSortedListWithElement(25);
            Console.WriteLine("Отсортированный список целых чисел после добавления числа 25:");
            newIntList.PrintList();

            Console.WriteLine("Отсортированный список целых чисел после добавления числа 50:");
            newIntList = intList.MakeSortedListWithElement(50);
            newIntList.PrintList();

            Console.WriteLine("Отсортированный список целых чисел после добавления числа 65:");
            newIntList = intList.MakeSortedListWithElement(65);
            newIntList.PrintList();

            LinkedList<string> stringList = new LinkedList<string>();

            for (int i = 1; i <= 5; i++)
            {
                stringList.AddElementToHead("*" + i.ToString() + "*");
            }

            Console.WriteLine("Список строк:");
            stringList.PrintList();
            
            System.Console.ReadLine();

        }
    }
}
