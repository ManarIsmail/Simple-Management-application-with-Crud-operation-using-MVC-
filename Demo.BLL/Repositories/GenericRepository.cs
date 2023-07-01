using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T :class
    {
        private readonly MVCAPPDBContext _context;

        public GenericRepository(MVCAPPDBContext context)
        {
            _context = context;
        }  

        public int Add(T obj)
        {
            _context.Set<T>().Add(obj);
            return _context.SaveChanges();
        }

        public int Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
            => _context.Set<T>().ToList();

        public T GetById(int? id)
            => _context.Set<T>().Find(id);

        public int Update(T obj)
        {
            _context.Set<T>().Update(obj);
            return _context.SaveChanges();
        }
    }
}
