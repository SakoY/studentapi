using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Services;
using TestAPI.Models;
using Microsoft.AspNetCore.Http;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Student>>> Get(int index, int count, int sort_order)
        {
            try
            {
                Response.Headers.Add("X-Total-Count", _studentService.Size(count).ToString());
                return await _studentService.Get(index, count, sort_order);
            }
            catch
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, @"Parameter Error:Please check documentation ");
            }
        }

          

        [HttpGet("{_id:length(24)}", Name = "GetStudent")]
        public ActionResult<Student> Get(string _id)
        {
            var student = _studentService.Get(_id);

            if (student == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, @"ID not found: please check ID");
            }

            return student;
        }

        [HttpPost]
        public ActionResult<Student> Create([FromBody] Student student)
        {
            try
            {
                _studentService.Create(student);
            }catch{
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, @"Formatting Error: please check documentation for proper format");
            }

            return CreatedAtRoute("GetStudent", new { _id = student._id }, student);
        }

        [HttpPut("{_id:length(24)}")]
        public IActionResult Update(string _id, [FromBody] Student bookIn)
        {
            var student = _studentService.Get(_id);

            if (student == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, @"ID not found: please check ID");
            }

            try
            {
                _studentService.Update(_id, bookIn);
            }
            catch
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, @"Formatting Error: please check documentation for proper format");
            }

            return Ok();
        }

        [HttpDelete("{_id:length(24)}")]
        public IActionResult Delete(string _id)
        {
            var student = _studentService.Get(_id);

            if (student == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, @"ID not found: please check ID");
            }

            _studentService.Remove(student._id);

            return Ok();
        }


    }

}