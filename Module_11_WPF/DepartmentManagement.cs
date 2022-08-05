using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    class DepartmentManagement
    {
        private ObservableCollection<Department> _departments;
        private uint _idDepartment;
        public DepartmentManagement()
        {
            _departments = new ObservableCollection<Department>();
        }
        public void Test()
        {
            _departments.Add(new Department("Finans"));
            _departments.Add(new Department("Management"));
            _departments.Add(new Department("Constructors"));
            _departments.Add(new Department("Dezhaners"));

            _departments[0].NewDepartment("finansDoun");
            _departments[0].NewDepartment("finansDoun2");
        }
        public void NewDepartment(string nameDepartment)
        {
            _departments.Add(new Department(nameDepartment));
        }
        public void NewDepartmentDyId(uint id)
        {
            _idDepartment = id;
            IEnumerable<Department> department = _departments.Where(Find);
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
        public void SerchDepartment(uint id)
        {

        }
        public bool Find(Department department)
        {
            return department.IdDepartment == 6;
        }
        public void DelateDepartment(Department department)
        {
            _departments.Remove(department);
        }
        public void EditDepartment(Department department)
        {
            
        }
        
        public ObservableCollection<Department> GetDepartment()
        {
            return _departments;
        }

    }
}
