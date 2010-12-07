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

namespace HikClientDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        FRealDataCallBack fRealDataCallBack;
        FMessCallBack fMessCallBack;
        FPlayDataCallBack fPlayDataCallBack;
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Hik_HcSdk sdk = new Hik_HcSdk();

            long userID = 0;

            bool succeed = Hik_HcSdk.NET_DVR_Init();

            if (succeed)
            {
                int isSupport = Hik_HcSdk.NET_DVR_IsSupport();

                succeed = Hik_HcSdk.NET_DVR_SetConnectTime(100, 100);
                fMessCallBack = new FMessCallBack(target1);

                Hik_HcSdk.NET_DVR_SetDVRMessCallBack(fMessCallBack);
                Hik_HcSdk.NET_DVR_SetShowMode(1, 256);
                Hik_HcSdk.NET_DVR_StartListen("159.226.75.28", 8003);

                NET_DVR_DEVICEINFO deviceInfo = new NET_DVR_DEVICEINFO();
                userID = Hik_HcSdk.NET_DVR_Login("159.226.75.29", 8000, "admin", "12345", ref deviceInfo);

                if (userID > -1)
                {
                    NET_DVR_CLIENTINFO clientinfo = new NET_DVR_CLIENTINFO();

                    long playId = Hik_HcSdk.NET_DVR_RealPlay(userID, ref clientinfo);

                    succeed = Hik_HcSdk.NET_DVR_CapturePicture(playId, "test.bpm");
                    //fRealDataCallBack = new FRealDataCallBack(target);

                    ushort dwUser = 0;

                    //succeed = Hik_HcSdk.NET_DVR_SetRealDataCallBack(playId, fRealDataCallBack, dwUser);
                    fPlayDataCallBack = new FPlayDataCallBack(target2);
                    Hik_HcSdk.NET_DVR_SetPlayDataCallBack(playId, fPlayDataCallBack, dwUser);

                    Hik_HcSdk.NET_DVR_PlayBackCaptureFile(playId, "test.bmp");

                    succeed = Hik_HcSdk.NET_DVR_Logout(userID);
                }
            }
        }

        private void target(long lRealHandle, ushort dwDataType, byte[] pBuffer, ushort dwBufSize, ushort dwUser)
        {

        }

        private bool target1(long lCommand, string sDVRIP, IntPtr pBuf, ushort dwBufLen)
        {
            return true;
        }

        private void target2(long lPlayHandle, ushort dwDataType, byte[] pBuffer, ushort dwBufSize, ushort dwUser)
        {

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
