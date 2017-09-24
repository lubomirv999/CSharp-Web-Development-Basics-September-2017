using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Course
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Price { get; set; }

    public List<StudentCourse> Students { get; set; } = new List<StudentCourse>();

    public List<Resource> Resources { get; set; } = new List<Resource>();

    public List<Homework> Homeworks { get; set; } = new List<Homework>();
}