namespace _01._StudentSystem
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        private static Random random = new Random();
        public static void Main()
        {
            using (var db = new StudentSystemDbContext())
            {
                db.Database.Migrate();

                //SeedinitialData(db);
                //SeedLicenses(db);

                //PrintStudentsWithHomeworks(db);

                //PrintCoursesAndResources(db);

                //PrintCoursesWithMoreThanFiveResources(db);

                //PrintCourseActionOnADate(db);

                //PrintStudentsWithPrices(db);

                //PrintCourseWithResourcesAndLicenses(db);

                //PrintStudentsWithCoursesAndResourcesAndLicenses(db);
            }
        }

        private static void SeedInitialData(StudentSystemDbContext db)
        {
            const int totalStudents = 25;
            const int totalCourses = 10;

            var currentDate = DateTime.Now;

            // Students
            for (int i = 0; i < totalStudents; i++)
            {
                db.Students.Add(new Student
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate.AddDays(i),
                    Birthday = currentDate.AddYears(-20).AddDays(i),
                    Phone = $"Random Phone {i}"
                });
            }

            db.SaveChanges();

            // Courses
            var addedCourses = new List<Course>();

            for (int i = 0; i < totalCourses; i++)
            {
                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course Details {i}",
                    Price = 100 * i,
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(20 + i)
                };

                addedCourses.Add(course);
                db.Courses.Add(course);
            }

            db.SaveChanges();

            // Students in Courses           
            var studentIds = db
                        .Students
                        .Select(s => s.Id)
                        .ToList();

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];
                var studentsInCourse = random.Next(2, totalStudents / 2);

                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studentIds[random.Next(0, studentIds.Count)];

                    if (!currentCourse.Students.Any(s => s.StudentId == studentId))
                    {
                        currentCourse.Students.Add(new StudentCourse
                        {
                            StudentId = studentId
                        });
                    }
                    else
                    {
                        j--;
                    }
                }

                var resoursesInCourse = random.Next(2, 20);
                var types = new[] { 0, 1, 2, 999 };

                for (int j = 0; j < resoursesInCourse; j++)
                {
                    currentCourse.Resources.Add(new Resource
                    {
                        Name = $"Resourse {i} {j}",
                        Url = $"Url {i} {j}",
                        Type = (ResourceType)types[random.Next(0, types.Length)]
                    });
                }
            }

            db.SaveChanges();

            // Homeworks

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];

                var studentInCourseIds = currentCourse
                    .Students
                    .Select(s => s.StudentId)
                    .ToList();

                for (int j = 0; j < studentInCourseIds.Count; j++)
                {
                    var totalHomeworks = random.Next(2, 5);

                    for (int k = 0; k < totalHomeworks; k++)
                    {
                        db.Homeworks.Add(new Homework
                        {
                            Content = $"Content Homework {i}",
                            SubmissionDate = currentDate.AddDays(-i),
                            Type = ContentType.Zip,
                            StudentId = studentInCourseIds[j],
                            CourseId = currentCourse.Id
                        });
                    }
                }

                db.SaveChanges();
            }
        }

        private static void SeedLicenses(StudentSystemDbContext db)
        {
            var resourceIds = db
                .Resourses
                .Select(r => r.Id)
                .ToList();

            for (int i = 0; i < resourceIds.Count; i++)
            {
                var totalLicenses = random.Next(1, 4);

                for (int j = 0; j < totalLicenses; j++)
                {
                    db.Licenses.Add(new License
                    {
                        Name = $"License {i} {j}",
                        ResourceId = resourceIds[i]
                    });
                }

                db.SaveChanges();
            }
        }

        private static void PrintStudentsWithHomeworks(StudentSystemDbContext db)
        {
            var result = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        h.Content,
                        h.Type
                    })
                })
                .ToList();

            foreach (var student in result)
            {
                Console.WriteLine(student.Name);

                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine($"---{homework.Content} - {homework.Type}");
                }
            }
        }

        private static void PrintCoursesAndResources(StudentSystemDbContext db)
        {
            var result = db
                .Courses
                .OrderBy(c => c.StartDate)
                .ThenBy(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources.Select(r => new
                    {
                        r.Name,
                        r.Url,
                        r.Type
                    })
                })
                .ToList();

            foreach (var course in result)
            {
                Console.WriteLine($"{course.Name} - {course.Description}");

                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"{resource.Name} - {resource.Type} - {resource.Url}");
                }
            }
        }

        private static void PrintCoursesWithMoreThanFiveResources(StudentSystemDbContext db)
        {
            var result = db
                .Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources.Count
                })
                .ToList();

            foreach (var course in result)
            {
                Console.WriteLine($"{course.Name} - {course.Resources}");
            }
        }

        private static void PrintCourseActionOnADate(StudentSystemDbContext db)
        {
            var date = DateTime.Now.AddDays(25);

            var result = db
                .Courses
                .Where(c => c.StartDate < date && date < c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = c.EndDate.Subtract(c.StartDate),
                    Students = c.Students.Count
                })
                .OrderByDescending(c => c.Students)
                .ThenByDescending(c => c.Duration)
                .ToList();

            foreach (var course in result)
            {
                Console.WriteLine($"{course.Name}: {course.StartDate.ToShortDateString()} - {course.EndDate.ToShortDateString()}");
                Console.WriteLine($"---Duration: {course.Duration.TotalDays}");
                Console.WriteLine($"---Students: {course.Students}");
                Console.WriteLine(new string('-', 10));
            }
        }

        private static void PrintStudentsWithPrices(StudentSystemDbContext db)
        {
            var result = db
                .Students
                .Where(s => s.Courses.Any())
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Course.Price),
                    AveragePrice = s.Courses.Average(c => c.Course.Price)
                })
                .OrderByDescending(c => c.TotalPrice)
                .ThenByDescending(c => c.Courses)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in result)
            {
                Console.WriteLine($"{student.Name} - {student.Courses} - {student.TotalPrice} - {student.AveragePrice}");
            }
        }

        private static void PrintCourseWithResourcesAndLicenses(StudentSystemDbContext db)
        {
            var result = db
                .Courses
                .OrderByDescending(c => c.Resources.Count)
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Resources = c
                        .Resources
                        .OrderByDescending(r => r.Licenses.Count)
                        .ThenBy(r => r.Name)
                        .Select(r => new
                        {
                            r.Name,
                            Licenses = r.Licenses.Select(l => l.Name)
                        })
                })
                .ToList();

            foreach (var course in result)
            {
                Console.WriteLine(course.Name);

                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"---{resource.Name}");

                    foreach (var license in resource.Licenses)
                    {
                        Console.WriteLine($"------{license}");
                    }
                }
            }
        }

        private static void PrintStudentsWithCoursesAndResourcesAndLicenses(StudentSystemDbContext db)
        {
            var result = db
                .Students
                .Where(s => s.Courses.Any())
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Courses.Count,
                    Resources = s.Courses.Sum(c => c.Course.Resources.Count),
                    Licenses = s.Courses.Sum(c => c.Course.Resources.Sum(r => r.Licenses.Count))
                })
                .ToList();

            foreach (var student in result)
            {
                Console.WriteLine($"{student.Name} - {student.Courses} - {student.Resources} - {student.Licenses}");
            }
        }
    }
}