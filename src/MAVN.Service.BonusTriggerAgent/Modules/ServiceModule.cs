using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.BonusTriggerAgent.DomainServices;
using MAVN.Service.BonusTriggerAgent.Managers;
using MAVN.Service.BonusTriggerAgent.Settings;
using Lykke.Service.CurrencyConvertor.Client;
using Lykke.Service.CustomerProfile.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.BonusTriggerAgent.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly AppSettings _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings.CurrentValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .SingleInstance()
                .AutoActivate();

            builder.RegisterModule(new AutofacModule());

            builder.RegisterCurrencyConvertorClient(_appSettings.CurrencyConvertorServiceClient);
            builder.RegisterCustomerProfileClient(_appSettings.CustomerProfileServiceClient);
        }
    }
}
