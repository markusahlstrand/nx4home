using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Nx4Home.Specs.Steps
{
    [Binding]
    public class Whens
    {
        [When(@"I request the system status")]
        public void WhenIRequestTheSystemStatus()
        {
            AlarmSystem alarmSystem = new AlarmSystem();
            ScenarioContext.Current.Set(alarmSystem);

            alarmSystem.ReadStatus();
        }
    }
}
