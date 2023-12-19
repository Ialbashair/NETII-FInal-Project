using LogicLayer;
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
using System.Windows.Shapes;
using DataObjects;
using System.Xml.Schema;

namespace NETIIFinalProject
{
    /// <summary>
    /// Interaction logic for SellPhone.xaml
    /// </summary>
    public partial class SellPhone : Window
    {
        PhoneManager _phoneManager;
        CustomerManager _customerManager;
        EmployeeManager _employeeManager;
        SaleManager _saleManager;
        double total = 0;
        public SellPhone(string employeeName)
        {
            _phoneManager = new PhoneManager();
            _customerManager = new CustomerManager();
            _employeeManager = new EmployeeManager();
            _saleManager = new SaleManager();
            InitializeComponent();            
            lblTotal.Content = total;          
            cboProtectionPlan.IsEnabled = false;
            cboCustomer.IsEnabled = false;
            btnRingSale.IsEnabled = false;
            lblEmployee.Content = employeeName;
        }

        private void cboPhone_Loaded(object sender, RoutedEventArgs e)
        {
            List<Phone> phones = new List<Phone>();

            try
            {
                phones = _phoneManager.GetAllPhonesUsingStatus("Ready");
                foreach (Phone phone in phones)
                {
                    cboPhone.Items.Add(phone.Model);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error Retrieving Phones", "Error");
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
           
        }

        private void partialProtection_Selected(object sender, RoutedEventArgs e)
        {
            total = total + 24.99;
            lblTotal.Content = total;
        }

        private void fullProtection_Selected(object sender, RoutedEventArgs e)
        {
            total = total + 49.99;
            lblTotal.Content = total;
        }

        private void cboPhone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string model = cboPhone.SelectedItem as string;
            double price = 0;
            total = 0;

            try
            {
                price = _phoneManager.GetPhoneUsingModel(model).Price;
            }
            catch (Exception )
            {
                MessageBox.Show("No Phone Found");
            }

            total = total + price;
            lblTotal.Content = total;

            cboCustomer.IsEnabled = true;
            cboProtectionPlan.SelectedIndex = 0;
            cboProtectionPlan.IsEnabled = true;
        }

        private void partialProtection_Unselected(object sender, RoutedEventArgs e)
        {
            total = total - 24.99;
            lblTotal.Content = total;
        }

        private void fullProtection_Unselected(object sender, RoutedEventArgs e)
        {
            total = total - 49.99;
            lblTotal.Content = total;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void cboCustomer_Loaded(object sender, RoutedEventArgs e)
        {
          
            List<Customer> customers = new List<Customer>();

            try
            {
                customers = _customerManager.GetAllCustomers();
                foreach (Customer customer in customers)
                {
                    cboCustomer.Items.Add(customer.GivenName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Retrieving Customers", "Error");
            }

        }

        private void btnRingSale_Click(object sender, RoutedEventArgs e)
        {
            string phone = cboPhone.SelectedValue.ToString();
            int phoneID = 0;
            try
            {
                phoneID = _phoneManager.GetPhoneUsingModel(phone).PhoneID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Phone ID not found" +  ex.Message, "Error");
            }
            
            string employee = lblEmployee.Content.ToString();
            int employeeID = 0;
            try
            {
                employeeID = _employeeManager.SelectEmployeeVMbyGivenName(employee).EmployeeID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("EmplyoeeID not found" + ex.Message, "Error");
            }

            string customer = cboCustomer.SelectedValue.ToString();
            int customerID = 0;
            try
            {
                customerID = _customerManager.GetCustomerByGivenName(customer).CustomerID;
            }
            catch (Exception ex)
            {

                MessageBox.Show("CustomerID not found" + ex.Message, "Error"); 
            }
            
            string dateOfSale = DateTime.Now.ToString("yyyy-dd-MM");

            string totalString = lblTotal.Content.ToString();
            double total = double.Parse(totalString);
            bool active = true;

            int sale;

            try
            {
                sale = _saleManager.addSale(phoneID, customerID, employeeID, dateOfSale, total, active);
                if (sale == 1)
                {
                    this.DialogResult = true; return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sale Could not be made" + ex.Message, "Error");
                this.DialogResult = false; return;
            }
             
            

        }

        private void cboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRingSale.IsEnabled = true;
        }
    }
}
