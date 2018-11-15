using AutoChem.Core.CentralDataServer.Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.Controllers.Simulated
{
    public class SimulatedInstruments
    {

        private readonly Dictionary<string, LiveInstrumentInfo> m_instruments;
        private int m_nextIndex;

        public SimulatedInstruments()
        {
            m_nextIndex = 1;
            m_instruments = new Dictionary<string, LiveInstrumentInfo>();
            AddInstrument("localhost", "local instrument");
            AddInstrument("117.24.56.6", "another instrument");
        }

        public IEnumerable<LiveInstrumentInfo> Instruments { get => m_instruments.Values; }

        public LiveInstrumentInfo AddInstrument(string hostAddress, string description)
        {
            var instrument = CreateInstrument(hostAddress, description);
            m_instruments.Add(hostAddress, instrument);
            return instrument;
        }

        public void DeleteInstrument(string hostAddress)
        {
            if (!m_instruments.ContainsKey(hostAddress))
                m_instruments.Remove(hostAddress);
        }

        private LiveInstrumentInfo CreateInstrument(string hostAddress, string description)
        {
            var liveInstrumentInfo = new LiveInstrumentInfo();
            liveInstrumentInfo.InstrumentInfo = new InstrumentInfo(InstrumentStatus.Verified, hostAddress, "RX-10", "6.0.177", "SER0001", description);
            liveInstrumentInfo.HostAddress = hostAddress;
            liveInstrumentInfo.Reactor1Value = "25.0";
            liveInstrumentInfo.Reactor2Value = "25.0"; ;
            liveInstrumentInfo.TimeDifference = TimeSpan.FromSeconds(3);
            liveInstrumentInfo.LastSuccessfulCommunication = DateTime.UtcNow;
            return liveInstrumentInfo;
        }
    }
}
