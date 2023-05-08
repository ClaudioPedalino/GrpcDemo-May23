using System.Reflection;

namespace GrpcServer
{
    public static class MethodTimeLogger
    {
        public static ILogger Logger;

        public static void Log(MethodBase methodBase, TimeSpan timeSpan, string message)
        {
            Logger.LogTrace($"{methodBase.DeclaringType!.Name}.{methodBase.Name} - {timeSpan.TotalMilliseconds}");
        }
    }
}
