using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//ЗАДАНИЕ 2
//Напишите программу, которая считает размер папки на диске (вместе со всеми
//вложенными папками и файлами).
//На вход метод принимает URL директории, в ответ — размер в байтах.
//Подсказка
//Чтобы учитывать вложенные папки, используйте рекурсию.


namespace Module8Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к директории: ");
            string path = Console.ReadLine();           
            try
            {
                if(!Directory.Exists(path))
                {
                    Console.WriteLine("Директория не найдена.");

                }
                else
                {
                    
                    long size = SizeOff(path);
                    Console.Clear();
                    Console.WriteLine($"Размер: {size} байт.");
                }
               

            }
            catch (Exception ex)
            {

                Console.WriteLine( ex.Message);
                
            }
           




            Console.ReadKey();
        }

        static long SizeOff(string path)
        {
            DirectoryInfo rootDir = new DirectoryInfo(path);
            long size = 0;
            foreach (var file in rootDir.GetFiles())
            {
                size += file.Length;
            }

            foreach (var dir in rootDir.GetDirectories())
            {
               size += SizeOff(dir.FullName);
            }
            return size;
        }



    }
}
