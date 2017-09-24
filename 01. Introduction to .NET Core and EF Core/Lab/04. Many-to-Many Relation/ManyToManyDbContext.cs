using Microsoft.EntityFrameworkCore;

public class ManyToManyDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=ManyToMany;Integrated Security=true;");

        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<StudentsCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

        builder
            .Entity<Student>()
            .HasMany(s => s.Courses)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentId);

        builder
            .Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(sc => sc.Course)
            .HasForeignKey(sc => sc.CourseId);
    }
}