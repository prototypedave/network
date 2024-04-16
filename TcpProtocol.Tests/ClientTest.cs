using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace TcpProtocol.Tests
{
    [TestFixture]
    public class ClientTests
    {
        private Server server;
        private Client client;

        [SetUp]
        public void Setup()
        {
            // Start the server
            server = new Server(32000);
            Task.Run(() => server.Start());

            // Create the client
            client = new Client(server.GetIp(), 32000);
        }

        [TearDown]
        public void TearDown()
        {
            // Stop the server and dispose of the client
            server.Dispose();
            client.Dispose();
        }

        [Test]
        public void TestSendHelloMessage()
        {
            int hello = client.SendHelloMessage();
            // Test success in sending hello message
            Assert.That(hello == 1, Is.True, "Hello message failed to sent");

            // receive message
            string json = client.ReceiveJson();
            //test if the welcome message was received successfully
            int welcome = client.ReceiveWelcomeMessage(json);
            Assert.That(welcome == 0, Is.True, "Welcome Message Failed");

            client.SendRequestDataMessage("hamlet.txt");
            bool fin = server.getEnd();
            while(!fin)
            {
                client.ReceiveData ();
                int nId = client.GetSupposedId();
                int cId = client.GetCurrentId();
                Assert.That(nId, Is.EqualTo(cId), "Missing ACK messages");
                fin = server.getEnd();
            }
        }
    }
}
