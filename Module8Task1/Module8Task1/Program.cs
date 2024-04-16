using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Module8Task1
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
                    SearchDirectory(rootDir);

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

        static void SearchDirectory(DirectoryInfo rootDir)
        {

            foreach (var file in rootDir.GetFiles())
            {
                if (DateTime.Now - file.LastAccessTime > TimeSpan.FromMinutes(30))
                {
                    file.Delete();
                    Console.WriteLine($"Файл не использовался {DateTime.Now - file.LastAccessTime}. Файл: {file.Name} удален.");
                }
            }

            foreach (var dir in rootDir.GetDirectories())
            {
                if (DateTime.Now - dir.LastAccessTime > TimeSpan.FromMinutes(30))
                {
                    SearchDirectory(dir);
                    dir.Delete(true);
                    Console.WriteLine($"Папка не использовалась {DateTime.Now - dir.LastAccessTime}. Папка: {dir.Name} удалена.");
                }

            }

        }
    }
}
