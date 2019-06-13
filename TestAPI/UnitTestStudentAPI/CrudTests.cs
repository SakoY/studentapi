using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestAPI.Services;
using TestAPI.Models;


namespace UnitTestStudentAPI
{
    [TestClass]
    public class CrudTests
    {

        public static class APIinit
        {
            static IConfiguration config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            static StudentService _studentService = new StudentService(config);

            public static StudentService studentService
            {
                get
                {
                    return _studentService;
                }
            }

        }

        [TestMethod]
        public async System.Threading.Tasks.Task ListCall()
        {
            var results = await APIinit.studentService.Get(1,1,1);
            int count = results.Count;
            Assert.AreEqual(count, 1);

            results = await APIinit.studentService.Get(1, 10, 1);
            count = results.Count;
            Assert.AreEqual(count, 10);

            results = await APIinit.studentService.Get(2, 5000, 1);
            count = results.Count;
            Assert.IsTrue(count < 5000);

            results = await APIinit.studentService.Get(3, 5000, 1);
            count = results.Count;
            Assert.AreEqual(count, 0);


        }
    }
}
