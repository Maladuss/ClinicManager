using ClinicManager.Model;
using ClinicManager.Service;
using System;
using System.Collections.Generic;
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
using Xceed.Wpf.Toolkit;

namespace ClinicManager
{
    /// <summary>
    /// Interaction logic for PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        private ServiceData serviceData { get; set; }
        private List<Person> employees { get; set; }
        private List<Person> patients { get; set; }
        private Person selectedPerson;
        private FunctionItem selectedFunction;
        private CollectionView viewPatient;
        private CollectionView view;
        private Person selectedEmployee;

        public PageRegistration(ServiceData serviceData, List<Person> employees, List<Person> patients)
        {
            this.serviceData = serviceData;
            this.employees = employees;
            this.patients = patients;
            InitializeComponent();

            listPatient.ItemsSource = patients;
            viewPatient = (CollectionView)CollectionViewSource.GetDefaultView(listPatient.ItemsSource);
            viewPatient.Filter = UserFilter;

            listEmpolyees.ItemsSource = employees;
            view = (CollectionView)CollectionViewSource.GetDefaultView(listEmpolyees.ItemsSource);
            view.Filter = DoctorFilter;
        }
        private bool UserFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxFilter.Text))
                return true;
            else
                return ((item as Person).Name.IndexOf(TextBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private bool DoctorFilter(object item)
        {
            if (ComboBoxFuntionTyp.SelectedItem == null || DateTimePicker.Value == null)
            {
                return false;
            }
            else
            {
                if ((item as Person).CheckFunctionTypesExists((FunctionType)ComboBoxFuntionTyp.SelectedItem) &&
                    (item as Person).CheckFreeTerm(DateTimePicker.Value, (FunctionType)ComboBoxFuntionTyp.SelectedItem))
                {
                    return true;
                }
                return false;
            }
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listPatient.ItemsSource).Refresh();

        }
        private void listPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPerson = ((sender as ListView).SelectedItem as Person);

            if (selectedPerson != null)
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
        }
        private bool updatePerson()
        {
            if (selectedPerson != null && validationField())
            {
                selectedPerson.Name = TextBoxName.Text;
                selectedPerson.LastName = TextBoxLastName.Text;
                selectedPerson.SSN = TextBoxSSN.Text;
                selectedPerson.Address.Street = TextBoxStreet.Text;
                selectedPerson.Address.PostNumber = TextBoxNumber.Text;
                selectedPerson.Address.City = TextBoxCity.Text;
                selectedPerson.Address.PostalCode = TextBoxPostalCode.Text;

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
                Person person = new Person(TextBoxName.Text, TextBoxLastName.Text, TextBoxSSN.Text, address, PersonType.Patient);
                serviceData.AddPerson(person);
                patients.Add(person);

                listPatient.Items.Refresh();
                viewPatient.Refresh();
                

                selectedPerson = null;
                listPatient.SelectedIndex = -1;
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
            listPatient.SelectedIndex = -1;
        }

        private void ButtonUpdatePerson(object sender, RoutedEventArgs e)
        {
            if (!updatePerson())
            {
                System.Windows.MessageBox.Show("Sprawdz poprawoność wpisanych danych", "Błąd");
            }
            else
            {
                viewPatient.Refresh();
            }
        }

        private void ButtonAddPerson(object sender, RoutedEventArgs e)
        {
            if (!addPerson())
            {
                System.Windows.MessageBox.Show("Sprawdz poprawoność wpisanych danych", "Błąd");
            }
        }

        private void ButtonRemovePeron(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                patients.Remove(selectedPerson);
                serviceData.DeletePerson(selectedPerson);

                clearControls();
                viewPatient.Refresh();
                selectedPerson = null;
            }
        }

        private void listEmpolyees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = ((sender as ListView).SelectedItem as Person);
        }

        private void ButtonSearch(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAdd(object sender, RoutedEventArgs e)
        {
            if (ComboBoxFuntionTyp.SelectedItem != null  && DateTimePicker.Value != null && selectedEmployee != null && selectedPerson != null)
            {
                if(selectedEmployee.CheckFreeTerm(DateTimePicker.Value, (FunctionType)ComboBoxFuntionTyp.SelectedItem))
                {
                    selectedEmployee.addCalendarItem(selectedPerson, (FunctionType)ComboBoxFuntionTyp.SelectedItem, DateTimePicker.Value);

                    CollectionViewSource.GetDefaultView(listEmpolyees.ItemsSource).Refresh();
                }
            
            }            
        }

        private void loadingEvent(object sender, RoutedEventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;

            if(picker != null) picker.Value = DateTime.Now;
        }

        private void LoadedFuntionType(object sender, RoutedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if(cb != null)
            {
                ComboBoxFuntionTyp.ItemsSource = Enum.GetValues(typeof(FunctionType)).Cast<FunctionType>();
            }
        }


        private void ComboBoxFuntionTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listEmpolyees.ItemsSource).Refresh();
        }

        private void DateTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CollectionViewSource.GetDefaultView(listEmpolyees.ItemsSource).Refresh();
        }

        private void loadingvent(object sender, RoutedEventArgs e)
        {

        }
    }
}
