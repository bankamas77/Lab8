using HRLib.Models;
using HRLib.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;
System.Globalization.CultureInfo.CurrentCulture = 
    new System.Globalization.CultureInfo("uk-UA");
Console.WriteLine("=== Лабораторна робота 8. Варіант 4 ===");
Console.WriteLine("    Відділ кадрів: ведення особових справ робітників\n");

HRCatalog catalog = new HRCatalog();

// Посади
Position posDev     = new Position("Розробник",       40, 45000);
Position posLead    = new Position("Тімлід",           45, 75000);
Position posAnalyst = new Position("Аналітик",         38, 40000);
Position posQA      = new Position("QA-інженер",       40, 38000);
Position posDevOps  = new Position("DevOps-інженер",   42, 55000);
Position posArch    = new Position("Архітектор",       35, 90000);
catalog.AddPosition(posDev);
catalog.AddPosition(posLead);
catalog.AddPosition(posAnalyst);
catalog.AddPosition(posQA);
catalog.AddPosition(posDevOps);
catalog.AddPosition(posArch);

// Підрозділи
Department depBackend  = new Department("Backend");
Department depFrontend = new Department("Frontend");
Department depQA       = new Department("QA");
catalog.AddDepartment(depBackend);
catalog.AddDepartment(depFrontend);
catalog.AddDepartment(depQA);

// Проекти
Project projAlpha = new Project("Alpha", 150000, "Нова ERP-система");
Project projBeta  = new Project("Beta",   80000, "Мобільний додаток");
Project projGamma = new Project("Gamma", 220000, "Хмарна міграція");
Project projDelta = new Project("Delta",  60000, "Автоматизація тестування");
catalog.AddProject(projAlpha);
catalog.AddProject(projBeta);
catalog.AddProject(projGamma);
catalog.AddProject(projDelta);

// 1. УПРАВЛІННЯ РОБІТНИКАМИ

// 1.1 Додавання робітників
Console.WriteLine("=== 1.1 Додавання робітників ===");
Worker ivan   = new Worker("Іван",   "Петренко",   "UA001", 5,  posDev,     depBackend);
Worker olena  = new Worker("Олена",  "Ковальчук",  "UA002", 8,  posAnalyst, depBackend);
Worker mykola = new Worker("Микола", "Бондаренко", "UA003", 3,  posQA,      depQA);
Worker sofia  = new Worker("Софія",  "Мельник",    "UA004", 6,  posDev,     depFrontend);
Manager taras = new Manager("Тарас", "Шевченко",   "UA005", 12, posLead,    depBackend, 3, 5);
Worker anna   = new Worker("Анна",   "Лисенко",    "UA006", 4,  posDevOps,  depBackend);

ivan.AddProject(projAlpha);   ivan.AddProject(projBeta);
olena.AddProject(projAlpha);  olena.AddProject(projGamma);
mykola.AddProject(projDelta);
sofia.AddProject(projBeta);
taras.AddProject(projAlpha);  taras.AddProject(projGamma);
anna.AddProject(projGamma);   anna.AddProject(projDelta);

catalog.AddWorker(ivan);
catalog.AddWorker(olena);
catalog.AddWorker(mykola);
catalog.AddWorker(sofia);
catalog.AddWorker(taras);
catalog.AddWorker(anna);

foreach (var w in catalog.Workers)
    Console.WriteLine($"  Додано: {w.FullName} | {w.Position.Title} | {w.Department.Name}");
Console.WriteLine();

// 1.2 Видалення робітника
Console.WriteLine("=== 1.2 Видалення робітника ===");
Console.WriteLine($"До видалення: {catalog.Workers.Count} робітників");
catalog.RemoveWorker(mykola);
Console.WriteLine($"Видалено: Микола Бондаренко");
Console.WriteLine($"Після видалення: {catalog.Workers.Count} робітників");
Console.WriteLine();

// 1.3 Зміна даних робітника
Console.WriteLine("=== 1.3 Зміна даних робітника ===");
Console.WriteLine("До:    " + ivan.GetInfo());
ivan.Update("Іван", "Петренко", "UA001-NEW", 6, posDev, depBackend);
Console.WriteLine("Після: " + ivan.GetInfo());
ivan.Update("Іван", "Петренко", "UA001", 5, posDev, depBackend);
Console.WriteLine();

// 1.4 Дані конкретного робітника
Console.WriteLine("=== 1.4 Дані конкретного робітника ===");
Console.WriteLine(ivan.GetInfo());
Console.WriteLine(taras.GetInfo());
Console.WriteLine();

// 1.5 Проекти конкретного робітника
Console.WriteLine("=== 1.5 Проекти робітника Олена Ковальчук ===");
foreach (var proj in olena.Projects)
    Console.WriteLine("  " + proj.GetInfo());
Console.WriteLine();

