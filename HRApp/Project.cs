using HRLib.Interfaces;

namespace HRLib.Models
{
    /// Проект, у якому беруть участь робітники.
    public class Project : ISearchable, IDisplayable
    {
        //Назва проекту
        public string Name { get; set; }

        //Вартість проекту
        public decimal Cost { get; set; }

        // Опис проекту
        public string Description { get; set; }

        /// Ініціалізує новий проект.
        public Project(string name, decimal cost, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва проекту не може бути порожньою.");
            if (cost < 0)
                throw new ArgumentException("Вартість проекту не може бути від'ємною.");

            Name = name;
            Cost = cost;
            Description = description;
        }

        public bool ContainsKeyword(string keyword)
        {
            string kw = keyword.ToLower();
            return Name.ToLower().Contains(kw) || Description.ToLower().Contains(kw);
        }

        public string GetInfo() =>
            $"Проект: {Name} | Вартість: {Cost:C} | Опис: {Description}";

        public override string ToString() => Name;
    }
}
