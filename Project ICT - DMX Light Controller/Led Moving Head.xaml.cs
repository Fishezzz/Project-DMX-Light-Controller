using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Threading;

namespace Project_ICT___DMX_Light_Controller
{
    /// <summary>
    /// Interaction logic for Led_Moving_Head.xaml
    /// </summary>
    public partial class Led_Moving_Head : Window
    {
        MainWindow mainWindow;
        public double channel1, channel2, channel3, channel4, channel5, channel6, channel7, channel8, channel9, channel10, channel11, channel12, channel13 = 0;

        public byte[] data = new byte[513];
        int startAdress = 160;

        public Led_Moving_Head(MainWindow pMainWindow)
        {
            InitializeComponent();
            mainWindow = pMainWindow;

            cbxPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxPorts.Items.Add(s);

            this.rbC10_1.IsChecked = true;
            this.rbC12_1.IsChecked = true;
            this.rbC13_1.IsChecked = true;
            this.rbC13_11.Content = "Positieve\nRegenboog";
            this.rbC13_12.Content = "Negatieve\nRegenboog";
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

                    sldrChannel1.IsEnabled = true; sldrChannel2.IsEnabled = true;
                    sldrChannel3.IsEnabled = true; sldrChannel4.IsEnabled = true;
                    sldrChannel5.IsEnabled = true; sldrChannel6.IsEnabled = true;
                    sldrChannel7.IsEnabled = true; tbxChannel7.IsEnabled = true;
                    sldrChannel8.IsEnabled = true; tbxChannel8.IsEnabled = true;
                    sldrChannel9.IsEnabled = true; tbxChannel9.IsEnabled = true;
                    lbChannel10.IsEnabled = true; sldrChannel11.IsEnabled = true;
                    lbChannel12.IsEnabled = true; lbChannel13.IsEnabled = true;
                    tbxStartAdress.IsEnabled = true; btnApply.IsEnabled = true;
                }
                else
                {
                    mainWindow.UpdateSerialPortName("None");
                    // COM-poort is niet te gebruiken
                    sldrChannel1.IsEnabled = false; sldrChannel2.IsEnabled = false;
                    sldrChannel3.IsEnabled = false; sldrChannel4.IsEnabled = false;
                    sldrChannel5.IsEnabled = false; sldrChannel6.IsEnabled = false;
                    sldrChannel7.IsEnabled = false; tbxChannel7.IsEnabled = false;
                    sldrChannel8.IsEnabled = false; tbxChannel8.IsEnabled = false;
                    sldrChannel9.IsEnabled = false; tbxChannel9.IsEnabled = false;
                    lbChannel10.IsEnabled = false; sldrChannel11.IsEnabled = false;
                    lbChannel12.IsEnabled = false; lbChannel13.IsEnabled = false;
                    tbxStartAdress.IsEnabled = false; btnApply.IsEnabled = false;
                }
            }
        }

        private void sldrValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateChannelsValue();
        }

        private void tbxChannel7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || IsFocused == false)
            {
                try
                {
                    channel7 = Convert.ToDouble(tbxChannel7.Text);
                    if (channel7 >= 0 && channel7 <= 255)
                        sldrChannel7.Value = channel7;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }

        }

        private void tbxChannel8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || IsFocused == false)
            {
                try
                {
                    channel8 = Convert.ToDouble(tbxChannel8.Text);
                    if (channel8 >= 0 && channel8 <= 255)
                        sldrChannel8.Value = channel8;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }

        }

        private void tbxChannel9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || IsFocused == false)
            {
                try
                {
                    channel9 = Convert.ToDouble(tbxChannel9.Text);
                    if (channel9 >= 0 && channel9 <= 255)
                        sldrChannel9.Value = channel9;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }

        }

        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            UpdateChannelsValue();
        }

        private void UpdateChannelsValue()
        {
            if (rbC10_1.IsChecked == true) channel10 = 4;
            if (rbC10_2.IsChecked == true) channel10 = 20;
            if (rbC10_3.IsChecked == true) channel10 = 30;
            if (rbC10_4.IsChecked == true) channel10 = 40;
            if (rbC10_5.IsChecked == true) channel10 = 60;
            if (rbC10_6.IsChecked == true) channel10 = 70;
            if (rbC10_7.IsChecked == true) channel10 = 90;
            if (rbC10_8.IsChecked == true) channel10 = 100;
            if (rbC10_9.IsChecked == true) channel10 = 110;
            if (rbC10_10.IsChecked == true) channel10 = 130;
            if (rbC10_11.IsChecked == true) channel10 = 140;
            if (rbC10_12.IsChecked == true) channel10 = 160;
            if (rbC10_13.IsChecked == true) channel10 = 180;
            if (rbC10_14.IsChecked == true) channel10 = 200;
            if (rbC10_15.IsChecked == true) channel10 = 210;
            if (rbC10_16.IsChecked == true) channel10 = 230;
            if (rbC10_17.IsChecked == true) channel10 = 250;

            if (rbC12_1.IsChecked == true) channel12 = 4;
            if (rbC12_2.IsChecked == true) channel12 = 20;
            if (rbC12_3.IsChecked == true) channel12 = 30;
            if (rbC12_4.IsChecked == true) channel12 = 50;
            if (rbC12_5.IsChecked == true) channel12 = 60;
            if (rbC12_6.IsChecked == true) channel12 = 80;
            if (rbC12_7.IsChecked == true) channel12 = 90;
            if (rbC12_8.IsChecked == true) channel12 = 110;
            if (rbC12_9.IsChecked == true) channel12 = 120;
            if (rbC12_10.IsChecked == true) channel12 = 140;
            if (rbC12_11.IsChecked == true) channel12 = 150;
            if (rbC12_12.IsChecked == true) channel12 = 170;
            if (rbC12_13.IsChecked == true) channel12 = 180;
            if (rbC12_14.IsChecked == true) channel12 = 200;
            if (rbC12_15.IsChecked == true) channel12 = 210;
            if (rbC12_16.IsChecked == true) channel12 = 230;
            if (rbC12_17.IsChecked == true) channel12 = 250;

            if (rbC13_1.IsChecked == true) channel13 = 10;
            if (rbC13_2.IsChecked == true) channel13 = 20;
            if (rbC13_3.IsChecked == true) channel13 = 30;
            if (rbC13_4.IsChecked == true) channel13 = 50;
            if (rbC13_5.IsChecked == true) channel13 = 60;
            if (rbC13_6.IsChecked == true) channel13 = 70;
            if (rbC13_7.IsChecked == true) channel13 = 80;
            if (rbC13_8.IsChecked == true) channel13 = 100;
            if (rbC13_9.IsChecked == true) channel13 = 110;
            if (rbC13_10.IsChecked == true) channel13 = 120;
            if (rbC13_11.IsChecked == true) channel13 = 160;
            if (rbC13_12.IsChecked == true) channel13 = 220;

            channel1 = sldrChannel1.Value;
            channel2 = sldrChannel2.Value;
            channel3 = sldrChannel3.Value;
            channel4 = sldrChannel4.Value;
            channel5 = sldrChannel5.Value;
            channel6 = sldrChannel6.Value;
            channel7 = sldrChannel7.Value;
            channel8 = sldrChannel8.Value;
            channel9 = sldrChannel9.Value;
            channel11 = sldrChannel11.Value;

            mainWindow.data[startAdress + 0] = Convert.ToByte(channel1);
            mainWindow.data[startAdress + 1] = Convert.ToByte(channel2);
            mainWindow.data[startAdress + 2] = Convert.ToByte(channel3);
            mainWindow.data[startAdress + 3] = Convert.ToByte(channel4);
            mainWindow.data[startAdress + 4] = Convert.ToByte(channel5);
            mainWindow.data[startAdress + 5] = Convert.ToByte(channel6);
            mainWindow.data[startAdress + 6] = Convert.ToByte(channel7);
            mainWindow.data[startAdress + 7] = Convert.ToByte(channel8);
            mainWindow.data[startAdress + 8] = Convert.ToByte(channel9);
            mainWindow.data[startAdress + 9] = Convert.ToByte(channel10);
            mainWindow.data[startAdress + 10] = Convert.ToByte(channel11);
            mainWindow.data[startAdress + 11] = Convert.ToByte(channel12);
            mainWindow.data[startAdress + 12] = Convert.ToByte(channel13);

            tbxChannel7.Text = channel7.ToString();
            tbxChannel8.Text = channel8.ToString();
            tbxChannel9.Text = channel9.ToString();

            lblPan.Content = string.Format("{0:F2}°", channel1 * 2.109375 + channel2 * 0.00827205882352941176470588235294);

            lblTilt.Content = string.Format("{0:F2}°", channel3 * 1.0546875 + channel4 * 0.00413602941176470588235294117647);

            lblPanTiltSpeed.Content = string.Format("Speed: {0:F2}%", (1 - channel5 / 255) * 100);

            if (channel6 >= 0 && channel6 <= 9)
            { lblShutter.Content = "Closed"; lblStrobe.Content = "OFF"; }
            if (channel6 >= 10 && channel6 <= 134)
            { lblShutter.Content = string.Format("{0:F2}% Open", (1 - (channel6 - 10) / 124) * 100); lblStrobe.Content = "OFF"; }
            if (channel6 >= 135 && channel6 <= 239)
            { lblShutter.Content = "Closed"; lblStrobe.Content = string.Format("Speed: {0:F2}%", ((channel6 - 135) / 104) * 100); }
            if (channel6 >= 240 && channel6 <= 255)
            { lblShutter.Content = "Open"; lblStrobe.Content = "OFF"; }

            lblLedSpeed.Content = string.Format("Speed: {0:F2}%", (1 - channel11 / 255) * 100);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mainWindow.end == false)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
