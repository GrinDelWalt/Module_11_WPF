using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Module_11_WPF
{
    class SerializationJSON
    {
        private List<Department> _departmensList;
        private ObservableCollection<Employee> _employeesList;

        JArray _departmens;
        public void Serelase(List<Department> departmensList, ObservableCollection<Employee> employeesList)
        {
            _departmensList = departmensList;
            _employeesList = employeesList;
            _departmens = new JArray();
            DepartmentSerelase(_departmensList, "null");
            JObject department = new JObject();
            department["ok"] = true;
            department["Company"] = _departmens;
            department["Employees"] = GetEmployeesJSON();
            string json = department.ToString();
            //File.WriteAllText("company.json", json);
        }
        private void DepartmentSerelase(List<Department> departments, string located)
        {
            

            foreach (var department in departments)
            {
                JObject dep = GetDepartmenJOSN(department, located);
                _departmens.Add(dep);
                if (department.Departments.Count != 0)
                {
                    DepartmentSerelase(department.Departments, department.NameDepartment);
                }
            }
        }
        private JObject GetDepartmenJOSN(Department department, string lacated)
        {
            JObject dep = new JObject
            {
                ["Located"] = lacated,
                ["Name Department"] = department.NameDepartment,
                ["Id Department"] = department.IdDepartment,
            };
            return dep;
        }

        private JArray GetEmployeesJSON()
        {
            
            JArray employeesJSON = new JArray();
            foreach (var employee in _employeesList)
            {
                JObject employeeJSON;
                var type = employee.GetType();
                switch (type.Name)
                {
                    case "Director":
                        employeeJSON = GetDirectorJSON((Director)employee);
                        break;
                    case "Worker":
                        employeeJSON = GetWorkerJSON((Worker)employee);
                        break;
                    case "Intern":
                        employeeJSON = GetInternJSON((Intern)employee);
                        break;
                    default:
                        employeeJSON = new JObject();
                        break;
                }
                employeesJSON.Add(employeeJSON);
            }
            return employeesJSON;
        }

        private JObject GetInternJSON(Intern employee)
        {
            JObject intern = new JObject
            {
                ["Type"] = "Intern",
                ["Name"] = employee.Name,
                ["Surname"] = employee.Surname,
                ["Age"] = employee.Age,
                ["Id Department"] = employee.IdDepartment,
                ["Post"] = employee.Post,
                ["Salary"] = employee.Salary,
            };
            return intern;
        }

        private JObject GetWorkerJSON(Worker employee)
        {
            JObject worker = new JObject
            {
                ["Type"] = "Worker",
                ["Name"] = employee.Name,
                ["Surname"] = employee.Surname,
                ["Age"] = employee.Age,
                ["Id Department"] = employee.IdDepartment,
                ["Post"] = employee.Post,
                ["Salary"] = employee.Salary,
                ["Hourly Payment"] = employee.HourlyPayment,
                ["Working Hours"] = employee.WorkingHours,
            };
            return worker;
        }

        private JObject GetDirectorJSON(Director employee)
        {
            JObject director = new JObject
            {
                ["Type"] = "Director",
                ["Name"] = employee.Name,
                ["Surname"] = employee.Surname,
                ["Age"] = employee.Age,
                ["Id Department"] = employee.IdDepartment,
                ["Post"] = employee.Post,
                ["Salary"] = employee.Salary,
            };
            return director;
        }
    }
}
