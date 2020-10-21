﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoticeApp.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Models.Tests
{
    [TestClass]
    public  class NoticeRepositoryAsyncTest
    {
        [TestMethod]
        public async Task NoticeRepositoryAsyncAllMethodTest()
        {
            //[0] 
            var options = new DbContextOptionsBuilder<NoticeAppDbContext>().UseInMemoryDatabase(databaseName: "$NoticeApp{Guid.NewGuid()}").Options;


            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();


            //[1] AddAsync ()
            using (var context = new NoticeAppDbContext(options) )
            {

                //[A] Arrange
                var repository = new NoticeRepositoryAsync(context, factory);
                var model = new Notice { Name = "관리자", Title = "공지사항", Content = "내용입니다" };

                //[B] Act
                await repository.Addsync(model);

            }

            using (var context = new NoticeAppDbContext(options))
            {

                //[C] Assert
                Assert.AreEqual(1, await context.Notices.CountAsync());
                var model = await context.Notices.Where(n => n.Id == 1).SingleOrDefaultAsync();
                Assert.AreEqual("관리자", model.Name);
            }
        }
    }
}
