using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitRepository : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitRepository(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            EmployeeRepository = employeeRepository;
            DepartmentRepository = departmentRepository;
        }
    }
}
