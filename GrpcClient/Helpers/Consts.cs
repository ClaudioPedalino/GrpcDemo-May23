namespace GrpcClient.Helpers
{
    internal static class Consts
    {
        internal const string GetPeopleOption = "GrpcServer - GetPeople() from data.proto";
        internal const string GetBitcoinPriceOption = "GrpcServer - GetBitcoinPrice() from price.proto";
        internal const string GetPeopleFromRestApiOption = "RestApi - GetPeople() from /people?quantity={quantity}";
        internal const string PushToBalanceOption = "GrpcServer - PushSatoshiToBalance() from price.proto";

        internal static readonly string[] Options = new string[]
        {
            GetPeopleOption,
            GetBitcoinPriceOption,
            GetPeopleFromRestApiOption,
            PushToBalanceOption
        };

    }
}
