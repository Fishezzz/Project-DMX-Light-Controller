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
    /// Interaction logic for Led_Panel.xaml
    /// </summary>
    public partial class Led_Panel : Window
    {
        MainWindow mainWindow;

        byte[] data = new byte[513];
        int startAdress = 301;
        double red, green, blue = 0;
        double c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31, c32, c33, c34, c35, c36, c37, c38, c39, c40, c41, c42, c43, c44, c45, c46, c47, c48 = 0;
        //Panel_Led led1 = new Panel_Led(); Panel_Led led2 = new Panel_Led(); Panel_Led led3 = new Panel_Led(); Panel_Led led4 = new Panel_Led(); Panel_Led led5 = new Panel_Led(); Panel_Led led6 = new Panel_Led(); Panel_Led led7 = new Panel_Led(); Panel_Led led8 = new Panel_Led(); Panel_Led led9 = new Panel_Led(); Panel_Led led10 = new Panel_Led(); Panel_Led led11 = new Panel_Led(); Panel_Led led12 = new Panel_Led(); Panel_Led led13 = new Panel_Led(); Panel_Led led14 = new Panel_Led(); Panel_Led led15 = new Panel_Led(); Panel_Led led16 = new Panel_Led();

        public Led_Panel(MainWindow pMainWindow)
        {
            InitializeComponent();
            mainWindow = pMainWindow;

            cbxPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxPorts.Items.Add(s);

            //led1.NummerLed = 1; led2.NummerLed = 2;  led3.NummerLed = 3; led4.NummerLed = 4; led5.NummerLed = 5; led6.NummerLed = 6; led7.NummerLed = 7; led8.NummerLed = 8; led9.NummerLed = 9; led10.NummerLed = 10; led11.NummerLed = 11; led12.NummerLed = 12; led13.NummerLed = 13; led14.NummerLed = 14; led15.NummerLed = 15; led16.NummerLed = 16;
            lblLed1.Content = lblLed2.Content = lblLed3.Content = lblLed4.Content = lblLed5.Content = lblLed6.Content = lblLed7.Content = lblLed8.Content = lblLed9.Content = lblLed10.Content = lblLed11.Content = lblLed12.Content = lblLed13.Content = lblLed14.Content = lblLed15.Content = lblLed16.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue);
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

                    sldrRed.IsEnabled = true; tbxRed.IsEnabled = true;
                    sldrGreen.IsEnabled = true; tbxGreen.IsEnabled = true;
                    sldrBlue.IsEnabled = true; tbxBlue.IsEnabled = true;

                    tbxStartAdress.IsEnabled = true; btnApply.IsEnabled = true;
                    btnSelectAll.IsEnabled = true; btnUnselectAll.IsEnabled = true;

                    cbxLed1.IsEnabled = true; cbxLed2.IsEnabled = true;
                    cbxLed3.IsEnabled = true; cbxLed4.IsEnabled = true;
                    cbxLed5.IsEnabled = true; cbxLed6.IsEnabled = true;
                    cbxLed7.IsEnabled = true; cbxLed8.IsEnabled = true;
                    cbxLed9.IsEnabled = true; cbxLed10.IsEnabled = true;
                    cbxLed11.IsEnabled = true; cbxLed12.IsEnabled = true;
                    cbxLed13.IsEnabled = true; cbxLed14.IsEnabled = true;
                    cbxLed15.IsEnabled = true; cbxLed16.IsEnabled = true;
                }
                else
                {
                    mainWindow.UpdateSerialPortName("None");

                    // COM-poort is niet te gebruiken
                    sldrRed.IsEnabled = false; tbxRed.IsEnabled = false;
                    sldrGreen.IsEnabled = false; tbxGreen.IsEnabled = false;
                    sldrBlue.IsEnabled = false; tbxBlue.IsEnabled = false;

                    tbxStartAdress.IsEnabled = false; btnApply.IsEnabled = false;
                    btnSelectAll.IsEnabled = false; btnUnselectAll.IsEnabled = false;

                    cbxLed1.IsEnabled = false; cbxLed2.IsEnabled = false;
                    cbxLed3.IsEnabled = false; cbxLed4.IsEnabled = false;
                    cbxLed5.IsEnabled = false; cbxLed6.IsEnabled = false;
                    cbxLed7.IsEnabled = false; cbxLed8.IsEnabled = false;
                    cbxLed9.IsEnabled = false; cbxLed10.IsEnabled = false;
                    cbxLed11.IsEnabled = false; cbxLed12.IsEnabled = false;
                    cbxLed13.IsEnabled = false; cbxLed14.IsEnabled = false;
                    cbxLed15.IsEnabled = false; cbxLed16.IsEnabled = false;
                }
            }
        }

        private void sldrValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateChannelsValue();
        }

        private void cbxChecked(object sender, RoutedEventArgs e)
        {
            UpdateChannelsValue();
        }

        private void cbxUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateChannelsValue();
        }

        private void UpdateChannelsValue()
        {
            red = sldrRed.Value;
            green = sldrGreen.Value;
            blue = sldrBlue.Value;

            tbxRed.Text = red.ToString();
            tbxGreen.Text = green.ToString();
            tbxBlue.Text = blue.ToString();

            if (cbxLed1.IsChecked == true)
            { c1 = red; c2 = green; c3 = blue; lblLed1.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed2.IsChecked == true)
            { c4 = red; c5 = green; c6 = blue; lblLed2.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed3.IsChecked == true)
            { c7 = red; c8 = green; c9 = blue; lblLed3.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed4.IsChecked == true)
            { c10 = red; c11 = green; c12 = blue; lblLed4.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); ; }

            if (cbxLed5.IsChecked == true)
            { c13 = red; c14 = green; c15 = blue; lblLed5.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed6.IsChecked == true)
            { c16 = red; c17 = green; c18 = blue; lblLed6.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed7.IsChecked == true)
            { c19 = red; c20 = green; c21 = blue; lblLed7.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed8.IsChecked == true)
            { c22 = red; c23 = green; c24 = blue; lblLed8.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed9.IsChecked == true)
            { c25 = red; c26 = green; c27 = blue; lblLed9.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed10.IsChecked == true)
            { c28 = red; c29 = green; c30 = blue; lblLed10.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed11.IsChecked == true)
            { c31 = red; c32 = green; c33 = blue; lblLed11.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed12.IsChecked == true)
            { c34 = red; c35 = green; c36 = blue; lblLed12.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed13.IsChecked == true)
            { c37 = red; c38 = green; c39 = blue; lblLed13.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed14.IsChecked == true)
            { c40 = red; c41 = green; c42 = blue; lblLed14.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed15.IsChecked == true)
            { c43 = red; c44 = green; c45 = blue; lblLed15.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            if (cbxLed16.IsChecked == true)
            { c46 = red; c47 = green; c48 = blue; lblLed16.Content = string.Format("R : {0}\nG : {1}\nB : {2}", red, green, blue); }

            mainWindow.data[startAdress + 0] = Convert.ToByte(c1);
            mainWindow.data[startAdress + 1] = Convert.ToByte(c2);
            mainWindow.data[startAdress + 2] = Convert.ToByte(c3);
            mainWindow.data[startAdress + 3] = Convert.ToByte(c4);
            mainWindow.data[startAdress + 4] = Convert.ToByte(c5);
            mainWindow.data[startAdress + 5] = Convert.ToByte(c6);
            mainWindow.data[startAdress + 6] = Convert.ToByte(c7);
            mainWindow.data[startAdress + 7] = Convert.ToByte(c8);
            mainWindow.data[startAdress + 8] = Convert.ToByte(c9);
            mainWindow.data[startAdress + 9] = Convert.ToByte(c10);
            mainWindow.data[startAdress + 10] = Convert.ToByte(c11);
            mainWindow.data[startAdress + 11] = Convert.ToByte(c12);
            mainWindow.data[startAdress + 12] = Convert.ToByte(c13);
            mainWindow.data[startAdress + 13] = Convert.ToByte(c14);
            mainWindow.data[startAdress + 14] = Convert.ToByte(c15);
            mainWindow.data[startAdress + 15] = Convert.ToByte(c16);
            mainWindow.data[startAdress + 16] = Convert.ToByte(c17);
            mainWindow.data[startAdress + 17] = Convert.ToByte(c18);
            mainWindow.data[startAdress + 18] = Convert.ToByte(c19);
            mainWindow.data[startAdress + 19] = Convert.ToByte(c20);
            mainWindow.data[startAdress + 20] = Convert.ToByte(c21);
            mainWindow.data[startAdress + 21] = Convert.ToByte(c22);
            mainWindow.data[startAdress + 22] = Convert.ToByte(c23);
            mainWindow.data[startAdress + 23] = Convert.ToByte(c24);
            mainWindow.data[startAdress + 24] = Convert.ToByte(c25);
            mainWindow.data[startAdress + 25] = Convert.ToByte(c26);
            mainWindow.data[startAdress + 26] = Convert.ToByte(c27);
            mainWindow.data[startAdress + 27] = Convert.ToByte(c28);
            mainWindow.data[startAdress + 28] = Convert.ToByte(c29);
            mainWindow.data[startAdress + 29] = Convert.ToByte(c30);
            mainWindow.data[startAdress + 30] = Convert.ToByte(c31);
            mainWindow.data[startAdress + 31] = Convert.ToByte(c32);
            mainWindow.data[startAdress + 32] = Convert.ToByte(c33);
            mainWindow.data[startAdress + 33] = Convert.ToByte(c34);
            mainWindow.data[startAdress + 34] = Convert.ToByte(c35);
            mainWindow.data[startAdress + 35] = Convert.ToByte(c36);
            mainWindow.data[startAdress + 36] = Convert.ToByte(c37);
            mainWindow.data[startAdress + 37] = Convert.ToByte(c38);
            mainWindow.data[startAdress + 38] = Convert.ToByte(c39);
            mainWindow.data[startAdress + 39] = Convert.ToByte(c40);
            mainWindow.data[startAdress + 40] = Convert.ToByte(c41);
            mainWindow.data[startAdress + 41] = Convert.ToByte(c42);
            mainWindow.data[startAdress + 42] = Convert.ToByte(c43);
            mainWindow.data[startAdress + 43] = Convert.ToByte(c44);
            mainWindow.data[startAdress + 44] = Convert.ToByte(c45);
            mainWindow.data[startAdress + 45] = Convert.ToByte(c46);
            mainWindow.data[startAdress + 46] = Convert.ToByte(c47);
            mainWindow.data[startAdress + 47] = Convert.ToByte(c48);
        }

        private void tbxRed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    red = Convert.ToDouble(tbxRed.Text);
                    if (red >= 0 && red <= 255)
                        sldrRed.Value = red;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }
        }

        private void tbxGreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    green = Convert.ToDouble(tbxGreen.Text);
                    if (green >= 0 && green <= 255)
                        sldrGreen.Value = green;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }
        }

        private void tbxBlue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    blue = Convert.ToDouble(tbxBlue.Text);
                    if (blue >= 0 && blue <= 255)
                        sldrBlue.Value = blue;
                    else
                        MessageBox.Show("Waarde moet tussen 0 en 255 zijn.", "Fout!");
                }
                catch (Exception)
                { MessageBox.Show("Er is een fout in de input.", "Fout!"); }
            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            cbxLed1.IsChecked = cbxLed2.IsChecked = cbxLed3.IsChecked = cbxLed4.IsChecked = cbxLed5.IsChecked = cbxLed6.IsChecked = cbxLed7.IsChecked = cbxLed8.IsChecked = cbxLed9.IsChecked = cbxLed10.IsChecked = cbxLed11.IsChecked = cbxLed12.IsChecked = cbxLed13.IsChecked = cbxLed14.IsChecked = cbxLed15.IsChecked = cbxLed16.IsChecked = true;
        }

        private void btnUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            cbxLed1.IsChecked = cbxLed2.IsChecked = cbxLed3.IsChecked = cbxLed4.IsChecked = cbxLed5.IsChecked = cbxLed6.IsChecked = cbxLed7.IsChecked = cbxLed8.IsChecked = cbxLed9.IsChecked = cbxLed10.IsChecked = cbxLed11.IsChecked = cbxLed12.IsChecked = cbxLed13.IsChecked = cbxLed14.IsChecked = cbxLed15.IsChecked = cbxLed16.IsChecked = false;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(tbxStartAdress.Text);
                if (a >= 0 && a <= 513)
                {
                    startAdress = a + 1;
                    //led1.StartAdress = led2.StartAdress = led3.StartAdress = led4.StartAdress = led5.StartAdress = led6.StartAdress = led7.StartAdress = led8.StartAdress = led9.StartAdress = led10.StartAdress = led11.StartAdress = led12.StartAdress = led13.StartAdress = led14.StartAdress = led15.StartAdress = led16.StartAdress = a;
                    gbStartAdress.Header = string.Format("Start Adress: {0}", startAdress - 1);
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
