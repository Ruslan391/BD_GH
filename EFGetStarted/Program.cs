using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
 
namespace EFGetStarted

{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (StudentContext db = new StudentContext())
            {
                Student student1 = new Student {Name = "Ruslan", Surname = "Meboniya", Age = 28, Gender = "Male"};
                Student student2 = new Student {Name = "Roma", Surname = "Shulga", Age = 28, Gender = "Male"};
                Student student3 = new Student {Name = "Dmitrii", Surname = "Juravlev", Age = 28, Gender = "Male"};
                Student student4 = new Student {Name = "Sergei", Surname = "Suslov", Age = 28, Gender = "Male"};
                //Person person3 = new Person {Name = "Sergei", Surname = "Shekshuev", Age = "28", Gender = "Female"};
                //db.Persons.AddRange(student3,student4,student3,student4);
                db.Persons.Add(student1);
                db.Persons.Add(student2);
                db.Persons.Add(student3);
                db.Persons.Add(student4);
                db.SaveChanges();
                Console.WriteLine("Пёрсоны добавлены");
//    Вывод пёрсонов
               
                var stud= (from s in db.Persons where s.Discriminator=="Student" select s).ToList();
                Console.WriteLine("Список Students:");
                foreach (Person qq in stud)
                {
                    Console.WriteLine($"{qq.Id}.Имя - {qq.Name}, Возраст - {qq.Age}, пол - {qq.Gender}, роль - {qq.Discriminator}");
                }


                Teacher teacher1 = new Teacher {Name = "Sergei", Surname = "Shekshuev", Age = 28, Gender = "Male", Salaru = 40, Car = "N.Kashkai", MinCost = 10, MaxCost = 20};
                Teacher teacher2 = new Teacher {Name = "Anton", Surname = "Borodashenko", Age = 40, Gender = "Male", Salaru = 60, Car = "K.Sportag", MinCost = 50, MaxCost = 70};
                db.Persons.AddRange(teacher1,teacher2);
                db.SaveChanges();
                Console.WriteLine("Тичеры добавлены");
                 var teach= (from t in db.Persons where t.Discriminator=="Teacher" select t).ToList();

                Console.WriteLine("Список Teachers:");
                foreach (Person q in teach)
                {
                    Console.WriteLine($"{q.Id}.Имя - {q.Name}, Возраст - {q.Age}, пол - {q.Gender}, роль - {q.Discriminator}");
                }
//    Вывод до изменения
                Console.WriteLine("Список Teachers до того как Шекша изнасиловали:");
                //var users2 = db.Persons.ToList();
                foreach (Person q in teach)
                {
                    Console.WriteLine($"{q.Id}.Имя - {q.Name}, Возраст - {q.Age}, пол - {q.Gender}, роль - {q.Discriminator}");
                }
//     Изменение
                Console.WriteLine("Список Teachers после того как Шекша изнасиловали:");
                foreach (Person q in teach)
                {
                    if (q.Name=="Sergei")
                     {
                         q.Gender="Pidoras";
                         db.Persons.Update(q);
                         db.SaveChanges();
                     }
//     Вывод после изменения
                Console.WriteLine($"{q.Id}.Имя - {q.Name}, Возраст - {q.Age}, пол - {q.Gender}, роль - {q.Discriminator}");
                }
                Lesson lesson1 = new Lesson {Title = "ПЯВУ", Level = 2};
                Lesson lesson2 = new Lesson {Title = "СЭВМ", Level = 3};
                //Person person3 = new Person {Name = "Sergei", Surname = "Shekshuev", Age = "28", Gender = "Female"};
                db.Lessons.AddRange(lesson1,lesson2);
                db.SaveChanges();
                Console.WriteLine("Курсы добавлены");

                student1.StudentLessons.Add(new StudentLesson { LessonId = lesson1.Id, StudentId = student1.Id });
                student2.StudentLessons.Add(new StudentLesson { LessonId = lesson2.Id, StudentId = student2.Id });
                student3.StudentLessons.Add(new StudentLesson { LessonId = lesson1.Id, StudentId = student3.Id });
                student4.StudentLessons.Add(new StudentLesson { LessonId = lesson2.Id, StudentId = student4.Id });
                teacher1.TeacherLessons.Add(new TeacherLesson { LessonId = lesson1.Id, TeacherId = teacher1.Id });
                teacher2.TeacherLessons.Add(new TeacherLesson { LessonId = lesson2.Id, TeacherId = teacher2.Id });
                
                db.SaveChanges();


                 var var_Lessons = db.Lessons.ToList();
                 // выводим все курсы
                 Console.WriteLine();
                 foreach (var c in  var_Lessons)
                    {
                      Console.WriteLine($" Lessons: {c.Title}");
                      var id_teacher = (from Id_t in db.TeacherLessons where Id_t.LessonId == c.Id select Id_t.TeacherId).First();
                // выводим преподов данного курса
                      Console.WriteLine($" Teacher: {(from t in db.Persons where t.Id == id_teacher select t.Name).First()}");
                      var students = c.StudentLessons.Select(sc => sc.Student).ToList();


                 // выводим всех студентов для данного кура
                      Console.Write($" Student:");
                      foreach (Person s in students) Console.Write($" {s.Name} ");
                      Console.WriteLine("\n");
                     }

            


               }
            Console.Read();
        }
    }
}