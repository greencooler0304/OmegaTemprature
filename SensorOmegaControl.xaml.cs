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

            tb1A.Text = sensorOmegaTemp.Device1.TitleA;
            tb2A.Text = sensorOmegaTemp.Device2.TitleA;
            tb3A.Text = sensorOmegaTemp.Device3.TitleA;
            tb4A.Text = sensorOmegaTemp.Device4.TitleA;
            tb5A.Text = sensorOmegaTemp.Device5.TitleA;

            tb1B.Text = sensorOmegaTemp.Device1.TitleB;
            tb2B.Text = sensorOmegaTemp.Device2.TitleB;
            tb3B.Text = sensorOmegaTemp.Device3.TitleB;
            tb4B.Text = sensorOmegaTemp.Device4.TitleB;
            tb5B.Text = sensorOmegaTemp.Device5.TitleB;
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
        [XmlAttribute] public string TitleA { get; set; }
        [XmlAttribute] public string CorrectionA { get; set; }
        [XmlAttribute] public string PlusA { get; set; }
        [XmlAttribute] public string TitleB { get; set; }
        [XmlAttribute] public string CorrectionB { get; set; }
        [XmlAttribute] public string PlusB { get; set; }
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
            public string titleA;
            public string correctionA; 
            public string plusA;
            public string titleB;
            public string correctionB;
            public string plusB;
        }

        OmegaDevice omgDev1, omgDev2, omgDev3, omgDev4, omgDev5;

        static List<OmegaDevice> arrOmegaDevices = new List<OmegaDevice>();

        const int RECVTIMEOUT = 60 * 1000; 


        public void onXmlLoaded()
        {
            OmegaDevice omgDevTemp;

            omgDevTemp.Name = Device1.Name;
            omgDevTemp.IpAddress = Device1.Address;
            omgDevTemp.titleA = Device1.TitleA;
            omgDevTemp.correctionA = Device1.CorrectionA;
            omgDevTemp.plusA = Device1.PlusA;
            omgDevTemp.titleB = Device1.TitleB;
            omgDevTemp.correctionB = Device1.CorrectionB;
            omgDevTemp.plusB = Device1.PlusB;

            arrOmegaDevices.Add(omgDevTemp);

            omgDevTemp.Name = Device2.Name;
            omgDevTemp.IpAddress = Device2.Address;
            omgDevTemp.titleA = Device2.TitleA;
            omgDevTemp.correctionA = Device2.CorrectionA;
            omgDevTemp.plusA = Device2.PlusA;
            omgDevTemp.titleB = Device2.TitleB;
            omgDevTemp.correctionB = Device2.CorrectionB;
            omgDevTemp.plusB = Device2.PlusB;

            arrOmegaDevices.Add(omgDevTemp);

            omgDevTemp.Name = Device3.Name;
            omgDevTemp.IpAddress = Device3.Address;
            omgDevTemp.titleA = Device3.TitleA;
            omgDevTemp.correctionA = Device3.CorrectionA;
            omgDevTemp.plusA = Device3.PlusA;
            omgDevTemp.titleB = Device3.TitleB;
            omgDevTemp.correctionB = Device3.CorrectionB;
            omgDevTemp.plusB = Device3.PlusB;

            arrOmegaDevices.Add(omgDevTemp);

            omgDevTemp.Name = Device4.Name;
            omgDevTemp.IpAddress = Device4.Address;
            omgDevTemp.titleA = Device4.TitleA;
            omgDevTemp.correctionA = Device4.CorrectionA;
            omgDevTemp.plusA = Device4.PlusA;
            omgDevTemp.titleB = Device4.TitleB;
            omgDevTemp.correctionB = Device4.CorrectionB;
            omgDevTemp.plusB = Device4.PlusB;

            arrOmegaDevices.Add(omgDevTemp);

            omgDevTemp.Name = Device5.Name;
            omgDevTemp.IpAddress = Device5.Address;
            omgDevTemp.titleA = Device5.TitleA;
            omgDevTemp.correctionA = Device5.CorrectionA;
            omgDevTemp.plusA = Device5.PlusA;
            omgDevTemp.titleB = Device5.TitleB;
            omgDevTemp.correctionB = Device5.CorrectionB;
            omgDevTemp.plusB = Device5.PlusB;

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
        DateTime last_start_delay_time1A;
        DateTime last_start_delay_time2A;
        DateTime last_start_delay_time3A;
        DateTime last_start_delay_time4A;
        DateTime last_start_delay_time5A;

        DateTime last_start_delay_time1B;
        DateTime last_start_delay_time2B;
        DateTime last_start_delay_time3B;
        DateTime last_start_delay_time4B;
        DateTime last_start_delay_time5B;

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

        string _Temprature1A = "0";
        string _Temprature2A = "0";
        string _Temprature3A = "0";
        string _Temprature4A = "0";
        string _Temprature5A = "0";

        string _Temprature1B = "0";
        string _Temprature2B = "0";
        string _Temprature3B = "0";
        string _Temprature4B = "0";
        string _Temprature5B = "0";

        [XmlIgnore]
        public string Temprature1A
        {
            get { return _Temprature1A; }
            set
            {
                if (_Temprature1A != value)
                {
                    _Temprature1A = value;
                    OnPropertyChange("Temprature1A");
                }
            }
        }

        public string Temprature2A
        {
            get { return _Temprature2A; }
            set
            {
                if (_Temprature2A != value)
                {
                    _Temprature2A = value;
                    OnPropertyChange("Temprature2A");
                }
            }
        }
        public string Temprature3A
        {
            get { return _Temprature3A; }
            set
            {
                if (_Temprature3A != value)
                {
                    _Temprature3A = value;
                    OnPropertyChange("Temprature3A");
                }
            }
        }
        public string Temprature4A
        {
            get { return _Temprature4A; }
            set
            {
                if (_Temprature4A != value)
                {
                    _Temprature4A = value;
                    OnPropertyChange("Temprature4A");
                }
            }
        }
        public string Temprature5A
        {
            get { return _Temprature5A; }
            set
            {
                if (_Temprature5A != value)
                {
                    _Temprature5A = value;
                    OnPropertyChange("Temprature5A");
                }
            }
        }

        public string Temprature1B
        {
            get { return _Temprature1B; }
            set
            {
                if (_Temprature1B != value)
                {
                    _Temprature1B = value;
                    OnPropertyChange("Temprature1B");
                }
            }
        }

        public string Temprature2B
        {
            get { return _Temprature2B; }
            set
            {
                if (_Temprature2B != value)
                {
                    _Temprature2B = value;
                    OnPropertyChange("Temprature2B");
                }
            }
        }
        public string Temprature3B
        {
            get { return _Temprature3B; }
            set
            {
                if (_Temprature3B != value)
                {
                    _Temprature3B = value;
                    OnPropertyChange("Temprature3B");
                }
            }
        }
        public string Temprature4B
        {
            get { return _Temprature4B; }
            set
            {
                if (_Temprature4B != value)
                {
                    _Temprature4B = value;
                    OnPropertyChange("Temprature4B");
                }
            }
        }
        public string Temprature5B
        {
            get { return _Temprature5B; }
            set
            {
                if (_Temprature5B != value)
                {
                    _Temprature5B = value;
                    OnPropertyChange("Temprature5B");
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
        public static bool messageReceived1A = false;
        public static bool messageReceived2A = false;
        public static bool messageReceived3A = false;
        public static bool messageReceived4A = false;
        public static bool messageReceived5A = false;

        public static bool messageReceived1B = false;
        public static bool messageReceived2B = false;
        public static bool messageReceived3B = false;
        public static bool messageReceived4B = false;
        public static bool messageReceived5B = false;

        protected int CheckMessage(string sData, IPEndPoint e, ref string sVal)
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
                if(sData.Substring(0,2) == "TA" || sData.Substring(0, 2) == "TB")
                {
                    //string sTemp = sData.Substring(2, 2);
                    string sTemp = ""; 

                   for(int j = 2; j < sData.Length; j++)
                    {
                        if (sData[j] != 'C')
                        {
                            sTemp += sData[j];
                        }
                        else
                            break; 
                    }

                    float f = 0;

                    // 숫자인지 아닌지 판단
                    result = float.TryParse(sTemp, out f);

                    if (result)
                    {
                        arrDevices.DataGetTime = DateTime.Now;
                        sVal = sTemp; 
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
                    return -1; 
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

                string sVal = ""; 

                int checkResult = CheckMessage(receiveString, e, ref sVal);

                IPAddress DataAddress = e.Address;
                string sAddress = DataAddress.ToString();

                double orgTemp = 0;
                double correctTemp = 0;
                double resultTemp = 0;


                switch (checkResult)
                {
                    case 0:
                        //string sTempValue = receiveString.Substring(2, 5);
                        //string sTempValue = sVal;
                        string sSensorKind = receiveString.Substring(0, 2);

                        float f = 0;

                        bool result = float.TryParse(sVal, out f);

                        if(result && f < 3000)
                        {
                            orgTemp = Convert.ToDouble(sVal);

                            if (sAddress == arrOmegaDevices[0].IpAddress)
                            {
                                if (sSensorKind == "TA")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[0].correctionA);

                                    if (arrOmegaDevices[0].plusA == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature1A = resultTemp.ToString();

                                    Logger.save1A(resultTemp.ToString());
                                    last_start_delay_time1A = DateTime.Now;
                                    messageReceived1A = true;
                                }
                                else if (sSensorKind == "TB")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[0].correctionB);

                                    if (arrOmegaDevices[0].plusB == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature1B = resultTemp.ToString();

                                    Logger.save1B(resultTemp.ToString());
                                    last_start_delay_time1B = DateTime.Now;
                                    messageReceived1B = true;
                                }
                            }
                            else if (sAddress == arrOmegaDevices[1].IpAddress)
                            {
                                if (sSensorKind == "TA")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[1].correctionA);

                                    if (arrOmegaDevices[1].plusA == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature2A = resultTemp.ToString();

                                    Logger.save2A(resultTemp.ToString());
                                    last_start_delay_time2A = DateTime.Now;
                                    messageReceived2A = true;
                                }
                                else if (sSensorKind == "TB")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[1].correctionB);

                                    if (arrOmegaDevices[1].plusB == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature2B = resultTemp.ToString();

                                    Logger.save2B(resultTemp.ToString());
                                    last_start_delay_time2B = DateTime.Now;
                                    messageReceived2B = true;
                                }
                            }
                            else if (sAddress == arrOmegaDevices[2].IpAddress)
                            {
                                if (sSensorKind == "TA")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[2].correctionA);

                                    if (arrOmegaDevices[2].plusA == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature3A = resultTemp.ToString().Substring(0, 5);

                                    Logger.save3A(resultTemp.ToString().Substring(0, 5));
                                    last_start_delay_time3A = DateTime.Now;
                                    messageReceived3A = true;
                                }
                                else if (sSensorKind == "TB")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[2].correctionB);

                                    if (arrOmegaDevices[1].plusB == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature3B = resultTemp.ToString().Substring(0, 5);

                                    Logger.save3B(resultTemp.ToString().Substring(0, 5));
                                    last_start_delay_time3B = DateTime.Now;
                                    messageReceived3B = true;
                                }
                            }
                            else if (sAddress == arrOmegaDevices[3].IpAddress)
                            {
                                if (sSensorKind == "TA")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[3].correctionA);

                                    if (arrOmegaDevices[3].plusA == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature4A = resultTemp.ToString();

                                    Logger.save4A(resultTemp.ToString());
                                    last_start_delay_time4A = DateTime.Now;
                                    messageReceived4A = true;
                                }
                                else if (sSensorKind == "TB")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[3].correctionB);

                                    if (arrOmegaDevices[3].plusB == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature4B = resultTemp.ToString();

                                    Logger.save4B(resultTemp.ToString());
                                    last_start_delay_time4B = DateTime.Now;
                                    messageReceived4B = true;
                                }
                            }
                            else if (sAddress == arrOmegaDevices[4].IpAddress )
                            {
                                if (sSensorKind == "TA")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[4].correctionA);

                                    if (arrOmegaDevices[4].plusA == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature5A = resultTemp.ToString();

                                    Logger.save5A(resultTemp.ToString());
                                    last_start_delay_time5A = DateTime.Now;
                                    messageReceived5A = true;
                                }
                                else if (sSensorKind == "TB")
                                {
                                    correctTemp = Convert.ToDouble(arrOmegaDevices[4].correctionB);

                                    if (arrOmegaDevices[4].plusB == "true") resultTemp = orgTemp + correctTemp;
                                    else resultTemp = orgTemp - correctTemp;

                                    Temprature5B = resultTemp.ToString();

                                    Logger.save5B(resultTemp.ToString());
                                    last_start_delay_time5B = DateTime.Now;
                                    messageReceived5B = true;
                                }
                            }
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
            else
            {
                u.EndReceive(ar, ref e);
            }

            messageReceived = true;

            //Console.WriteLine($"Received: {receiveString}");
        }

        //static IPEndPoint e = new IPEndPoint(IPAddress.Any, 2000);
        //static UdpClient u = new UdpClient(e);
        //static UdpState s = new UdpState();

        static IPEndPoint e;
        static UdpClient u;
        static UdpState s;

        public void ReceiveMessages()
        {
            // Receive Time out 10 seconds
            var timeToWait = TimeSpan.FromSeconds(10);

            s.e = e;
            s.u = u;

            if (thread_run)
            {
                messageReceived = false;
                messageReceived1A = false;
                messageReceived2A = false;
                messageReceived3A = false;
                messageReceived4A = false;
                messageReceived5A = false;

                messageReceived1B = false;
                messageReceived2B = false;
                messageReceived3B = false;
                messageReceived4B = false;
                messageReceived5B = false;

                //Console.WriteLine("listening for messages");
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

                // Do some work while we wait for a message. For this example, we'll just sleep
                //while (!messageReceived)
                {
                    
                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time1A).TotalMilliseconds) & !messageReceived1A)
                    {
                        PrintLog("Omega", "Device1 TA Stop");
                        Logger.save1A("Stop");
                        last_start_delay_time1A = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time2A).TotalMilliseconds) & !messageReceived2A)
                    {
                        PrintLog("Omega", "Device2 TA Stop");
                        Logger.save2A("Stop");
                        last_start_delay_time2A = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time3A).TotalMilliseconds) & !messageReceived3A)
                    {
                        PrintLog("Omega", "Device3 TA Stop");
                        Logger.save3A("Stop");
                        last_start_delay_time3A = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time4A).TotalMilliseconds) & !messageReceived4A)
                    {
                        PrintLog("Omega", "Device4 TA Stop");
                        Logger.save4A("Stop");
                        last_start_delay_time4A = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time5A).TotalMilliseconds) & !messageReceived5A)
                    {
                        PrintLog("Omega", "Device5 TA Stop");
                        Logger.save5A("Stop");
                        last_start_delay_time5A = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time1B).TotalMilliseconds) & !messageReceived1B)
                    {
                        PrintLog("Omega", "Device1 TB Stop");
                        Logger.save1B("Stop");
                        last_start_delay_time1B = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time2B).TotalMilliseconds) & !messageReceived2B)
                    {
                        PrintLog("Omega", "Device2 TB Stop");
                        Logger.save2B("Stop");
                        last_start_delay_time2B = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time3B).TotalMilliseconds) & !messageReceived3B)
                    {
                        PrintLog("Omega", "Device3 TB Stop");
                        Logger.save3B("Stop");
                        last_start_delay_time3B = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time4B).TotalMilliseconds) & !messageReceived4B)
                    {
                        PrintLog("Omega", "Device4 TB Stop");
                        Logger.save4B("Stop");
                        last_start_delay_time4B = DateTime.Now;
                    }

                    if ((RECVTIMEOUT < DateTime.Now.Subtract(last_start_delay_time5B).TotalMilliseconds) & !messageReceived5B)
                    {
                        PrintLog("Omega", "Device5 TB Stop");
                        Logger.save5B("Stop");
                        last_start_delay_time5B = DateTime.Now;
                    }

                    Thread.Sleep(100);
                }
            }
        }

        public override void init()
        {
            e = new IPEndPoint(IPAddress.Any, 2000);
            u = new UdpClient(e);
            s = new UdpState();

            base.init();
        }
        public override void terminate()
        {
            thread_run = false;

            Logger.finish(); 

            u.Close(); 

            // xml 저장 
            //Thread.CurrentThread.Join();
            //Thread.CurrentThread.Interrupt();

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


            last_start_delay_time1A = DateTime.Now;
            last_start_delay_time2A = DateTime.Now;
            last_start_delay_time3A = DateTime.Now;
            last_start_delay_time4A = DateTime.Now;
            last_start_delay_time5A = DateTime.Now;

            last_start_delay_time1B = DateTime.Now;
            last_start_delay_time2B = DateTime.Now;
            last_start_delay_time3B = DateTime.Now;
            last_start_delay_time4B = DateTime.Now;
            last_start_delay_time5B = DateTime.Now;

            Logger.save1A("start");
            Logger.save2A("start");
            Logger.save3A("start");
            Logger.save4A("start");
            Logger.save5A("start");

            Logger.save1B("start");
            Logger.save2B("start");
            Logger.save3B("start");
            Logger.save4B("start");
            Logger.save5B("start");

            //SaveIPAddress(Address,Int32.Parse(PortNum));

            thread_run = true;
            
            state = State.Ready; 

            Temprature1A = "0";
            Temprature2A = "0";
            Temprature3A = "0";
            Temprature4A = "0";
            Temprature5A = "0";

            Temprature1B = "0";
            Temprature2B = "0";
            Temprature3B = "0";
            Temprature4B = "0";
            Temprature5B = "0";

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
                    Thread.CurrentThread.Join();
                }

            })).Start();


            base.start();
        }
        public override void stop(bool byError = false)
        {
            thread_run = false;
            state = State.Stop;

            Logger.info("AP stop");

            Logger.save1A("AP stop");
            Logger.save2A("AP stop");
            Logger.save3A("AP stop");
            Logger.save4A("AP stop");
            Logger.save5A("AP stop");

            Logger.save1B("AP stop");
            Logger.save2B("AP stop");
            Logger.save3B("AP stop");
            Logger.save4B("AP stop");
            Logger.save5B("AP stop");

            base.stop(byError);
        }
    }
}
