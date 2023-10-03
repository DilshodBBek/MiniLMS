using MiniLMS.Domain.States;

namespace MiniLMS.Domain.Entities;
public class BaseEntity
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

}
