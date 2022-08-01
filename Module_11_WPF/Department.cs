using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    class Department
    {
        public Department(string nameDepartment)
        {
            _nameDepartment = nameDepartment;
            _idDepartment = NextId();
            _departments = new ObservableCollection<Department>();
            _idWorker = new List<uint>();
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
        public void NewDeirector(uint id)
        {
            _idDirector = id;
        }
        public void NewWorker(uint id)
        {
            _idWorker.Add(id);
        }
        public void NewIntern(uint id)
        {
            _idIntern.Add(id);
        }
        public ObservableCollection<Department> Departments { get { return this._departments; } set { this._departments = value; } }
        public string NameDepartment { get { return this._nameDepartment; } set { this._nameDepartment = value; } }
        public uint IdDepartment { get { return _idDepartment; } }
        public uint NumberOfMamagedDepartments { get { return this._numberOfMamagedDepartments; } set { this._numberOfMamagedDepartments = value; } }
        public uint IdDirector { get { return this._idDirector; } set { this._idDirector = value; } }
        public List<uint> IdWorker { get { return this._idWorker; } set { this._idWorker = value; } }
        public List<uint> IdIntern { get { return this._idIntern; } set { this._idIntern = value; } }

        private static uint _staticId;
        private uint _idDepartment;
        private string _nameDepartment;
        public ObservableCollection<Department> _departments { get; private set; }
        private uint _idDirector;
        private List<uint> _idWorker;
        private List<uint> _idIntern;
        private uint _numberOfMamagedDepartments;
    }
}
