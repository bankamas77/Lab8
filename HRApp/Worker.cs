using HRLib.Interfaces;

namespace HRLib.Models
{

    // Базовий клас робітника відділу кадрів.
    // Асоціація з Position та Department.
    // Агрегація з Project (проекти існують незалежно від робітника).
    public class Worker : ISearchable, IDisplayable
    {
        //Ім'я робітника.
        public string FirstName { get; set; }

        //Прізвище робітника.
        public string LastName { get; set; }

        //Номер рахунку заробітної плати.
        public string SalaryAccountNumber { get; set; }

        //Трудовий стаж у роках.
        public int ExperienceYears { get; set; }

        //Посада робітника (асоціація).
        public Position Position { get; set; }

        //Підрозділ робітника (асоціація).
        public Department Department { get; set; }

        //Список проектів робітника (агрегація).
        private readonly List<Project> _projects = new();

        //Публічний доступ до проектів (тільки читання).
        public IReadOnlyList<Project> Projects => _projects.AsReadOnly();

        //Сумарна вартість усіх проектів робітника.
        public decimal TotalProjectsCost => _projects.Sum(p => p.Cost);

        // Ініціалізує нового робітника.
        public Worker(string firstName, string lastName, string salaryAccountNumber,
                      int experienceYears, Position position, Department department)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Ім'я не може бути порожнім.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Прізвище не може бути порожнім.");
            if (experienceYears < 0)
                throw new ArgumentException("Стаж не може бути від'ємним.");

            FirstName = firstName;
            LastName = lastName;
            SalaryAccountNumber = salaryAccountNumber;
            ExperienceYears = experienceYears;
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        //Повне ім'я робітника.
        public string FullName => $"{FirstName} {LastName}";

        //Додає проект до списку проектів робітника.
        public void AddProject(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            if (!_projects.Contains(project))
                _projects.Add(project);
        }

        //Видаляє проект зі списку проектів робітника.
        public void RemoveProject(Project project)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            _projects.Remove(project);
        }

        //Змінює основні дані робітника.
        public virtual void Update(string firstName, string lastName,
                                   string accountNumber, int experienceYears,
                                   Position position, Department department)
        {
            FirstName = firstName;
            LastName = lastName;
            SalaryAccountNumber = accountNumber;
            ExperienceYears = experienceYears;
            Position = position;
            Department = department;
        }

        public virtual bool ContainsKeyword(string keyword)
        {
            string kw = keyword.ToLower();
            return FirstName.ToLower().Contains(kw)
                || LastName.ToLower().Contains(kw)
                || SalaryAccountNumber.ToLower().Contains(kw)
                || Position.Title.ToLower().Contains(kw)
                || Department.Name.ToLower().Contains(kw);
        }

      
        public virtual string GetInfo() =>
            $"Робітник: {FullName} | Рахунок: {SalaryAccountNumber} | " +
            $"Підрозділ: {Department.Name} | Посада: {Position.Title} | Стаж: {ExperienceYears} р.";

      
        public override string ToString() => FullName;
    }

    // Узагальнення (наслідування): Manager IS-A Worker
 // Менеджер — розширений робітник з додатковими повноваженнями.
    // Відношення узагальнення (наслідування) від Worker.
    public class Manager : Worker
    {
        ///Рівень доступу менеджера.
        public int AccessLevel { get; set; }

        ///Кількість підлеглих.
        public int SubordinatesCount { get; set; }

        /// Ініціалізує нового менеджера
        public Manager(string firstName, string lastName, string salaryAccountNumber,
                       int experienceYears, Position position, Department department,
                       int accessLevel, int subordinatesCount)
            : base(firstName, lastName, salaryAccountNumber, experienceYears, position, department)
        {
            AccessLevel = accessLevel;
            SubordinatesCount = subordinatesCount;
        }

      
        public override string GetInfo() =>
            base.GetInfo() +
            $" | Рівень доступу: {AccessLevel} | Підлеглих: {SubordinatesCount}";

      
        public override bool ContainsKeyword(string keyword) =>
            base.ContainsKeyword(keyword) ||
            AccessLevel.ToString().Contains(keyword);
    }
}
