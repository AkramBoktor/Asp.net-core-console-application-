using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Linq_Example_3
{
   public class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            // method syntax order by  order by descending , then by , then by descending 
            var resultorder = employeeList.Join(departmentList,
                emp => emp.DepartmentId,
                dep => dep.Id,
                (emp, dept) => new
                {
                    Id = emp.Id,
                    FullName = emp.FirstName + " " + emp.LastName,
                    Annulasalary = emp.AnuualSalary,
                    DepartmentId = emp.DepartmentId,
                    DepartmentName = dept.LongName,
                }).OrderBy(emp => emp.DepartmentId).ThenBy(emp => emp.Annulasalary); ;

            foreach (var result in resultorder)
            {
                Console.WriteLine($"Method syntax Id {result.Id}-FullName {result.FullName} AnnualSalary {result.Annulasalary} \t DepartmentName  {result.DepartmentName} \t DepartmetId {result.DepartmentId}");
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            // Query syntax order by  order by descending , then by , then by descending 
            var resultorderQuery = from emp in employeeList
                                   join dept in departmentList
                                   on emp.DepartmentId equals dept.Id
                                   orderby emp.DepartmentId , emp.AnuualSalary descending
                                   select new
                                   {
                                       Id = emp.Id,
                                       FullName = emp.FirstName + " " + emp.LastName,
                                       Annulasalary = emp.AnuualSalary,
                                       DepartmentId = emp.DepartmentId,
                                       DepartmentName = dept.LongName,
                                   };

                foreach (var result in resultorderQuery)
            {

                Console.WriteLine($"Query sysntax Id {result.Id}-FullName {result.FullName} AnnualSalary {result.Annulasalary} \t DepartmentName  {result.DepartmentName} \t DepartmetId {result.DepartmentId}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            ///////////////////////////////////////////////// Grouping ////////////////////////////////////////////////////////////////////
            //// Grouping Operator
            ///  Group by
            ///Method Group by
            var resultGroup = employeeList.GroupBy(emp => emp.DepartmentId);
            foreach (var emplyee in resultGroup)
            {
                Console.WriteLine($"Group sysntax Id {emplyee.Key}");
                foreach (var employeelist in emplyee)
                {
                    Console.WriteLine($"Employee Name  {employeelist.FirstName} { employeelist.LastName}");

                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            ///Query Group by
            var resultGroupQuery = from emp in employeeList
                                   group emp by emp.DepartmentId;
            foreach (var emplyee in resultGroupQuery)
            {
                Console.WriteLine($"Group sysntax Id {emplyee.Key}");
                foreach (var employeelist in emplyee)
                {
                    Console.WriteLine($"Employee Name  {employeelist.FirstName} { employeelist.LastName}");

                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            //tolookup like group by but lookup immediate execution ( Group by Deferred  )
            var resultGrouplook = employeeList.OrderByDescending(o=>o.DepartmentId).ToLookup(emp => emp.DepartmentId);
            foreach (var emplyee in resultGrouplook)
            {
                Console.WriteLine($"tolookup sysntax Id {emplyee.Key}");
                foreach (var employeelist in emplyee)
                {
                    Console.WriteLine($"Employee Name  {employeelist.FirstName} { employeelist.LastName}");

                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            ////////////////////////////////// All-Any-contains Operator ////////////////////////////////////
            ///all of this return true or false
            var resultbool = employeeList.All(e => e.AnuualSalary > 1000);
            if (resultbool)
            {
                Console.WriteLine($"All salaries above 1000");
            }
            else
            {
                Console.WriteLine($"All salaries under 1000");

            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            //////Any
            var resultboolAny = employeeList.Any(e => e.FirstName.ToLower() == "John");
            if (resultboolAny)
            {
                Console.WriteLine($"There is at least an employee with first name John");
            }
            else
            {
                Console.WriteLine($"No employee with first name John");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            ///contains operator
            var compareemployee = new Employee()
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnuualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            var searchemployee = employeeList.Contains(compareemployee);
            if (searchemployee)
            {
                Console.WriteLine($"Employee with {compareemployee.FirstName} - {compareemployee.LastName} was found ");
            }
            else
            {
                Console.WriteLine($"Employee with {compareemployee.FirstName} - {compareemployee.LastName} wasn't found ");

            }

            var searchingemployee = employeeList.Contains(compareemployee,new compareEmployee());
            if (searchingemployee)
            {
                Console.WriteLine($"Employee with {compareemployee.FirstName} - {compareemployee.LastName} was found ");
            }
            else
            {
                Console.WriteLine($"Employee with {compareemployee.FirstName} - {compareemployee.LastName} wasn't found ");

            }

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            /////////////////////////////////////////////////////
            ///////////////////////// of type , where //////////////////////////
            ArrayList mixollectionarray = Data.GetHeterogeneousDateCollection();
            var stringdata = from datastring in mixollectionarray.OfType<string>()
                             select datastring;

            foreach (var item in stringdata)
            {
                Console.WriteLine($"String Data  {item}");
            }

            var intdata = from i in mixollectionarray.OfType<int>()
                             select i;

            foreach (var item in intdata)
            {
                Console.WriteLine($"int Data  {item}");
            }
            var employeedata = from e in mixollectionarray.OfType<Employee>()
                          select e;

            foreach (var e in employeedata)
            {
                Console.WriteLine($"employee Data  {e.FirstName} - {e.LastName}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            ////////////////////////////////////////////////// Element Operator ( ElementAt , ElementAtorDefault , First , firstordefault ,last , lastorDefault , single , singleordefault /////////////////////////////////////////////////////
            var employeeAt = employeeList.ElementAt(2);
                Console.WriteLine($"Empolyee At: {employeeAt.FirstName} {employeeAt.LastName} Salary: {employeeAt.AnuualSalary}");

            var employeeAtordefault = employeeList.ElementAtOrDefault(50);
            if(employeeAtordefault!=null)
            Console.WriteLine($"Empolyee At or default: {employeeAtordefault.FirstName} {employeeAtordefault.LastName} Salary: {employeeAtordefault.AnuualSalary}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            ///first firstordefault last last or default 
            List<int> integerlist = new List<int> { 1, 3, 9, 7, 9 };
            var resultint = integerlist.FirstOrDefault(i => i % 2 == 0);
            if(resultint!=0)
            Console.WriteLine($"Result : {resultint}");
            else
             Console.WriteLine($"No Result");



            Console.ReadKey();
        }

    }


    ///// Data 
    public class compareEmployee : IEqualityComparer<Employee>
    {
        public bool Equals([AllowNull] Employee x, [AllowNull] Employee y)
        {
            if(x.Id==y.Id && x.FirstName.ToLower()==y.FirstName.ToLower() && x.LastName.ToLower() == y.LastName.ToLower())
             return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.Id.GetHashCode();
        }




    }
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal AnuualSalary { get; set; }

        public bool IsManager { get; set; }

        public int DepartmentId { get; set; }

    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee()
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnuualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee()
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Gendy",
                AnuualSalary = 4000.7m,
                IsManager = true,
                DepartmentId = 3
            };
            employees.Add(employee);
            employee = new Employee()
            {
                Id = 3,
                FirstName = "John",
                LastName = "Gad",
                AnuualSalary = 5000.7m,
                IsManager = false,
                DepartmentId = 3
            };
            employees.Add(employee);
            employee = new Employee()
            {
                Id = 4,
                FirstName = "Peter",
                LastName = "Samuel",
                AnuualSalary = 8000.7m,
                IsManager = false,
                DepartmentId = 1
            };
            employees.Add(employee);
            return employees;
        }


        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            Department department = new Department()
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resource"
            };
            departments.Add(department);
            department = new Department()
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Financie"
            };
            departments.Add(department);
            department = new Department()
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);
            return departments;
        }


        public static ArrayList GetHeterogeneousDateCollection()
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add("Akram1");
            arrayList.Add("Akram2");
            arrayList.Add(100);
            arrayList.Add(200);
            arrayList.Add(300);

            arrayList.Add(new Employee
            {
                FirstName = "Akram Boktor 2",
                LastName = "Boktor Ibrahim",
                AnuualSalary = 4000,
                IsManager = true,
                DepartmentId = 1,
                Id = 6
            });
            return arrayList;

        }



    }




}
