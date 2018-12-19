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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project_ICT___DMX_Light_Controller
{
    /// <summary>
    /// Interaction logic for Led_Spot.xaml
    /// </summary>
    public partial class Led_Spot : Window
    {
        MainWindow mainWindow;

        byte[] data = new byte[513];
        int startAdress = 20;
        double channel1, channel2, channel3, channel4, channel5, channel6 = 0;

        public Led_Spot(MainWindow pMainWindow)
        {
            InitializeComponent();
            mainWindow = pMainWindow;

            cbxPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxPorts.Items.Add(s);

            this.channel5 = 15; this.channel6 = 16;
            this.sldrChannel6.Minimum = 16;
        }

        public void cbxPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow.sp != null)
            {
                if (mainWindow.sp.IsOpen)
                    mainWindow.sp.Close();

                if (cbxPorts.SelectedItem.ToString() != "None")
                {
                    // COM-poort is bruikbaar
                    mainWindow.UpdateSerialPortName(cbxPorts.SelectedItem.ToString());

                    sldrChannel1.IsEnabled = true; tbxChannel1.IsEnabled = true;
                    sldrChannel2.IsEnabled = true; tbxChannel2.IsEnabled = true;
                    sldrChannel3.IsEnabled = true; tbxChannel3.IsEnabled = true;
                    sldrChannel4.IsEnabled = true;
                    sldrChannel5.IsEnabled = true;
                    sldrChannel6.IsEnabled = true;
                    tbxStartAdress.IsEnabled = true; btnApply.IsEnabled = true;
                }
                else
                {
                    mainWindow.UpdateSerialPortName("None");

                    // COM-poort is niet te gebruiken
                    sldrChannel1.IsEnabled = false; tbxChannel1.IsEnabled = false;
                    sldrChannel2.IsEnabled = false; tbxChannel2.IsEnabled = false;
                    sldrChannel3.IsEnabled = false; tbxChannel3.IsEnabled = false;
                    sldrChannel4.IsEnabled = false;
                    sldrChannel5.IsEnabled = false;
                    sldrChannel6.IsEnabled = false;
                    tbxStartAdress.IsEnabled = false; btnApply.IsEnabled = false;
                }
            }
        }

        private void tbxChannel1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || IsFocused == false)
            {
                try
                {
                    channel1 = Convert.ToDouble(tbxChannel1.Text);
                    if (channel1 >= 0 && channel1 <= 255)
                        sldrChannel1.Value = channel1;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }
        }

        private void tbxChannel2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    channel2 = Convert.ToDouble(tbxChannel2.Text);
                    if (channel2 >= 0 && channel2 <= 255)
                        sldrChannel2.Value = channel2;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }
        }

        private void tbxChannel3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    channel3 = Convert.ToDouble(tbxChannel3.Text);
                    if (channel3 >= 0 && channel3 <= 255)
                        sldrChannel3.Value = channel3;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(tbxStartAdress.Text);
                if (a >= 0 && a <= 513)
                {
                    startAdress = a;
                    gbStartAdress.Header = string.Format("Start Adress: {0}", startAdress);
                }
                else
                    MessageBox.Show("Waarde moet tussen 0 en 513 zijn.", "Fout!");
            }
            catch (Exception)
            { MessageBox.Show("Er is een fout in de input.\nEnkel volledige getallen, geen kommagetallen.\nGeen speciale tekens of letters.", "Fout!"); }
        }

        private void Channel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            channel1 = sldrChannel1.Value;
            channel2 = sldrChannel2.Value;
            channel3 = sldrChannel3.Value;
            channel4 = sldrChannel4.Value;
            channel5 = sldrChannel5.Value;
            channel6 = sldrChannel6.Value;

            mainWindow.data[startAdress + 0] = Convert.ToByte(channel1);
            mainWindow.data[startAdress + 1] = Convert.ToByte(channel2);
            mainWindow.data[startAdress + 2] = Convert.ToByte(channel3);
            mainWindow.data[startAdress + 3] = Convert.ToByte(channel4);
            mainWindow.data[startAdress + 4] = Convert.ToByte(channel5);
            mainWindow.data[startAdress + 5] = Convert.ToByte(channel6);

            tbxChannel1.Text = channel1.ToString();
            tbxChannel2.Text = channel2.ToString();
            tbxChannel3.Text = channel3.ToString();

            // Macro-label aanpassen
            if (channel4 == 0)
                lblMacro.Content = "OFF";
            else
                lblMacro.Content = "Color Combo " + Convert.ToString(channel4 / 8);

            // Strobe-label aanpassen
            if (channel5 == 0)
                lblStrobe.Content = "OFF";
            else
                lblStrobe.Content = string.Format("Strobe Speed: {0:F1}%", channel5 / 240.0 * 100.0);

            // Mode-label aanpassen
            if (channel6 == 16)
                lblMode.Content = "Color Setting";
            if (channel6 == 48)
                lblMode.Content = "Fade OUT";
            if (channel6 == 80)
                lblMode.Content = "Fade IN";
            if (channel6 == 112)
                lblMode.Content = "Fade OUT & IN";
            if (channel6 == 144)
                lblMode.Content = "Mix Color Automatic";
            if (channel6 == 176)
                lblMode.Content = "3-Color Flash";
            if (channel6 == 208)
                lblMode.Content = "7-Color Flash";
            if (channel6 == 240)
                lblMode.Content = "Sound Action";
        }

        private void Led_Spot_window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mainWindow.end == false)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
