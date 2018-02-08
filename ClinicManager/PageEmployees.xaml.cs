using ClinicManager.Model;
using ClinicManager.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PageEmployees.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        private ServiceData serviceData { get; set; }
        private List<Person> employees { get; set; }
        private Person selectedPerson;
        private FunctionItem selectedFunction;
        private CollectionView view;

        public PageEmployees(ServiceData serviceData, List<Person> employees)
        {
            this.serviceData = serviceData;
            this.employees = employees;
            InitializeComponent();

            listEmpolyees.ItemsSource = employees;
            view = (CollectionView)CollectionViewSource.GetDefaultView(listEmpolyees.ItemsSource);
            view.Filter = UserFilter;


        }
        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxFilter.Text))
                return true;
            else
                return ((item as Person).LastName.IndexOf(TextBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listEmpolyees.ItemsSource).Refresh();

        }
        
        private void listEmpolyees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPerson = ((sender as ListView).SelectedItem as Person);

            if(selectedPerson != null)
            {
                fillData(selectedPerson);
            }
        }
        private void fillData(Person person)
        {
            TextBoxName.Text = person.Name;
            TextBoxLastName.Text = person.LastName;
            TextBoxSSN.Text = person.SSN;
            TextBoxStreet.Text = person.Address.Street;
            TextBoxNumber.Text = person.Address.PostNumber;
            TextBoxCity.Text = person.Address.City;
            TextBoxPostalCode.Text = person.Address.PostalCode;
            ComboBoxFuntionTyp.ItemsSource = Enum.GetValues(typeof(FunctionType)).Cast<FunctionType>();
            listFuntion.ItemsSource = person.FunctionTypes;
        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            if(selectedPerson != null && ComboBoxFuntionTyp.SelectedItem != null && !string.IsNullOrEmpty(TextBoxPrice.Text) && !string.IsNullOrEmpty(TextBoxTime.Text))
            {
                FunctionItem item = new FunctionItem((FunctionType)ComboBoxFuntionTyp.SelectedItem, new PriceItem(PriceItemType.STANDARD, float.Parse(TextBoxPrice.Text, CultureInfo.InvariantCulture.NumberFormat)), new TimeSpan(0, Convert.ToInt32(TextBoxTime.Text), 0));
                selectedPerson.FunctionTypes.Add(item);
                serviceData.AddFunction(item, selectedPerson.PersonId);

                listFuntion.Items.Refresh();
            }
        }

        private void ButtonRemove(object sender, RoutedEventArgs e)
        {

            if(selectedPerson != null && selectedFunction != null)
            {
                serviceData.DeleteFunction(selectedFunction.FunctionId);
                selectedPerson.FunctionTypes.Remove(selectedFunction);
                listFuntion.Items.Refresh();
                selectedFunction = null;
            }
        }

        private void listFuntion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFunction = ((sender as ListView).SelectedItem as FunctionItem);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");//^\d*[\.\,]\d*$    "^[0-9]+$"   "^[0-9\.\,0-9]+$"
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void clearControls()
        {
            TextBoxName.Text = string.Empty;
            TextBoxLastName.Text = string.Empty;
            TextBoxSSN.Text = string.Empty;
            TextBoxStreet.Text = string.Empty;
            TextBoxNumber.Text = string.Empty;
            TextBoxCity.Text = string.Empty;
            TextBoxPostalCode.Text = string.Empty;
            ComboBoxFuntionTyp.ItemsSource = Enum.GetValues(typeof(FunctionType)).Cast<FunctionType>();
            listFuntion.ItemsSource = null;
        }
        private bool updatePerson()
        {
            if(selectedPerson != null && validationField())
            {
                selectedPerson.Name = TextBoxName.Text;
                selectedPerson.LastName = TextBoxLastName.Text;
                selectedPerson.SSN = TextBoxSSN.Text;
                selectedPerson.Address.Street = TextBoxStreet.Text;
                selectedPerson.Address.PostNumber = TextBoxNumber.Text;
                selectedPerson.Address.City = TextBoxCity.Text;
                selectedPerson.Address.PostalCode = TextBoxPostalCode.Text;
                selectedPerson.FunctionTypes = listFuntion.ItemsSource as List<FunctionItem>;

                serviceData.UpdatePerson(selectedPerson);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool validationField()
        {
            if (string.IsNullOrEmpty(TextBoxName.Text) ||
                string.IsNullOrEmpty(TextBoxLastName.Text) ||
                string.IsNullOrEmpty(TextBoxSSN.Text) ||
                string.IsNullOrEmpty(TextBoxStreet.Text) ||
                string.IsNullOrEmpty(TextBoxCity.Text) ||
                string.IsNullOrEmpty(TextBoxPostalCode.Text) ||
                string.IsNullOrEmpty(TextBoxNumber.Text)) return false;

            return true;
        }
        private bool addPerson()
        {
            if (validationField())
            {
                Address address = new Address() { City = TextBoxCity.Text, PostalCode = TextBoxPostalCode.Text, PostNumber = TextBoxPostalCode.Text, Street = TextBoxStreet.Text };
                Person person = new Person(TextBoxName.Text, TextBoxLastName.Text, TextBoxSSN.Text, address, PersonType.Doctor, listFuntion.ItemsSource == null ? null : listFuntion.ItemsSource as List<FunctionItem>);

                serviceData.AddPerson(person);
                employees.Add(person);

                view.Refresh();
                selectedPerson = null;
                listEmpolyees.SelectedIndex = -1;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ButtonNew(object sender, RoutedEventArgs e)
        {
            clearControls();
            listEmpolyees.SelectedIndex = -1;
        }

        private void ButtonUpdatePerson(object sender, RoutedEventArgs e)
        {
            if (!updatePerson())
            {
                MessageBox.Show("Sprawdz poprawoność wpisanych danych", "Błąd");
            }
            else
            {
                view.Refresh();
            }
        }

        private void ButtonAddPerson(object sender, RoutedEventArgs e)
        {
            if (!addPerson())
            {
                MessageBox.Show("Sprawdz poprawoność wpisanych danych", "Błąd");
            }
        }

        private void ButtonRemovePeron(object sender, RoutedEventArgs e)
        {
            if(selectedPerson != null)
            {
                employees.Remove(selectedPerson);
                serviceData.DeletePerson(selectedPerson);

                clearControls();
               // listEmpolyees.Items.Refresh();
                view.Refresh();
                selectedPerson = null;
            }
        }
    }
}
