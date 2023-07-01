using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchValue = "")
        {

            if (string.IsNullOrEmpty(SearchValue))
            {
                var employees = _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployees);
            }
            else
            {
                var employees = _unitOfWork.EmployeeRepository.Search(SearchValue);
                var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployees);
            }
            //var employees= _unitOfWork.EmployeeRepository.GetAll();

            //foreach (var item in employees)
            //{
            //    var department = _unitOfWork.DepartmentRepository.GetById(item.DepartmentID);

            //    ViewBag.DepartmentName = department.Name;

            //}
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employees.Id,
            //    Address = employees.Address,
            //    DateOfCreation = employees.DateOfCreation,
            //    Email = employees.Email,
            //    HireDate = employees.HireDate,
            //    IsActive = employees.IsActive,
            //    Name = employees.Name,
            //    Salary = employees.Salary
            //};
            //var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            //return View(mappedEmployees);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "imgs");
                var employee = _mapper.Map<EmployeeViewModel, Employee>(model);
                _unitOfWork.EmployeeRepository.Add(employee);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
         
            ViewBag.Employees = _unitOfWork.EmployeeRepository.GetById(id);
            return View();
           
        }

        [HttpPost]
        public IActionResult Edit(int id, EmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<EmployeeViewModel, Employee>(model);
                _unitOfWork.EmployeeRepository.Update(employee);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetById(id);
            return View();

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id, Employee employee)
        {
           
                _unitOfWork.EmployeeRepository.Delete(employee);

                return RedirectToAction(nameof(Index));
            
        }
    }
}
