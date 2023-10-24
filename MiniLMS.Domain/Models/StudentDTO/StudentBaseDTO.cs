using MiniLMS.Domain.States;
using System.ComponentModel.DataAnnotations;

namespace MiniLMS.Domain.Models.StudentDTO;
public class StudentBaseDTO
{
    //[MaxLength(5)]
    [StringLength(5, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    public string Name { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string Major { get; set; }
    public IEnumerable<int> TeachersIds { get; set; }

}
