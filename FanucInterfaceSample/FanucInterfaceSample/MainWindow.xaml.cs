using System;
using System.Collections.Generic;
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
using FanucRobot;

namespace FanucInterfaceSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        FanucRobIntelface fi;
        void PrintResult(string title, Array obj)
        {
            this.txtResult.Text += title + "=";
            for (int i = 0; i < obj.Length; i++)
            {
                this.txtResult.Text += obj.GetValue(i).ToString() + ",";
            }
            this.txtResult.Text += "\r\n";
        }
        private void ConnectButtonClicked(object sender, RoutedEventArgs e)
        {
            fi = new FanucRobIntelface(this.ipTxt.Text);
            this.cmdgrid.IsEnabled = fi.Connect();
        }

        private void ReadRClicked(object sender, RoutedEventArgs e)
        {
            fi.Refresh();
            PrintResult($"int - R[{fi.intRegion[0]}-{fi.intRegion[1]}]", fi.intRegs);
            PrintResult($"float -R[{fi.floatRegion[0]}-{fi.floatRegion[1]}]", fi.floatRegs);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void WriteRClicked(object sender, RoutedEventArgs e)
        {
            var val = new int[] { 1, 2, 3, 4, 5, 6 };
            var issuccess = fi.WriteR(val, 1);
            this.txtResult.Text += $"{issuccess} = fi.WriteR(val, 1);\r\n";
            var real = new float[] { 1.1f, 1.2f, 1.3f };
            fi.WriteR(real, 101);
            this.txtResult.Text += $"{issuccess} = fi.WriteR(real, 101);\r\n";

        }

        private void ReadPRClicked(object sender, RoutedEventArgs e)
        {
            fi.Refresh();
            for (int i = 0; i < fi.prRegs.Length; i++)
            {
                PrintResult($"PR[{fi.prRegion[0] + i}]", fi.prRegs[i].pc);
            }
        }

        private void DisconnectButtonClicked(object sender, RoutedEventArgs e)
        {
            fi.Disconnect();
            fi = null;
            this.txtResult.Clear();
            this.cmdgrid.IsEnabled = false;
        }

        private void ClearBtnClicked(object sender, RoutedEventArgs e)
        {
            this.txtResult.Clear();
        }

        private void WritePRClicked(object sender, RoutedEventArgs e)
        {
            var issuccess = fi.WritePR(new PR { pc = new float[] { 1, 2, 3, 4, 5, 6 } }, 1);
            this.txtResult.Text += $"{issuccess} = fi.WritePR();\r\n";
        }

        private void ReadCurPosClicked(object sender, RoutedEventArgs e)
        {
            fi.Refresh();
            PrintResult($"CurrentPos-XYZ", fi.curPos.pc);
            PrintResult($"CurrentPos-J", fi.curPos.pj);
        }

        private void ReadSdoClicked(object sender, RoutedEventArgs e)
        {
            var startIdx = 101;
            var count = 10;
            var ret = fi.ReadSdo(startIdx, count);
            PrintResult($"SDO[{startIdx}-{count + startIdx - 1}]", ret);
        }

        private void ReadSdiClicked(object sender, RoutedEventArgs e)
        {
            var startIdx = 101;
            var count = 20;
            var ret = fi.ReadSdI(startIdx, count);
            PrintResult($"SDO[{startIdx}-{count + startIdx - 1}]", ret);
        }

        private void ReadRdoClicked(object sender, RoutedEventArgs e)
        {
            var startIdx = 1;
            var count = 8;
            var ret = fi.ReadRdo(startIdx, count);
            PrintResult($"RDO[{startIdx}-{count + startIdx - 1}]", ret);
        }

        private void ReadRdiClicked(object sender, RoutedEventArgs e)
        {
            var startIdx = 1;
            var count = 8;
            var ret = fi.ReadRdi(startIdx, count);
            PrintResult($"RDI[{startIdx}-{count + startIdx - 1}]", ret);
        }

        private void WriteSdoClicked(object sender, RoutedEventArgs e)
        {
            var b = new bool[100];
            for (int i = 0; i < 100; i++)
            {
                b[i] = true;
            }
            var issuccess = fi.WriteSdo(b, 101);
            this.txtResult.Text += $"{issuccess} = fi.WriteSdo();\r\n";

        }

        private void WriteRdoClicked(object sender, RoutedEventArgs e)
        {
            var issuccess = fi.WriteRdo(new bool[] { true, false, true, true }, 1);
            this.txtResult.Text += $"{issuccess} = fi.WriteRdo();\r\n";

        }

        private void ReadSrClicked(object sender, RoutedEventArgs e)
        {
            fi.Refresh();
            PrintResult($"SR[{fi.strRegion[0]}-{fi.strRegion[1]}]", fi.strRegs);
        }

        private void WriteSrClicked(object sender, RoutedEventArgs e)
        {
            var issuccess = fi.WriteSR(new string[] { "123", "456", "789" }, 1);
            this.txtResult.Text += $"{issuccess} = fi.WriteSR();\r\n";

        }
    }
}
