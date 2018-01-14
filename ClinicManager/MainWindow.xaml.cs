using ClinicManager.Mock;
using ClinicManager.Model;
using ClinicManager.Service;
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

namespace ClinicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clinic clinic { get; set; }
        private ServiceData serviceData { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            serviceData = new ServiceData();

            clinic = serviceData.getClinicWithData("Medicover");
            Main.Content = new PageDashboard(serviceData, clinic);
        }
    }
}
