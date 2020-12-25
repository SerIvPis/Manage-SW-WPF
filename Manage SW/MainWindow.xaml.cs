using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolidWorks.API.BoxingSW;
using SolidWorks.API.Specification;
using SolidWorks.Interop.sldworks;

namespace Manage_SW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow( )
        {
            InitializeComponent();
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            SWconnect SwApp = new SWconnect();
            ModelDoc2 SwModel = (ModelDoc2)SwApp.SwObject.ActiveDoc;
            SwDocumentProperty swProp = new SwDocumentProperty( SwModel );


            swProp.WriteJson();


            //foreach (var item in swProp.GetPropertys(""))
            //{
            //    lbTest.Items.Add( $"\t{item.Name} = {item.Value}" );

            //    //lbTest.Items.Add( $"{item}" );
            //    //foreach (var i in swProp._ConfigPropertys[ item ])
            //    //{
            //    //}
            //}

        }
    }
}
