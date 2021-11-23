using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardMVC.Common.Exceptions
{
    public class DuplicatedException : Exception
    {

        public string EntityName { get; }
        public DuplicatedException()
        {
        }

        public DuplicatedException(string message)
        : base(message)
        {

        }

        public DuplicatedException(string message, Exception inner)
        : base(message, inner)
        {
        }

        public DuplicatedException(string message, string entityName)
        : this(message)
        {
            EntityName = entityName;
        }
    }
}
