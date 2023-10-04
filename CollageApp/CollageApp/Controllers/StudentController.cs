using CollageApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetALL")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudent()
        {
            var students = new List<StudentDTO>();
            foreach (var item in CollageRepository.Students)
            {
                StudentDTO ojb = new StudentDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Address = item.Address,
                    Email = item.Email,
                };
                students.Add(ojb);
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("id/{id:int}", Name = "GetId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest($"Vui long nhap id > 0");
            var x = CollageRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            if (x == null)
                return NotFound($"Khong tim thay id la {x}");
            return Ok(x);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Loi!");
            var n = CollageRepository.Students.Where(n => n.Name == name).FirstOrDefault();
            if (n == null)
                return NotFound("Khong tim thay ten");
            return Ok(n);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> DeleteById(int id)
        {
            if (id < 0)
                return BadRequest("Id can lon hon 0");
            var x = CollageRepository.Students.Where(n =>n.Id == id).FirstOrDefault();
            if (x == null) return NotFound();
            CollageRepository.Students.Remove(x);
            return Ok(x);
        }
    }
}
