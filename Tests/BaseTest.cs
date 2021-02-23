using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Tests
{
    public class BaseTest
    {
        protected DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDatabase").Options;
            var dbContext = new DataContext(options);
            
            return dbContext;
        }
    }
}
