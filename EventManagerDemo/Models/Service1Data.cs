using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerDemo.Models
{
    internal class Service1Data
    {
        internal string Id { get; set; } = Guid.NewGuid().ToString();
        internal string Document { get; set; }

        public Service1Data(string data)
        {
            Document = data;
        }
    }
}
