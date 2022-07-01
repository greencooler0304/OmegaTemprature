using OmegaTempCollector.Common;
using OmegaTempCollector.Control;
using OmegaTempCollector.Simulate;
using OmegaTempCollector.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OmegaTempCollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static RaonCollector raonCollector;
        static SimulateWindow simulateWindow;

        public MainWindow()
        {
            var dlg = new SetConfig();

            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {

            }

            InitializeComponent();
            loadModule();
        }

        void loadModule()
        {
            //raonCollector = Xml.load<RaonCollector>("./config.xml");
            if (raonCollector == null)
            {
                raonCollector = new RaonCollector();
                //SaveModule();

                raonCollector.onXmlLoaded();
            }
        }
        void SaveModule()
        {
            Xml.save("./config.xml", raonCollector);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.start();

            raonCollector.init();
            this.DataContext = raonCollector;
#if debug
            simulateWindow = new SimulateWindow();
            simulateWindow.collector = raonCollector;
            simulateWindow.Left = this.Left + this.Width;
            simulateWindow.Top = this.Top;
            simulateWindow.Height = this.Height;
            simulateWindow.Show();
#endif
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.DataContext = null;
            raonCollector.terminate();
            //SaveModule();

            Logger.finish();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            raonCollector.stop();
#if debug
            simulateWindow.Close();
#endif
        }
    }
  

    public class RaonCollector : BaseModel, Xml.ITarget
    {
        public Project project { get; set; } = new Project();

        public SensorOmegaTemp Temp1 { get; set; } = new SensorOmegaTemp();

        public MonServer monServer { get; set; } = new MonServer();

        public int uploadTimeMs { get; set; } = 1000;

        private System.Timers.Timer timer = null;


   
        public void onXmlLoaded()
        {
        }

        public void onXmlSaveing()
        {            
        }

        public override void init()
        {
            clearChild();

            addChild(project);
            addChild(Temp1);
            addChild(monServer);

            base.init();
        }

        public override void terminate()
        {
            base.terminate();

            clearChild();
        }


        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //monServer.upload(
            //    monServer.Pos,
            //    //temperature.Temp,
            //    //moisture.Temp1,
            //    0,
            //    0,
            //    0,
            //    0
            //    //co2.Co2ppm,
            //    //light.LightLx
            //    );
        }
        private void startTimer()
        {
            //timer = new System.Timers.Timer(uploadTimeMs);
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void stopTime()
        {
            timer?.Stop();
            timer = null;
        }

        protected override void Model_Started(object sender, EventArgs arg)
        {
            if (sender == project)
            {
                Temp1.start();
                startTimer();
            }

            base.Model_Started(sender, arg);
        }
        protected override void Model_Finshed(object sender, EventArgs arg)
        {
            if (sender == project)
            {
                stopTime();

                Temp1.stop();

                monServer.stop();
            }
            base.Model_Finshed(sender, arg);
        }
        protected override void Model_StatusChanged(object sender, StatuseEventArg arg)
        {
            base.Model_StatusChanged(sender, arg);
        }
    }
}
