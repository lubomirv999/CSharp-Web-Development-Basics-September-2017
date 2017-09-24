namespace _02._One_to_Many_Relation
{
    public class StartUp
    {
        public static void Main()
        {
            // Tasks 2, 3 done here
            var db = new OneToManyDbContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var department = new Department { Name = "Test" };

            department.Employees.Add(new Employee { Name = "Pesho" });

            db.Departments.Add(department);

            db.SaveChanges();
        }
    }
}