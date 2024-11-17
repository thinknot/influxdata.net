using InfluxData.Net.Common.Enums;

namespace InfluxData.Net.Integration.Kapacitor
{
    public class IntegrationFixture_v_1_0_0 : IntegrationFixtureBase
    {
        public IntegrationFixture_v_1_0_0()
            :base ("InfluxDbEndpointUri", InfluxDbVersion.v_1_0_0, "KapacitorEndpointUri", KapacitorVersion.v_1_0_0)
        {
        }
    }
}