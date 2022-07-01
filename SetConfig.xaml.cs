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

            Title1TextBox.Text = arrOmegaDevices[0].title;
            Address1TextBox.Text = arrOmegaDevices[0].IpAddress;
            Correct1TextBox.Text = arrOmegaDevices[0].correction;

            if (arrOmegaDevices[0].plus == "true") Plus1ListBox.SelectedIndex = 0; 
            else                                         Plus1ListBox.SelectedIndex = 1;

            Title2TextBox.Text = arrOmegaDevices[1].title;
            Address2TextBox.Text = arrOmegaDevices[1].IpAddress;
            Correct2TextBox.Text = arrOmegaDevices[1].correction;

            if (arrOmegaDevices[1].plus == "true") Plus2ListBox.SelectedIndex = 0;
            else                                         Plus2ListBox.SelectedIndex = 1;

            Title3TextBox.Text = arrOmegaDevices[2].title;
            Address3TextBox.Text = arrOmegaDevices[2].IpAddress;
            Correct3TextBox.Text = arrOmegaDevices[2].correction;

            if (arrOmegaDevices[2].plus == "true") Plus3ListBox.SelectedIndex = 0;
            else Plus3ListBox.SelectedIndex = 1;

            Title4TextBox.Text = arrOmegaDevices[3].title;
            Address4TextBox.Text = arrOmegaDevices[3].IpAddress;
            Correct4TextBox.Text = arrOmegaDevices[3].correction;

            if (arrOmegaDevices[3].plus == "true") Plus4ListBox.SelectedIndex = 0;
            else                                         Plus4ListBox.SelectedIndex = 1;

            Title5TextBox.Text = arrOmegaDevices[4].title;
            Address5TextBox.Text = arrOmegaDevices[4].IpAddress;
            Correct5TextBox.Text = arrOmegaDevices[4].correction;

            if (arrOmegaDevices[4].plus == "true") Plus5ListBox.SelectedIndex = 0;
            else Plus5ListBox.SelectedIndex = 1;
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
            [XmlAttribute] public string Title { get; set; }
            [XmlAttribute] public string Correction { get; set; }
            [XmlAttribute] public string Plus { get; set; }

        }

        public struct OmegaDevice
        {
            public string IpAddress;
            public string Name;
            public string title;
            public string correction;
            public string plus;
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
                omgDevTemp.title = Device1.Title;
                omgDevTemp.IpAddress = Device1.Address;
                omgDevTemp.correction = Device1.Correction;
                omgDevTemp.plus = Device1.Plus;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device2.Name;
                omgDevTemp.title = Device2.Title;
                omgDevTemp.IpAddress = Device2.Address;
                omgDevTemp.correction = Device2.Correction;
                omgDevTemp.plus = Device2.Plus;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device3.Name;
                omgDevTemp.title = Device3.Title;
                omgDevTemp.IpAddress = Device3.Address;
                omgDevTemp.correction = Device3.Correction;
                omgDevTemp.plus = Device3.Plus;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device4.Name;
                omgDevTemp.title = Device4.Title;
                omgDevTemp.IpAddress = Device4.Address;
                omgDevTemp.correction = Device4.Correction;
                omgDevTemp.plus = Device4.Plus;

                arrOmegaDevices.Add(omgDevTemp);

                omgDevTemp.Name = Device5.Name;
                omgDevTemp.title = Device5.Title;
                omgDevTemp.IpAddress = Device5.Address;
                omgDevTemp.correction = Device5.Correction;
                omgDevTemp.plus = Device5.Plus;

                arrOmegaDevices.Add(omgDevTemp);

            }

            public void onXmlSaveing()
            {
                //Xml.save("./config.xml", this);
            }

        }


        private void Title1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          sensorOmegaTemp.Device1.Title = Title1TextBox.Text; 
        }

        private void Title2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.Title = Title2TextBox.Text;
        }
        private void Title3TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.Title = Title3TextBox.Text;
        }
        private void Title4TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.Title = Title4TextBox.Text;
        }
        private void Title5TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.Title = Title5TextBox.Text;
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

        private void Correct1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device1.Correction = Correct1TextBox.Text;
        }

        private void Correct2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device2.Correction = Correct2TextBox.Text;
        }
        private void Correct3TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device3.Correction = Correct3TextBox.Text;
        }
        private void Correct4TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device4.Correction = Correct4TextBox.Text;
        }
        private void Correct5TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sensorOmegaTemp.Device5.Correction = Correct5TextBox.Text;
        }

        private void Plus1ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sensorOmegaTemp != null)
            {
                if (Plus1ListBox.SelectedIndex == 0) sensorOmegaTemp.Device1.Plus = "true";
                else sensorOmegaTemp.Device1.Plus = "false";
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
