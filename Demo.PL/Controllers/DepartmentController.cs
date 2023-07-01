using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            //if (departments is null)
            //    return NotFound();
            //ViewData["Message"] = "Hello From View Data";

           // ViewBag.Message = "Hello From View Bag";

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(department);
                TempData["Message"] = "Department Created Successfully!!";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();
            var department = _repository.GetById(id);

            if (department is null)
                return NotFound();
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _repository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return NotFound();

            var department = _repository.GetById(id);

            if (department is null)
                return NotFound();
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id, Department department)
        {
            if (id != department.Id)
                return NotFound();
            _repository.Delete(department);
            return RedirectToAction(nameof(Index));
        }
    }
}
