using Autofac;
using Kairos.Business.Job;
using Kairos.Core.Business.App;
using Kairos.Core.DataAccess.JobManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kairos.Core
{
    public static class ServiceProvider
    {
        public static Autofac.IContainer Container { get; set; }

        public static void PrepareStartup()
        {
            Container = ConfigureServices();
        }

        private static  Autofac.IContainer ConfigureServices()
        {

                var containerBuilder = new ContainerBuilder();

                //  Registers all the platform-specific implementations of services.
                containerBuilder.RegisterType<DataAccess.ConfigManager.UserConfig.UserConfigManager>()
                               .As<Kairos.Core.Business.Config.IUserConfigManager>()
                               .SingleInstance();

                containerBuilder.RegisterType<JobManager>()
                               .As<IJobManager>()
                               .SingleInstance();

                var container = containerBuilder.Build();
                return container;
        }
    }
}
