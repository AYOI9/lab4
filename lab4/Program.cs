using System;
using System.Collections.Generic;
using System.Linq;

// Обобщенный класс STUDENT
public class STUDENT<T> : ICloneable, IComparable<STUDENT<T>> where T : IComparable
{
    public string FullName { get; set; }     // Фамилия и инициалы
    public T GroupNumber { get; set; }      // Номер группы (обобщенное поле)
    public int[] Grades { get; set; }       // Успеваемость (массив из 5 оценок)

    public STUDENT(string fullName, T groupNumber, int[] grades)
    {
        FullName = fullName;
        GroupNumber = groupNumber;
        Grades = grades;
    }

    // ICloneable
    public object Clone()
    {
        return new STUDENT<T>(this.FullName, this.GroupNumber, (int[])this.Grades.Clone());
    }

    // IComparable (для сортировки по ФИО)
    public int CompareTo(STUDENT<T> other)
    {
        return this.FullName.CompareTo(other.FullName);
    }

    // Метод для проверки наличия двойки
    public bool HasGradeTwo()
    {
        return Grades.Contains(2);
    }

    // Метод для вычисления среднего балла
    public double GetAverageGrade()
    {
        return Grades.Average();
    }
}

// Класс для сортировки по среднему баллу (реализует IComparer)
public class AverageGradeComparer<T> : IComparer<STUDENT<T>> where T : IComparable
{
    public int Compare(STUDENT<T> x, STUDENT<T> y)
    {
        return x.GetAverageGrade().CompareTo(y.GetAverageGrade());
    }
}

        // Массив из 10 студентов
        STUDENT<string>[] students = new STUDENT<string>[10];

        Console.WriteLine("Введите данные 10 студентов:");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"\nСтудент {i + 1}:");

            Console.Write("Фамилия и инициалы: ");
            string name = Console.ReadLine();

            Console.Write("Номер группы: ");
            string group = Console.ReadLine();

            Console.WriteLine("Введите 5 оценок через пробел:");
            int[] grades = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            students[i] = new STUDENT<string>(name, group, grades);
        }

        // Сортировка по алфавиту
        Array.Sort(students);

        // Студенты с двойками
        Console.WriteLine("\nСтуденты с оценками 2:");
        bool hasTwos = false;

        foreach (var student in students)
        {
            if (student.HasGradeTwo())
            {
                Console.WriteLine($"{student.FullName}, группа {student.GroupNumber}");
                hasTwos = true;
            }
        }

        if (!hasTwos)
        {
            Console.WriteLine("Студентов с оценками 2 не найдено.");
        }

        // Клонирование
        var original = students[0];
        var clone = (STUDENT<string>)original.Clone();
        Console.WriteLine($"\nКлон студента: {clone.FullName}, группа {clone.GroupNumber}");

        // Сортировка по среднему баллу
        Array.Sort(students, new AverageGradeComparer<string>());
        Console.WriteLine("\nСтуденты, отсортированные по среднему баллу:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.FullName}, средний балл: {student.GetAverageGrade():F2}");
        }
