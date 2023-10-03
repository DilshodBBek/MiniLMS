using Microsoft.AspNetCore.Mvc;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models;
using MiniLMS.Domain.Models.StudentDTO;

namespace MiniLMS.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ResponseModel<IEnumerable<StudentGetDTO>>> GetAll()
    {
        IEnumerable<StudentGetDTO> students =
            (await _studentService.GetAllAsync())
            .Select(x => new StudentGetDTO
            {
                BirthDate = x.BirthDate,
                FullName = x.FullName,
                Gender = x.Gender,
                Id = x.Id,
                Login = x.Login,
                Major = x.Major,
                PhoneNumber = x.PhoneNumber,
                TeachersIds = x.Teachers?.Select(x => x.Id)
            });

        return new(students);
    }

    [HttpGet]
    public async Task<ResponseModel<StudentGetDTO>> GetById(int id)
    {
        Student studentEntity = await _studentService.GetByIdAsync(id);
        var studentDto = new StudentGetDTO
        {
            BirthDate = studentEntity.BirthDate,
            FullName = studentEntity.FullName,
            Gender = studentEntity.Gender,
            Id = studentEntity.Id,
            Login = studentEntity.Login,
            Major = studentEntity.Major,
            PhoneNumber = studentEntity.PhoneNumber,
            TeachersIds = studentEntity.Teachers?.Select(x => x.Id)
        };
        return new(studentDto);
    }
    [HttpPost]
    public async Task<ResponseModel<StudentGetDTO>> Create(StudentCreateDTO studentCreateDto)
    {
        Student mappedStudent = new()
        {
            BirthDate = studentCreateDto.BirthDate,
            FullName = studentCreateDto.FullName,
            Gender = studentCreateDto.Gender,
            Login = studentCreateDto.Login,
            Major = studentCreateDto.Major,
            PhoneNumber = studentCreateDto.PhoneNumber,
            Teachers = studentCreateDto.TeachersIds.Select(x => new Teacher()
            {
                Id = x
            }).ToList(),
            Password = studentCreateDto.Password
        };

        Student studentEntity = await _studentService.CreateAsync(mappedStudent);

        var studentDto = new StudentGetDTO
        {
            BirthDate = studentEntity.BirthDate,
            FullName = studentEntity.FullName,
            Gender = studentEntity.Gender,
            Id = studentEntity.Id,
            Login = studentEntity.Login,
            Major = studentEntity.Major,
            PhoneNumber = studentEntity.PhoneNumber,
            TeachersIds = studentEntity.Teachers.Select(x => x.Id)
        };
        return new(studentDto);
    }


}
