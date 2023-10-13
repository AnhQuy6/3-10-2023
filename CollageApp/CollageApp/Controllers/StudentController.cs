using CollageApp.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        [Route("Create")]
        //api/controller/Create
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
                return BadRequest();
            int newid = CollageRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student()
            {
                Id = newid,
                Age = model.Age,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
            };
            CollageRepository.Students.Add(student);
            model.Id = student.Id; // Cập nhật Id mới nhất
            return Ok(student);
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

        [HttpPut]
        [Route("Update")]
        //app/Student/Update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();
            var existingStudent = CollageRepository.Students.Where(s => s.Id ==  model.Id).FirstOrDefault();
            if (existingStudent == null)
                return NotFound();
            existingStudent.Age = model.Age;
            existingStudent.Name = model.Name;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.Email;
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //app/Student/UpdatePartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdatePartialStudent(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();
            var existingStudent = CollageRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            if (existingStudent == null)
                return NotFound();
            var StudentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                Name = existingStudent.Name,
                Age = existingStudent.Age,
                Address = existingStudent.Address,
                Email = existingStudent.Email
            };

            patchDocument.ApplyTo(StudentDTO, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.Age = StudentDTO.Age;
            existingStudent.Name = StudentDTO.Name;
            existingStudent.Address = StudentDTO.Address;
            existingStudent.Email = StudentDTO.Email;
            return NoContent();
        }

    }
}
