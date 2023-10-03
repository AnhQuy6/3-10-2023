namespace CollageApp.Models
{
    public static class CollageRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            {
                Id = 1,
                Name = "AnhQuy",
                Age = 20,
                Address = "Ha Tinh",
                Email = "hoangquy3125@gmail.com"
            },
            
            new Student
            {
                Id= 2,
                Name = "VanThao",
                Age = 20,
                Address = "Ninh Binh",
                Email = "vanthao236@gmail.com"
            },

            new Student
            {
                Id = 3,
                Name = "HoangLong",
                Age = 30,
                Address = "Thai Lan",
                Email = "long@gmail.com"
            }
        };
    }
}
