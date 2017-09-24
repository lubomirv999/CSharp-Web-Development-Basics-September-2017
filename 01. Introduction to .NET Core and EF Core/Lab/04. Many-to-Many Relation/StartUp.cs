namespace _04._Many_to_Many_Relation
{
    public class StartUp
    {
        public static void Main()
        {
            //Task 3 done here
            var db = new ManyToManyDbContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}