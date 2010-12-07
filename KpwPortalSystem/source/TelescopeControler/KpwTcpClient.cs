using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using log4net;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class KpwTcpClient
    {
        private TcpClient _tcpClient = null;
        private string _hostName;
        private int _port;
        private StringBuilder _msg = new StringBuilder();
        private DateTime _flag = DateTime.Now;
        private object _locker = new object();

        Thread _thread;

        private TelescopeStatusDataModel _telescopeStatus = new TelescopeStatusDataModel();
        private static ILog logger = log4net.LogManager.GetLogger(typeof(KpwTcpClient));

        public TelescopeStatusDataModel TelescopeStatus
        {
            get
            {
                

                return this._telescopeStatus;
            }
        }

        public string Message
        {
            get
            {
                string str = this._msg.ToString();
                this._msg = new StringBuilder();
                return str;
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.TestConnect();
            }
        }

        public KpwTcpClient(string hostName, int port)
        {
            this._hostName = hostName;
            this._port = port;

            this._thread = new Thread(ReadMessage);

            try
            {
                this._tcpClient = new TcpClient(this._hostName, this._port);
            }
            catch
            {
                this._tcpClient = null;
            }

            this._thread.Start();
        }

        public bool SendMsg(string msg)
        {
            NetworkStream sendStream = null;

            try
            {
                if (this._tcpClient == null)
                {
                    try
                    {
                        this._tcpClient = new TcpClient(this._hostName, this._port);
                    }
                    catch (Exception ex)
                    {
                        this._tcpClient = null;
                        sendStream = null;
                        //this._thread.Suspend();
                    }
                }

                if (this._tcpClient != null)
                {
                    logger.Debug("Send command: " + msg);
                    sendStream = this._tcpClient.GetStream();
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
                    sendStream.Write(sendBytes, 0, sendBytes.Length);

                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Send Mgs failed!. Command Msg:{0}.", msg), ex);
                this._tcpClient = null;
            }

            return false;
        }

        private void ReadMessage()
        {
            while (true)
            {
                byte[] bytes = new byte[3000];
                int bytesread = 0;

                NetworkStream sendStream = null;
                lock (this._locker)
                {
                    try
                    {
                        if (this._tcpClient == null)
                        {
                            try
                            {
                                this._tcpClient = new TcpClient(this._hostName, this._port);
                            }
                            catch
                            {
                                this._tcpClient = null;
                                sendStream = null;
                            }
                        }

                        if (this._tcpClient != null)
                        {
                            sendStream = this._tcpClient.GetStream();
                            bytesread = sendStream.Read(bytes, 0, bytes.Length);
                            string str = this._msg.Append(Encoding.ASCII.GetString(bytes, 0, bytesread)).ToString();
                            this._msg = new StringBuilder();

                            int index = str.LastIndexOf("\r\n");

                            if (index > 0)
                            {
                                this._msg.Append(str.Substring(index + 2));
                                str = str.Substring(0, index + 2);
                            }

                            this.FormatMessage(str);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ReadMessage failed.", ex);
                        this._tcpClient = null;
                    }
                }

                System.Threading.Thread.Sleep(500);
            }
        }

        private void FormatMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            string[] array = message.Replace("\r\n","&").Split("&".ToCharArray());

            foreach (var item in array)
            {
                if (item.StartsWith(KpwConstance.KPW) && item.EndsWith(KpwConstance.OK))
                {
                    string str = item.Replace(KpwConstance.KPW, "").Replace(KpwConstance.OK, "");
                    string[] words = str.Split(":".ToCharArray());

                    switch (words[0])
                    {
                        case "POSI":
                            {
                                string ra = words[1].Substring(0, 7);
                                string dec = words[1].Substring(7, 7);

                                this._telescopeStatus.RaPos = new RaPosition(ra);
                                this._telescopeStatus.DecPos = new DecPosition(dec);
                                break;
                            }
                        case "STAT":
                            {
                                char[] arrayChar = words[1].ToCharArray();

                                if (arrayChar[0] == '+')
                                {
                                    this._telescopeStatus.RaOverflow = RaOverFlow.Positive;
                                }
                                else if (arrayChar[0] == '-')
                                {
                                    this._telescopeStatus.RaOverflow = RaOverFlow.Negative;
                                }
                                else
                                {
                                    this._telescopeStatus.RaOverflow = RaOverFlow.None;
                                }

                                if (arrayChar[1] == '+')
                                {
                                    this._telescopeStatus.DecOverflow = DecOverFlow.Positive;
                                }
                                else if (arrayChar[1] == '-')
                                {
                                    this._telescopeStatus.DecOverflow = DecOverFlow.Negative;
                                }
                                else
                                {
                                    this._telescopeStatus.DecOverflow = DecOverFlow.None;
                                }

                                if (arrayChar[2] == 'N')
                                {
                                    this._telescopeStatus.LevelOverflow = false;
                                }
                                else
                                {
                                    this._telescopeStatus.LevelOverflow = true;
                                }

                                if (arrayChar[3] == 'O')
                                {
                                    this._telescopeStatus.MirrorStatus = MirrorCoverStatus.O;
                                }
                                else if (arrayChar[3] == 'C')
                                {
                                    this._telescopeStatus.MirrorStatus = MirrorCoverStatus.C;
                                }
                                else if (arrayChar[3] == 'R')
                                {
                                    this._telescopeStatus.MirrorStatus = MirrorCoverStatus.R;
                                }

                                if (arrayChar[4] == 'P')
                                {
                                    this._telescopeStatus.SearchStatus = SearchStarStatus.P;
                                }
                                else if (arrayChar[4] == 'S')
                                {
                                    this._telescopeStatus.SearchStatus = SearchStarStatus.S;
                                }
                                else if (arrayChar[4] == 'F')
                                {
                                    this._telescopeStatus.SearchStatus = SearchStarStatus.F;
                                }

                                if (arrayChar[5] == 'P')
                                {
                                    this._telescopeStatus.DomeStatus = DomeStatus.P;
                                }
                                else if (arrayChar[5] == 'S')
                                {
                                    this._telescopeStatus.DomeStatus = DomeStatus.S;
                                }
                                else if (arrayChar[5] == 'F')
                                {
                                    this._telescopeStatus.DomeStatus = DomeStatus.F;
                                }

                                break;

                            }
                    }

                    this._flag = DateTime.Now;
                }
                else
                {
                    continue;
                }
            }
        }

        static int count = 0;
        private bool TestConnect()
        {

            TimeSpan span = DateTime.Now - this._flag;
           
            if (span.TotalSeconds > 5)
            {
                this._tcpClient = null;
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public void Close()
        {
            this._tcpClient.Close();
        }
    }
}
