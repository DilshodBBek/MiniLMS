using Microsoft.EntityFrameworkCore;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Infrastructure.DataAccess;

namespace MiniLMS.Infrastructure.Services;
public class TeacherService : ITeacherService
{
    private readonly MiniLMSDbContext _context;

    public TeacherService(MiniLMSDbContext context)
    {
        _context = context;
    }

    public async Task<Teacher> CreateAsync(Teacher entity)
    {
        await _context.Teachers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        Teacher? entity = await _context.Teachers.FindAsync(Id);
        if (entity == null)
            return false;

        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<IEnumerable<Teacher>> GetAllAsync()
    {
        IEnumerable<Teacher> teachers = _context.Teachers.AsNoTracking().AsEnumerable();
        return Task.FromResult(teachers);
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        Teacher? teacherEntity = await _context.Teachers.FindAsync(id);
        return teacherEntity;
    }

    public async Task<bool> UpdateAsync(Teacher entity)
    {
        _context.Teachers.Update(entity);
        var executedRows = await _context.SaveChangesAsync();

        return executedRows > 0;
    }
}
