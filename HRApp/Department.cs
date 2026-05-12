using HRLib.Interfaces;

namespace HRLib.Models
{
    /// Підрозділ організації. Агрегує робітників.
    public class Department : ISearchable, IDisplayable
    {
        //Назва підрозділу.
        public string Name { get; set; }

        //Список робітників підрозділу (агрегація — робітники існують незалежно)
        private readonly List<Worker> _workers = new();

        //Публічний доступ до списку робітників (тільки для читання)
        public IReadOnlyList<Worker> Workers => _workers.AsReadOnly();

        /// Ініціалізує новий підрозділ.
        public Department(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва підрозділу не може бути порожньою.");
            Name = name;
        }

        //Додає робітника до підрозділу
        public void AddWorker(Worker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));
            if (!_workers.Contains(worker))
                _workers.Add(worker);
        }

        //Видаляє робітника з підрозділу.
        public void RemoveWorker(Worker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));
            _workers.Remove(worker);
        }

        //Змінює назву підрозділу
        public void Update(string name) => Name = name;

        /// Повертає список робітників, відсортованих за посадою.
        public IEnumerable<Worker> GetWorkersSortedByPosition() =>
            _workers.OrderBy(w => w.Position.Title);

        /// Повертає список робітників, відсортованих за сумарною вартістю проектів.
        public IEnumerable<Worker> GetWorkersSortedByProjectsCost() =>
            _workers.OrderByDescending(w => w.TotalProjectsCost);

        public bool ContainsKeyword(string keyword) =>
            Name.ToLower().Contains(keyword.ToLower());

        public string GetInfo() =>
            $"Підрозділ: {Name} | Кількість робітників: {_workers.Count}";

        public override string ToString() => Name;
    }
}
