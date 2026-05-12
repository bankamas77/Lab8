using HRLib.Interfaces;
using HRLib.Models;

namespace HRLib.Services
{
    // Головний каталог відділу кадрів.
    // Композиція: містить і повністю контролює списки робітників, підрозділів, посад, проектів.
    // При видаленні каталогу всі дані видаляються разом з ним.
    public class HRCatalog
    {
        private readonly List<Worker> _workers = new();
        private readonly List<Department> _departments = new();
        private readonly List<Position> _positions = new();
        private readonly List<Project> _projects = new();

        //Публічні колекції (тільки читання)
        public IReadOnlyList<Worker> Workers => _workers.AsReadOnly();
        public IReadOnlyList<Department> Departments => _departments.AsReadOnly();
        public IReadOnlyList<Position> Positions => _positions.AsReadOnly();
        public IReadOnlyList<Project> Projects => _projects.AsReadOnly();


        // Управління робітниками


//Додає нового робітника
        public void AddWorker(Worker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));
            _workers.Add(worker);
            worker.Department.AddWorker(worker);
        }

        //Видаляє робітника
        public void RemoveWorker(Worker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));
            worker.Department.RemoveWorker(worker);
            _workers.Remove(worker);
        }

        //Повертає робітника за повним іменем
        public Worker? GetWorkerByName(string fullName) =>
            _workers.FirstOrDefault(w =>
                w.FullName.ToLower() == fullName.ToLower());

        //Повертає всіх робітників відсортованих за іменем
        public IEnumerable<Worker> GetWorkersSortedByFirstName() =>
            _workers.OrderBy(w => w.FirstName);

        //Повертає всіх робітників відсортованих за прізвищем
        public IEnumerable<Worker> GetWorkersSortedByLastName() =>
            _workers.OrderBy(w => w.LastName);

        //Повертає всіх робітників відсортованих за зарплатою
        public IEnumerable<Worker> GetWorkersSortedBySalary() =>
            _workers.OrderByDescending(w => w.Position.BaseSalary);

        // Управління підрозділами

        //Додає новий підрозділ
        public void AddDepartment(Department department)
        {
            if (department == null) throw new ArgumentNullException(nameof(department));
            _departments.Add(department);
        }

        //Повертає підрозділ за назвою
        public Department? GetDepartmentByName(string name) =>
            _departments.FirstOrDefault(d =>
                d.Name.ToLower() == name.ToLower());

        // Управління посадами

        //Додає нову посаду
        public void AddPosition(Position position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            _positions.Add(position);
        }

        //Повертає 5 найбільш привабливих посад за співвідношенням зарплата/години.
        public IEnumerable<Position> GetTop5AttractivePositions() =>
            _positions.OrderByDescending(p => p.AttractivenessRatio).Take(5);

        // Визначає найбільш прибуткового робітника на заданій посаді.
        // Критерій: трудовий стаж / сумарна вартість проектів.
        public Worker? GetMostProfitableWorkerOnPosition(Position position)
        {
            var workersOnPosition = _workers
                .Where(w => w.Position == position && w.TotalProjectsCost > 0)
                .ToList();

            if (!workersOnPosition.Any()) return null;

            return workersOnPosition
                .OrderByDescending(w => (double)w.ExperienceYears / (double)w.TotalProjectsCost)
                .First();
        }

        // Управління проектами

        //Додає новий проект до каталогу
        public void AddProject(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            _projects.Add(project);
        }

        // Пошук

        //Пошук серед робітників по ключовому слову.
        public IEnumerable<Worker> SearchWorkers(string keyword) =>
            _workers.Where(w => w.ContainsKeyword(keyword));

        //Пошук серед проектів по ключовому слову.
        public IEnumerable<Project> SearchProjects(string keyword) =>
            _projects.Where(p => p.ContainsKeyword(keyword));

        //Пошук по всіх даних (робітники, проекти, посади, підрозділи)
        public (IEnumerable<Worker> Workers,
                IEnumerable<Project> Projects,
                IEnumerable<Position> Positions,
                IEnumerable<Department> Departments) SearchAll(string keyword)
        {
            return (
                _workers.Where(w => w.ContainsKeyword(keyword)),
                _projects.Where(p => p.ContainsKeyword(keyword)),
                _positions.Where(p => p.ContainsKeyword(keyword)),
                _departments.Where(d => d.ContainsKeyword(keyword))
            );
        }

        // Розширений пошук робітника за прізвищем та номером рахунку.
        public Worker? SearchWorkerAdvanced(string lastName, string accountNumber) =>
            _workers.FirstOrDefault(w =>
                w.LastName.ToLower() == lastName.ToLower() &&
                w.SalaryAccountNumber == accountNumber);
    }
}
