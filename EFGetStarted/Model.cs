using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Discriminator { get; set; }
}

    public class Teacher : Person
{
    public int Salaru { get; set; } // ЗП
    public string Car { get; set; }
    public int MinCost { get; set; }
    public int MaxCost { get; set; }
     public List<TeacherLesson> TeacherLessons { get; set; } // связь с классом Lessons, многие ко многим, список учителей, которые преподают урок
    public Teacher()
    {
        TeacherLessons = new List<TeacherLesson>();
    }
}

 public class Student : Person
{
    public string FatherSWork { get; set; } // Работа отца
    public int FatherSSalaru { get; set; } // ЗП отца
    public string MatherSWork { get; set; } // Работа матери
    public int MatherSalaru { get; set; } // ЗП матери
    public Group Group { get; set; } // связь с классом Group, один ко многим, студент в одной группе
    public List<StudentLesson> StudentLessons { get; set; } // связь с классом Lesson, многие ко многим, список уроков студента
    public Student()
    {
        StudentLessons = new List<StudentLesson>();
    }
}
 public class Group
{
    public int Id { get; set; }
    public string Title { get; set; } // ЗП
    public int Level { get; set; }
    public int Max { get; set; }
    public int Min { get; set; }
    public List<Student> Students { get; set; } // связь с классом Student, один ко многим, список студентов в группе
}
 public class Lesson
{
    public int Id { get; set; }
    public string Title { get; set; } // ЗП
    public int Level { get; set; }
    public int Max_cost { get; set; }
    public int Min_cost { get; set; }
    public List<StudentLesson> StudentLessons { get; set; } // связь с классом Student, многие ко многим, список студентов, которые изучают урок
    
    public List<TeacherLesson> TeacherLessons { get; set; } // связь с классом Teacher, многие ко многим, список учителей, которые преподают урок
    public Lesson()
    {
        StudentLessons = new List<StudentLesson>();
        TeacherLessons = new List<TeacherLesson>();
    }
}
public class StudentLesson
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
 
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; }
}
public class TeacherLesson
{
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
 
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; }
}

    public class StudentContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<TeacherLesson> TeacherLessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentLesson>()
            .HasKey(t => new { t.StudentId, t.LessonId });
 
        modelBuilder.Entity<StudentLesson>()
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentLessons)
            .HasForeignKey(sc => sc.StudentId);
 
        modelBuilder.Entity<StudentLesson>()
            .HasOne(sc => sc.Lesson)
            .WithMany(c => c.StudentLessons)
            .HasForeignKey(sc => sc.LessonId);

            

        modelBuilder.Entity<TeacherLesson>()
            .HasKey(t => new { t.TeacherId, t.LessonId });
 
        modelBuilder.Entity<TeacherLesson>()
            .HasOne(sc => sc.Teacher)
            .WithMany(s => s.TeacherLessons)
            .HasForeignKey(sc => sc.TeacherId);
 
        modelBuilder.Entity<TeacherLesson>()
            .HasOne(sc => sc.Lesson)
            .WithMany(c => c.TeacherLessons)
            .HasForeignKey(sc => sc.LessonId);

            //modelBuilder.Entity<Teacher>().Property(u => u.Age).HasDefaultValue(18);
    }
         
        public StudentContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=StudentContext.db");
        }
    }
}
