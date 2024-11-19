using System.Net;

namespace TestProject
{
    [TestClass]
    public class MessageTest
    {
        private const string ENDPOINT = "https://localhost:7006/api/message/";

        [TestMethod]
        public void AddTest()
        {
            Helper helper = new Helper();

            var data = new
            {
                id = 0,
                title = "test message " + DateTime.Now.ToString(),
                active = true,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                deletedAt = DBNull.Value,
                UserId = Guid.NewGuid()
            };

            var result = helper.execApiPost(true, ENDPOINT, "Add", data).Result;

            Assert.AreEqual(result, HttpStatusCode.OK);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Helper helper = new Helper();

            var data = new
            {
                id = 1,
                title = "test message update" + DateTime.Now.ToString(),
                active = true,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                deletedAt = DBNull.Value,
                UserId = Guid.NewGuid()
            };

            var result = helper.execApiPost(true, ENDPOINT, "Update", data).Result;

            Assert.AreEqual(result, HttpStatusCode.OK);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Helper helper = new Helper();

            var data = new
            {
                id = 1,
                title = "test delete",
                active = false,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                deletedAt = DateTime.Now,
                UserId = Guid.NewGuid()
            };

            var result = helper.execApiPost(true, ENDPOINT, "Delete", data).Result;

            Assert.AreEqual(result, HttpStatusCode.OK);
        }

        [TestMethod]
        public void ListTest()
        {
            Helper helper = new Helper();

            var result = helper.execApiPost(true, ENDPOINT, "List").Result;

            Assert.AreEqual(result, HttpStatusCode.OK);
        }

        [TestMethod]
        public void ListActivesMessagesTest()
        {
            Helper helper = new Helper();

            var result = helper.execApiPost(true, ENDPOINT, "ListActivesMessages").Result;

            Assert.AreEqual(result, HttpStatusCode.OK);
        }
    }
}
