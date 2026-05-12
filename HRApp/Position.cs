using HRLib.Interfaces;

namespace HRLib.Models
{
    /// Посада у відділі кадрів.
    public class Position : ISearchable, IDisplayable
    {
        //Назва посади
        public string Title { get; set; }

        //Кількість робочих годин на тиждень
        public int WorkingHoursPerWeek { get; set; }

        //Базова заробітна плата
        public decimal BaseSalary { get; set; }

        /// Коефіцієнт привабливості посади (зарплата / години).
        public double AttractivenessRatio =>
            WorkingHoursPerWeek > 0 ? (double)BaseSalary / WorkingHoursPerWeek : 0;

        /// Ініціалізує нову посаду.
        public Position(string title, int workingHoursPerWeek, decimal baseSalary)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Назва посади не може бути порожньою.");
            if (workingHoursPerWeek <= 0)
                throw new ArgumentException("Кількість годин має бути більше нуля.");
            if (baseSalary < 0)
                throw new ArgumentException("Зарплата не може бути від'ємною.");

            Title = title;
            WorkingHoursPerWeek = workingHoursPerWeek;
            BaseSalary = baseSalary;
        }

        //Змінює дані посади
        public void Update(string title, int hours, decimal salary)
        {
            Title = title;
            WorkingHoursPerWeek = hours;
            BaseSalary = salary;
        }

        public bool ContainsKeyword(string keyword) =>
            Title.ToLower().Contains(keyword.ToLower());

        public string GetInfo() =>
            $"Посада: {Title} | Годин/тиждень: {WorkingHoursPerWeek} | Зарплата: {BaseSalary:C} | Коефіцієнт: {AttractivenessRatio:F2}";

        public override string ToString() => Title;
    }
}
