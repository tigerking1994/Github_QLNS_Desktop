using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupDatabase
{
    public class ConnectionStrings
    {
        public string SqlServer { get; set; }
        public string LocalDb { get; set; }
    }

    public class DbSettings
    {
        public string ConnectionType { get; set; }
    }
}
