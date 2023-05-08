using Grpc.Net.Client;

namespace GrpcClient.Clients
{
    internal static class PriceClient
    {
        public static async Task GetBitcoinPrice()
        {
            try
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7053");
                var client = new PriceProtoService.PriceProtoServiceClient(channel);

                var cancellationToken = new CancellationTokenSource().Token;
                double previusPrice = default;

                Console.WriteLine("Bitcoin price updates:");

                using var response = client.GetBitcoinPrice(new BitcoinPriceRequest(), cancellationToken: cancellationToken);

                while (await response.ResponseStream.MoveNext(cancellationToken))
                {
                    var currentPrice = response.ResponseStream.Current.CurrentPrice;

                    printInfo(previusPrice, currentPrice);

                    previusPrice = currentPrice;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }



        private static void printInfo(double previusPrice, double currentPrice)
        {
            if (currentPrice > previusPrice)
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (currentPrice < previusPrice)
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else
                Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"Current price: {currentPrice:N2}");
        }
    }
}
