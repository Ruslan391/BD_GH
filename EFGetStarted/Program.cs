using System;
using System.Linq;
 
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
                //Person person3 = new Person {Name = "Sergei", Surname = "Shekshuev", Age = "28", Gender = "Female"};
                db.Persons.AddRange(student1,student2);
                db.SaveChanges();
                Console.WriteLine("Пёрсоны добавлены");
//    Вывод пёрсонов
                var users1 = db.Persons.ToList();
                Console.WriteLine("Список Пёрсонов:");
                foreach (Person q in users1)
                {
                    Console.WriteLine($"{q.Id}.Имя - {q.Name}, Возраст - {q.Age}, пол - {q.Gender}, роль - {q.Discriminator}");
                }


                Teacher teacher1 = new Teacher {Name = "Sergei", Surname = "Shekshuev", Age = 28, Gender = "Male", Salaru = 40, Car = "N.Kashkai", MinCost = 10, MaxCost = 20};
                Teacher teacher2 = new Teacher {Name = "Anton", Surname = "Borodashenko", Age = 40, Gender = "Male", Salaru = 60, Car = "K.Sportag", MinCost = 50, MaxCost = 70};
                db.Persons.AddRange(teacher1,teacher2);
                db.SaveChanges();
                Console.WriteLine("Тичеры добавлены");
//    Вывод до изменения
                Console.WriteLine("Список объектов до того как Шекша изнасиловали:");
                var users2 = db.Persons.ToList();
                foreach (Person q in users2)
                {
                    Console.WriteLine($"{q.Id}.Имя - {q.Name}, Возраст - {q.Age}, пол - {q.Gender}, роль - {q.Discriminator}");
                }
//     Изменение
                Console.WriteLine("Список объектов после того как Шекша изнасиловали:");
                foreach (Person q in users2)
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
//     Удаление
               /* Console.WriteLine("Список объектов после удаления Шекша:");
                foreach (Person q in users2)
                {
                    if (q.Name=="Sergei")
                     {
                         q.Gender="Pidoras";
                         db.Persons.Remove(q);
                         db.SaveChanges();
                        // break;
                     }
//     Вывод после изменения
                }
                var users3 = db.Persons.ToList();
                foreach (Person q in users3)
                {
                    Console.WriteLine($"{q.Id}.Имя - {q.Name}, Возраст - {q.Age}, пол - {q.Gender}, роль - {q.Discriminator}");
                }*/


                Lesson lesson1 = new Lesson {Title = "ПЯВУ", Level = 2};
                Lesson lesson2 = new Lesson {Title = "СЭВМ", Level = 3};
                //Person person3 = new Person {Name = "Sergei", Surname = "Shekshuev", Age = "28", Gender = "Female"};
                db.Lessons.AddRange(lesson1,lesson2);
                db.SaveChanges();
                Console.WriteLine("Курсы добавлены");

                student1.StudentLessons.Add(new StudentLesson { LessonId = lesson1.Id, StudentId = student1.Id });
                student2.StudentLessons.Add(new StudentLesson { LessonId = lesson2.Id, StudentId = student2.Id });
                teacher1.TeacherLessons.Add(new TeacherLesson { LessonId = lesson1.Id, TeacherId = teacher1.Id });
                teacher2.TeacherLessons.Add(new TeacherLesson { LessonId = lesson2.Id, TeacherId = teacher2.Id });
                db.SaveChanges();

                 var var_Lessons = db.Lessons.ToList();
                 var var_Teacher = db.Persons.Select(t => t.Discriminator=="Teacher").ToList();
                 // выводим все курсы
                 foreach (var c in  var_Lessons)
                    {
                      Console.WriteLine($"\n Course: {c.Title}");
                // выводим всех студентов для данного кура
                      var id_t = (from Id_t in db.TeacherLessons
                              where Id_t.LessonId == c.Id
                              select Id_t);
                      /*Console.WriteLine($"\n Course: {(from Id_t in db.TeacherLessons
                              where Id_t.LessonId == c.Id
                              select Id_t)}");*/
                      var students = c.StudentLessons.Select(sc => sc.Student).ToList();
                      foreach (Person s in students)
                    Console.WriteLine($"{s.Name}");
                     }

            
                /*  User user = db.Users.FirstOrDefault();*/

               }
            Console.Read();
        }
    }
}