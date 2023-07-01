using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using System.Collections.Generic;
using System.Linq;


namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAPPDBContext _context;

        public EmployeeRepository(MVCAPPDBContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> Search(string name)
        {
            return _context.Employees.Where(e => e.Name.Contains(name)).ToList();
        }

        

        //public int Add(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //    => _context.Employees.ToList();

        //public Employee GetById(int? id)
        //    => _context.Employees.FirstOrDefault(x => x.Id == id);

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}
    }
}
