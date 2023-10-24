using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models;
using MiniLMS.Domain.Models.StudentDTO;
using Newtonsoft.Json;
using Serilog;

namespace MiniLMS.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    private readonly ITestService _testService;
    private readonly IDistributedCache _redis;
    private readonly Serilog.ILogger _logger;
    public string _cacheKey = "MyKey";
    public StudentController(IStudentService studentService, IMapper mapper, ITestService testService, IDistributedCache redis, Serilog.ILogger logger)
    {
        _studentService = studentService;
        _mapper = mapper;
        _testService = testService;
        _redis = redis;
        _logger = logger;
    }
    [HttpGet]
    public void LogData()
    {
        _logger.Information("aaaaaaaaaaa");
        _logger.Information("bbbbbbbbbbbbbb");
    }
    [HttpGet]
    public string RedisCache()
    {
        try
        {
            string? cacheValue = _redis.GetString(_cacheKey);

            throw new InvalidOperationException();
            if (string.IsNullOrEmpty(cacheValue))
            {
                cacheValue = "Today";
                _redis.SetString(_cacheKey, cacheValue, new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });
            }
            return cacheValue;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //_logger.LogError(ex.Message);
            return "Dasturda xatolik yuzaga keldi.\nIltimos keyinroq urinib ko`ring yoki Call Center ga murojaat qiling! Tel:99871557799";
        }
    }

    [HttpPost]
    public void GetService(StudentCreateDTO studentCreateDto)
    {
        //if (!ModelState.IsValid) throw new Exception();
        Console.WriteLine(_testService.Random);
        _studentService.Get();
    }

    [HttpGet]
    //[ResponseCache(Duration = 65)]
    //[OutputCache(Duration = 35)]
    public async Task<ResponseModel<IEnumerable<StudentGetDTO>>> GetAll()
    {
        string? cacheValue = _redis.GetString("GetAll1");

        IEnumerable<StudentGetDTO> students;

        if (string.IsNullOrEmpty(cacheValue))
        {
            students = (await _studentService.GetAllAsync())
                .Select(x => new StudentGetDTO
                {
                    BirthDate = x.BirthDate,
                    Name = x.FullName,
                    Gender = x.Gender,
                    Id = x.Id,
                    //Login = x.Login,
                    Major = x.Major,
                    PhoneNumber = x.PhoneNumber,
                    TeachersIds = x.Teachers?.Select(x => x.Id)
                });

            cacheValue = JsonConvert.SerializeObject(students);
            _redis.SetString("GetAll1", cacheValue, new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

        }

        else
        {
            students = JsonConvert.DeserializeObject<IEnumerable<StudentGetDTO>>(cacheValue);
        }
        return new(students);
    }

    [HttpGet]
    public async Task<ResponseModel<StudentGetDTO>> GetById(int id)
    {


        Student studentEntity = await _studentService.GetByIdAsync(id);
        var studentDto = new StudentGetDTO
        {
            BirthDate = studentEntity.BirthDate,
            Name = studentEntity.FullName,
            Gender = studentEntity.Gender,
            Id = studentEntity.Id,
            //Login = studentEntity.Login,
            Major = studentEntity.Major,
            PhoneNumber = studentEntity.PhoneNumber,
            TeachersIds = studentEntity.Teachers?.Select(x => x.Id)
        };
        return new(studentDto);
    }
    [HttpPost]
    public async Task<ResponseModel<StudentGetDTO>> Create(StudentCreateDTO studentCreateDto)
    {


        Student mappedStudent = _mapper.Map<Student>(studentCreateDto);


        //Student mappedStudent = new()
        //{
        //    BirthDate = studentCreateDto.BirthDate,
        //    Name = studentCreateDto.Name,
        //    Gender = studentCreateDto.Gender,
        //    Login = studentCreateDto.Login,
        //    Major = studentCreateDto.Major,
        //    PhoneNumber = studentCreateDto.PhoneNumber,
        //    Teachers = studentCreateDto.TeachersIds.Select(x => new Teacher()
        //    {
        //        Id = x
        //    }).ToList(),
        //    Password = studentCreateDto.Password
        //};

        Student studentEntity = await _studentService.CreateAsync(mappedStudent);

        var studentDto = new StudentGetDTO()
        {

            BirthDate = studentEntity.BirthDate,
            Name = studentEntity.FullName,
            Gender = studentEntity.Gender,
            Id = studentEntity.Id,
            Login = studentEntity.Login,
            Major = studentEntity.Major,
            PhoneNumber = studentEntity.PhoneNumber,
            TeachersIds = studentEntity.Teachers?.Select(x => x.Id)
        };
        return new(studentDto);
    }


}