// 1.6 Список всіх робітників
Console.WriteLine("=== 1.6.1 Сортування за іменем ===");
foreach (var w in catalog.GetWorkersSortedByFirstName())
    Console.WriteLine($"  {w.FullName}");
Console.WriteLine();

Console.WriteLine("=== 1.6.2 Сортування за прізвищем ===");
foreach (var w in catalog.GetWorkersSortedByLastName())
    Console.WriteLine($"  {w.FullName}");
Console.WriteLine();

Console.WriteLine("=== 1.6.3 Сортування за зарплатою ===");
foreach (var w in catalog.GetWorkersSortedBySalary())
    Console.WriteLine($"  {w.FullName} — {w.Position.BaseSalary:C}");
Console.WriteLine();

// 2. УПРАВЛІННЯ ПІДРОЗДІЛАМИ

// 2.2 Додавання підрозділу
Console.WriteLine("=== 2.2 Додавання підрозділу ===");
Department depDevOps = new Department("DevOps");
catalog.AddDepartment(depDevOps);
Console.WriteLine($"Додано підрозділ: {depDevOps.Name}");
Console.WriteLine($"Всього підрозділів: {catalog.Departments.Count}");
Console.WriteLine();

// 2.1 Зміна даних підрозділу
Console.WriteLine("=== 2.1 Зміна даних підрозділу ===");
Console.WriteLine("До:    " + depDevOps.GetInfo());
depDevOps.Update("DevOps & Infrastructure");
Console.WriteLine("Після: " + depDevOps.GetInfo());
Console.WriteLine();

// 2.3 Дані конкретного підрозділу
Console.WriteLine("=== 2.3 Дані підрозділу Backend ===");
Console.WriteLine(depBackend.GetInfo());
Console.WriteLine();

// 2.4 Список робітників підрозділу
Console.WriteLine("=== 2.4.1 Робітники Backend за посадою ===");
foreach (var w in depBackend.GetWorkersSortedByPosition())
    Console.WriteLine($"  {w.FullName} — {w.Position.Title}");
Console.WriteLine();

Console.WriteLine("=== 2.4.2 Робітники Backend за вартістю проектів ===");
foreach (var w in depBackend.GetWorkersSortedByProjectsCost())
    Console.WriteLine($"  {w.FullName} — {w.TotalProjectsCost:C}");
Console.WriteLine();

// 3. УПРАВЛІННЯ ПОСАДАМИ

// 3.1 Зміна даних посади
Console.WriteLine("=== 3.1 Зміна даних посади ===");
Console.WriteLine("До:    " + posQA.GetInfo());
posQA.Update("Senior QA-інженер", 40, 50000);
Console.WriteLine("Після: " + posQA.GetInfo());
Console.WriteLine();

// 3.2 Топ-5 привабливих посад
Console.WriteLine("=== 3.2 Топ-5 найбільш привабливих посад ===");
foreach (var pos in catalog.GetTop5AttractivePositions())
    Console.WriteLine($"  {pos.Title} — коефіцієнт: {pos.AttractivenessRatio:F2}");
Console.WriteLine();

// 3.3 Найбільш прибутковий робітник на посаді
Console.WriteLine("=== 3.3 Найбільш прибутковий робітник на посаді Розробник ===");
var best = catalog.GetMostProfitableWorkerOnPosition(posDev);
Console.WriteLine(best != null ? best.GetInfo() : "Не знайдено.");
Console.WriteLine();

// 4. ПОШУК

// 4.1 Пошук серед робітників
Console.WriteLine("=== 4.1 Пошук робітників по слову 'backend' ===");
foreach (var w in catalog.SearchWorkers("backend"))
    Console.WriteLine($"  {w.FullName}");
Console.WriteLine();

// 4.2 Пошук серед проектів
Console.WriteLine("=== 4.2 Пошук проектів по слову 'міграція' ===");
foreach (var p in catalog.SearchProjects("міграція"))
    Console.WriteLine("  " + p.GetInfo());
Console.WriteLine();

// 4.3 Глобальний пошук
Console.WriteLine("=== 4.3 Глобальний пошук по слову 'alpha' ===");
var (workers, projects, positions, departments) = catalog.SearchAll("alpha");
Console.WriteLine("Робітники:");
foreach (var w in workers) Console.WriteLine("  " + w.FullName);
Console.WriteLine("Проекти:");
foreach (var p in projects) Console.WriteLine("  " + p.Name);
Console.WriteLine();

// 4.4 Розширений пошук
Console.WriteLine("=== 4.4 Розширений пошук: прізвище=Петренко, рахунок=UA001 ===");
var found = catalog.SearchWorkerAdvanced("Петренко", "UA001");
Console.WriteLine(found != null ? found.GetInfo() : "Не знайдено.");
Console.WriteLine();

Console.WriteLine("=== Програму завершено ===");
