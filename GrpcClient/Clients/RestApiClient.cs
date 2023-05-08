using Common;
using Newtonsoft.Json;

namespace GrpcClient.Clients
{
    internal static class RestApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task GetPeople()
        {
            string? jsonResponse = default;

            try
            {
                const int Quantity = 5;

                string url = $"https://localhost:7067/People?quantity={Quantity}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse!);
                foreach (var item in apiResponse.Value)
                    GrpcClient.Helpers.ConsoleHelper.PrintPersonInfo(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        public class ApiResponse
        {
            public List<Person> Value { get; set; }
            public int StatusCode { get; set; }
            public string ContentType { get; set; }
        }
    }
}
