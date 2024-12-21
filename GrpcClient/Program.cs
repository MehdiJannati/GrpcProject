using Grpc.Net.Client;
using GrpcSharedProtoFile;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a channel to the gRPC server
            using var channel = GrpcChannel.ForAddress("https://localhost:7089");
            var client = new UserService.UserServiceClient(channel);

            // Make the gRPC call
            var reply = await client.CreateUserAsync(new UserRequest()
            {
                Id = "test",
                BirthDate = DateTime.Now.ToString(),
                FirstName = "Test",
                LastName = "Grpc",
                NationalCode = "1234567890",
            });
            Console.WriteLine("UserService: " + reply.Message);
        }
    }
}
