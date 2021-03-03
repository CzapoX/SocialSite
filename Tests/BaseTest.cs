using Application.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Tests
{
    public class BaseTest
    {
        protected readonly IMapper _mapper;

        public BaseTest()
        {
            if (_mapper == null)
            {
                var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
                _mapper = mockMapper.CreateMapper();
            }
        }

        protected DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDatabase").Options;
            var dbContext = new DataContext(options);

            return dbContext;
        }
    }
}
