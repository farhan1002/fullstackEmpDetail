using fullstackApi.data;
using fullstackApi.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fullstackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private readonly EmployeeDbcontext _context;
        public employeeController(EmployeeDbcontext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllEmployee()
        {
          var employees= await _context.Employees.ToListAsync();

            return Ok(employees);
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee employeeReq) { 

            employeeReq.Id = Guid.NewGuid();

            await _context.Employees.AddAsync(employeeReq);
           await _context.SaveChangesAsync();
            return Ok(employeeReq);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getEmployee([FromRoute] Guid id)
        {

            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }


            return Ok(employee);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id, Employee updateEmployee) {

           var employee= await _context.Employees.FindAsync(id);

            if (employee==null) {

                return NotFound();
            
            }

            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Salary = updateEmployee.Salary;
            employee.Phone = updateEmployee.Phone;
            await _context.SaveChangesAsync();
             return Ok(employee);
        
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteById([FromRoute] Guid id) {

         var employee=   await _context.Employees.FindAsync(id);

            if (employee== null) {

                return NotFound();
            
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        
        
        }



    }
}
