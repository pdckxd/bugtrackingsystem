using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class TcpServer
    {
        private ThreadStart _start;
        private Thread _listenThread;
        private bool _bListening = false;
        private System.Net.IPAddress _myIP = null;
        private TcpListener _listener = null;
        private StringBuilder msg = new StringBuilder();
        private TcpClient _client;

        public TcpServer(IPAddress ip, int port)
        {
            this._myIP = ip;
            _listener = new TcpListener(this._myIP, port);

            this._start = new ThreadStart(StartListen);
            this._listenThread = new Thread(this._start);
        }

        public void Start()
        {
            this._bListening = true;
            this._listenThread.Start();
        }

        public void Stop()
        {
            this._bListening = false;
        }

        public string GetMessage()
        {
            string str = msg.ToString();
            msg = new StringBuilder();
            return str;
        }

        private void StartListen()
        {
            _listener.Start();
            //接收数据
            while (this._bListening)
            {
                //测试是否有数据
                try
                {
                    if(this._client == null)
                    this._client = this._listener.AcceptTcpClient();
                    NetworkStream ns = this._client.GetStream();
                    //StreamReader sr = new StreamReader(ns);//流读写器
                    //字组处理
                    byte[] bytes = new byte[1024];
                    int bytesread = ns.Read(bytes, 0, bytes.Length);
                    msg.Append(Encoding.ASCII.GetString(bytes, 0, bytesread));
                    ns.Close();
                }
                catch (Exception re)
                {
                }

            }
            _listener.Stop();
            //
        }
    }
}
