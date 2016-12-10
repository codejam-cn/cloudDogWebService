using System.ServiceProcess;

namespace cdws
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            //var servicesToRun = new ServiceBase[] 
            //{ 
            //    new Cdws() 
            //};
            //ServiceBase.Run(servicesToRun);

            Cdws.RunGet();
        }
    }
}
