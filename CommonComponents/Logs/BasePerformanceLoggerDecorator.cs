using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CommonComponents.Logs
{
    public abstract class BasePerformanceLoggerDecorator<T>
    {
        private static readonly LogLevel _LogLevel = LogLevel.Trace;

        protected ILogger _Logger;

        public BasePerformanceLoggerDecorator(
            ILogger logger)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<K> ExecuteFunction<K>(string methodName, Func<Task<K>> action)
        {
            var callInfo = new PerformanceLogObject(typeof(T), methodName);
            _Logger.Log(_LogLevel, PerformanceLogObject.Message, callInfo.GetCallBeginLogModel());
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = await action();
                stopwatch.Stop();
                _Logger.Log(_LogLevel, PerformanceLogObject.Message, callInfo.GetFinishSuccessLogModel(stopwatch.ElapsedMilliseconds));
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _Logger.Log(_LogLevel, ex, PerformanceLogObject.Message, callInfo.GetErrorLogModel(stopwatch.ElapsedMilliseconds));
                throw;
            }
        }

        protected async Task ExecuteAction(string methodName, Func<Task> action)
        {
            var callInfo = new PerformanceLogObject(typeof(T), methodName);
            _Logger.Log(_LogLevel, PerformanceLogObject.Message, callInfo.GetCallBeginLogModel());
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await action();
                stopwatch.Stop();
                _Logger.Log(_LogLevel, PerformanceLogObject.Message, callInfo.GetFinishSuccessLogModel(stopwatch.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _Logger.Log(_LogLevel, ex, PerformanceLogObject.Message, callInfo.GetErrorLogModel(stopwatch.ElapsedMilliseconds));
                throw;
            }
        }
    }
}
