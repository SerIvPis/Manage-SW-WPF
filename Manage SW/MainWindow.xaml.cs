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
using SolidWorks.API.Specification;

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
            string organization = "АБВГ";
            int qualify = 123456;
            int serialNum = 123;
            byte ver = 3;
            CodeDocument code = new CodeDocument( CodeDoc.PE4 );

            string des = "Цкди";
            Designation designation_3 = new Designation( des );

            // Создаем три обозначения для теста Equals
            Designation designation = new Designation( organization, qualify, serialNum, code, ver );
            Designation designation_1 = new Designation( organization, qualify, serialNum, code, ver );
            Designation designation_2 = new Designation( organization, qualify, 001, code, ver );

            lbTest.Items.Add( designation.ToString() );
        }
    }
}
