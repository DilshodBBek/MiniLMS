using Microsoft.EntityFrameworkCore;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Infrastructure.DataAccess;

namespace MiniLMS.Infrastructure.Services;
public class StudentService : IStudentService
{
    private readonly MiniLMSDbContext _context;
    private readonly ITestService _test;
    public StudentService(MiniLMSDbContext context, ITestService test)
    {
        _context = context;
        _test = test;
    }


    public async Task<Student> CreateAsync(Student entity)
    {
        await _context.Students.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        Student? entity = await _context.Students.FindAsync(Id);
        if (entity == null)
            return false;

        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public void Get()
    {
        Console.WriteLine(_test.Random);
    }

    public Task<IEnumerable<Student>> GetAllAsync()
    {
        IEnumerable<Student> students = _context.Students.Include(x=>x.Teachers).AsNoTracking().AsEnumerable();
        return Task.FromResult(students);
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        Student? studentEntity = await _context.Students.FindAsync(id);
        return studentEntity;
    }

    public async Task<bool> UpdateAsync(Student entity)
    {
        _context.Students.Update(entity);
        var executedRows = await _context.SaveChangesAsync();

        return executedRows > 0;
    }
}
