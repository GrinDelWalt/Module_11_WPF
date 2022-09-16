using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF.Models
{
    public class Department
    {
        public Department()
        {

        }
        public Department(string nameDepartment)
        {
            _nameDepartment = nameDepartment;
            _idDepartment = NextId();
            _departments = new List<Department>();
        }
        static Department()
        {
            _staticId = 0;
        }
        private static uint NextId()
        {
            return ++_staticId;
        }
        public void NewDepartment(string nameDepartment)
        {
            _departments.Add(new Department(nameDepartment));
        }
        public void DelateDepartment(Department department)
        {
            bool result = _departments.Contains(department);
            if (result)
            {
                _departments.Remove(department);
            }
            else
            {
                for (int i = 0; i < _departments.Count; i++)
                {
                    _departments[i].DelateDepartment(department);
                }
            }
        }
        public void NewDepartment(string locatedName, string nameDepartment)
        {
            Department department;
            if (locatedName == null)
            {
                NewDepartment(nameDepartment);
                return;
            }
            else
            {
                department = SearchDepartment(locatedName);
            }
            department.NewDepartment(nameDepartment);
        }
        private Department SearchDepartment(string name)
        {
            foreach (var depertment in _departments)
            {
                if (depertment.NameDepartment == name)
                {
                    return depertment;
                }
                if (depertment.Departments.Count != 0)
                {
                    depertment.SearchDepartment(name);
                }
            }
            return null;
        }
        public List<Department> Departments { get { return _departments; } set { _departments = value; } }
        public string NameDepartment { get { return _nameDepartment; } set { _nameDepartment = value; } }
        public uint IdDepartment { get { return _idDepartment; } }
        public uint NumberOfMamagedDepartments { get { return _numberOfMamagedDepartments; } set { _numberOfMamagedDepartments = value; } }

        private static uint _staticId;
        private uint _idDepartment;
        private string _nameDepartment;
        public List<Department> _departments { get; private set; }
        private uint _numberOfMamagedDepartments;
    }
}
