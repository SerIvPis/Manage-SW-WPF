using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorks.API.BoxingSW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorks.API.BoxingSW.Tests
{
    [TestClass()]
    public class SwPropertyTests
    {
        [TestMethod()]
        public void SwPropertyTest( )
        {
            SwProperty _swProperty = new SwProperty( "свойство1", "значение свойства1", swCustomInfoType_e.swCustomInfoText );

            Assert.IsTrue( "свойство1"==_swProperty.Name );
            Assert.IsTrue( "значение свойства1" == _swProperty.Value );
        }

        [TestMethod()]
        public void EqualsTest( )
        {

            SwProperty _swProperty = new SwProperty( "свойство1", "значение свойства1", swCustomInfoType_e.swCustomInfoText );
            SwProperty _swProperty_2 = new SwProperty( "свойство2", "значение свойства1", swCustomInfoType_e.swCustomInfoText );
            Assert.IsFalse( _swProperty == _swProperty_2 );
        }

       
    }
}

