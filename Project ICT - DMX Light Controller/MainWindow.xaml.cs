using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Registry;

namespace Project_ICT___DMX_Light_Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Led_Spot led_Spot;
        Led_Panel led_Panel;
        Led_Moving_Head led_Moving_Head;

        public SerialPort sp = new SerialPort();
        public byte[] data = new byte[513];
        public bool end = false;

        Account emiel = new Account();
        Account admin = new Account();

        DispatcherTimer dt;

        public MainWindow() 
        {
            InitializeComponent();
            sp.BaudRate = 250000;
            sp.StopBits = StopBits.Two;

            emiel.UserName = "emiel"; emiel.PassWord = "1234";
            admin.UserName = "admin"; admin.PassWord = "Admin";

            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(23);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            TransferData(data);
            lblDataOutput.Text = BitConverter.ToString(data);
        }

        private void ControlPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.led_Spot = new Led_Spot(this);
            this.led_Panel = new Led_Panel(this);
            this.led_Moving_Head = new Led_Moving_Head(this);
        }

        private void btnLedSpot_Click(object sender, RoutedEventArgs e)
        {
            led_Spot.Show();
        }

        private void btnLedPanel_Click(object sender, RoutedEventArgs e)
        {
            led_Panel.Show();
        }

        private void btnLedMovingHead_Click(object sender, RoutedEventArgs e)
        {
            led_Moving_Head.Show();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void pwbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            lblFoutWW.Content = "";

            if (tbxGebruiker.Text == emiel.UserName)
            {
                if (pwbCode.Password == emiel.PassWord)
                {
                    lblLedMovingHead.Visibility = lblLedPanel.Visibility = lblLedPar.Visibility = btnLedMovingHead.Visibility = btnLedPanel.Visibility = btnLedPar.Visibility = Visibility.Visible;
                    lblGebruiker.Visibility = tbxGebruiker.Visibility = lblCode.Visibility = pwbCode.Visibility = btnOk.Visibility = lblFoutWW.Visibility = Visibility.Hidden;
                    lblFoutWW.Content = "";
                }
                else
                    lblFoutWW.Content = "Wachtwoord is onjuist!";
            }
            else
            {
                lblFoutWW.Content = "Gebruikersnaam is niet gevonden!";
                if (tbxGebruiker.Text == admin.UserName)
                {
                    if (pwbCode.Password == admin.PassWord)
                    {
                        lblLedMovingHead.Visibility = lblLedPanel.Visibility = lblLedPar.Visibility = btnLedMovingHead.Visibility = btnLedPanel.Visibility = btnLedPar.Visibility = lblDataOutput.Visibility = Visibility.Visible;
                        lblGebruiker.Visibility = tbxGebruiker.Visibility = lblCode.Visibility = pwbCode.Visibility = btnOk.Visibility = lblFoutWW.Visibility = Visibility.Hidden;
                        lblFoutWW.Content = "";
                        ControlPanel.Height = 525;
                        ControlPanel.Title = ControlPanel.Title + " (Admin)";
                    }
                    else
                        lblFoutWW.Content = "Wachtwoord is onjuist!";
                }
                else
                    lblFoutWW.Content = "Gebruikersnaam niet gevonden!";
            }
        }

        public void UpdateSerialPortName(string spName)
        {
            sp.PortName = spName;
            led_Spot.cbxPorts.SelectedItem = spName;
            led_Panel.cbxPorts.SelectedItem = spName;
            led_Moving_Head.cbxPorts.SelectedItem = spName;

            if (!sp.IsOpen && sp.PortName != "None")
                sp.Open();
        }

        public void TransferData(byte[] pData)
        {
            if (sp != null && sp.IsOpen)
            {
                sp.BreakState = true;
                Thread.Sleep(1);
                sp.BreakState = false;
                Thread.Sleep(1);
                sp.Write(pData, 0, 513);
            }
        }

        private void ControlPanel_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sp.IsOpen)
                TransferData(new byte[513]);

            if (sp != null)
            {
                if (sp.IsOpen)
                    TransferData(new byte[513]);
                sp.Dispose();
            }
            end = true;
            led_Spot.Close();
            led_Panel.Close();
            led_Moving_Head.Close();
        }
    }
}
