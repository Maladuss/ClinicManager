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
    /// Interaction logic for PageDashboard.xaml
    /// </summary>
    public partial class PageDashboard : Page
    {
        private Clinic clinic { get; set; }
        private ServiceData serviceData { get; set; }
        public PageDashboard(ServiceData serviceData, Clinic clinic)
        {
            this.clinic = clinic;
            this.serviceData = serviceData;
            InitializeComponent();

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageRegistration(serviceData, clinic.getEmployee(), clinic.getPatients()));

        }

        private void CardIndex_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCardIndex(serviceData, clinic.getEmployee()));
        }

        private void Empoloyees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageEmployees(serviceData, clinic.getEmployee()));
        }
    }
}
