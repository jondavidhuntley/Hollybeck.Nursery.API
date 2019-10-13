using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseryServices.Entities.Query
{
    public class BaseQueryResult
    {
        public BaseQueryResult()
        {
            Started = DateTime.Now;
        }

        public bool NotFound { get; set; }

        public bool InnerException
        {
            get
            {
                return !string.IsNullOrEmpty(FaultMessage);
            }
        }

        public string FaultMessage { get; set; }

        public DateTime Started { get; set; }

        public DateTime Completed { get; set; }
    }
}