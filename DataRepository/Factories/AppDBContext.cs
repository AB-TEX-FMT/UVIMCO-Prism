using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataRepository.Factories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}