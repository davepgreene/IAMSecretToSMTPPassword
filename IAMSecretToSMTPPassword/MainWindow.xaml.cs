using System;
using System.Security.Cryptography;
using System.Windows;

namespace IAMSecretToSMTPPassword
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.IAMKey.Text))
            {
                MessageBoxResult result = MessageBox.Show("You must enter an IAM key.");
                return;
            }
            //key = AWS Secret Access Key;
            string IAMKey = this.IAMKey.Text;

            //message = "SendRawEmail";
            string message = "SendRawEmail";
            
            //versionInBytes = 0x02;
            byte versionInBytes = 0x02;
            
            //signatureInBytes = HmacSha256(message, key);
            byte[] signatureInBytes = HashHMAC(message, IAMKey);

            //signatureAndVer = Concatenate(versionInBytes, signatureInBytes);
            byte[] signatureAndVer = new byte[signatureInBytes.Length + 1];
            signatureInBytes.CopyTo(signatureAndVer, 1);
            signatureAndVer[0] = versionInBytes;
           
            //smtpPassword = Base64(signatureAndVer);
            string smtpPassword = Convert.ToBase64String(signatureAndVer);

            this.SMTPPass.Text = smtpPassword;
        }

        private byte[] HashHMAC(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return hashmessage;
            }
        }
    }
}
