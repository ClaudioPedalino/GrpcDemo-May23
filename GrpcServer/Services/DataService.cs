using Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MethodTimer;

namespace GrpcServer.Services
{
    public class DataService : DataProtoService.DataProtoServiceBase
    {
        private readonly ILogger<DataService> _logger;
        private const string errorMessage = "Quantity must be greater than 0";

        public DataService(ILogger<DataService> logger)
        {
            _logger = logger;
        }

        [Time]
        public override async Task<PeopleReply> GetPeople(PeopleRequest request,
                                                          ServerCallContext context)
        {
            List<PersonReply> response = new();

            if (request.Quantity <= 0)
            {
                _logger.LogError(errorMessage);
                throw new RpcException(new Status(StatusCode.InvalidArgument, errorMessage));
            }


            var result = await MockDataContext.GetMockPeople(request.Quantity);

            foreach (var item in result)
            {
                response.Add(new PersonReply()
                {
                    Id = item.Id.ToString(),
                    FullName = item.FullName,
                    Age = item.Age,
                    Email = item.Email,
                    Birth = Timestamp.FromDateTime(item.Birth)
                });
            }

            return new PeopleReply() { People = { response } };
        }
    }
}
