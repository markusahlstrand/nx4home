using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Nx4Home.Specs.Steps
{
    [Binding]
    public class Thens
    {
        [Then(@"the result shoud be disarmed")]
        public void ThenTheResultShoudBeDisarmed()
        {
            Thread.Sleep(2000);

            int a = 0;
        }
    }
}
