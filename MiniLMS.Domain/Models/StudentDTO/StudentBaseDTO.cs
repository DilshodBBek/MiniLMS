﻿using MiniLMS.Domain.States;

namespace MiniLMS.Domain.Models.StudentDTO;
public class StudentBaseDTO
{
    public string FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string Major { get; set; }
    public IEnumerable<int> TeachersIds { get; set; }

}
