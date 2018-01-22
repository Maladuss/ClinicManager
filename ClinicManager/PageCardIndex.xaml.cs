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
    /// Interaction logic for PageCardIndex.xaml
    /// </summary>
    public partial class PageCardIndex : Page
    {
        private ServiceData serviceData { get; set; }
        private List<Person> employees { get; set; }
        private List<PersonItem> personItems { get; set; }
        private List<IFunction> cards { get; set; }
        private PersonItem selectedPersonItem;
        private CollectionView view;

        public PageCardIndex(ServiceData serviceData, List<Person> employees)
        {
            this.serviceData = serviceData;
            this.employees = employees;
            InitializeComponent();

            cards = new List<IFunction>();
            
            listPatient.ItemsSource = getPersonItems(employees);
            view = (CollectionView)CollectionViewSource.GetDefaultView(listPatient.ItemsSource);
            view.Filter = UserFilter;
        }
        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxFilter.Text))
                return true;
            else
                return ((item as PersonItem).LastName.IndexOf(TextBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private List<PersonItem> getPersonItems(List<Person> employees)
        {
            List<PersonItem> items = new List<PersonItem>();

            foreach (var em in employees)
            {
                foreach(var calendarItem in em.Calendar)
                {
                    foreach(var fun in calendarItem.CalendarItems)
                    {
                        if(!items.Exists(x => x.SSN == fun.patient.SSN))
                        {
                            items.Add(new PersonItem(fun.patient.Name, fun.patient.LastName, fun.patient.SSN));
                        }
                        cards.Add(fun);
                    }
                }
            }
            return items;
        }
        private void ButtonView(object sender, RoutedEventArgs e)
        {
            if(selectedPersonItem != null)
            {
                listCardIndex.ItemsSource = cards.Where(x => x.patient.SSN == selectedPersonItem.SSN).ToList();
                listCardIndex.Items.Refresh();
            }
            
        }

        private void listPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPersonItem = ((sender as ListView).SelectedItem as PersonItem);
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listPatient.ItemsSource).Refresh();
        }
    }
}
