using System;
using System.Security.Cryptography.X509Certificates;
using Test;
class Program
{

    public static List<Pupil> Pupils = new List<Pupil>();
    public static List<Teacher> Teachers = new List<Teacher>();
    public static List<MyTask> Tasks = new List<MyTask>();
    static void Main(string[] args)
    {

        while(true)
        {
            int who = TeacherOrPupil();
            int Id;

             if(who == (int)EnumTeacherOrPupil.Teacher)
            {
                Id = RegisterOrEnter(who);
                int doesId = WhatWillDoTeacher();
                Tasks = CheckTask(teacher: Teachers.Where(x => x.Id == Id).FirstOrDefault(), Tasks);
            }
            else if (who == (int)EnumTeacherOrPupil.Pupil)
            {
                Id = RegisterOrEnter(who);
                int doesId = WhatWillDoPupil();

                if(doesId == (int)EnumWhatWillDoPupil.SentTask)
                {
                    Tasks = AddTask(pupil:Pupils.Where(x => x.Id == Id).FirstOrDefault(), tasks:Tasks);
                }
                else
                {
                    ShowTasks(Tasks);
                }

            }
        }

    }

    public static int TeacherOrPupil()
    {
        Console.WriteLine("Kimsiz");
        Console.WriteLine("1: Uqituvchi");
        Console.WriteLine("2: Uquvchi");

        bool isNumber = int.TryParse(Console.ReadLine(), out int who);
        if (isNumber)
        {
            return who;
        }

        return 0;
    }

    public static int WhatWillDoPupil()
    {
        Console.WriteLine("Nima qilmoqchisiz?");
        Console.WriteLine("1. Vazifa yubormoqchiman");
        Console.WriteLine("2. Vazifamni kurmoqchiman");

        bool isNumber = int.TryParse(Console.ReadLine(), out int who);
        if (isNumber)
        {
            return who;
        }

        return 0;
    }

    public static int WhatWillDoTeacher()
    {
        Console.WriteLine("Nima qilmoqchisiz?");
        Console.WriteLine("1. Vazifani tekshirmoqchiman");

        bool isNumber = int.TryParse(Console.ReadLine(), out int who);
        if (isNumber)
        {
            return who;
        }

        return 0;
    }

    public static int RegisterOrEnter(int who)
    {
        Console.WriteLine("registratsiyadan utganmisiz ha/yuq");
        string answer = Console.ReadLine();
        if (answer == "ha")
        {
            Console.WriteLine("id ni kiriting");
            return int.Parse(Console.ReadLine());
        }
        else
        {
            if (who == (int)EnumTeacherOrPupil.Teacher)
            {
                Teachers = Register(Teachers);
                return Teachers.LastOrDefault().Id;
            }
            else
            {
                Pupils = Register(Pupils);
                return Pupils.LastOrDefault().Id;
            }
        }
    }

    public static List<Teacher> Register(List<Teacher> list)
    {
        Console.WriteLine("Ismingiz");
        string name = Console.ReadLine();
        Console.WriteLine("Id kiriting");
        int id = int.Parse(Console.ReadLine());

        var teacher = new Teacher()
        {
            Name = name,
            Id = id
        };

        list.Add(teacher);

        return list;
    }

    public static List<Pupil> Register(List<Pupil> list)
    {
        Console.WriteLine("Ismingiz");
        string name = Console.ReadLine();
        Console.WriteLine("Id kiriting");
        int id = int.Parse(Console.ReadLine());

        var pupil = new Pupil()
        {
            Name = name,
            Id = id
        };
        list.Add(pupil);
        return list;
    }

    public static List<MyTask> AddTask(Pupil pupil, List<MyTask> tasks)
    {
        Console.WriteLine("Bajarganini kirit:");
        string task = Console.ReadLine();

        var newTtask = new MyTask()
        {
            Id = tasks.Count,
            PupulId = pupil.Id,
            Pupil = pupil,
            Message = task,
        };

        tasks.Add(newTtask);
        return tasks;
    }

    public static List<MyTask> CheckTask(Teacher teacher, List<MyTask> tasks)
    {
        tasks = tasks.Where(x => !x.IsChecked).ToList();

        foreach(var t in tasks)
        {
            Console.WriteLine($"TaskId {t.Id}");
            Console.WriteLine($"TaskMessage {t.Message}");
            Console.WriteLine($"PupilName {t.Pupil.Name}");
        }
        Console.WriteLine("Qaysi birini tekshirmoqchisiz(Id) kiriting");

        int id = int.Parse(Console.ReadLine());

        var task = tasks.Where(x => x.Id == id).FirstOrDefault();

        Console.WriteLine($"TaskId {task.Id}");
        Console.WriteLine($"TaskMessage {task.Message}");
        Console.WriteLine($"PupilName {task.Pupil.Name}");

        Console.WriteLine("Necha ball quymoqchisiz");
        int ball = int.Parse(Console.ReadLine());

        task.Teacher = teacher;
        task.TeacherId = teacher.Id;
        task.Ball = ball;
        task.IsChecked = true;
        task.CheckedTime = DateTime.Now;

        return tasks;
    }

    public static void ShowTasks(List<MyTask> tasks)
    {
        Console.WriteLine("Tekshirilgan vazifalar");

        foreach (var task in tasks.Where(x => x.IsChecked))
        {
            Console.WriteLine("taskId:" + task.Id);
            Console.WriteLine("Message:" + task.Message);
            Console.WriteLine("PupilName:" + task.Pupil.Name);
            Console.WriteLine("TeacherNaem:" + task.Teacher.Name);
            Console.WriteLine("Ball:" + task.Ball);
            Console.WriteLine("Time:" + task.CheckedTime.ToString());
        }
    }

}
enum EnumTeacherOrPupil
{
    Teacher = 1,
    Pupil
}

enum EnumWhatWillDoPupil
{
    SentTask = 1,
    ViewDoneTask = 2
}
