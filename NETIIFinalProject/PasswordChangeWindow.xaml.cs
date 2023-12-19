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

namespace NETIIFinalProject
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        private string _email;
        public PasswordChangeWindow(string email)
        {
            _email = email;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManager employeeManager = new EmployeeManager();

            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string cfrmPassword = pwdConfirmPassword.Password;

            // newuser check

            if (newPassword == "newuser")
            {
                MessageBox.Show("New password can not be the same as the default password.", "Invalid Password",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdConfirmPassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            if (newPassword == oldPassword)
            {
                MessageBox.Show("You may not reuse current password.", "Invalid Password",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdConfirmPassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            if (newPassword.Length < 7 || newPassword.Length > 100)
            {
                MessageBox.Show("Password must be 7 or more characters.", "Invalid Password",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdNewPassword.Password = "";
                pwdConfirmPassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            if (cfrmPassword != newPassword)
            {
                MessageBox.Show("Confirm password does not match new password", "Password Mismatch",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
                pwdConfirmPassword.Password = "";
                pwdConfirmPassword.Focus();
                return;
            }

            try
            {
                employeeManager.ResetPassword(_email, pwdOldPassword.Password, pwdNewPassword.Password);

                this.DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsDefault = false;
            pwdOldPassword.Focus();
        }
    }
}
