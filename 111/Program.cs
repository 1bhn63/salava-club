using System;
using System.Collections.Generic;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public User(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }
}

public class InformationSystem
{
    private List<User> users;

    public InformationSystem()
    {
        users = new List<User>();
    }

    public void RegisterUser(string username, string password, string role)
    {
        User newUser = new User(username, password, role);
        users.Add(newUser);
    }

    public User AuthenticateUser(string username, string password)
    {
        return users.Find(u => u.Username == username && u.Password == password);
    }
}

public class Program
{
    public static void Main()
    {
        InformationSystem system = new InformationSystem();

        // Регистрация пользователей
        system.RegisterUser("admin", "admin123", "admin");
        system.RegisterUser("cashier", "cashier123", "cashier");
        system.RegisterUser("manager", "manager123", "manager");

        Console.WriteLine("Добро пожаловать в информационную систему магазина!");

        // Авторизация пользователя
        Console.WriteLine("Введите логин:");
        string username = Console.ReadLine();
        Console.WriteLine("Введите пароль:");
        string password = Console.ReadLine();

        User currentUser = system.AuthenticateUser(username, password);
        if (currentUser != null)
        {
            Console.WriteLine($"Вы успешно авторизовались как {currentUser.Role} ({currentUser.Username})!");

            // Здесь можно добавить логику для каждой роли
            // Например, для администратора вывести меню с возможностью просмотра и добавления пользователей

            // Пример работы с менеджером персонала
            if (currentUser.Role == "manager")
            {
                Console.WriteLine("Вы зарегистрированы как менеджер персонала.");
                // Дополнительная логика для менеджера персонала
            }
        }
        else
        {
            Console.WriteLine("Ошибка авторизации. Проверьте введенные данные.");
        }

        Console.ReadLine();
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class PersonalManager
{
    private List<Employee> employees;
    private string employeesFilePath;

    public PersonalManager(string employeesFile)
    {
        employees = new List<Employee>();
        employeesFilePath = employeesFile;

        LoadEmployees();
    }

    public void Menu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Меню менеджера персонала");
            Console.WriteLine("1. Показать список сотрудников");
            Console.WriteLine("2. Поиск информации о сотруднике");
            Console.WriteLine("3. Добавить сотрудника");
            Console.WriteLine("4. Изменить информацию о сотруднике");
            Console.WriteLine("5. Удалить сотрудника");
            Console.WriteLine("6. Выход");

            Console.Write("Выберите пункт меню: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowEmployees();
                    break;
                case 2:
                    SearchEmployee();
                    break;
                case 3:
                    AddEmployee();
                    break;
                case 4:
                    UpdateEmployee();
                    break;
                case 5:
                    DeleteEmployee();
                    break;
                case 6:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    private void ShowEmployees()
    {
        Console.Clear();
        Console.WriteLine("Список сотрудников:");

        foreach (Employee employee in employees)
        {
            Console.WriteLine($"{employee.Id}. {employee.Name}");
        }
    }

    private void SearchEmployee()
    {
        Console.Clear();
        Console.WriteLine("Поиск сотрудников по атрибутам:");
        Console.WriteLine("1. Поиск по ID");
        Console.WriteLine("2. Поиск по имени");
        Console.WriteLine("3. Поиск по должности");
        Console.WriteLine("4. Поиск по отделу");

        Console.Write("Выберите пункт поиска: ");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Введите ID сотрудника: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employeeById = employees.FirstOrDefault(emp => emp.Id == employeeId);
                if (employeeById != null)
                {
                    Console.WriteLine($"ID: {employeeById.Id}");
                    Console.WriteLine($"Имя: {employeeById.Name}");
                    Console.WriteLine($"Должность: {employeeById.Position}");
                    Console.WriteLine($"Отдел: {employeeById.Department}");
                }
                else
                {
                    Console.WriteLine("Сотрудник с таким ID не найден.");
                }
                break;
            case 2:
                Console.Write("Введите имя сотрудника: ");
                string employeeName = Console.ReadLine();
                List<Employee> employeesByName = employees.Where(emp => emp.Name.ToLower() == employeeName.ToLower()).ToList();
                if (employeesByName.Count > 0)
                {
                    foreach (Employee emp in employeesByName)
                    {
                        Console.WriteLine($"ID: {emp.Id}");
                        Console.WriteLine($"Имя: {emp.Name}");
                        Console.WriteLine($"Должность: {emp.Position}");
                        Console.WriteLine($"Отдел: {emp.Department}");
                    }
                }
                else
                {
                    Console.WriteLine("Сотрудник с таким именем не найден.");
                }
                break;
            case 3:
                Console.Write("Введите должность сотрудника: ");
                string employeePosition = Console.ReadLine();
                List<Employee> employeesByPosition = employees.Where(emp => emp.Position.ToLower() == employeePosition.ToLower()).ToList();
                if (employeesByPosition.Count > 0)
                {
                    foreach (Employee emp in employeesByPosition)
                    {
                        Console.WriteLine($"ID: {emp.Id}");
                        Console.WriteLine($"Имя: {emp.Name}");
                        Console.WriteLine($"Должность: {emp.Position}");
                        Console.WriteLine($"Отдел: {emp.Department}");
                    }
                }
                else
                {
                    Console.WriteLine("Сотрудник с такой должностью не найден.");
                }
                break;
            case 4:
                Console.Write("Введите отдел сотрудника: ");
                string employeeDepartment = Console.ReadLine();
                List<Employee> employeesByDepartment = employees.Where(emp => emp.Department.ToLower() == employeeDepartment.ToLower()).ToList();
                if (employeesByDepartment.Count > 0)
                {
                    foreach (Employee emp in employeesByDepartment)
                    {
                        Console.WriteLine($"ID: {emp.Id}");
                        Console.WriteLine($"Имя: {emp.Name}");
                        Console.WriteLine($"Должность: {emp.Position}");
                        Console.WriteLine($"Отдел: {emp.Department}");
                    }
                }
                else
                {
                    Console.WriteLine("Сотрудник из такого отдела не найден.");
                }
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }

    private void AddEmployee()
    {
        Console.Clear();
        Console.WriteLine("Добавление нового сотрудника:");

        Console.Write("Введите имя сотрудника: ");
        string name = Console.ReadLine();

        Console.Write("Введите должность сотрудника: ");
        string position = Console.ReadLine();

        Console.Write("Введите отдел сотрудника: ");
        string department = Console.ReadLine();

        Console.Write("Введите ID пользователя (если привязываете): ");
        int userId;
        int.TryParse(Console.ReadLine(), out userId);

        // Проверяем, есть ли уже сотрудник с таким пользователем
        if (userId != 0)
        {
            Employee employeeWithUser = employees.FirstOrDefault(emp => emp.UserId == userId);
            if (employeeWithUser != null)
            {
                Console.WriteLine("Указанный пользователь уже привязан к другому сотруднику.");
                return;
            }
        }

        Employee newEmployee = new Employee(name, position, department, userId);
        employees.Add(newEmployee);

        SaveEmployees();

        Console.WriteLine("Сотрудник успешно добавлен.");
    }

    private void UpdateEmployee()
    {
        Console.Clear();
        Console.WriteLine("Изменение информации о сотруднике:");

        Console.Write("Введите ID сотрудника: ");
        int employeeId = int.Parse(Console.ReadLine());

        Employee employeeToUpdate = employees.FirstOrDefault(emp => emp.Id == employeeId);

        if (employeeToUpdate != null)
        {
            Console.WriteLine($"Текущая информация о сотруднике {employeeToUpdate.Name}:");
            Console.WriteLine($"1. ID: {employeeToUpdate.Id}");
            Console.WriteLine($"2. Имя: {employeeToUpdate.Name}");
            Console.WriteLine($"3. Должность: {employeeToUpdate.Position}");
            Console.WriteLine($"4. Отдел: {employeeToUpdate.Department}");

            Console.Write("Введите номер пункта, который необходимо изменить: ");
            int fieldNumber = int.Parse(Console.ReadLine());

            switch (fieldNumber)
            {
                case 1:
                    Console.Write("Введите новый ID сотрудника: ");
                    int newId = int.Parse(Console.ReadLine());
                    employeeToUpdate.Id = newId;
                    break;
                case 2:
                    Console.Write("Введите новое имя сотрудника: ");
                    string newName = Console.ReadLine();
                    employeeToUpdate.Name = newName;
                    break;
                case 3:
                    Console.Write("Введите новую должность сотрудника: ");
                    string newPosition = Console.ReadLine();
                    employeeToUpdate.Position = newPosition;
                    break;
                case 4:
                    Console.Write("Введите новый отдел сотрудника: ");
                    string newDepartment = Console.ReadLine();
                    employeeToUpdate.Department = newDepartment;
                    break;
                default:
                    Console.WriteLine("Некорректный номер пункта.");
                    break;
            }

            Console.WriteLine("Информация о сотруднике успешно изменена.");

            SaveEmployees();
        }
        else
        {
            Console.WriteLine("Сотрудник с таким ID не найден.");
        }
    }

    private void DeleteEmployee()
    {
        Console.Clear();
        Console.WriteLine("Удаление сотрудника:");

        Console.Write("Введите ID сотрудника: ");
        int employeeId = int.Parse(Console.ReadLine());

        Employee employeeToDelete = employees.FirstOrDefault(emp => emp.Id == employeeId);

        if (employeeToDelete != null)
        {
            employees.Remove(employeeToDelete);

            Console.WriteLine("Сотрудник успешно удален.");

            SaveEmployees();
        }
        else
        {
            Console.WriteLine("Сотрудник с таким ID не найден.");
        }
    }

    private void LoadEmployees()
    {
        try
        {
            if (File.Exists(employeesFilePath))
            {
                string json = File.ReadAllText(employeesFilePath);
                employees = JsonConvert.DeserializeObject<List<Employee>>(json);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Ошибка при загрузке данных о сотрудниках.");
        }
    }

    private void SaveEmployees()
    {
        try
        {
            string json = JsonConvert.SerializeObject(employees);
            File.WriteAllText(employeesFilePath, json);
        }
        catch (Exception)
        {
            Console.WriteLine("Ошибка при сохранении данных о сотрудниках.");
        }
    }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public int UserId { get; set; }

    public Employee(string name, string position, string department, int userId)
    {
        Id = 0; // при создании нового сотрудника предполагаем, что у него пока нет ID
        Name = name;
        Position = position;
        Department = department;
        UserId = userId;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string employeesFile = "employees.json";

        PersonalManager manager = new PersonalManager(employeesFile);
        manager.Menu();
    }
}