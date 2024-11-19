using System.Net;

namespace TestProject
{
    [TestClass]
    public class UserTest
    {
        private const string ENDPOINT = "https://localhost:7006/api/";

        [TestMethod]
        public void CreateTokenIdentityTest()
        {
            Helper helper = new Helper();

            var data = new
            {
                email = "test.user" + new Random().Next().ToString() + "@testmail.com",
                password = "Teste@1234",
                document = Guid.NewGuid().ToString()
        };

            var result = helper.execApiPost(false, ENDPOINT, "CreateTokenIdentity", data).Result;

            Assert.AreEqual(result, HttpStatusCode.OK);
        }

        

    }
}