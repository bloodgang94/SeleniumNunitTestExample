using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunitTestExample.Settings
{
    public class ConnectionStrings
    {
        public string SelenoidUrl { get; set; }
        public string SiteUrl { get; set; }
        public string ShareFolder { get; set; }
    }
    public class Root
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
