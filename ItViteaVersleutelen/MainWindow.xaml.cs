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
using System.IO;
using Microsoft.Win32;

namespace ItViteaVersleutelen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string password, plaintext, txtEncrypted;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_SafeFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtOutput.Text);
        }

        private void Btn_SetPassword_Click(object sender, RoutedEventArgs e)
        {
            password = txtPassword.Text;
            lableMsg.Content = "Password set";
        }

        private void Btn_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (password == null || password.Length <= 0)
                lableMsg.Content = "No password selected.";
            else
            {
                plaintext = txtInput.Text;
                txtOutput.Text = Cipher.Encrypt(plaintext, password);
            }
       
        }

        private void Btn_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (password == null || password.Length <= 0)
                lableMsg.Content = "No password selected.";
            else
            {
                txtEncrypted = txtInput.Text;
                txtOutput.Text = Cipher.Decrypt(txtEncrypted, password);
            }
        }
    }
}

