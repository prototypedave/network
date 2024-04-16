using NUnit.Framework;
using System.Net;
using TcpProtocol;
using Protocol;

namespace TcpProtocol.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void TestProgram()
        {
            // Arrange
            var server = new Server(32000);
            var client = new Client(server.GetIp(), 32000);

            // Act
            // Run the program's logic (e.g., starting server and client)

            // Assert
            Assert.NotNull(server);
            Assert.NotNull(client);
            // Add more assertions as needed
        }
    }
}
