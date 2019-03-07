using CluedinLibrary;
using CluedInLibrary;
using CluedInLibrary.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CluedInConsole
{
    class Program
    {
        public static List<CompanyModel> Companies = new List<CompanyModel>();
        public static List<int> CompanyIds = new List<int>();

        static void Main(string[] args)
        {
            APIHelper.InitializeClient();
            Task task = GetListOfCompanies();

            task.Wait(1000);
            GetAvailableCompanyIds();

            string choice = "";

            while (choice != "x")
            {
                Menu();
                choice = Console.ReadLine()?.ToLower();

                switch (choice)
                {
                    case "1":
                        ListCompanies();
                        break;
                    case "2":
                        MenuCompanyInfo();
                        break;
                    case "3":
                        MenuEmployeesByCompanyId();
                        break;
                    case "4":
                        MenuCustomersByCompanyId();
                        break;
                    case "5":
                        MenuShowFullInfoByCompanyId();
                        break;
                }
            }
        }

        static async Task GetListOfCompanies()
        {
            Companies = await DataProcessor.GetCompanies();
        }
        static void GetAvailableCompanyIds()
        {
            foreach (var company in Companies)
            {
                CompanyIds.Add(company.Id);
            }
        }

        static void ListCompany(CompanyModel company)
        {
            Console.WriteLine($"Id: {company.Id}");
            Console.WriteLine($"Name: {company.Name}");
            Console.WriteLine($"Email: {company.Email}");
            Console.WriteLine($"PhoneNumber: {company.PhoneNumber}");
            Console.WriteLine("");
        }

        static void ListCompanies()
        {
            MenuHeader();

            foreach (var company in Companies)
            {
                ListCompany(company);
            }

            Console.WriteLine("Press any key to go back to menu");
            Console.ReadKey();
        }

        static void ListEmployee(CompanyEmployeeModel employee)
        {
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"CompanyId: {employee.CompanyId}");
            Console.WriteLine($"Firstname: {employee.FirstName}");
            Console.WriteLine($"Lastname: {employee.LastName}");
            Console.WriteLine($"Email: {employee.Email}");
            Console.WriteLine($"Title: {employee.Title}");
            Console.WriteLine("");
        }
        static void ListEmployees(List<CompanyEmployeeModel> employees)
        {
            MenuHeader();

            foreach (var employee in employees)
            {
                ListEmployee(employee);
            }
            Console.WriteLine("Press any key to go back to menu");
            Console.ReadKey();
        }

        static void ListCustomer(CompanyCustomerModel customer)
        {
            Console.WriteLine($"Id: {customer.Id}");
            Console.WriteLine($"CompanyId: {customer.CompanyId}");
            Console.WriteLine($"CustomerName: {customer.CustomerName}");
            Console.WriteLine($"CustomerValue: {customer.CustomerValue}");
            Console.WriteLine("");
        }
        static void ListCustomers(List<CompanyCustomerModel> customers)
        {
            MenuHeader();

            foreach (var customer in customers)
            {
                ListCustomer(customer);
            }

            Console.WriteLine("Press any key to go back to menu");
            Console.ReadKey();
        }

        static void ListCompanyComplete(CompanyCompleteModel companyComplete)
        {
            MenuHeader();

            Console.WriteLine("Company Information:");
            ListCompany(companyComplete.Company);

            Console.WriteLine("Company Employees:");
            foreach (var employee in companyComplete.CompanyEmployees)
            {
                ListEmployee(employee);
            }

            Console.WriteLine("Company Customers:");
            foreach (var customer in companyComplete.CompanyCustomers)
            {
                ListCustomer(customer);
            }

            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();
        }

        static void MenuHeader()
        {
            Console.Clear();
            Console.WriteLine("CluedIn API Test");
            Console.WriteLine("");
        }
        static void MenuAvailableCompanyIds()
        {
            Console.WriteLine("CompanyIDs which are available: ");
            Console.WriteLine("");

            foreach (var company in Companies)
            {
                Console.Write(company.Id + " ");
            }
        }
        static void Menu()
        {
            MenuHeader();

            Console.WriteLine("1. Show full List of companies");
            Console.WriteLine("2. Get more info on a company");
            Console.WriteLine("3. Get list of employees of a company");
            Console.WriteLine("4. Get list of customers of a company");
            Console.WriteLine("5. List a company's full information details");
            Console.Write("Select a number from 1-5: ");
        }

        static async void MenuCompanyInfo()
        {
            MenuHeader();
            MenuAvailableCompanyIds();

            Console.WriteLine("");
            Console.WriteLine("Choose the id of the company you want to view or any other value to return to main menu:");

            var choice = Convert.ToInt32(Console.ReadLine());

            if (CompanyIds.IndexOf(choice) == -1) return;
            {
                var company = await DataProcessor.GetCompanyById(choice);

                Console.WriteLine("");
                ListCompany(company);
            }
        }
        static async void MenuEmployeesByCompanyId()
        {
            MenuHeader();
            MenuAvailableCompanyIds();

            Console.WriteLine("");
            Console.WriteLine("Choose the id of the company you want to view employees from or any other value to return to main menu:");

            var choice = Convert.ToInt32(Console.ReadLine());

            if (CompanyIds.IndexOf(choice) == -1) return;
            {
                var employees = await DataProcessor.GetEmployeesByCompanyId(choice);

                ListEmployees(employees);
            }
        }
        static async void MenuCustomersByCompanyId()
        {
            MenuHeader();
            MenuAvailableCompanyIds();

            Console.WriteLine("");
            Console.WriteLine("Choose the id of the company you want to view customers from or any other value to return to main menu:");

            var choice = Convert.ToInt32(Console.ReadLine());

            if (CompanyIds.IndexOf(choice) == -1) return;
            {
                var customers = await DataProcessor.GetCustomersByCompanyId(choice);

                ListCustomers(customers);
            }

        }
        static async void MenuShowFullInfoByCompanyId()
        {
            MenuHeader();
            MenuAvailableCompanyIds();

            Console.WriteLine("");
            Console.WriteLine("Choose the id of the company you want to list full info of or any other value to return to main menu:");

            var choice = Convert.ToInt32(Console.ReadLine());

            if (CompanyIds.IndexOf(choice) == -1) return;
            {
                var companyComplete = new CompanyCompleteModel
                {
                    Company = await DataProcessor.GetCompanyById(choice),
                    CompanyCustomers = await DataProcessor.GetCustomersByCompanyId(choice),
                    CompanyEmployees = await DataProcessor.GetEmployeesByCompanyId(choice)
                };
//                Thread.Sleep(250);
                await Task.Delay(250);

                Console.WriteLine("");
                ListCompanyComplete(companyComplete);
            }
        }
    }
}
