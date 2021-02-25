using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using Topshelf;


namespace GenoanyLocalService
{
    public class ServiceRunner : ServiceControl
    {

        private IDisposable hostObject;

        private string port => ConfigurationManager.AppSettings.Get("Port");
        public bool Start(HostControl hostControl)
        {
            hostObject = WebApp.Start<RegisterRoutesStartup>($"http://localhost:{port}"); 
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            hostObject.Dispose();

            return true;
        }
    }


}
