using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    class DeserializationJSON
    {
        private readonly string _json;
        DepartmentManagement _departmentManagement;
        EmployeeManagement _employeeManagement;
        private List<List<object>> _listWorker;
        private List<List<object>> _listItern;
        private List<List<object>> _listDirector;
        public DeserializationJSON()
        {
            _departmentManagement = new DepartmentManagement();
            _employeeManagement = new EmployeeManagement();
        }
        public DepartmentManagement GetDepartmentManagement()
        {
            return _departmentManagement;
        }
        public EmployeeManagement GetEmployeeManagement()
        {
            return _employeeManagement;
        }
        public void DeserializJSON()
        {
            string json = File.ReadAllText("company.json");
            DeserializDepartment(json);
            DeserializEmaployee(json);
        }

        private void DeserializEmaployee(string json)
        {
            JToken[] employyees = JObject.Parse(json)["Employees"].ToArray();
            foreach (var employee in employyees)
            {
                switch (employee["Type"].ToString())
                {
                    case "Director":
                        DeserializDirector(employee);
                        break;
                    case "Worker":
                        DeserializWorker(employee);
                        break;
                    case "Intern":
                        DeserializIntern(employee);
                        break;
                    default:
                        break;
                }
            }
        }

        private void DeserializIntern(JToken employee)
        {
            string name = employee["Name"].ToString();
            string surname = employee["Surname"].ToString();
            uint age = Convert.ToUInt32(employee["Age"]);
            uint salary = Convert.ToUInt32(employee["Salary"]);
            uint idDepartment = Convert.ToUInt32(employee["Id Department"]);

            _employeeManagement.NewIntern(name, surname, idDepartment, age, salary);
        }
        private void DeserializWorker(JToken employee)
        {
            string name = employee["Name"].ToString();
            string surname = employee["Surname"].ToString();
            string post = employee["Post"].ToString();
            uint age = Convert.ToUInt32(employee["Age"]);
            uint hourlyPayment = Convert.ToUInt32(employee["Hourly Payment"]);
            uint workingHours = Convert.ToUInt32(employee["Working Hours"]);
            uint idDepartment = Convert.ToUInt32(employee["Id Department"]);

            _employeeManagement.NewWorker(name, surname, idDepartment, age, workingHours, hourlyPayment, post);
        }
        private void DeserializDirector(JToken employee)
        {
            string name = employee["Name"].ToString();
            string surname = employee["Surname"].ToString();
            string post = employee["Post"].ToString();
            uint age = Convert.ToUInt32(employee["Age"]);
            uint idDepartment = Convert.ToUInt32(employee["Id Department"]);

            _employeeManagement.NewDirektor(name, surname, age, idDepartment, post);
        }

        private void DeserializDepartment(string json)
        {
            JToken[] dep = JObject.Parse(json)["Company"].ToArray();
            foreach (var department in dep)
            {
                string name = department["Name Department"].ToString();
                string located = department["Located"].ToString();
                _departmentManagement.NewDepartment(located, name);
            }
        }
        
    }
}
