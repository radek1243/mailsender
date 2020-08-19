using System;
using System.Windows;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace MailSender
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog FileDialog { set; get; }
        private String Sender { get; }
        private String Pass;
        private String SMTP_Server { get; }
        private int Port { get; }
        private SmtpClient SMTP_Client;
        private MailMessage Message;

        public MainWindow()
        {
            InitializeComponent();
            this.FileDialog = new OpenFileDialog();
            this.Sender = 
            this.Pass = 
            this.SMTP_Server = 
            this.Port = 587;
            this.SMTP_Client = new SmtpClient(this.SMTP_Server, this.Port);
            this.SMTP_Client.Credentials = new NetworkCredential(this.Sender, this.Pass);
            this.Message = new MailMessage();
        }

        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            this.FileDialog.ShowDialog();
            this.lblFile.Content = this.FileDialog.SafeFileName;
        }
        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.FileDialog.FileName=="")
                {
                    System.Windows.MessageBox.Show("Nie wybrano pliku do wysłania!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (this.txtBoxEmail.Text=="")
                {
                    System.Windows.MessageBox.Show("Nie podaneo adresu email do wysłania!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else {
                    this.Message.Attachments.Add(new Attachment(this.FileDialog.FileName));
                    this.Message.From = new MailAddress(this.Sender);
                    this.Message.To.Add(this.txtBoxEmail.Text.Trim());
                    this.Message.Subject = "Plik";
                    this.SMTP_Client.Send(this.Message);
                    System.Windows.MessageBox.Show("Wysłano email", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("Błąd: "+ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
