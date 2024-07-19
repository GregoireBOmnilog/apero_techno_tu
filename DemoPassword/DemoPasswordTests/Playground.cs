using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPasswordTests
{
    public class Playground
    {
        [Test]
        public void test1()
        {
            Assert.AreEqual("é".ToUpperInvariant(), "E");
        }
    }
}
