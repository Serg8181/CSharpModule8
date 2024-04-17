using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module8Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке: ");
            string path = Console.ReadLine();

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Папки не существует.");
            }
            else
            {
                try
                {
                    DirectoryInfo rootDir = new DirectoryInfo(path);
                    long startSize = SizeOff(path);
                    Console.Clear();
                    Console.WriteLine($"Исходный размер папки {rootDir.Name}: {startSize} байт");
                    Console.WriteLine();
                    int count = 0;
                    DeleteDirectory(rootDir, ref count);
                    long endSize = SizeOff(path);
                    Console.WriteLine($"Удалено: {count} файлов");
                    Console.WriteLine();
                    Console.WriteLine($"Освобождено: {startSize - endSize} байт");
                    Console.WriteLine();
                    Console.WriteLine($"Текущий размер папки {rootDir.Name}: {endSize} байт");

                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            Console.ReadKey();
        }
        static void DeleteDirectory(DirectoryInfo rootDir,ref int count)
        {

            foreach (var file in rootDir.GetFiles())
            {

                if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(1))
                {
                    file.Delete();
                    //Console.WriteLine($"Файл не использовался {DateTime.Now - file.LastAccessTime}. Файл: {file.Name} удален.");
                    count ++;
                }
            }


            foreach (var dir in rootDir.GetDirectories())
            {
                if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(1))
                {

                    DeleteDirectory(dir,ref count);
                    dir.Delete(true);
                    //Console.WriteLine($"Папка не использовалась {DateTime.Now - dir.LastAccessTime}. Папка: {dir.Name} удалена.");

                }

            }


        }
        static long SizeOff(string path)
        {
            long size = 0;
            DirectoryInfo rootDir = new DirectoryInfo(path);
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
