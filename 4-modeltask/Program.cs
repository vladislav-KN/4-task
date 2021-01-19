using System;
using System.Collections.Generic;

namespace _4_modeltask
{
    class Program
    {
        static List<int[]> BubbleSortA(List<int[]> mas)
        {
            int[] temp;
            for (int i = 0; i < mas.Count - 1; i++)
            {
                for (int j = 0; j < mas.Count - i - 1; j++)
                {
                    if (mas[j + 1][0] < mas[j][0])
                    {
                        temp = mas[j + 1];
                        mas[j + 1] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
            return mas;
        }
        static List<int[]> BubbleSortB(List<int[]> mas)
        {
            int last = mas.Count;
            for (bool sorted = last == 0; !sorted; --last)
            {
                sorted = true;
                for (int i = 1; i < last; ++i)
                {
                    if (mas[i - 1][1] < mas[i][1])
                    {
                        sorted = false;

                        int[] tmp = mas[i - 1];
                        mas[i - 1] = mas[i];
                        mas[i] = tmp;
                    }
                }
            }
            return mas;
        }
        static int WhenEnds(List<int[]> GrA, List<int[]> GrB, int ind)
        {
            int time = 0;
            int indx = 0;
            for (int i = 0; i < GrA.Count; i++, indx++)
            {
                int prevtime = time;
                time += GrA[i][0];
                if(indx == ind)
                {
                    return time;
                }
            }
            for (int i = 0; i < GrB.Count; i++, indx++)
            {
                int prevtime = time;
                time += GrB[i][0];
                if (indx == ind)
                {
                    return time;
                }
            }
            return time;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите кол-во задач");
            int n = int.Parse(Console.ReadLine());
            int[][] mass = new int[n][];
            for(int i = 0; i <n; i++)
            {
                int[] task = new int[2];
                Console.Write($"Длительность первого этапа, задачи {i}: ");
                task[0] = int.Parse(Console.ReadLine());
                Console.Write($"Длительность второго этапа, задачи {i}: ");
                task[1] = int.Parse(Console.ReadLine());
                mass[i] = task;
            }
            List<int[]> GrA = new List<int[]>();
            List<int[]> GrB = new List<int[]>();
            int indx = 0;
            foreach(int[] k in mass)
            {
                int[] add = new int[] { k[0], k[1], indx };
                if (k[0] > k[1])
                {
                   
                    GrB.Add(add);
                }
                else
                {
                    GrA.Add(add);
                }
                indx++;
            }
            GrA = BubbleSortA(GrA);
            GrB = BubbleSortB(GrB);
            string posl = "";
            for (int i = 0; i < GrA.Count;i++)
            {
                posl += GrA[i][2];
                if (i != GrA.Count - 1)
                {
                    posl += ", ";
                }
            }
            if (GrB.Count > 0)
            {
                posl += ", ";
                for (int i = 0; i < GrB.Count; i++)
                {
                    posl += GrB[i][2];
                    if (i != GrB.Count - 1)
                    {
                        posl += ", ";
                    }
                }
            }
            Console.WriteLine($"В результате получим последовательность: {posl}");
            string[] poslArr = posl.Split(", ");
            int time = 0;
            Console.WriteLine("1ый конвейер");
            indx = 0;
            for (int i = 0; i < GrA.Count; i++, indx++)
            {
                int prevtime = time;
                time += GrA[i][0];
                Console.Write($"|{poslArr[indx]} ({prevtime}-{time})|");
            }
            for (int i = 0; i < GrB.Count; i++, indx++)
            {
                int prevtime = time;
                time += GrB[i][0];
                Console.Write($"|{poslArr[indx]} ({prevtime}-{time})|");
            }
            Console.WriteLine("2ой конвейер");
            
            indx = 0;
            time = WhenEnds( GrA,GrB,indx);
            for (int i = 0; i < GrA.Count; i++, indx++)
            {
                int prevtime = WhenEnds(GrA, GrB, indx);
                if (time >= prevtime)
                {
                    prevtime = time;
                    time += GrA[i][1];
                }
                else
                {
                    time = GrA[i][1] + prevtime;
                }
                Console.Write($"|{poslArr[indx]} ({prevtime}-{time})|");
            }
            for (int i = 0; i < GrB.Count; i++, indx++)
            {
                int prevtime = WhenEnds(GrA, GrB, indx);
                if (time >= prevtime)
                {
                    prevtime = time;
                    time += GrB[i][1];
                }
                else
                {
                    time = GrB[i][1] + prevtime;
                }
                Console.Write($"|{poslArr[indx]} ({prevtime}-{time})|");
            }
        }
    }
}
