using OmegaTempCollector.Common;
using OmegaTempCollector.Server;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OmegaTempCollector.Control
{
    /// <summary>
    /// SensorMoistureControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SensorOmegaControl : UserControl
    {
        static SensorOmegaTemp sensorOmegaTemp;

        public SensorOmegaControl()
        {
            InitializeComponent();
            loadModule();

            tb1.Text = sensorOmegaTemp.Device1.Title;
            tb2.Text = sensorOmegaTemp.Device2.Title;
            tb3.Text = sensorOmegaTemp.Device3.Title;
            tb4.Text = sensorOmegaTemp.Device4.Title;
            tb5.Text = sensorOmegaTemp.Device5.Title;
        }

        void loadModule()
        {
            sensorOmegaTemp = Xml.load<SensorOmegaTemp>("./config.xml");
            if (sensorOmegaTemp == null)
            {
                sensorOmegaTemp = new SensorOmegaTemp();
                SaveModule();
                sensorOmegaTemp.onXmlLoaded();
            }
        }

        void SaveModule()
        {
            Xml.save("./config.xml", sensorOmegaTemp);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SensorOmegaTemp sensor = this.DataContext as SensorOmegaTemp;
            if (sensor != null)
            {
                sensor.OnLog += Li_OnLog;
                //sensor.Finshed += terminate;
            }
        }

        private void Li_OnLog(object sender, BaseModel.LogEventArg arg)
        {
            lsLog.print(sender, arg);
        }

        private void terminate(object sender, EventArgs arg)
        {
         //   SaveModule(); 
        }
    }

    public class Device
    {
        [XmlAttribute] public string Name { get; set; }
        [XmlAttribute] public string Address { get; set; }
        [XmlAttribute] public string Title { get; set; }
        [XmlAttribute] public string Correction { get; set; }
        [XmlAttribute] public string Plus { get; set; }

    }

    public class SensorOmegaTemp : BaseModel, Xml.ITarget
    {
        [XmlElement] public Device Device1 { get; set; }
        [XmlElement] public Device Device2 { get; set; }
        [XmlElement] public Device Device3 { get; set; }
        [XmlElement] public Device Device4 { get; set; }
        [XmlElement] public Device Device5 { get; set; }


        public struct OmegaDevice
        {
            public string IpAddress;
            public string Name;
            public string title;
            public string correction; 
            public string plus; 
        }

        OmegaDevice omgDev1, omgDev2, omgDev3, omgDev4, omgDev5;

        static List<OmegaDevice> arrOmegaDevices = new List<OmegaDevice>();

        const int RECVTIMEOUT = 60 * 1000; 


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

        enum State
        {
            None, Ready, SendReceive, CheckError, Break, Stop, Finshed, Error
        }

        State state = State.None;

        [XmlIgnore] public TCPClient UdpServer = null;

        [XmlAttribute] public string TemptureValue { get; set; }

        DateTime run_time;
        DateTime last_start_delay_time1;
        DateTime last_start_delay_time2;
        DateTime last_start_delay_time3;
        DateTime last_start_delay_time4;
        DateTime last_start_delay_time5;

        public class _Service : Service
        {
            SensorOmegaTemp parent;
            public _Service(SensorOmegaTemp parent, string name) : base(name)
            {
                this.parent = parent;
            }

            [XmlIgnore] public TCPServer server = null;

            public override int OnReceive(byte[] recBuffer)
            {
                Logger?.info("Received : " + Utils.ByteToStringForLog(recBuffer));

                //float value = 0;
                //if (float.TryParse(Utils.ByteToString(recBuffer), out value))
                //    parent.Temprature1 = value;

                DoSend(recBuffer);
                return recBuffer.Length;

            }

        }
        [XmlIgnore] public _Service service = null;

        string _Temprature1 = "0";
        string _Temprature2 = "0";
        string _Temprature3 = "0";
        string _Temprature4 = "0";
        string _Temprature5 = "0";

        [XmlIgnore]
        public string Temprature1
        {
            get { return _Temprature1; }
            set
            {
                if (_Temprature1 != value)
                {
                    _Temprature1 = value;
                    OnPropertyChange("Temprature1");
                }
            }
        }

        public string Temprature2
        {
            get { return _Temprature2; }
            set
            {
                if (_Temprature2 != value)
                {
                    _Temprature2 = value;
                    OnPropertyChange("Temprature2");
                }
            }
        }
        public string Temprature3
        {
            get { return _Temprature3; }
            set
            {
                if (_Temprature3 != value)
                {
                    _Temprature3 = value;
                    OnPropertyChange("Temprature3");
                }
            }
        }
        public string Temprature4
        {
            get { return _Temprature4; }
            set
            {
                if (_Temprature4 != value)
                {
                    _Temprature4 = value;
                    OnPropertyChange("Temprature4");
                }
            }
        }
        public string Temprature5
        {
            get { return _Temprature5; }
            set
            {
                if (_Temprature5 != value)
                {
                    _Temprature5 = value;
                    OnPropertyChange("Temprature5");
                }
            }
        }

        public SensorOmegaTemp()
        {
            //string sName = Device1.Name; 

            Logger.SetlogfileName("Device1", "Device2", "Device3", "Device4", "Device5");
            Logger.start();
        }

        int getRunSec()
        {
            return (int)(DateTime.Now - run_time).TotalSeconds;
        }

        public struct UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }

        public struct UdpTimeouts
        {
            public string IPAddress; 
            public DateTime DataGetTime; 
        }

        UdpTimeouts arrDevices;

        public static bool messageReceived = false;
        public static bool messageReceived1 = false;
        public static bool messageReceived2 = false;
        public static bool messageReceived3 = false;
        public static bool messageReceived4 = false;
        public static bool messageReceived5 = false;


        protected int CheckMessage(string sData, IPEndPoint e)
        {
            IPAddress DataAddress = e.Address;
            string sAddress = DataAddress.ToString();

            bool bMatch = true;
            bool result = false;
            int iResult = 0;

            //if(arrDevices.IPAddress == sAddress)
            //{
            //    bMatch = true;
            //}

            if (bMatch)
            {
                string sTemp = sData.Substring(2, 2);

                int i = 0;
                
                // 숫자인지 아닌지 판단
                result = int.TryParse(sTemp, out i);

                if(result)
                {
                    arrDevices.DataGetTime = DateTime.Now; 
                    iResult = 0; 
                }
                else
                {
                    // 온도값 이외의 데이터 수신
                    iResult = 2;
                }
            }
            else 
            {
                // 지정된 Device 외 수신 Data 
                iResult = 1; 
            }

            return iResult; 
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = ((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = ((UdpState)(ar.AsyncState)).e;

            //if(CheckMessage(receiveString, e) && thread_run)
            if (thread_run)
            {
                byte[] receiveBytes = u.EndReceive(ar, ref e);
                string receiveString = Encoding.ASCII.GetString(receiveBytes);

                int checkResult = CheckMessage(receiveString, e);

                IPAddress DataAddress = e.Address;
                string sAddress = DataAddress.ToString();

                double orgTemp = 0;
                double correctTemp = 0;
                double resultTemp = 0;


                switch (checkResult)
                {
                    case 0:
                        string sTempValue = receiveString.Substring(2, 5);
                        //Logger.info(sTempValue);
                        orgTemp = Convert.ToDouble(sTempValue);

                        if (sAddress == arrOmegaDevices[0].IpAddress)
                        {
                            correctTemp = Convert.ToDouble(arrOmegaDevices[0].correction);

                            if (arrOmegaDevices[0].plus == "true")    resultTemp = orgTemp + correctTemp; 
                            else                                      resultTemp = orgTemp - correctTemp;

                            Temprature1 = resultTemp.ToString();

                            Logger.save1(resultTemp.ToString());
                            last_start_delay_time1 = DateTime.Now;
                            messageReceived1 = true; 

                        }
                        else if (sAddress == arrOmegaDevices[1].IpAddress)
                        {
                            correctTemp = Convert.ToDouble(arrOmegaDevices[1].correction);

                            if (arrOmegaDevices[1].plus == "true") resultTemp = orgTemp + correctTemp;
                            else resultTemp = orgTemp - correctTemp;

                            Temprature2 = resultTemp.ToString().Substring(0,5);

                            Logger.save2(resultTemp.ToString().Substring(0, 5));
                            last_start_delay_time2 = DateTime.Now;
                            messageReceived2 = true;
                        }
                        else if (sAddress == arrOmegaDevices[2].IpAddress)
                        {
                            correctTemp = Convert.ToDouble(arrOmegaDevices[2].correction);

                            if (arrOmegaDevices[2].plus == "true") resultTemp = orgTemp + correctTemp;
                            else resultTemp = orgTemp - correctTemp;

                            Temprature3 = resultTemp.ToString();

                            Logger.save3(resultTemp.ToString());
                            last_start_delay_time3 = DateTime.Now;
                            messageReceived3 = true;

                        }
                        else if (sAddress == arrOmegaDevices[3].IpAddress)
                        {
                            correctTemp = Convert.ToDouble(arrOmegaDevices[3].correction);

                            if (arrOmegaDevices[3].plus == "true") resultTemp = orgTemp + correctTemp;
                            else resultTemp = orgTemp - correctTemp;

                            Temprature4 = resultTemp.ToString();

                            Logger.save3(resultTemp.ToString());
                            last_start_delay_time4 = DateTime.Now;
                            messageReceived4 = true;

                        }
                        else if (sAddress == arrOmegaDevices[4].IpAddress)
                        {
                            correctTemp = Convert.ToDouble(arrOmegaDevices[4].correction);

                            if (arrOmegaDevices[4].plus == "true") resultTemp = orgTemp + correctTemp;
                            else resultTemp = orgTemp - correctTemp;

                            Temprature5 = resultTemp.ToString();

                            Logger.save5(resultTemp.ToString());
                            last_start_delay_time5 = DateTime.Now;
                            messageReceived5 = true;

                        }
                        break;
                    case 1:
                        //Logger.save("Other Device");
                        Logger.info("Other Device");
                        break;
                    case 2:
                        //Logger.save("No Temp Data");
                        Logger.info("No Temp Data");
                        break;
                    default:
                        break;

                }

                string sDisplay = "From " + sAddress + " : " + receiveString; 

                PrintLog("Omega", sDisplay);


                //last_start_delay_time = DateTime.Now;
            }


            messageReceived = true;

            //Console.WriteLine($"Received: {receiveString}");
        }

        static IPEndPoint e = new IPEndPoint(IPAddress.Any, 2000);
        static UdpClient u = new UdpClient(e);
        static UdpState s = new UdpState();

        public void ReceiveMessages()
        {
            // Receive Time out 10 seconds
            var timeToWait = TimeSpan.FromSeconds(10);

            s.e = e;
            s.u = u;

            if (thread_run)
            {
                messageReceived = false;
                messageReceived1 = false;
                messageReceived2 = false;
                messageReceived3 = false;
                messageReceived4 = false;
                messageReceived5 = false;

                //Console.WriteLine("listening for messages");
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

                // Do some work while we wait for a message. For this example, we'll just sleep
                //while (!messageReceived)
                {
                    
                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time1).TotalMilliseconds) & !messageReceived1)
                    {
                        PrintLog("Omega", "Device1 Stop");
                        Logger.save1("Stop");
                        last_start_delay_time1 = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time2).TotalMilliseconds) & !messageReceived2)
                    {
                        PrintLog("Omega", "Device2 Stop");
                        Logger.save2("Stop");
                        last_start_delay_time2 = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time3).TotalMilliseconds) & !messageReceived3)
                    {
                        PrintLog("Omega", "Device3 Stop");
                        Logger.save3("Stop");
                        last_start_delay_time3 = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time4).TotalMilliseconds) & !messageReceived4)
                    {
                        PrintLog("Omega", "Device4 Stop");
                        Logger.save4("Stop");
                        last_start_delay_time4 = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time5).TotalMilliseconds) & !messageReceived5)
                    {
                        PrintLog("Omega", "Device5 Stop");
                        Logger.save5("Stop");
                        last_start_delay_time5 = DateTime.Now;
                    }

                    Thread.Sleep(100);

                }
            }
        }

        public override void init()
        {
            base.init();
        }
        public override void terminate()
        {
            thread_run = false;

            // xml 저장 
            //Thread.CurrentThread.Join();
            Thread.CurrentThread.Interrupt();

            base.terminate();


        }

        public void SaveIPAddress(string IP, int PortNum)
        {
            arrDevices.IPAddress = IP;
            arrDevices.DataGetTime = DateTime.Now;

        }

        public override void start()
        {
            Device1 = new Device();
            Device2 = new Device();
            Device3 = new Device();
            Device4 = new Device();
            Device5 = new Device();


            last_start_delay_time1 = DateTime.Now;
            last_start_delay_time2 = DateTime.Now;
            last_start_delay_time3 = DateTime.Now;
            last_start_delay_time4 = DateTime.Now;
            last_start_delay_time5 = DateTime.Now;

            Logger.save1("start");
            Logger.save2("start");
            Logger.save3("start");
            Logger.save4("start");
            Logger.save5("start");

            //SaveIPAddress(Address,Int32.Parse(PortNum));

            thread_run = true;
            
            state = State.Ready; 

            Temprature1 = "0";
            Temprature2 = "0";
            Temprature3 = "0";
            Temprature4 = "0";
            Temprature5 = "0";

            (new Thread(() =>
            {
                DateTime start_t = DateTime.Now;
                long old_log_Time = 0;

                while (thread_run)
                {
                    try
                    {
                        ReceiveMessages();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.Print(ex.ToString());
                    }

                    Thread.Sleep(1000 * 5);        // 30초 마다 온도값 수신(open,Na,temp 각각 10초 소요) 
                }

                if(!thread_run)
                {
                    PrintLog("Omega", "Stop");
                    Logger.info("Stop");
                }

            })).Start();


            base.start();
        }
        public override void stop(bool byError = false)
        {
            thread_run = false;
            state = State.Stop;

            


            Logger.info("AP stop");

            Logger.save1("AP stop");
            Logger.save2("AP stop");
            Logger.save3("AP stop");
            Logger.save4("AP stop");
            Logger.save5("AP stop");

            base.stop(byError);
        }
    }
}
