using System.Runtime.CompilerServices;

namespace CommonComponents.Logs
{
    public class PerformanceLogObject
    {
        public const string Message = "{message}";

        public record BeginLogModel(
            Guid CallId,
            string MethodFullName, 
            string Status = "BEGIN");

        public record FinishSuccessLogModel(
            Guid CallId,
            string MethodFullName,
            long DurationTime,
            string Status = "SUCCESS");

        public record ErrorLogModel(
            Guid CallId,
            string MethodFullName,
            long DurationTime,
            string Status = "FAILED");

        public PerformanceLogObject(
            Type methodClass,
            string methodName)
        {
            _MethodFullName = $@"{methodClass.FullName}.{methodName}";
            _CallId = Guid.NewGuid();
        }

        private string _MethodFullName { get; }
        private Guid _CallId { get; set; }

        public BeginLogModel GetCallBeginLogModel()
        {
            return new BeginLogModel(_CallId, _MethodFullName);
        }

        public FinishSuccessLogModel GetFinishSuccessLogModel(long durationTime)
        {
            return new FinishSuccessLogModel(_CallId, _MethodFullName, durationTime);
        }

        public ErrorLogModel GetErrorLogModel(long durationTime)
        {
            return new ErrorLogModel(_CallId, _MethodFullName, durationTime);
        }
    }
}
