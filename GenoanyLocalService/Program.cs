using Topshelf;

namespace GenoanyLocalService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.UseLog4Net();
                x.RunAsLocalSystem();
                x.Service<ServiceRunner>();
                x.SetDescription(string.Format("{0} Ver:{1}", "GenoanyLocalService", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));
                x.SetDisplayName("GenoanyLocalService");
                x.SetServiceName("GenoanyLocalService");
            });
        }
    }
}
