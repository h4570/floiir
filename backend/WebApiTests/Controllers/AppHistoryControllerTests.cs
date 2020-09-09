using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Controllers.Tests
{
    [TestClass()]
    public class AppHistoryControllerTests
    {
        public AppDbContext _context;
        private  AppHistoryController appHistoryController;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new AppDbContext(options);
            appHistoryController = new AppHistoryController(options);
        }

        #region GetAll

        [TestMethod("GetAll_AddOneAppHistory")]
        public async Task GetAll_AddOneAppHistory_ReturnOneAppHistoryRow()
        {
            // arrange 
            _context.AppHistory.Add(new AppHistory() 
            {
                TableId=AppTable.TableName,
                Type = AppHistoryType.Add,
                UserId=1,
                ElementId=1,
                DateTime=DateTime.Now
            });
            _context.SaveChanges();

            // act
            var actionResult = await appHistoryController.GetAll(0,1);
            var resultValue = actionResult.Value as List<AppHistory>;

            // assert
            Assert.AreEqual(resultValue.Count(),1);
        }

        [TestMethod("GetAll_SetWrongElement")]
        public async Task GetAll_SetWrongElement_ReturnEmptyAppHistory()
        {
            // arrange 
            _context.AppHistory.Add(new AppHistory()
            {
                TableId = AppTable.TableName,
                Type = AppHistoryType.Add,
                UserId = 1,
                ElementId = 1,
                DateTime = DateTime.Now
            });
            _context.SaveChanges();

            // act
            var actionResult = await appHistoryController.GetAll(0, 12);
            var resultValue = actionResult.Value as List<AppHistory>;

            // assert
            Assert.AreEqual(resultValue.Count(), 0);
        }

        [TestMethod("GetAll_GetWrongTable")]
        public async Task GetAll_GetWrongTable_ReturnEmptyAppHistory()
        {
            // arrange 
            _context.AppHistory.Add(new AppHistory()
            {
                TableId = AppTable.TableName,
                Type = AppHistoryType.Add,
                UserId = 1,
                ElementId = 1,
                DateTime = DateTime.Now
            });
            _context.SaveChanges();

            // act
            var actionResult = await appHistoryController.GetAll(1, 1);
            var resultValue = actionResult.Value as List<AppHistory>;

            // assert
            Assert.AreEqual(resultValue.Count(), 0);
        }

        #endregion
    }
}