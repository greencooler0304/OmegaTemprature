using OmegaTempCollector.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace OmegaTempCollector.View
{
    /// <summary>
    /// SetConfig.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SetConfig : Window
    {
        SensorOmegaTemp sensorOmegaTemp;

        public SetConfig()
        {
            InitializeComponent();
            loadModule();

            // A 센서
            Title1ATextBox.Text = arrOmegaDevices[0].TitleA;
            Address1TextBox.Text = arrOmegaDevices[0].IpAddress;
            Correct1ATextBox.Text = arrOmegaDevices[0].correctionA;

            if (arrOmegaDevices[0].plusA == "true") Plus1AListBox.SelectedIndex = 0; 
            else                                         Plus1AListBox.SelectedIndex = 1;

            Title2ATextBox.Text = arrOmegaDevices[1].TitleA;
            Address2TextBox.Text = arrOmegaDevices[1].IpAddress;
            Correct2ATextBox.Text = arrOmegaDevices[1].correctionA;

            if (arrOmegaDevices[1].plusA == "true") Plus2AListBox.SelectedIndex = 0;
            else                                         Plus2AListBox.SelectedIndex = 1;

            Title3ATextBox.Text = arrOmegaDevices[2].TitleA;
            Address3TextBox.Text = arrOmegaDevices[2].IpAddress;
            Correct3ATextBox.Text = arrOmegaDevices[2].correctionA;

            if (arrOmegaDevices[2].plusA == "true") Plus3AListBox.SelectedIndex = 0;
            else Plus3AListBox.SelectedIndex = 1;

            Title4ATextBox.Text = arrOmegaDevices[3].TitleA;
            Address4TextBox.Text = arrOmegaDevices[3].IpAddress;
            Correct4ATextBox.Text = arrOmegaDevices[3].correctionA;

            if (arrOmegaDevices[3].plusA == "true") Plus4AListBox.SelectedIndex = 0;
            else                                         Plus4AListBox.SelectedIndex = 1;

            Title5ATextBox.Text = arrOmegaDevices[4].TitleA;
            Address5TextBox.Text = arrOmegaDevices[4].IpAddress;
            Correct5ATextBox.Text = arrOmegaDevices[4].correctionA;

            if (arrOmegaDevices[4].plusA == "true") Plus5AListBox.SelectedIndex = 0;
            else Plus5AListBox.SelectedIndex = 1;

            // B 센서

            Title1BTextBox.Text = arrOmegaDevices[0].TitleB;
            Correct1BTextBox.Text = arrOmegaDevices[0].correctionB;

            if (arrOmegaDevices[0].plusB == "true") Plus1BListBox.SelectedIndex = 0;
            else Plus1BListBox.SelectedIndex = 1;

            Title2BTextBox.Text = arrOmegaDevices[1].TitleB;
            Correct2BTextBox.Text = arrOmegaDevices[1].correctionB;

            if (arrOmegaDevices[1].plusB == "true") Plus2BListBox.SelectedIndex = 0;
            else Plus2BListBox.SelectedIndex = 1;

            Title3BTextBox.Text = arrOmegaDevices[2].TitleB;
            Correct3BTextBox.Text = arrOmegaDevices[2].correctionB;

            if (arrOmegaDevices[2].plusB == "true") Plus3BListBox.SelectedIndex = 0;
            else Plus3BListBox.SelectedIndex = 1;

            Title4BTextBox.Text = arrOmegaDevices[3].TitleB;
            Correct4BTextBox.Text = arrOmegaDevices[3].correctionB;

            if (arrOmegaDevices[3].plusB == "true") Plus4BListBox.SelectedIndex = 0;
            else Plus4BListBox.SelectedIndex = 1;

            Title5BTextBox.Text = arrOmegaDevices[4].TitleB;
            Correct5BTextBox.Text = arrOmegaDevices[4].correctionB;

            if (arrOmegaDevices[4].plusB == "true") Plus5BListBox.SelectedIndex = 0;
            else Plus5BListBox.SelectedIndex = 1;

        }

        void loadModule()
        {
            sensorOmegaTemp = Xml.load<SensorOmegaTemp>("./config.xml");
            if (sensorOmegaTemp == null)
            {
                sensorOmegaTemp = new SensorOmegaTemp();
                //SaveModule();
                //sensorOmegaTemp.onXmlLoaded();
            }
        }

        void SaveModule()
        {
            Xml.save("./config.xml", sensorOmegaTemp);
        }

        public class Device
        {
            [XmlAttribute] public string Name { get; set; }
            [XmlAttribute] public string Address { get; set; }
            [XmlAttribute] public string TitleA { get; set; }
            [XmlAttribute] public string TitleB { get; set; }
            [XmlAttribute] public string CorrectionA { get; set; }
            [XmlAttribute] public string CorrectionB { get; set; }
            [XmlAttribute] public string PlusA { get; set; }
            [XmlAttribute] public string PlusB { get; set; }
        }

        public struct OmegaDevice
        {
            public string IpAddress;
            public string Name;
            public string TitleA;
            public string TitleB;
            public string correctionA;
            public string correctionB;
            public string plusA;
            public string plusB;
        }

        static List<OmegaDevice> arrOmegaDevices = new List<OmegaDevice>();

        public class SensorOmegaTemp : Xml.ITarget
        {
            [XmlElement] public Device Device1 { get; set; }
            [XmlElement] public Device Device2 { get; set; }
            [XmlElement] public Device Device3 { get; set; }
            [XmlElement] public Device Device4 { get; set; }
            [XmlElement] public Device Device5 { get; set; }

            public void onXmlLoaded()
            {
                OmegaDevice omgDevTemp;

                omgDevTemp.Name = Device1.Name;
                omgDevTemp.TitleA = Device1.TitleA;
                omgDevTemp.TitleB = Device1.TitleB;
                omgDevTemp.IpAddress = Device1.Address;
                omgDevTemp.correctionA = Device1.CorrectionA;
                omgDevTemp.correctionB = Device1.CorrectionB;
                omgDevTemp.plusA = Device1.PlusA;
                omgDevTemp.plusB = Device1.PlusB;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device2.Name;
                omgDevTemp.TitleA = Device2.TitleA;
                omgDevTemp.TitleB = Device2.TitleB;
                omgDevTemp.IpAddress = Device2.Address;
                omgDevTemp.correctionA = Device2.CorrectionA;
                omgDevTemp.correctionB = Device2.CorrectionB;
                omgDevTemp.plusA = Device2.PlusA;
                omgDevTemp.plusB = Device2.PlusB;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device3.Name;
                omgDevTemp.TitleA = Device3.TitleA;
                omgDevTemp.TitleB = Device3.TitleB;
                omgDevTemp.IpAddress = Device3.Address;
                omgDevTemp.correctionA = Device3.CorrectionA;
                omgDevTemp.correctionB = Device3.CorrectionB;
                omgDevTemp.plusA = Device3.PlusA;
                omgDevTemp.plusB = Device3.PlusB;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device4.Name;
                omgDevTemp.TitleA = Device4.TitleA;
                omgDevTemp.TitleB = Device4.TitleB;
                omgDevTemp.IpAddress = Device4.Address;
                omgDevTemp.correctionA = Device4.CorrectionA;
                omgDevTemp.correctionB = Device4.CorrectionB;
                omgDevTemp.plusA = Device4.PlusA;
                omgDevTemp.plusB = Device4.PlusB;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device5.Name;
                omgDevTemp.TitleA = Device5.TitleA;
                omgDevTemp.TitleB = Device5.TitleB;
                omgDevTemp.IpAddress = Device5.Address;
                omgDevTemp.correctionA = Device5.CorrectionA;
                omgDevTemp.correctionB = Device5.CorrectionB;
                omgDevTemp.plusA = Device5.PlusA;
                omgDevTemp.plusB = Device5.PlusB;

                arrOmegaDevices.Add(omgDevTemp);

            }

            public void onXmlSaveing()
            {
                //Xml.save("./config.xml", this);
            }

        }


        private void Title1ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          sensorOmegaTemp.Device1.TitleA = Title1ATextBox.Text; 
        }

        private void Title2ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.TitleA = Title2ATextBox.Text;
        }
        private void Title3ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.TitleA = Title3ATextBox.Text;
        }
        private void Title4ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.TitleA = Title4ATextBox.Text;
        }
        private void Title5ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.TitleA = Title5ATextBox.Text;
        }
        private void Title1BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device1.TitleB = Title1BTextBox.Text;
        }

        private void Title2BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.TitleB = Title2BTextBox.Text;
        }
        private void Title3BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.TitleB = Title3BTextBox.Text;
        }
        private void Title4BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.TitleB = Title4BTextBox.Text;
        }
        private void Title5BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.TitleB = Title5BTextBox.Text;
        }

        private void Address1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device1.Address = Address1TextBox.Text;
        }

        private void Address2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.Address = Address2TextBox.Text;
        }
        private void Address3TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.Address = Address3TextBox.Text;
        }
        private void Address4TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.Address = Address4TextBox.Text;
        }
        private void Address5TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.Address = Address5TextBox.Text;
        }

        private void Correct1ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device1.CorrectionA = Correct1ATextBox.Text;
        }

        private void Correct2ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.CorrectionA = Correct2ATextBox.Text;
        }
        private void Correct3ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.CorrectionA = Correct3ATextBox.Text;
        }
        private void Correct4ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.CorrectionA = Correct4ATextBox.Text;
        }
        private void Correct5ATextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.CorrectionA = Correct5ATextBox.Text;
        }

        private void Correct1BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device1.CorrectionB = Correct1BTextBox.Text;
        }

        private void Correct2BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.CorrectionB = Correct2BTextBox.Text;
        }
        private void Correct3BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.CorrectionB = Correct3BTextBox.Text;
        }
        private void Correct4BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.CorrectionB = Correct4BTextBox.Text;
        }
        private void Correct5BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.CorrectionB = Correct5BTextBox.Text;
        }

        private void Plus1AListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sensorOmegaTemp != null)
            {
                if (Plus1AListBox.SelectedIndex == 0) sensorOmegaTemp.Device1.PlusA = "true";
                else sensorOmegaTemp.Device1.PlusA = "false";
            }
        }

        private void Plus2AListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus2AListBox.SelectedIndex == 0) sensorOmegaTemp.Device1.PlusA = "true";
                else sensorOmegaTemp.Device2.PlusA = "false";
            }
        }
        private void Plus3AListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus3AListBox.SelectedIndex == 0) sensorOmegaTemp.Device3.PlusA = "true";
                else sensorOmegaTemp.Device3.PlusA = "false";
            }
        }
        private void Plus4AListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus4AListBox.SelectedIndex == 0) sensorOmegaTemp.Device4.PlusA = "true";
                else sensorOmegaTemp.Device4.PlusA = "false";
            }
        }
        private void Plus5AListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus5AListBox.SelectedIndex == 0) sensorOmegaTemp.Device5.PlusA = "true";
                else sensorOmegaTemp.Device5.PlusA = "false";
            }
        }

        private void Plus1BListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus1BListBox.SelectedIndex == 0) sensorOmegaTemp.Device1.PlusB = "true";
                else sensorOmegaTemp.Device1.PlusB = "false";
            }
        }

        private void Plus2BListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus2BListBox.SelectedIndex == 0) sensorOmegaTemp.Device1.PlusB = "true";
                else sensorOmegaTemp.Device2.PlusB = "false";
            }
        }
        private void Plus3BListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus3BListBox.SelectedIndex == 0) sensorOmegaTemp.Device3.PlusB = "true";
                else sensorOmegaTemp.Device3.PlusB = "false";
            }
        }
        private void Plus4BListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus4BListBox.SelectedIndex == 0) sensorOmegaTemp.Device4.PlusB = "true";
                else sensorOmegaTemp.Device4.PlusB = "false";
            }
        }
        private void Plus5BListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sensorOmegaTemp != null)
            {
                if (Plus5BListBox.SelectedIndex == 0) sensorOmegaTemp.Device5.PlusB = "true";
                else sensorOmegaTemp.Device5.PlusB = "false";
            }
        }

        private void onSaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Dialog box accepted
            SaveModule(); 
            DialogResult = true;
        }

        private void onCancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Dialog box accepted
            DialogResult = false;
        }

    }


}
