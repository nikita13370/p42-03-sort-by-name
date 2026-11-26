using System.Globalization;

class Program
{
    // Отсортируйте массив студентов по имени (Name) в алфавитном порядке, прямо в этом массиве.
    // Сравнение строк: string.Compare(a.Name, b.Name) < 0 — значит a стоит раньше b.
    // Напишите сортировку сами (например, пузырьком), меняя местами элементы массива.
    static void SortByName(Student[] students)
    {
       for (int i = 0; i < students.Length - 1; i++)
{
    for (int j = 0; j < students.Length - 1 - i; j++)
    {
        if (string.Compare(students[j].Name, students[j + 1].Name) > 0)
        {
            Student temp = students[j];
            students[j] = students[j + 1];
            students[j + 1] = temp;
        }
    }
}
    }

    // Перенесите сюда свой метод FindHigh из Задания 8:
    // новый массив из студентов, у которых есть оценки и средний балл >= minAverage.
    static Student[] FindHigh(Student[] group, double minAverage)
    {
        int count = 0;

foreach (Student s in group)
{
    if (s.HasScores && s.Average >= minAverage)
    {
        count++;
    }
}

Student[] result = new Student[count];

int index = 0;

foreach (Student s in group)
{
    if (s.HasScores && s.Average >= minAverage)
    {
        result[index] = s;
        index++;
    }
}

return result;
    }

    // Верните студентов с высоким баллом, отсортированных по имени.
    // Используйте FindHigh и SortByName. Исходный массив group менять нельзя.
    static Student[] HighSortedByName(Student[] group, double minAverage)
    {
        Student[] result = FindHigh(group, minAverage);

SortByName(result);

return result;
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("ru-RU"); // дробная часть через запятую и русский алфавит

        Student[] three =
        {
            SampleData.Make("Ким Анна", 90),
            SampleData.Make("Ким Алексей", 80),
            SampleData.Make("Ахметова Айгерим", 95),
        };
        SortByName(three);
        Check("SortByName(Ким Анна, Ким Алексей, Ахметова Айгерим)", Names(three),
              "Ахметова Айгерим, Ким Алексей, Ким Анна");

        Student[] all = SampleData.Group();
        SortByName(all);
        Check("SortByName(вся группа)", Names(all),
              "Ахметова Айгерим, Бекова Мадина, Денисова Ольга, Жумабаев Нурлан, Ким Анна, Ли Виктор, Петровых Александр, Сериков Дамир");

        Student[] group = SampleData.Group();
        Check("HighSortedByName(group, 85)", Names(HighSortedByName(group, 85)),
              "Ахметова Айгерим, Денисова Ольга, Жумабаев Нурлан, Ким Анна");
        Check("HighSortedByName(group, 90)", Names(HighSortedByName(group, 90)),
              "Ахметова Айгерим, Жумабаев Нурлан, Ким Анна");
        Check("group после HighSortedByName (порядок не изменился)", Names(group),
              "Сериков Дамир, Ахметова Айгерим, Ким Анна, Петровых Александр, Бекова Мадина, Ли Виктор, Жумабаев Нурлан, Денисова Ольга");

        Console.WriteLine();
        Console.WriteLine("Средний балл от 85, по алфавиту:");
        foreach (Student s in HighSortedByName(group, 85))
            Console.WriteLine(s);
    }

    // Имена через запятую; null в массиве показывается как "null".
    static string Names(Student[] a)
    {
        if (a.Length == 0) return "(пусто)";
        string result = a[0]?.Name ?? "null";
        for (int i = 1; i < a.Length; i++)
            result += ", " + (a[i]?.Name ?? "null");
        return result;
    }

    static void Check(string what, string result, string expected)
    {
        string status = result == expected ? "OK" : "ОШИБКА";
        Console.WriteLine($"{what}: получено {result}, ожидалось {expected} - {status}");
    }
}
