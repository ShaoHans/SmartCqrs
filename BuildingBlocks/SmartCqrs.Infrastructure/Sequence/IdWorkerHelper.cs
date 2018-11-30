using Snowflake;
using System;

namespace SmartCqrs.Infrastructure.Sequence
{
    public sealed class IdWorkerHelper
    {
        private static IdWorker worker = new IdWorker(1, 1,9);
        
        public static long NewId()
        {
            return worker.NextId();
        }

        public static string GenerateSequenceNo()
        {
            string id = NewId().ToString();
            return string.Concat(DateTime.Now.ToString("yyyyMMdd"), id);
        }
    }
}
