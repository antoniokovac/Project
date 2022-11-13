using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using Project.Common;
using Project.DAL;
using Project.Model.DatabaseModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Tests.GenericRepositoryTest
{
    public class GenericRepositoryTest
    {
        [Fact]
        public async Task GetAll_ValidRequest_ReturnsList()
        {
            var testMockObject = new TestMockObject();

            testMockObject.MockVehicleDbContext
                .Setup(x => x.Set<TestDbEntity>())
                .Returns(GetQueryableMockDbSet());

            var query = new QueryParameters();
            Expression<Func<TestDbEntity, bool>> testFilterExpression = x => x.Id.ToString() == query.Filter;
            Expression<Func<TestDbEntity, string>> testSortExpression = x => x.Id.ToString();
            var getAll = await testMockObject.GenericRepository.GetAll(testFilterExpression, testSortExpression, query);
            Assert.NotNull(getAll);
  
         }
        private static DbSet<TestDbEntity> GetQueryableMockDbSet()
        {
            var sourceList = new List<TestDbEntity>();
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<TestDbEntity>>();
            dbSet.As<IQueryable<TestDbEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<TestDbEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<TestDbEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<TestDbEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.As<IQueryable<TestDbEntity>>().Setup(m => m.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(sourceList);

            return dbSet.Object;
        }
    }
    public class TestMockObject
    {
        public TestMockObject()
        {
            MockVehicleDbContext = new Mock<VehicleDbContext>(() => new VehicleDbContext(new DbContextOptions<VehicleDbContext>()));
            GenericRepository = new GenericRepository(MockVehicleDbContext.Object);
        }
        public IGenericRepository GenericRepository { get; }
        public Mock<VehicleDbContext> MockVehicleDbContext { get; }
    }
    public class TestDbEntity
    {
        public TestDbEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id{ get;}

    }

}
    