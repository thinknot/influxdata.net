using System;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.Kapacitor.ClientModules;
using InfluxData.Net.Kapacitor.Infrastructure;
using InfluxData.Net.Kapacitor.RequestClients;
using System.Net.Http;
using InfluxData.Net.Common.Infrastructure;

namespace InfluxData.Net.Kapacitor
{
    public class KapacitorClient : IKapacitorClient
    {
        private IKapacitorRequestClient _requestClient;

        private Lazy<ITaskClientModule> _taskClientModule;

        public ITaskClientModule Task
        {
            get { return _taskClientModule.Value; }
        }

        public KapacitorClient(string uri, KapacitorVersion kapacitorVersion, HttpClient httpClient = null, bool throwOnWarning = false)
            : this(new KapacitorClientConfiguration(new Uri(uri), null, null, kapacitorVersion, httpClient, throwOnWarning))
        {
        }

        public KapacitorClient(IKapacitorClientConfiguration configuration)
        {
            switch (configuration.KapacitorVersion)
            {
                case KapacitorVersion.Latest:
                case KapacitorVersion.v_1_7:
                    this.BootstrapKapacitorLatest(configuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("kapacitorClientConfiguration", String.Format("Unknown version {0}.", configuration.KapacitorVersion));
            }
        }

        protected virtual void BootstrapKapacitorLatest(IKapacitorClientConfiguration configuration)
        {
            _requestClient = new KapacitorRequestClient(configuration);

            _taskClientModule = new Lazy<ITaskClientModule>(() => new TaskClientModule(_requestClient));
        }
    }
}