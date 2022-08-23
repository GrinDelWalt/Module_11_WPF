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
        DepartmentManagement _departmentManagement;
        EmployeeManagement _employeeManagement;
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
        //    public void JSONRead()
        //    {
        //        string json = File.ReadAllText("C:/Users/Гоша/Desktop/С#/WorkerJson.json");
        //        char key = 'д';
        //        do
        //        {
        //            Print("Введите путь к файлу");
        //            string path = ExceptionsString();
        //            bool result = File.Exists(path);

        //            if (true == result)
        //            {
        //                string json = File.ReadAllText(path);

        //                var dep = JObject.Parse(json)["company"].ToArray();

        //                int id = 0;
        //                foreach (var e in dep)
        //                {
        //                    int tempNumberDep = this.dep.Count;
        //                    string[] data = new string[2];

        //                    data[0] = e["NameDep"].ToString();
        //                    data[1] = e["Data"].ToString();
        //                    string stringIdEmployee = e["id_Workers"].ToString();
        //                    string[] separators = { ",", ".", "!", "?", ";", ":", " " };                                //конвертация строки в масив чисел
        //                    string[] arrayStringIdEmployee = stringIdEmployee.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //                    List<int> intIdEmployee = new List<int>();
        //                    for (int d = 0; d < arrayStringIdEmployee.Length; d++)
        //                    {
        //                        intIdEmployee.Add(Convert.ToInt32(arrayStringIdEmployee[d]));                           //запись колекции чисел
        //                    }
        //                    List<uint> initializerList = new List<uint>();
        //                    initializerList.Add(1_000_001);
        //                    List<int> initializerList = new List<int>();
        //                    initializerList.Add(-1);
        //                    AddDep(new Department(data[0], DateTime.Parse(data[1]), Convert.ToUInt32(tempNumberDep + 1), initializerList));
        //                    int idDep = tempNumberDep + 1;

        //                    string worker = Convert.ToString(dep[id]);
        //                    var employee = JObject.Parse(worker)["workers"].ToArray();

        //                    if (intIdEmployee[0] != 1_000_001)
        //                        if (intIdEmployee[0] != -1)
        //                        {
        //                            foreach (var item in employee)
        //                            {
        //                                int index = idDep;
        //                                //Количество рабочих в базе
        //                                object idWorker = this.workers.Count;
        //                                if (idWorker == null)
        //                                {
        //                                    this.index = 0;
        //                                }
        //                                else
        //                                {
        //                                    this.index = Convert.ToUInt32(idWorker);
        //                                    this.index = Convert.ToInt32(idWorker);
        //                                }

        //                                string[] arrayEmployee = new string[5];

        //                                arrayEmployee[0] = item["Surname"].ToString();

        //                                arrayEmployee[1] = item["Name"].ToString();

        //                                arrayEmployee[2] = item["Age"].ToString();

        //                                arrayEmployee[3] = item["Projeck"].ToString();

        //                                arrayEmployee[4] = item["Salari"].ToString();

        //                                AddWorker(new Worker(arrayEmployee[1], arrayEmployee[0], Convert.ToUInt32(arrayEmployee[2]), Convert.ToUInt32(arrayEmployee[4]), Convert.ToUInt32(arrayEmployee[3]), this.index, Convert.ToUInt32(index)));
        //                                AddWorker(new Worker(arrayEmployee[1], arrayEmployee[0], Convert.ToUInt32(arrayEmployee[2]), Convert.ToUInt32(arrayEmployee[4]), Convert.ToUInt32(arrayEmployee[3]), Convert.ToUInt32(this.index), Convert.ToUInt32(index)));

        //                                List<uint> idList = new List<uint>();
        //                                if (this.dep[index - 1].Id[0] == 1_000_001)
        //                                    List<int> idList = new List<int>();
        //                                if (this.dep[index - 1].Id[0] == -1)
        //                                {
        //                                    this.dep[index - 1].Id[0] = this.index;
        //                                }
        //                                else
        //                                {
        //                                    this.dep[index - 1].AddId(this. );
        //                                }
        //                                int countId = this.dep[index - 1].Id.Count;
        //                                idList.Add(this.index + 1);
        //                            }
        //                        }
        //                    id++;
        //                }
        //            }
        //            else
        //            {
        //                Print("неверный путь к файлу");
        //            }

        //            Print("Повторить ввод? д/н");
        //            key = Console.ReadKey(true).KeyChar;
        //        } while (char.ToLower(key) == 'д');
        //    }
    }
}
