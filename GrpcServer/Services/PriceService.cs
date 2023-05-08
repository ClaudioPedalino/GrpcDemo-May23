using Grpc.Core;

namespace GrpcServer.Services
{
    public class PriceService : PriceProtoService.PriceProtoServiceBase
    {
        private readonly ILogger<PriceService> _logger;

        readonly int delayInMilliseconds = 1000;
        double btcBasePrice = 29000;
        double satoshiBalance = 0;
        readonly double adaBasePrice = 0.38;
        readonly double ethBasePrice = 1900;
        readonly double solBasePrice = 21;


        public PriceService(ILogger<PriceService> logger)
        {
            _logger = logger;
        }


        public override async Task GetBitcoinPrice(BitcoinPriceRequest request,
                                                   IServerStreamWriter<BitcoinPriceReply> responseStream,
                                                   ServerCallContext context)
        {
            var rand = new Random();


            while (true)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Service was requested to be canceled");
                    break;
                }

                var deviation = rand.NextDouble() * 0.004 - 0.002;

                btcBasePrice *= (1 + deviation);

                var reply = new BitcoinPriceReply() { CurrentPrice = btcBasePrice };

                _logger.LogInformation($"reply is: {reply}");
                await responseStream.WriteAsync(reply, context.CancellationToken);

                await Task.Delay(delayInMilliseconds);
            }
        }

        public override async Task<BalanceReply> PushSatoshiToBalance(IAsyncStreamReader<PushSatoshisRequest> requestStream,
                                                                      ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                satoshiBalance += request.Satoshis;
            }

            var usdbalance = satoshiBalance / 3550;

            var response = new BalanceReply()
            {
                UsdBalance = usdbalance / 1,
                BtcBalance = usdbalance / btcBasePrice,
                EthBalance = usdbalance / ethBasePrice,
                AdaBalance = usdbalance / adaBasePrice,
                SolBalance = usdbalance / solBasePrice,
            };

            return response;
        }
    }
}
