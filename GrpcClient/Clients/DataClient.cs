using Grpc.Net.Client;

namespace GrpcClient.Clients
{
    internal static class DataClient
    {
        public static async Task GetPeople()
        {
            PeopleReply? response = default;

            try
            {
                //var channel = GrpcChannel.ForAddress("https://localhost:7053");
                
                var channel = GrpcChannel.ForAddress("https://localhost:7053", new GrpcChannelOptions()
                {
                    MaxReceiveMessageSize = int.MaxValue,
                });

                var client = new DataProtoService.DataProtoServiceClient(channel);

                var request = new PeopleRequest { Quantity = 50 };

                response = await client.GetPeopleAsync(request);
                foreach (var person in response!.People)
                    GrpcClient.Helpers.ConsoleHelper.PrintPersonInfo(person);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
