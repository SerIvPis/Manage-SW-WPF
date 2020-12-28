using SolidWorks.API.BoxingSW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorks.API.BoxingSW.Tests
{
    [TestClass()]
    public class SwDocumentPropertyTests
    {
        /// <summary>
        /// Переменные для тестов
        /// </summary>
        public static SWconnect SwApp = new SWconnect();
        public static ModelDoc2 SwModel = (ModelDoc2)SwApp.SwObject.ActiveDoc;
        public static SwDocumentProperty swProp = new SwDocumentProperty( SwModel );
        public static string[ ] _nameProp = { "Test property", "Test property another", "Some property" };
        public static string[ ] _valueProp = { "Test value", "Test value another", "Some value" };

        [TestMethod()]
        public void SwDocumentPropertyTest( )
        {
            Assert.IsTrue( SwApp.SwObject.ActiveDoc == swProp.SwModel );
        }

        [TestMethod()]
        public void GetConfigurationTest( )
        {
            Assert.IsTrue( swProp.GetConfiguration().Count() == 2 );
            Assert.IsTrue( swProp.GetConfiguration()[ 0 ] == "" );
            Assert.IsTrue( swProp.GetConfiguration()[ 1 ] == "По умолчанию" );
        }

        [TestMethod()]
        public void GetPropertysTest( )
        {
            swProp.ClearProtertys( "" );
            swProp.ClearProtertys( "По умолчанию" );
            swProp.Add( "", new SwProperty( _nameProp[ 0 ], _valueProp[ 0 ], swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "", new SwProperty( _nameProp[ 1 ], _valueProp[ 1 ], swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( _nameProp[ 2 ], _valueProp[ 2 ], swCustomInfoType_e.swCustomInfoText ) );

            Assert.IsTrue( swProp.GetPropertys( "По " ) == null );
            Assert.IsTrue( swProp.GetPropertys( "По умолчанию" )[ 0 ].Name == _nameProp[ 2 ] );
            Assert.IsTrue( swProp.GetPropertys( "По умолчанию" )[ 0 ].Value == _valueProp[ 2 ] );
        }

        [TestMethod()]
        public void AddTest( )
        {
            swProp.Add( "", new SwProperty( _nameProp[ 0 ], _valueProp[ 0 ], swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "", new SwProperty( _nameProp[ 1 ], _valueProp[ 1 ], swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( _nameProp[ 2 ], _valueProp[ 2 ], swCustomInfoType_e.swCustomInfoText ) );
            Assert.IsTrue( swProp.GetPropertys( "" )[ swProp.GetPropertys( "" ).Count - 1 ].Value == _valueProp[ 1 ] );
        }

        [TestMethod()]
        public void RemoveTest( )
        {
            
            swProp.Add( "", new SwProperty( _nameProp[ 0 ], _valueProp[ 0 ], swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "", new SwProperty( _nameProp[ 1 ], _valueProp[ 1 ], swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( _nameProp[ 2 ], _valueProp[ 2 ], swCustomInfoType_e.swCustomInfoText ) );

            Assert.IsTrue( swProp.Remove( "",
                new SwProperty( _nameProp[ 0 ], _valueProp[ 0 ], swCustomInfoType_e.swCustomInfoText ) ) == 0 );
            Assert.IsTrue( swProp.Remove( "",
                 new SwProperty( _nameProp[ 1 ], _valueProp[ 1 ], swCustomInfoType_e.swCustomInfoText ) ) == 0 );
            Assert.IsTrue( swProp.Remove( "По умолчанию",
                new SwProperty( _nameProp[ 2 ], _valueProp[ 2 ], swCustomInfoType_e.swCustomInfoText ) ) == 0 );
            Assert.IsFalse( swProp.Remove( "По умолчанию",
                new SwProperty( _nameProp[ 0 ], _valueProp[ 2 ], swCustomInfoType_e.swCustomInfoText ) ) == 0 );
        }


        [TestMethod()]
        public void ClearPropetyesModelSwTest( )
        {
            swProp.ClearProtertys( "" );
            swProp.ClearPropetyesModelSw( "" );
            Assert.IsTrue( swProp.GetPropertys( "" ).Count == 0);
        }

        [TestMethod()]
        public void ClearProtertysTest( )
        {
            swProp.ClearProtertys( "" );
            Assert.IsTrue( swProp.GetPropertys( "" ).Count == 0 );
        }
     

        [TestMethod()]
        public void ReadXmlFileTest( )
        {
            List<SwProperty> xml = swProp.ReadXmlFile( "Test.xml" );
            Assert.IsTrue( xml.Contains( new SwProperty( "Revision", "1", swCustomInfoType_e.swCustomInfoText )) );
        }
    }
}