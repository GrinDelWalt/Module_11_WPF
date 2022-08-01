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
        public ObservableCollection<Department> _departments;

        public DepartmentManagement()
        {
            _departments = new ObservableCollection<Department>();
        }
        public void Test()
        {
            _departments.Add(new Department("Finans"));
            _departments.Add(new Department("Management"));
            _departments.Add(new Department("Constructors"));
            _departments.Add(new Department("Dezaners"));

            _departments[0].NewDepartment("finansDoun");
            _departments[0].NewDepartment("finansDoun2");
        }
        public void NewDepartment(string nameDepartment)
        {
            _departments.Add(new Department(nameDepartment));
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
        
        public ObservableCollection<Department> GetDepartment()
        {
            return _departments;
        }

    }
}
