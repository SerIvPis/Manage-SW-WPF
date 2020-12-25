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
        public static SWconnect SwApp = new SWconnect();
        public static ModelDoc2 SwModel = (ModelDoc2)SwApp.SwObject.ActiveDoc;
        public static SwDocumentProperty swProp = new SwDocumentProperty( SwModel );


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
            Assert.IsTrue( swProp.GetPropertys( "По " ) == null );
            Assert.IsTrue( swProp.GetPropertys( "По умолчанию" )[ 0 ].Name == "Обозначение" );
            Assert.IsTrue( swProp.GetPropertys( "По умолчанию" )[ 0 ].Value == "" );
        }

        [TestMethod()]
        public void AddTest( )
        {
            swProp.Add( "", new SwProperty( "Пример", "Новое значение", swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "", new SwProperty( "Основное свойство", "Значение", swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( "Пример", "Значение", swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( "Добавление свойства", "Изменненое Test", swCustomInfoType_e.swCustomInfoText ) );
            Assert.IsTrue( swProp.GetPropertys( "" )[ swProp.GetPropertys( "" ).Count - 1 ].Value == "Значение" );
        }

        [TestMethod()]
        public void RemoveTest( )
        {
            swProp.Add( "", new SwProperty( "Пример", "Новое значение", swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "", new SwProperty( "Основное свойство", "Значение", swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( "Пример", "Значение", swCustomInfoType_e.swCustomInfoText ) );
            swProp.Add( "По умолчанию", new SwProperty( "Добавление свойства", "Изменненое Test", swCustomInfoType_e.swCustomInfoText ) );

            Assert.IsTrue( swProp.Remove( "",
                new SwProperty( "Пример", "Новое значение", swCustomInfoType_e.swCustomInfoText ) ) == 0);
            Assert.IsTrue( swProp.Remove( "",
                new SwProperty( "Основное свойство", "Значение", swCustomInfoType_e.swCustomInfoText ) ) ==0);
            Assert.IsTrue( swProp.Remove( "По умолчанию", 
                new SwProperty( "Пример", "Значение", swCustomInfoType_e.swCustomInfoText ) ) == 0 );
            Assert.IsTrue( swProp.Remove( "По умолчанию",
                new SwProperty( "Добавление свойства", "Изменненое Test", swCustomInfoType_e.swCustomInfoText ) ) == 0 );
            Assert.IsFalse( swProp.Remove( "По умолчанию",
                new SwProperty( "Доба", "Test", swCustomInfoType_e.swCustomInfoText ) ) == 0 );
        }
    }
}