using System.Collections.Generic;

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<StudentsCourse> Students { get; set; } = new List<StudentsCourse>();
}