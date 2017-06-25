using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShare.ViewModels
{
    class AboutViewModel
    {
        private string _appVersion;
        private string _assemblyVersion;
        private string _serverVersion;

        public AboutViewModel(string appVersion, string assemblyVersion, string serverVersion)
        {
            _appVersion = appVersion;
            _assemblyVersion = assemblyVersion;
            _serverVersion = serverVersion;
        }

        public string AssemblyVersion{ get => _assemblyVersion; set => _assemblyVersion = value; }
        public string ServerVersion { get => _serverVersion; set => _serverVersion = value; }
        public string AppVersion { get => _appVersion; set => _appVersion = value; }
    }
}
