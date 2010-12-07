using System;
using System.Collections.Generic;
using System.Linq;
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
using Nairc.KpwFramework.TelescopeControler;
using Nairc.KpwFramework.DataModel;

namespace KpwStandAlone
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class KpwMain : Window
    {
        private TcpServer tcpServer;
        private KpwTcpClient tcpClient;

        public KpwMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Net.IPAddress myIP = System.Net.IPAddress.Parse("159.226.75.28");
            tcpServer = new TcpServer(myIP, 8280);
        }

        private void btnStartLicsening_Click(object sender, RoutedEventArgs e)
        {
            tcpServer.Start();
        }

        private void btnRaFH_Click(object sender, RoutedEventArgs e)
        {
            //if (tcpClient == null)
            //{
            //    tcpClient = new KpwTcpClient("159.226.75.28", 8280);
            //}

            //tcpClient.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaForwardFast.ToString()));
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaForwardFast.ToString()));

        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            //this.txtMessage.Text = this.txtMessage.Text + KpwRuntime.Instance.CommandSender.Message + "\n";

            this.txtMessage.Text = this.txtMessage.Text + string.Format("Ra: {0} ; Dec: {1} ", 
                KpwRuntime.Instance.TelescopeStatus.RaPos.ToString(), 
                KpwRuntime.Instance.TelescopeStatus.DecPos.ToString()) + "\n";
        }

        private void btnCloseLicsening_Click(object sender, RoutedEventArgs e)
        {
            tcpServer.Stop();
        }

        private void btnRaStop_Click(object sender, RoutedEventArgs e)
        {
            //if (tcpClient == null)
            //{
            //    tcpClient = new KpwTcpClient("159.226.75.28", 8280);
            //}

            //tcpClient.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaStop.ToString()));
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaStop.ToString()));

        }

        private void btnRaFL_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void btnRaFL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaStop.ToString()));
        }

        private void btnRaFH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaForwardFast.ToString()));

        }

        private void btnRaFL_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaForwardSlow.ToString()));

        }

        private void btnRaFL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(CommandMessage.RaStop.ToString()));
        }
     
    }
}
