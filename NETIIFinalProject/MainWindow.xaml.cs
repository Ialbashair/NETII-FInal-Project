using DataObjects;
using DateObjects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETIIFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        EmployeeManager _employeeManager = null;
        SaleManager _saleManager = null;
        EmployeeVM loggedInEmployee = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _employeeManager = new EmployeeManager();
            _saleManager = new SaleManager();
            txtEmail.Focus();
            btnLogin.IsDefault = true;            
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = "";
        }

        private void pwdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pwdPassword.Password = "";
        }

        private void btnFirstLogInClick_Click(object sender, RoutedEventArgs e)
        {
            // On click, hide button and show login button and input feilds.
            btnFirstLogInClick.Visibility = Visibility.Hidden;
            btnLogin.Visibility = Visibility.Visible;
            txtEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;    

            // need to add labels to the input fields. Take off default values if labels are added.
            txtEmail.Focus();
            btnLogin.IsDefault = true; // test this!

        }     

        private void updateUIfForLogOut()
        {
            lblGreeting.Content = "You are not currently logged in.";
            statMessage.Content = "Welcomem, please log in to continue.";

            // clear login section

            txtEmail.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;


            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;

            loggedInEmployee = null;

            
            btnRingSales.Visibility = Visibility.Visible;
            datRental.Visibility = Visibility.Hidden;
            lblRecentSales.Visibility = Visibility.Hidden;
            calender.Visibility = Visibility.Visible;
            btnShowAll.Visibility = Visibility.Hidden;
        }
        

        private void updateUIForEmployee()
        {
            
            lblGreeting.Content = "Welcome, " + loggedInEmployee.GivenName + ". Your are logged in.";
            statMessage.Content = "Logged in on " + DateTime.Now.ToLongDateString() +
                " at " + DateTime.Now.ToShortTimeString() +
                ". Please rememeber to log out before leaving your workstation!";

            // clear login section
            txtEmail.Text = "";
            txtEmail.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;

            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;

            if (loggedInEmployee.GivenName.Equals("Joanne"))
            {
                btnShowAll.Visibility = Visibility.Visible;
                lblRecentSales.Content = "Recent Sales - Double tap to edit sale";
            }

            btnRingSales.Visibility = Visibility.Visible;
            datRental.Visibility = Visibility.Visible;
            lblRecentSales.Visibility = Visibility.Visible;

            calender.Visibility = Visibility.Hidden;
            

          

        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log In")
            {
                var email = txtEmail.Text;
                var password = pwdPassword.Password;

                // error checks - needs to be done
                if (!email.IsValidEmail())
                {
                    MessageBox.Show("That is not a valid email address", "Invalid Email",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmail.SelectAll();
                    txtEmail.Focus();
                    return;
                }
                if (!password.IsValidPassword())
                {
                    MessageBox.Show("Please enter a valid password", "Invalid password",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    pwdPassword.SelectAll();
                    pwdPassword.Focus();
                    return;
                }

                try
                {
                    loggedInEmployee = _employeeManager.LoginEmployee(email, password);

                    // we need to check for first login as newuser
                    if (pwdPassword.Password.ToString() == "newuser")
                    {
                        try
                        {
                            var passwordWindow = new PasswordChangeWindow(loggedInEmployee.Email);
                            var result = passwordWindow.ShowDialog();
                            if (result == true)
                            {
                                MessageBox.Show("Password updated.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                                updateUIForEmployee();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Password Not Changed.\n" +
                                    " You must change your password to continue.", "Logging Out",
                                    MessageBoxButton.OK, MessageBoxImage.Error);

                                updateUIfForLogOut();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                                "Update Failed",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        updateUIForEmployee();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    // you may never throw exceptions from the presentation layer
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Login Failed",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    pwdPassword.Clear();
                    txtEmail.Clear();
                    txtEmail.Focus();
                    return;
                }
            }
            else if (btnLogin.Content.ToString() == "Log Out")
            {
                string password = pwdPassword.Password;
                string email = txtEmail.Text;

                updateUIfForLogOut();

                // keep last login info in feilds - fix this
                pwdPassword.Password = password;
                txtEmail.Text = email;

                txtEmail.Focus();

            }
        }


        private void mnuChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInEmployee == null)
            {
                MessageBox.Show("You must be logged in to update your password",
                    "Login Required", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                try
                {
                    var passwordWindow = new PasswordChangeWindow(loggedInEmployee.Email);
                    var result = passwordWindow.ShowDialog();
                    if (result == true)
                    {
                        MessageBox.Show("Password updated.", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Password Not Changed.", "Opeation Aborted",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Update Failed",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRingSales_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInEmployee == null)
            {
                MessageBox.Show("You must be logged in before ringing sales!", "Please Log In");
            }
            else
            {
                try
                {
                    var ringSaleWindow = new SellPhone(loggedInEmployee.GivenName);
                    var result = ringSaleWindow.ShowDialog();
                    if (result == true)
                    {
                        MessageBox.Show("Sale Success!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        updatedatRental();
                    }
                    else
                    {
                        MessageBox.Show("Unsuccessful Sale.", "Opeation Aborted",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }

        private void datRental_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void updatedatRental()
        {
            List<Sale> sales = new List<Sale>();
            try
            {
                sales = _saleManager.getActiveSales();
                datRental.ItemsSource = sales;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed loading sales", "Error");
            }
        }
        private void datRental_Loaded(object sender, RoutedEventArgs e)
        {
            updatedatRental();
        }

        private void lblDate_Loaded(object sender, RoutedEventArgs e)
        {
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            lblDate.Content = date.ToString().Split()[0];
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (btnShowAll.Content.Equals("Show Inactive Sales"))
            {
                List<Sale> sales = new List<Sale>();
                try
                {
                    sales = _saleManager.getAllSales();
                    datRental.ItemsSource = sales;
                }
                catch (Exception ex)
                {

                    throw new ApplicationException("Sales Not found", ex);
                }
                
                btnShowAll.Content = "Hide Inactive Sales";
            }
            else 
            {
                updatedatRental();
                btnShowAll.Content = "Show Inactive Sales";
            }
        }

        private void datRental_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (loggedInEmployee.GivenName.Equals("Joanne"))
            {

                if (datRental.SelectedItems.Count != 0)
                {
                    var sale = datRental.SelectedItem as Sale;
                    int saleID = sale.saleID;
                    var editWindow = new EditPhone(loggedInEmployee.GivenName, saleID);
                    editWindow.ShowDialog();
                    if (editWindow.DialogResult == true)
                    {
                        MessageBox.Show("Update Complete", "Success");

                        if (btnShowAll.Content.Equals("Show Inactive Sales"))
                        {
                            updatedatRental();
                           
                        }
                        else
                        {
                            List<Sale> sales = new List<Sale>();
                            try
                            {
                                sales = _saleManager.getAllSales();
                                datRental.ItemsSource = sales;
                            }
                            catch (Exception ex)
                            {

                                throw new ApplicationException("Sales Not found", ex);
                            }
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Update Failed", "Failed");
                    }

                }
            }
            else 
            {
                MessageBox.Show("You're account can not preform this action", "Access Denied");
            }
        }
    }
}
