using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Application.Settings
{
    public class AppSettings
    {
        public const string SectionName = "AppSettings";
        public string AppName { get; set; } = string.Empty;

        public string AppVersion { get; set; } = string.Empty;

        public bool EnableDetailedLogging { get; set; } = false;

        public int MaxItemsPerPage { get; set; } = 10;

        public int MaxYear { get; set; } = 2200;
    }
}
