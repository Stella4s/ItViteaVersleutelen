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
using System.Security.Cryptography;

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
            blockMsg.Text = "Password set";
            blockMsg.ToolTip = null;
        }

        private void Btn_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (password == null || password.Length <= 0)
                blockMsg.Text = "No password selected.";
            else
            {
                plaintext = txtInput.Text;
                blockMsg.Text = "Encryption Succesful";
                blockMsg.ToolTip = null;
                try
                {
                    txtOutput.Text = Cipher.Encrypt(plaintext, password);
                }
                catch (CryptographicException ex)
                {
                    blockMsg.Text = "Invalid Password.";
                    blockMsg.ToolTip = String.Format("CryptographicException: {0}", ex.Message.ToString());
                }
                catch (FormatException ex)
                {
                    blockMsg.Text = "Invalid input. Text isn't in base64.";
                    blockMsg.ToolTip = String.Format("FormatException: {0}", ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    blockMsg.Text = "Unexpected error.";
                    blockMsg.ToolTip = String.Format("Unknown Exception: {0}", ex.Message.ToString());
                }
            }
          
        }

        private void Btn_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (password == null || password.Length <= 0)
                blockMsg.Text = "No password selected.";
            else
            {
                txtEncrypted = txtInput.Text;
                try
                {
                    txtOutput.Text = Cipher.Decrypt(txtEncrypted, password);
                    blockMsg.Text = "Decryption Succesful";
                    blockMsg.ToolTip = null;
                }
                catch (CryptographicException ex)
                {
                    blockMsg.Text = "Invalid Password.";
                    blockMsg.ToolTip = String.Format("CryptographicException: {0}", ex.Message.ToString());
                }
                catch (FormatException ex)
                {
                    blockMsg.Text = "Invalid input. Text isn't in base64.";
                    blockMsg.ToolTip = String.Format("FormatException: {0}", ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    blockMsg.Text = "Unexpected error.";
                    blockMsg.ToolTip = String.Format("Unknown Exception: {0}", ex.Message.ToString());
                }
                
            }
        }
    }
}

