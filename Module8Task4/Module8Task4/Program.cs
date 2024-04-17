using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Module8Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //создаем список студентов
            List<Student> studentsToFile = new List<Student>
            {
                new Student { Name = "Андрей", Group = "23", DateOfBirth = new DateTime(1985, 05, 02), AverageScore = 4M },
                new Student { Name = "Сергей", Group = "23", DateOfBirth = new DateTime(1985, 06, 01), AverageScore = 5M},
                new Student { Name = "Дмитрий", Group = "12", DateOfBirth = new DateTime(1990, 11, 23), AverageScore =2.5M},
                new Student { Name = "Михаил", Group = "2", DateOfBirth = new DateTime(1995, 07, 28), AverageScore = 2M},
                new Student { Name = "Олег", Group = "23", DateOfBirth = new DateTime(1985, 05, 02), AverageScore = 4M },
                new Student { Name = "Константин", Group = "2", DateOfBirth = new DateTime(1981, 02, 01), AverageScore = 5M},
                new Student { Name = "Григорий", Group = "12", DateOfBirth = new DateTime(1991, 12, 23), AverageScore =2.9M},
                new Student { Name = "Михаил", Group = "2", DateOfBirth = new DateTime(1990, 03, 18), AverageScore = 5M}
            };
            //записываем данные в файл в бинарном виде
            WriteToFile("students.dat", studentsToFile);

            //считываем данные из бинарного файла
            List<Student> students = ReadFromFile("students.dat");

            //выводим результат в консоль
            foreach (var item in students)
            {
                Console.WriteLine($"{item.Name} {item.Group} {item.DateOfBirth} {item.AverageScore}");
            }

            //
            Console.WriteLine();
            Console.WriteLine("Введите путь к папке: ");
            string path = Console.ReadLine();
            DirectoryInfo dir = new DirectoryInfo(path);
            //проверяем существует ли директория
            if (!dir.Exists)
            {
                //если нет, создаем папку Students
                dir.Create();
               
            }
            //создаем и записываем данные в файл в текстовом ввиде
            foreach (var item in students)
            {
                string fileName = path + "\\Group " + item.Group + ".txt";
                FileStream fs = new FileStream(fileName, FileMode.Append);
                WriteToFile(fs, item);

            }

            Console.WriteLine();
            Console.WriteLine("Сортировка по группам завершена.");
            Console.ReadKey();
        }
        //запись в текстовый файл построчно
        static void WriteToFile(FileStream fs, Student student)
        {

            using (StreamWriter writer = new StreamWriter(fs,Encoding.UTF8))
            {
               writer.WriteLine(student.Name + " " + student.DateOfBirth+ " " + student.AverageScore);                           

            }
            
        }
        //запись в бинарный файл
        static void WriteToFile(string fileName ,List<Student> students)
        {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName,FileMode.OpenOrCreate)))
            {
                foreach(var student in students)
                {
                    writer.Write(student.Name );
                    writer.Write(student.Group );
                    writer.Write(student.DateOfBirth.ToBinary());
                    writer.Write(student.AverageScore );              
                }

            }


        }
        //чтение из бинарного файла
        static List<Student> ReadFromFile(string fileName)
        {
            List<Student> students = new List<Student>();

            using(BinaryReader reader = new BinaryReader(File.Open(fileName,FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    Student student = new Student();
                    student.Name = reader.ReadString();
                    student.Group = reader.ReadString();
                    long score = reader.ReadInt64();
                    student.DateOfBirth = DateTime.FromBinary(score);
                    student.AverageScore = reader.ReadDecimal();
                    students.Add(student);

                }
               

            }

            return students;
        }
    }
}
