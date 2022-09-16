using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_11_WPF.Models;

namespace Module_11_WPF
{
    class DepartmentManagement
    {
        private List<Department> _departments;
        private uint _idDepartment;

        public DepartmentManagement()
        {
            _departments = new List<Department>();
        }
        public void Test()
        {
            _departments.Add(new Department("RoflanEPAM"));

            _departments[0].NewDepartment("Finans");
            _departments[0].NewDepartment("Management");
            _departments[0].NewDepartment("Constructors");
            _departments[0].NewDepartment("Dezhaners");

            _departments[0].Departments[1].NewDepartment("ManagementDone1");
            _departments[0].Departments[1].NewDepartment("ManagementDone2");
        }
        public void NewDepartment(string nameDepartment)
        {
            if (_departments.Count == 0)
            {
                _departments.Add(new Department(nameDepartment));
            }
            else
            {
                _departments[0].NewDepartment(nameDepartment);
            }
        }
        public void NewDepartmentDyId(uint id)
        {
            _idDepartment = id;
            IEnumerable<Department> department = _departments[0].Departments.Where(Find);
        }
        public uint GetNumber()
        {
            uint number;
            try
            {
                number = Convert.ToUInt32(Console.Read());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error --> {e}"); //  в последствии обобщить ошибку
                number = GetNumber();
            }
            return number;
        }
        public void NewDepartment(string locatedName, string nameDepartment)
        {
            if (locatedName == "null")
            {
                NewDepartment(nameDepartment);
            }
            else if (locatedName == _departments[0].NameDepartment)
            {
                _departments[0].NewDepartment(nameDepartment);
            }
            else
            {
                _departments[0].NewDepartment(locatedName, nameDepartment);
            }
        }
        public bool Find(Department department)
        {
            return department.IdDepartment == 6;
        }
        public void DelateDepartment(Department department)
        {
            _departments[0].DelateDepartment(department);
        }


        public List<Department> GetDepartment()
        {
            return _departments;
        }

    }
}
