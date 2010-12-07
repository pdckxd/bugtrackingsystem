using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HikClientDemo
{
    #region Delegate
    ///////////////////////////////////////////////////////////////////////////////////////////////
    //注意：关于回调函数。因为vb不支持多线程，所以当回调函数是VB声明的函数时，在vc的线程中调用vb的函数，会有问题。
    //		详见：Microsoft Knowledge Base Article - Q198607 “PRB: Access Violation in VB Run-Time Using AddressOf ”。 
    ///////////////////////////////////////////////////////////////////////////////////////////////

    public delegate bool FMessCallBack(long lCommand, string sDVRIP, IntPtr pBuf, ushort dwBufLen);

    public delegate bool FMessCallBack_EX(long lCommand, long lUserID, IntPtr pBuf, ushort dwBufLen);

    /// <summary>
    /// bool (CALLBACK *fMessCallBack_NEW)(long lCommand,string sDVRIP,IntPtr pBuf,ushort dwBufLen, int dwLinkDVRPort)
    /// </summary>
    /// <param name="lCommand"></param>
    /// <param name="sDVRIP"></param>
    /// <param name="pBuf"></param>
    /// <param name="dwBufLen"></param>
    /// <param name="dwLinkDVRPort"></param>
    /// <returns></returns>
    public delegate bool FMessCallBack_NEW(long lCommand, string sDVRIP, IntPtr pBuf, ushort dwBufLen, int dwLinkDVRPort);

    public delegate bool FMessageCallBack(long lCommand, string sDVRIP, IntPtr pBuf, ushort dwBufLen, ushort dwUser);

    public delegate void FDrawFun(long lRealHandle, IntPtr hDc, ushort dwUser);

    /// <summary>
    /// 注：此函数包括开始和停止用户处理客户端收到的数据，当fRealDataCallBack不为NULL时，开始用户处理客户端收到的数据，
    /// 当设置为NULL表示停止用户处理客户端收到的数据.当用户开始接收数据时，第一个包是40个字节的文件头,
    /// 用户可以用这个头来打开播放器,以后回调的就是压缩的码流。
    /// IP Camera设备下调用此接口返回的是经过转化的HIKVISION格式的MPEG4流数据。
    /// </summary>
    /// <param name="lRealHandle">NET_DVR_RealPlay()的返回值</param>
    /// <param name="dwDataType">#define NET_DVR_SYSHEAD 1 //系统头数据 #define NET_DVR_STREAMDATA 2 //流数据</param>
    /// <param name="pBuffer">存放数据的缓冲区指针</param>
    /// <param name="dwBufSize">缓冲区的大小</param>
    /// <param name="dwUser">用户数据，就是上面输入的用户数据</param>
    public delegate void FRealDataCallBack(long lRealHandle, ushort dwDataType, byte[] pBuffer, ushort dwBufSize, ushort dwUser);

    public delegate void FPlayDataCallBack(long lPlayHandle, ushort dwDataType, byte[] pBuffer, ushort dwBufSize, ushort dwUser);

    public delegate void FVoiceDataCallBack(long lVoiceComHandle, IntPtr pRecvDataBuffer, ushort dwBufSize, byte byAudioFlag, ushort dwUser);

    public delegate void FSerialDataCallBack(long lSerialHandle, IntPtr pRecvDataBuffer, ushort dwBufSize, ushort dwUser);
    #endregion

    public sealed class Hik_HcSdk
    {
        #region Const
        /// NET_DVR_NOERROR -> 0
        public const int NET_DVR_NOERROR = 0;

        /// NET_DVR_PASSint_ERROR -> 1
        public const int NET_DVR_PASSint_ERROR = 1;

        /// NET_DVR_NOENOUGHPRI -> 2
        public const int NET_DVR_NOENOUGHPRI = 2;

        /// NET_DVR_NOINIT -> 3
        public const int NET_DVR_NOINIT = 3;

        /// NET_DVR_CHANNEL_ERROR -> 4
        public const int NET_DVR_CHANNEL_ERROR = 4;

        /// NET_DVR_OVER_MAXLINK -> 5
        public const int NET_DVR_OVER_MAXLINK = 5;

        /// NET_DVR_VERSIONNOMATCH -> 6
        public const int NET_DVR_VERSIONNOMATCH = 6;

        /// NET_DVR_NETWORK_FAIL_CONNECT -> 7
        public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;

        /// NET_DVR_NETWORK_SEND_ERROR -> 8
        public const int NET_DVR_NETWORK_SEND_ERROR = 8;

        /// NET_DVR_NETWORK_RECV_ERROR -> 9
        public const int NET_DVR_NETWORK_RECV_ERROR = 9;

        /// NET_DVR_NETWORK_RECV_TIMEOUT -> 10
        public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10;

        /// NET_DVR_NETWORK_ERRORDATA -> 11
        public const int NET_DVR_NETWORK_ERRORDATA = 11;

        /// NET_DVR_ORDER_ERROR -> 12
        public const int NET_DVR_ORDER_ERROR = 12;

        /// NET_DVR_OPERNOPERMIT -> 13
        public const int NET_DVR_OPERNOPERMIT = 13;

        /// NET_DVR_COMMANDTIMEOUT -> 14
        public const int NET_DVR_COMMANDTIMEOUT = 14;

        /// NET_DVR_ERRORSERIALPORT -> 15
        public const int NET_DVR_ERRORSERIALPORT = 15;

        /// NET_DVR_ERRORALARMPORT -> 16
        public const int NET_DVR_ERRORALARMPORT = 16;

        /// NET_DVR_PARAMETER_ERROR -> 17
        public const int NET_DVR_PARAMETER_ERROR = 17;

        /// NET_DVR_CHAN_EXCEPTION -> 18
        public const int NET_DVR_CHAN_EXCEPTION = 18;

        /// NET_DVR_NODISK -> 19
        public const int NET_DVR_NODISK = 19;

        /// NET_DVR_ERRORDISKNUM -> 20
        public const int NET_DVR_ERRORDISKNUM = 20;

        /// NET_DVR_DISK_FULL -> 21
        public const int NET_DVR_DISK_FULL = 21;

        /// NET_DVR_DISK_ERROR -> 22
        public const int NET_DVR_DISK_ERROR = 22;

        /// NET_DVR_NOSUPPORT -> 23
        public const int NET_DVR_NOSUPPORT = 23;

        /// NET_DVR_BUSY -> 24
        public const int NET_DVR_BUSY = 24;

        /// NET_DVR_MODIFY_FAIL -> 25
        public const int NET_DVR_MODIFY_FAIL = 25;

        /// NET_DVR_PASSint_FORMAT_ERROR -> 26
        public const int NET_DVR_PASSint_FORMAT_ERROR = 26;

        /// NET_DVR_DISK_FORMATING -> 27
        public const int NET_DVR_DISK_FORMATING = 27;

        /// NET_DVR_DVRNORESOURCE -> 28
        public const int NET_DVR_DVRNORESOURCE = 28;

        /// NET_DVR_DVROPRATEFAILED -> 29
        public const int NET_DVR_DVROPRATEFAILED = 29;

        /// NET_DVR_OPENHOSTSOUND_FAIL -> 30
        public const int NET_DVR_OPENHOSTSOUND_FAIL = 30;

        /// NET_DVR_DVRVOICEOPENED -> 31
        public const int NET_DVR_DVRVOICEOPENED = 31;

        /// NET_DVR_TIMEINPUTERROR -> 32
        public const int NET_DVR_TIMEINPUTERROR = 32;

        /// NET_DVR_NOSPECFILE -> 33
        public const int NET_DVR_NOSPECFILE = 33;

        /// NET_DVR_CREATEFILE_ERROR -> 34
        public const int NET_DVR_CREATEFILE_ERROR = 34;

        /// NET_DVR_FILEOPENFAIL -> 35
        public const int NET_DVR_FILEOPENFAIL = 35;

        /// NET_DVR_OPERNOTFINISH -> 36
        public const int NET_DVR_OPERNOTFINISH = 36;

        /// NET_DVR_GETPLAYTIMEFAIL -> 37
        public const int NET_DVR_GETPLAYTIMEFAIL = 37;

        /// NET_DVR_PLAYFAIL -> 38
        public const int NET_DVR_PLAYFAIL = 38;

        /// NET_DVR_FILEFORMAT_ERROR -> 39
        public const int NET_DVR_FILEFORMAT_ERROR = 39;

        /// NET_DVR_DIR_ERROR -> 40
        public const int NET_DVR_DIR_ERROR = 40;

        /// NET_DVR_ALLOC_RESOUCE_ERROR -> 41
        public const int NET_DVR_ALLOC_RESOUCE_ERROR = 41;

        /// NET_DVR_AUDIO_MODE_ERROR -> 42
        public const int NET_DVR_AUDIO_MODE_ERROR = 42;

        /// NET_DVR_NOENOUGH_BUF -> 43
        public const int NET_DVR_NOENOUGH_BUF = 43;

        /// NET_DVR_CREATESOCKET_ERROR -> 44
        public const int NET_DVR_CREATESOCKET_ERROR = 44;

        /// NET_DVR_SETSOCKET_ERROR -> 45
        public const int NET_DVR_SETSOCKET_ERROR = 45;

        /// NET_DVR_MAX_NUM -> 46
        public const int NET_DVR_MAX_NUM = 46;

        /// NET_DVR_USERNOTEXIST -> 47
        public const int NET_DVR_USERNOTEXIST = 47;

        /// NET_DVR_WRITEFLASHERROR -> 48
        public const int NET_DVR_WRITEFLASHERROR = 48;

        /// NET_DVR_UPGRADEFAIL -> 49
        public const int NET_DVR_UPGRADEFAIL = 49;

        /// NET_DVR_CARDHAVEINIT -> 50
        public const int NET_DVR_CARDHAVEINIT = 50;

        /// NET_DVR_PLAYERFAILED -> 51
        public const int NET_DVR_PLAYERFAILED = 51;

        /// NET_DVR_MAX_USERNUM -> 52
        public const int NET_DVR_MAX_USERNUM = 52;

        /// NET_DVR_GETLOCALIPANDMACFAIL -> 53
        public const int NET_DVR_GETLOCALIPANDMACFAIL = 53;

        /// NET_DVR_NOENCODEING -> 54
        public const int NET_DVR_NOENCODEING = 54;

        /// NET_DVR_IPMISMATCH -> 55
        public const int NET_DVR_IPMISMATCH = 55;

        /// NET_DVR_MACMISMATCH -> 56
        public const int NET_DVR_MACMISMATCH = 56;

        /// NET_DVR_UPGRADELANGMISMATCH -> 57
        public const int NET_DVR_UPGRADELANGMISMATCH = 57;

        /// NET_DVR_DDRAWDEVICENOSUPPORT -> 58
        public const int NET_DVR_DDRAWDEVICENOSUPPORT = 58;

        /// NET_DVR_FILE_SUCCESS -> 1000
        public const int NET_DVR_FILE_SUCCESS = 1000;

        /// NET_DVR_FILE_NOFIND -> 1001
        public const int NET_DVR_FILE_NOFIND = 1001;

        /// NET_DVR_ISFINDING -> 1002
        public const int NET_DVR_ISFINDING = 1002;

        /// NET_DVR_NOMOREFILE -> 1003
        public const int NET_DVR_NOMOREFILE = 1003;

        /// NET_DVR_FILE_EXCEPTION -> 1004
        public const int NET_DVR_FILE_EXCEPTION = 1004;

        /// NET_DVR_SUPPORT_DDRAW -> 0x01
        public const int NET_DVR_SUPPORT_DDRAW = 1;

        /// NET_DVR_SUPPORT_BLT -> 0x02
        public const int NET_DVR_SUPPORT_BLT = 2;

        /// NET_DVR_SUPPORT_BLTFOURCC -> 0x04
        public const int NET_DVR_SUPPORT_BLTFOURCC = 4;

        /// NET_DVR_SUPPORT_BLTSHRINKX -> 0x08
        public const int NET_DVR_SUPPORT_BLTSHRINKX = 8;

        /// NET_DVR_SUPPORT_BLTSHRINKY -> 0x10
        public const int NET_DVR_SUPPORT_BLTSHRINKY = 16;

        /// NET_DVR_SUPPORT_BLTSTRETCHX -> 0x20
        public const int NET_DVR_SUPPORT_BLTSTRETCHX = 32;

        /// NET_DVR_SUPPORT_BLTSTRETCHY -> 0x40
        public const int NET_DVR_SUPPORT_BLTSTRETCHY = 64;

        /// NET_DVR_SUPPORT_SSE -> 0x80
        public const int NET_DVR_SUPPORT_SSE = 128;

        /// NET_DVR_SUPPORT_MMX -> 0x100
        public const int NET_DVR_SUPPORT_MMX = 256;

        /// SET_PRESET -> 8
        public const int SET_PRESET = 8;

        /// CLE_PRESET -> 9
        public const int CLE_PRESET = 9;

        /// GOTO_PRESET -> 39
        public const int GOTO_PRESET = 39;

        /// LIGHT_PWRON -> 2
        public const int LIGHT_PWRON = 2;

        /// WIPER_PWRON -> 3
        public const int WIPER_PWRON = 3;

        /// FAN_PWRON -> 4
        public const int FAN_PWRON = 4;

        /// HEATER_PWRON -> 5
        public const int HEATER_PWRON = 5;

        /// AUX_PWRON -> 6
        public const int AUX_PWRON = 6;

        /// ZOOM_IN -> 11
        public const int ZOOM_IN = 11;

        /// ZOOM_OUT -> 12
        public const int ZOOM_OUT = 12;

        /// FOCUS_NEAR -> 13
        public const int FOCUS_NEAR = 13;

        /// FOCUS_FAR -> 14
        public const int FOCUS_FAR = 14;

        /// IRIS_OPEN -> 15
        public const int IRIS_OPEN = 15;

        /// IRIS_CLOSE -> 16
        public const int IRIS_CLOSE = 16;

        /// TILT_UP -> 21
        public const int TILT_UP = 21;

        /// TILT_DOWN -> 22
        public const int TILT_DOWN = 22;

        /// PAN_LEFT -> 23
        public const int PAN_LEFT = 23;

        /// PAN_RIGHT -> 24
        public const int PAN_RIGHT = 24;

        /// UP_LEFT -> 25
        public const int UP_LEFT = 25;

        /// UP_RIGHT -> 26
        public const int UP_RIGHT = 26;

        /// DOWN_LEFT -> 27
        public const int DOWN_LEFT = 27;

        /// DOWN_RIGHT -> 28
        public const int DOWN_RIGHT = 28;

        /// PAN_AUTO -> 29
        public const int PAN_AUTO = 29;

        /// FILL_PRE_SEQ -> 30
        public const int FILL_PRE_SEQ = 30;

        /// SET_SEQ_DWELL -> 31
        public const int SET_SEQ_DWELL = 31;

        /// SET_SEQ_SPEED -> 32
        public const int SET_SEQ_SPEED = 32;

        /// CLE_PRE_SEQ -> 33
        public const int CLE_PRE_SEQ = 33;

        /// STA_MEM_CRUISE -> 34
        public const int STA_MEM_CRUISE = 34;

        /// STO_MEM_CRUISE -> 35
        public const int STO_MEM_CRUISE = 35;

        /// RUN_CRUISE -> 36
        public const int RUN_CRUISE = 36;

        /// RUN_SEQ -> 37
        public const int RUN_SEQ = 37;

        /// STOP_SEQ -> 38
        public const int STOP_SEQ = 38;

        /// NET_DVR_SYSHEAD -> 1
        public const int NET_DVR_SYSHEAD = 1;

        /// NET_DVR_STREAMDATA -> 2
        public const int NET_DVR_STREAMDATA = 2;

        /// NET_DVR_PLAYSTART -> 1
        public const int NET_DVR_PLAYSTART = 1;

        /// NET_DVR_PLAYSTOP -> 2
        public const int NET_DVR_PLAYSTOP = 2;

        /// NET_DVR_PLAYPAUSE -> 3
        public const int NET_DVR_PLAYPAUSE = 3;

        /// NET_DVR_PLAYRESTART -> 4
        public const int NET_DVR_PLAYRESTART = 4;

        /// NET_DVR_PLAYFAST -> 5
        public const int NET_DVR_PLAYFAST = 5;

        /// NET_DVR_PLAYSLOW -> 6
        public const int NET_DVR_PLAYSLOW = 6;

        /// NET_DVR_PLAYNORMAL -> 7
        public const int NET_DVR_PLAYNORMAL = 7;

        /// NET_DVR_PLAYFRAME -> 8
        public const int NET_DVR_PLAYFRAME = 8;

        /// NET_DVR_PLAYSTARTAUDIO -> 9
        public const int NET_DVR_PLAYSTARTAUDIO = 9;

        /// NET_DVR_PLAYSTOPAUDIO -> 10
        public const int NET_DVR_PLAYSTOPAUDIO = 10;

        /// NET_DVR_PLAYAUDIOVOLUME -> 11
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;

        /// NET_DVR_PLAYSETPOS -> 12
        public const int NET_DVR_PLAYSETPOS = 12;

        /// NET_DVR_PLAYGETPOS -> 13
        public const int NET_DVR_PLAYGETPOS = 13;

        /// NET_DVR_PLAYGETTIME -> 14
        public const int NET_DVR_PLAYGETTIME = 14;

        /// NET_DVR_PLAYGETFRAME -> 15
        public const int NET_DVR_PLAYGETFRAME = 15;

        /// NET_DVR_GETTOTALFRAMES -> 16
        public const int NET_DVR_GETTOTALFRAMES = 16;

        /// NET_DVR_GETTOTALTIME -> 17
        public const int NET_DVR_GETTOTALTIME = 17;

        /// NET_DVR_THROWBFRAME -> 20
        public const int NET_DVR_THROWBFRAME = 20;

        /// NET_DVR_GET_DEVICECFG -> 100
        public const int NET_DVR_GET_DEVICECFG = 100;

        /// NET_DVR_SET_DEVICECFG -> 101
        public const int NET_DVR_SET_DEVICECFG = 101;

        /// NET_DVR_GET_NETCFG -> 102
        public const int NET_DVR_GET_NETCFG = 102;

        /// NET_DVR_SET_NETCFG -> 103
        public const int NET_DVR_SET_NETCFG = 103;

        /// NET_DVR_GET_PICCFG -> 104
        public const int NET_DVR_GET_PICCFG = 104;

        /// NET_DVR_SET_PICCFG -> 105
        public const int NET_DVR_SET_PICCFG = 105;

        /// NET_DVR_GET_COMPRESSCFG -> 106
        public const int NET_DVR_GET_COMPRESSCFG = 106;

        /// NET_DVR_SET_COMPRESSCFG -> 107
        public const int NET_DVR_SET_COMPRESSCFG = 107;

        /// NET_DVR_GET_COMPRESSCFG_EX -> 204
        public const int NET_DVR_GET_COMPRESSCFG_EX = 204;

        /// NET_DVR_SET_COMPRESSCFG_EX -> 205
        public const int NET_DVR_SET_COMPRESSCFG_EX = 205;

        /// NET_DVR_GET_RECORDCFG -> 108
        public const int NET_DVR_GET_RECORDCFG = 108;

        /// NET_DVR_SET_RECORDCFG -> 109
        public const int NET_DVR_SET_RECORDCFG = 109;

        /// NET_DVR_GET_DECODERCFG -> 110
        public const int NET_DVR_GET_DECODERCFG = 110;

        /// NET_DVR_SET_DECODERCFG -> 111
        public const int NET_DVR_SET_DECODERCFG = 111;

        /// NET_DVR_GET_RS232CFG -> 112
        public const int NET_DVR_GET_RS232CFG = 112;

        /// NET_DVR_SET_RS232CFG -> 113
        public const int NET_DVR_SET_RS232CFG = 113;

        /// NET_DVR_GET_ALARMINCFG -> 114
        public const int NET_DVR_GET_ALARMINCFG = 114;

        /// NET_DVR_SET_ALARMINCFG -> 115
        public const int NET_DVR_SET_ALARMINCFG = 115;

        /// NET_DVR_GET_ALARMOUTCFG -> 116
        public const int NET_DVR_GET_ALARMOUTCFG = 116;

        /// NET_DVR_SET_ALARMOUTCFG -> 117
        public const int NET_DVR_SET_ALARMOUTCFG = 117;

        /// NET_DVR_GET_TIMECFG -> 118
        public const int NET_DVR_GET_TIMECFG = 118;

        /// NET_DVR_SET_TIMECFG -> 119
        public const int NET_DVR_SET_TIMECFG = 119;

        /// NET_DVR_GET_PREVIEWCFG -> 120
        public const int NET_DVR_GET_PREVIEWCFG = 120;

        /// NET_DVR_SET_PREVIEWCFG -> 121
        public const int NET_DVR_SET_PREVIEWCFG = 121;

        /// NET_DVR_GET_VIDEOOUTCFG -> 122
        public const int NET_DVR_GET_VIDEOOUTCFG = 122;

        /// NET_DVR_SET_VIDEOOUTCFG -> 123
        public const int NET_DVR_SET_VIDEOOUTCFG = 123;

        /// NET_DVR_GET_USERCFG -> 124
        public const int NET_DVR_GET_USERCFG = 124;

        /// NET_DVR_SET_USERCFG -> 125
        public const int NET_DVR_SET_USERCFG = 125;

        /// NET_DVR_GET_EXCEPTIONCFG -> 126
        public const int NET_DVR_GET_EXCEPTIONCFG = 126;

        /// NET_DVR_SET_EXCEPTIONCFG -> 127
        public const int NET_DVR_SET_EXCEPTIONCFG = 127;

        /// NET_DVR_GET_SHOWSTRING -> 130
        public const int NET_DVR_GET_SHOWSTRING = 130;

        /// NET_DVR_SET_SHOWSTRING -> 131
        public const int NET_DVR_SET_SHOWSTRING = 131;

        /// NET_DVR_GET_EVENTCOMPCFG -> 132
        public const int NET_DVR_GET_EVENTCOMPCFG = 132;

        /// NET_DVR_SET_EVENTCOMPCFG -> 133
        public const int NET_DVR_SET_EVENTCOMPCFG = 133;

        /// NET_DVR_GET_AUXOUTCFG -> 140
        public const int NET_DVR_GET_AUXOUTCFG = 140;

        /// NET_DVR_SET_AUXOUTCFG -> 141
        public const int NET_DVR_SET_AUXOUTCFG = 141;

        /// NET_DVR_GET_PREVIEWCFG_AUX -> 142
        public const int NET_DVR_GET_PREVIEWCFG_AUX = 142;

        /// NET_DVR_SET_PREVIEWCFG_AUX -> 143
        public const int NET_DVR_SET_PREVIEWCFG_AUX = 143;

        /// NET_DVR_GET_PICCFG_EX -> 200
        public const int NET_DVR_GET_PICCFG_EX = 200;

        /// NET_DVR_SET_PICCFG_EX -> 201
        public const int NET_DVR_SET_PICCFG_EX = 201;

        /// NET_DVR_GET_USERCFG_EX -> 202
        public const int NET_DVR_GET_USERCFG_EX = 202;

        /// NET_DVR_SET_USERCFG_EX -> 203
        public const int NET_DVR_SET_USERCFG_EX = 203;

        /// NET_DVR_GET_NETAPPCFG -> 222
        public const int NET_DVR_GET_NETAPPCFG = 222;

        /// NET_DVR_SET_NETAPPCFG -> 223
        public const int NET_DVR_SET_NETAPPCFG = 223;

        /// NET_DVR_GET_NFSCFG -> 230
        public const int NET_DVR_GET_NFSCFG = 230;

        /// NET_DVR_SET_NFSCFG -> 231
        public const int NET_DVR_SET_NFSCFG = 231;

        /// NET_DVR_GET_NETCFG_OTHER -> 244
        public const int NET_DVR_GET_NETCFG_OTHER = 244;

        /// NET_DVR_SET_NETCFG_OTHER -> 245
        public const int NET_DVR_SET_NETCFG_OTHER = 245;

        /// NET_DVR_GET_EMAILPARACFG -> 250
        public const int NET_DVR_GET_EMAILPARACFG = 250;

        /// NET_DVR_SET_EMAILPARACFG -> 251
        public const int NET_DVR_SET_EMAILPARACFG = 251;

        /// NET_DVR_GET_DDNSCFG_EX -> 274
        public const int NET_DVR_GET_DDNSCFG_EX = 274;

        /// NET_DVR_SET_DDNSCFG_EX -> 275
        public const int NET_DVR_SET_DDNSCFG_EX = 275;

        /// COMM_ALARM -> 0x1100
        public const int COMM_ALARM = 4352;

        /// COMM_TRADEINFO -> 0x1500
        public const int COMM_TRADEINFO = 5376;

        /// EXCEPTION_AUDIOEXCHANGE -> 0x8001
        public const int EXCEPTION_AUDIOEXCHANGE = 32769;

        /// EXCEPTION_ALARM -> 0x8002
        public const int EXCEPTION_ALARM = 32770;

        /// EXCEPTION_PREVIEW -> 0x8003
        public const int EXCEPTION_PREVIEW = 32771;

        /// EXCEPTION_SERIAL -> 0x8004
        public const int EXCEPTION_SERIAL = 32772;

        /// EXCEPTION_RECONNECT -> 0x8005
        public const int EXCEPTION_RECONNECT = 32773;

        /// NAME_LEN -> 32
        public const int NAME_LEN = 32;

        /// SERIALNO_LEN -> 48
        public const int SERIALNO_LEN = 48;

        /// MACADDR_LEN -> 6
        public const int MACADDR_LEN = 6;

        /// MAX_ETHERNET -> 2
        public const int MAX_ETHERNET = 2;

        /// PATHNAME_LEN -> 128
        public const int PATHNAME_LEN = 128;

        /// PASSWD_LEN -> 16
        public const int PASSWD_LEN = 16;

        /// MAX_CHANNUM -> 16
        public const int MAX_CHANNUM = 16;

        /// MAX_ALARMOUT -> 4
        public const int MAX_ALARMOUT = 4;

        /// MAX_TIMESEGMENT -> 4
        public const int MAX_TIMESEGMENT = 4;

        /// MAX_PRESET -> 128
        public const int MAX_PRESET = 128;

        /// MAX_DAYS -> 7
        public const int MAX_DAYS = 7;

        /// PHONENUMBER_LEN -> 32
        public const int PHONENUMBER_LEN = 32;

        /// MAX_DISKNUM -> 16
        public const int MAX_DISKNUM = 16;

        /// MAX_WINDOW -> 16
        public const int MAX_WINDOW = 16;

        /// MAX_VGA -> 1
        public const int MAX_VGA = 1;

        /// MAX_USERNUM -> 16
        public const int MAX_USERNUM = 16;

        /// MAX_EXCEPTIONNUM -> 16
        public const int MAX_EXCEPTIONNUM = 16;

        /// MAX_LINK -> 6
        public const int MAX_LINK = 6;

        /// MAX_ALARMIN -> 16
        public const int MAX_ALARMIN = 16;

        /// MAX_VIDEOOUT -> 2
        public const int MAX_VIDEOOUT = 2;

        /// MAX_NAMELEN -> 16
        public const int MAX_NAMELEN = 16;

        /// MAX_RIGHT -> 32
        public const int MAX_RIGHT = 32;

        /// CARDNUM_LEN -> 20
        public const int CARDNUM_LEN = 20;

        /// MAX_SHELTERNUM -> 4
        public const int MAX_SHELTERNUM = 4;

        /// MAX_DECPOOLNUM -> 4
        public const int MAX_DECPOOLNUM = 4;

        /// MAX_DECNUM -> 4
        public const int MAX_DECNUM = 4;

        /// MAX_TRANSPARENTNUM -> 2
        public const int MAX_TRANSPARENTNUM = 2;

        /// MAX_STRINGNUM -> 4
        public const int MAX_STRINGNUM = 4;

        /// MAX_AUXOUT -> 4
        public const int MAX_AUXOUT = 4;

        /// MAX_NFS_DISK -> 8
        public const int MAX_NFS_DISK = 8;

        /// MAX_CYCLE_CHAN -> 16
        public const int MAX_CYCLE_CHAN = 16;

        /// MAX_DOMAIN_NAME -> 64
        public const int MAX_DOMAIN_NAME = 64;

        /// MAX_SERIAL_NUM -> 64
        public const int MAX_SERIAL_NUM = 64;

        /// NET_IF_10M_HALF -> 1
        public const int NET_IF_10M_HALF = 1;

        /// NET_IF_10M_FULL -> 2
        public const int NET_IF_10M_FULL = 2;

        /// NET_IF_100M_HALF -> 3
        public const int NET_IF_100M_HALF = 3;

        /// NET_IF_100M_FULL -> 4
        public const int NET_IF_100M_FULL = 4;

        /// NET_IF_AUTO -> 5
        public const int NET_IF_AUTO = 5;

        /// DVR -> 1
        public const int DVR = 1;

        /// ATMDVR -> 2
        public const int ATMDVR = 2;

        /// DVS -> 3
        public const int DVS = 3;

        /// DEC -> 4
        public const int DEC = 4;

        /// ENC_DEC -> 5
        public const int ENC_DEC = 5;

        /// DVR_HC -> 6
        public const int DVR_HC = 6;

        /// DVR_HT -> 7
        public const int DVR_HT = 7;

        /// DVR_HF -> 8
        public const int DVR_HF = 8;

        /// DVR_HS -> 9
        public const int DVR_HS = 9;

        /// DVR_HTS -> 10
        public const int DVR_HTS = 10;

        /// DVR_HB -> 11
        public const int DVR_HB = 11;

        /// DVR_HCS -> 12
        public const int DVR_HCS = 12;

        /// DVS_A -> 13
        public const int DVS_A = 13;

        /// DVR_HC_S -> 14
        public const int DVR_HC_S = 14;

        /// DVR_HT_S -> 15
        public const int DVR_HT_S = 15;

        /// DVR_HF_S -> 16
        public const int DVR_HF_S = 16;

        /// DVR_HS_S -> 17
        public const int DVR_HS_S = 17;

        /// ATMDVR_S -> 18
        public const int ATMDVR_S = 18;

        /// DVR_7000H -> 19
        public const int DVR_7000H = 19;

        /// DEC_MAT -> 20
        public const int DEC_MAT = 20;

        /// DVR_MOBILE -> 21
        public const int DVR_MOBILE = 21;

        /// DVR_HD_S -> 22
        public const int DVR_HD_S = 22;

        /// DVR_HD_SL -> 23
        public const int DVR_HD_SL = 23;

        /// DVR_HC_SL -> 24
        public const int DVR_HC_SL = 24;

        /// DVR_HS_ST -> 25
        public const int DVR_HS_ST = 25;

        /// DVS_HW -> 26
        public const int DVS_HW = 26;

        /// IPCAM -> 30
        public const int IPCAM = 30;

        /// IPDOME -> 40
        public const int IPDOME = 40;

        /// IPMOD -> 50
        public const int IPMOD = 50;

        /// NOACTION -> 0x0
        public const int NOACTION = 0;

        /// WARNONMONITOR -> 0x1
        public const int WARNONMONITOR = 1;

        /// WARNONAUDIOOUT -> 0x2
        public const int WARNONAUDIOOUT = 2;

        /// UPTOCENTER -> 0x4
        public const int UPTOCENTER = 4;

        /// TRIGGERALARMOUT -> 0x8
        public const int TRIGGERALARMOUT = 8;

        /// YOULI -> 0
        public const int YOULI = 0;

        /// LILIN_1016 -> 1
        public const int LILIN_1016 = 1;

        /// LILIN_820 -> 2
        public const int LILIN_820 = 2;

        /// PELCO_P -> 3
        public const int PELCO_P = 3;

        /// DM_QUICKBALL -> 4
        public const int DM_QUICKBALL = 4;

        /// HD600 -> 5
        public const int HD600 = 5;

        /// JC4116 -> 6
        public const int JC4116 = 6;

        /// PELCO_DWX -> 7
        public const int PELCO_DWX = 7;

        /// PELCO_D -> 8
        public const int PELCO_D = 8;

        /// VCOM_VC_2000 -> 9
        public const int VCOM_VC_2000 = 9;

        /// NETSTREAMER -> 10
        public const int NETSTREAMER = 10;

        /// SAE -> 11
        public const int SAE = 11;

        /// SAMSUNG -> 12
        public const int SAMSUNG = 12;

        /// KALATEL_KTD_312 -> 13
        public const int KALATEL_KTD_312 = 13;

        /// CELOTEX -> 14
        public const int CELOTEX = 14;

        /// TLPELCO_P -> 15
        public const int TLPELCO_P = 15;

        /// TL_HHX2000 -> 16
        public const int TL_HHX2000 = 16;

        /// BBV -> 17
        public const int BBV = 17;

        /// RM110 -> 18
        public const int RM110 = 18;

        /// KC3360S -> 19
        public const int KC3360S = 19;

        /// ACES -> 20
        public const int ACES = 20;

        /// ALSON -> 21
        public const int ALSON = 21;

        /// INV3609HD -> 22
        public const int INV3609HD = 22;

        /// HOWELL -> 23
        public const int HOWELL = 23;

        /// TC_PELCO_P -> 24
        public const int TC_PELCO_P = 24;

        /// TC_PELCO_D -> 25
        public const int TC_PELCO_D = 25;

        /// AUTO_M -> 26
        public const int AUTO_M = 26;

        /// AUTO_H -> 27
        public const int AUTO_H = 27;

        /// ANTEN -> 28
        public const int ANTEN = 28;

        /// CHANGLIN -> 29
        public const int CHANGLIN = 29;

        /// DELTADOME -> 30
        public const int DELTADOME = 30;

        /// XYM_12 -> 31
        public const int XYM_12 = 31;

        /// ADR8060 -> 32
        public const int ADR8060 = 32;

        /// EVI -> 33
        public const int EVI = 33;

        /// Demo_Speed -> 34
        public const int Demo_Speed = 34;

        /// DM_PELCO_D -> 35
        public const int DM_PELCO_D = 35;

        /// ST_832 -> 36
        public const int ST_832 = 36;

        /// LC_D2104 -> 37
        public const int LC_D2104 = 37;

        /// HUNTER -> 38
        public const int HUNTER = 38;

        /// A01 -> 39
        public const int A01 = 39;

        /// TECHWIN -> 40
        public const int TECHWIN = 40;

        /// WEIHAN -> 41
        public const int WEIHAN = 41;

        /// LG -> 42
        public const int LG = 42;

        /// D_MAX -> 43
        public const int D_MAX = 43;

        /// PANASONIC -> 44
        public const int PANASONIC = 44;

        /// KTD_348 -> 45
        public const int KTD_348 = 45;

        /// INFINOVA -> 46
        public const int INFINOVA = 46;

        /// PIH_7625 -> 47
        public const int PIH_7625 = 47;

        /// IDOME_IVIEW_LCU -> 48
        public const int IDOME_IVIEW_LCU = 48;

        /// Dennar_dDome -> 49
        public const int Dennar_dDome = 49;

        /// Philips -> 50
        public const int Philips = 50;

        /// SAMPLE -> 51
        public const int SAMPLE = 51;

        /// PLD -> 52
        public const int PLD = 52;

        /// PARCO -> 53
        public const int PARCO = 53;

        /// HY -> 54
        public const int HY = 54;

        /// NAIJIE -> 55
        public const int NAIJIE = 55;

        /// CAT_KING -> 56
        public const int CAT_KING = 56;

        /// YH_06 -> 57
        public const int YH_06 = 57;

        /// SP9096X -> 58
        public const int SP9096X = 58;

        /// M_PANEL -> 59
        public const int M_PANEL = 59;

        /// M_MV2050 -> 60
        public const int M_MV2050 = 60;

        /// SAE_QUICKBALL -> 61
        public const int SAE_QUICKBALL = 61;

        /// RED_APPLE -> 62
        public const int RED_APPLE = 62;

        /// NKO8G -> 63
        public const int NKO8G = 63;

        /// DH_CC440 -> 64
        public const int DH_CC440 = 64;

        /// TX_CONTROL_232 -> 65
        public const int TX_CONTROL_232 = 65;

        /// VCL_SPEED_DOME -> 66
        public const int VCL_SPEED_DOME = 66;

        /// ST_2C160 -> 67
        public const int ST_2C160 = 67;

        /// TDWY -> 68
        public const int TDWY = 68;

        /// TWHC -> 69
        public const int TWHC = 69;

        /// USNT -> 70
        public const int USNT = 70;

        /// KLT_NVD2200PS -> 71
        public const int KLT_NVD2200PS = 71;

        /// VIDO_B01 -> 72
        public const int VIDO_B01 = 72;

        /// LG_MULTIX -> 73
        public const int LG_MULTIX = 73;

        /// ENKEL -> 74
        public const int ENKEL = 74;

        /// YT_PELCOD -> 75
        public const int YT_PELCOD = 75;

        /// HIKVISION -> 76
        public const int HIKVISION = 76;

        /// PE60 -> 77
        public const int PE60 = 77;

        /// LiAo -> 78
        public const int LiAo = 78;

        /// NK16 -> 79
        public const int NK16 = 79;

        /// DaLi -> 80
        public const int DaLi = 80;

        /// HN_4304 -> 81
        public const int HN_4304 = 81;

        /// VIDEOTEC -> 82
        public const int VIDEOTEC = 82;

        /// HNDCB -> 83
        public const int HNDCB = 83;

        /// Lion_2007 -> 84
        public const int Lion_2007 = 84;

        /// LG_LVC_C372 -> 85
        public const int LG_LVC_C372 = 85;

        /// Gold_Video -> 86
        public const int Gold_Video = 86;

        /// NVD1600PS -> 87
        public const int NVD1600PS = 87;

        /// TC615P -> 88
        public const int TC615P = 88;

        /// NANWANG -> 89
        public const int NANWANG = 89;

        /// NANWANG_1602 -> 90
        public const int NANWANG_1602 = 90;

        /// SIEMENS -> 91
        public const int SIEMENS = 91;

        /// WVCS850 -> 92
        public const int WVCS850 = 92;

        /// PHLIPS_2 -> 93
        public const int PHLIPS_2 = 93;

        /// PHLIPS_3 -> 94
        public const int PHLIPS_3 = 94;

        /// AD -> 95
        public const int AD = 95;

        /// TYCO_AD -> 96
        public const int TYCO_AD = 96;

        /// VICON -> 97
        public const int VICON = 97;

        /// TKC676 -> 98
        public const int TKC676 = 98;

        /// YAAN_NEW -> 99
        public const int YAAN_NEW = 99;

        /// DL_NVS_1Z -> 100
        public const int DL_NVS_1Z = 100;

        /// I3_Z1200 -> 101
        public const int I3_Z1200 = 101;

        /// I3_Z2200 -> 102
        public const int I3_Z2200 = 102;

        /// MAJOR_ALARM -> 0x1
        public const int MAJOR_ALARM = 1;

        /// MINOR_ALARM_IN -> 0x1
        public const int MINOR_ALARM_IN = 1;

        /// MINOR_ALARM_OUT -> 0x2
        public const int MINOR_ALARM_OUT = 2;

        /// MINOR_MOTDET_START -> 0x3
        public const int MINOR_MOTDET_START = 3;

        /// MINOR_MOTDET_STOP -> 0x4
        public const int MINOR_MOTDET_STOP = 4;

        /// MINOR_HIDE_ALARM_START -> 0x5
        public const int MINOR_HIDE_ALARM_START = 5;

        /// MINOR_HIDE_ALARM_STOP -> 0x6
        public const int MINOR_HIDE_ALARM_STOP = 6;

        /// MAJOR_EXCEPTION -> 0x2
        public const int MAJOR_EXCEPTION = 2;

        /// MINOR_VI_LOST -> 0x21
        public const int MINOR_VI_LOST = 33;

        /// MINOR_ILLEGAL_ACCESS -> 0x22
        public const int MINOR_ILLEGAL_ACCESS = 34;

        /// MINOR_HD_FULL -> 0x23
        public const int MINOR_HD_FULL = 35;

        /// MINOR_HD_ERROR -> 0x24
        public const int MINOR_HD_ERROR = 36;

        /// MINOR_DCD_LOST -> 0x25
        public const int MINOR_DCD_LOST = 37;

        /// MINOR_IP_CONFLICT -> 0x26
        public const int MINOR_IP_CONFLICT = 38;

        /// MINOR_NET_BROKEN -> 0x27
        public const int MINOR_NET_BROKEN = 39;

        /// MAJOR_OPERATION -> 0x3
        public const int MAJOR_OPERATION = 3;

        /// MINOR_START_DVR -> 0x41
        public const int MINOR_START_DVR = 65;

        /// MINOR_STOP_DVR -> 0x42
        public const int MINOR_STOP_DVR = 66;

        /// MINOR_STOP_ABNORMAL -> 0x43
        public const int MINOR_STOP_ABNORMAL = 67;

        /// MINOR_LOCAL_LOGIN -> 0x50
        public const int MINOR_LOCAL_LOGIN = 80;

        /// MINOR_LOCAL_LOGOUT -> 0x51
        public const int MINOR_LOCAL_LOGOUT = 81;

        /// MINOR_LOCAL_CFG_PARM -> 0x52
        public const int MINOR_LOCAL_CFG_PARM = 82;

        /// MINOR_LOCAL_PLAYBYFILE -> 0x53
        public const int MINOR_LOCAL_PLAYBYFILE = 83;

        /// MINOR_LOCAL_PLAYBYTIME -> 0x54
        public const int MINOR_LOCAL_PLAYBYTIME = 84;

        /// MINOR_LOCAL_START_REC -> 0x55
        public const int MINOR_LOCAL_START_REC = 85;

        /// MINOR_LOCAL_STOP_REC -> 0x56
        public const int MINOR_LOCAL_STOP_REC = 86;

        /// MINOR_LOCAL_PTZCTRL -> 0x57
        public const int MINOR_LOCAL_PTZCTRL = 87;

        /// MINOR_LOCAL_PREVIEW -> 0x58
        public const int MINOR_LOCAL_PREVIEW = 88;

        /// MINOR_LOCAL_MODIFY_TIME -> 0x59
        public const int MINOR_LOCAL_MODIFY_TIME = 89;

        /// MINOR_LOCAL_UPGRADE -> 0x5a
        public const int MINOR_LOCAL_UPGRADE = 90;

        /// MINOR_LOCAL_COPYFILE -> 0x5b
        public const int MINOR_LOCAL_COPYFILE = 91;

        /// MINOR_REMOTE_LOGIN -> 0x70
        public const int MINOR_REMOTE_LOGIN = 112;

        /// MINOR_REMOTE_LOGOUT -> 0x71
        public const int MINOR_REMOTE_LOGOUT = 113;

        /// MINOR_REMOTE_START_REC -> 0x72
        public const int MINOR_REMOTE_START_REC = 114;

        /// MINOR_REMOTE_STOP_REC -> 0x73
        public const int MINOR_REMOTE_STOP_REC = 115;

        /// MINOR_START_TRANS_CHAN -> 0x74
        public const int MINOR_START_TRANS_CHAN = 116;

        /// MINOR_STOP_TRANS_CHAN -> 0x75
        public const int MINOR_STOP_TRANS_CHAN = 117;

        /// MINOR_REMOTE_GET_PARM -> 0x76
        public const int MINOR_REMOTE_GET_PARM = 118;

        /// MINOR_REMOTE_CFG_PARM -> 0x77
        public const int MINOR_REMOTE_CFG_PARM = 119;

        /// MINOR_REMOTE_GET_STATUS -> 0x78
        public const int MINOR_REMOTE_GET_STATUS = 120;

        /// MINOR_REMOTE_ARM -> 0x79
        public const int MINOR_REMOTE_ARM = 121;

        /// MINOR_REMOTE_DISARM -> 0x7a
        public const int MINOR_REMOTE_DISARM = 122;

        /// MINOR_REMOTE_REBOOT -> 0x7b
        public const int MINOR_REMOTE_REBOOT = 123;

        /// MINOR_START_VT -> 0x7c
        public const int MINOR_START_VT = 124;

        /// MINOR_STOP_VT -> 0x7d
        public const int MINOR_STOP_VT = 125;

        /// MINOR_REMOTE_UPGRADE -> 0x7e
        public const int MINOR_REMOTE_UPGRADE = 126;

        /// MINOR_REMOTE_PLAYBYFILE -> 0x7f
        public const int MINOR_REMOTE_PLAYBYFILE = 127;

        /// MINOR_REMOTE_PLAYBYTIME -> 0x80
        public const int MINOR_REMOTE_PLAYBYTIME = 128;

        /// MINOR_REMOTE_PTZCTRL -> 0x81
        public const int MINOR_REMOTE_PTZCTRL = 129;

        /// PARA_VIDEOOUT -> 0x1
        public const int PARA_VIDEOOUT = 1;

        /// PARA_IMAGE -> 0x2
        public const int PARA_IMAGE = 2;

        /// PARA_ENCODE -> 0x4
        public const int PARA_ENCODE = 4;

        /// PARA_NETWORK -> 0x8
        public const int PARA_NETWORK = 8;

        /// PARA_ALARM -> 0x10
        public const int PARA_ALARM = 16;

        /// PARA_EXCEPTION -> 0x20
        public const int PARA_EXCEPTION = 32;

        /// PARA_DECODER -> 0x40
        public const int PARA_DECODER = 64;

        /// PARA_RS232 -> 0x80
        public const int PARA_RS232 = 128;

        /// PARA_PREVIEW -> 0x100
        public const int PARA_PREVIEW = 256;

        /// PARA_SECURITY -> 0x200
        public const int PARA_SECURITY = 512;

        /// PARA_DATETIME -> 0x400
        public const int PARA_DATETIME = 1024;

        /// PARA_FRAMETYPE -> 0x800
        public const int PARA_FRAMETYPE = 2048;

        /// NET_DEC_STARTDEC -> 1
        public const int NET_DEC_STARTDEC = 1;

        /// NET_DEC_STOPDEC -> 2
        public const int NET_DEC_STOPDEC = 2;

        /// NET_DEC_STOPCYCLE -> 3
        public const int NET_DEC_STOPCYCLE = 3;

        /// NET_DEC_CONTINUECYCLE -> 4
        public const int NET_DEC_CONTINUECYCLE = 4;

        /// PICNAME_ITEM_DEV_NAME -> 1
        public const int PICNAME_ITEM_DEV_NAME = 1;

        /// PICNAME_ITEM_DEV_NO -> 2
        public const int PICNAME_ITEM_DEV_NO = 2;

        /// PICNAME_ITEM_DEV_IP -> 3
        public const int PICNAME_ITEM_DEV_IP = 3;

        /// PICNAME_ITEM_CHAN_NAME -> 4
        public const int PICNAME_ITEM_CHAN_NAME = 4;

        /// PICNAME_ITEM_CHAN_NO -> 5
        public const int PICNAME_ITEM_CHAN_NO = 5;

        /// PICNAME_ITEM_TIME -> 6
        public const int PICNAME_ITEM_TIME = 6;

        /// PICNAME_ITEM_CARDNO -> 7
        public const int PICNAME_ITEM_CARDNO = 7;

        /// PICNAME_MAXITEM -> 15
        public const int PICNAME_MAXITEM = 15;


        #endregion

        /// <summary>
        /// 功能：初始化SDK 
        /// 返回值：TRUE表示成功，FALSE表示失败
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();
       
        /// <summary>
        /// 功能：释放SDK资源
        /// 返回值：TRUE表示成功，FALSE表示失败。
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nMessage"></param>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessage(uint nMessage, IntPtr hWnd);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fMessCallBack"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack(FMessCallBack fMessCallBack);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fMessCallBack_EX"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_EX(FMessCallBack_EX fMessCallBack_EX);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fMessCallBack_NEW"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_NEW(FMessCallBack_NEW fMessCallBack_NEW);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fMessageCallBack"></param>
        /// <param name="dwUser"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack(FMessageCallBack fMessageCallBack, ushort dwUser);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwWaitTime"></param>
        /// <param name="dwTryTimes"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConnectTime(ushort dwWaitTime, ushort dwTryTimes);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern ushort NET_DVR_GetSDKVersion();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern ushort NET_DVR_GetSDKBuildVersion();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_IsSupport();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sLocalIP"></param>
        /// <param name="wLocalPort"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StartListen(string sLocalIP, int wLocalPort);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDVRIP"></param>
        /// <param name="wDVRPort"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassint"></param>
        /// <param name="lpDeviceInfo"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_Login(string sDVRIP, int wDVRPort, string sUserName, string sPassint, ref NET_DVR_DEVICEINFO lpDeviceInfo);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lUserID"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout(long lUserID);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern ushort NET_DVR_GetLastError();

        /// <summary>
        /// <param name="colorKey">
        ///     用户设置的透明色，透明色相当于一层透视膜，显示的画面只能穿过这种颜色，而其他的颜色将
        ///     挡住显示的画面。用户应该在显示窗口中涂上这种颜色，那样才能看到显示画面。一般应该使
        ///     用一种不常用的颜色作为透明色。这是一个双字节值0x00rrggbb,最高字节为0，后三个字节分别表示r,g,b的值。
        /// </param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetShowMode(ushort dwShowType, int colorKey);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sServerIP"></param>
        /// <param name="wServerPort"></param>
        /// <param name="sDVRName"></param>
        /// <param name="wDVRNameLen"></param>
        /// <param name="sDVRSerialNumber"></param>
        /// <param name="wDVRSerialLen"></param>
        /// <param name="sGetIP"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr(string sServerIP, int wServerPort, IntPtr sDVRName, int wDVRNameLen, IntPtr sDVRSerialNumber, int wDVRSerialLen, byte[] sGetIP);

        
        /// <summary>
        /// 功能：启动图像实时预览
        /// 返回值：-1表示失败，其他值作为NET_DVR_StopRealPlay等函数的参数
        /// </summary>
        /// <param name="lUserID">用户登录ID，NET_DVR_Login的返回值</param>
        /// <param name="lpClientInfo">指向NET_DVR_CLIENTINFO结构的指针</param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_RealPlay(long lUserID, ref NET_DVR_CLIENTINFO lpClientInfo);
        
        /// <summary>
        /// 功能：关闭图像预览功能
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay的返回值</param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(long lRealHandle);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetRealPlayerIndex(long lRealHandle);
        //视频参数是索引值 1-10
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwBrightValue"></param>
        /// <param name="dwContrastValue"></param>
        /// <param name="dwSaturationValue"></param>
        /// <param name="dwHueValue"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetVideoEffect(long lRealHandle, ushort dwBrightValue, ushort dwContrastValue, ushort dwSaturationValue, ushort dwHueValue);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="pBrightValue"></param>
        /// <param name="pContrastValue"></param>
        /// <param name="pSaturationValue"></param>
        /// <param name="pHueValue"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetVideoEffect(long lRealHandle, ref ushort pBrightValue, ref ushort pContrastValue, ref ushort pSaturationValue, ref ushort pHueValue);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="fDrawFun"></param>
        /// <param name="dwUser"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterDrawFun(long lRealHandle, FDrawFun fDrawFun, ushort dwUser);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwBufNum"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayerBufNumber(long lRealHandle, ushort dwBufNum);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwNum"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ThrowBFrame(long lRealHandle, ushort dwNum);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwMode"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAudioMode(ushort dwMode);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound(long lRealHandle);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSoundShare(long lRealHandle);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSoundShare(long lRealHandle);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="wVolume"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_Volume(long lRealHandle, int wVolume);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData(long lRealHandle, string sFileName);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopSaveRealData(long lRealHandle);

        /// <summary>
        /// 功能：设置回调函数，用户自己处理客户端收到的数据
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay()的返回值</param>
        /// <param name="fRealDataCallBack">回调函数</param>
        /// <param name="dwUser">用户数据</param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRealDataCallBack(long lRealHandle, FRealDataCallBack fRealDataCallBack, ushort dwUser);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lUserID"></param>
        /// <param name="lChannel"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrame(long lUserID, long lChannel);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lUserID"></param>
        /// <param name="lChannel"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrameSub(long lUserID, long lChannel);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lPlayHandle"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshPlay(long lPlayHandle);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="sPicFileName"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture(long lRealHandle, string sPicFileName);

        
        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwPTZCommand"></param>
        /// <param name="dwStop"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl(long lRealHandle, ushort dwPTZCommand, ushort dwStop);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lUserID"></param>
        /// <param name="lChannel"></param>
        /// <param name="dwPTZCommand"></param>
        /// <param name="dwStop"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_Other(long lUserID, long lChannel, ushort dwPTZCommand, ushort dwStop);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="pPTZCodeBuf"></param>
        /// <param name="dwBufSize"></param>
        /// <returns></returns>
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ(long lRealHandle, IntPtr pPTZCodeBuf, ushort dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_Other(long lUserID, long lChannel, IntPtr pPTZCodeBuf, ushort dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset(long lRealHandle, ushort dwPTZPresetCmd, ushort dwPresetIndex);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_Other(long lUserID, long lChannel, ushort dwPTZPresetCmd, ushort dwPresetIndex);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_EX(long lRealHandle, ushort dwPTZCommand, ushort dwStop);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_EX(long lRealHandle, ushort dwPTZPresetCmd, ushort dwPresetIndex);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise(long lRealHandle, ushort dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, int wInput);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_Other(long lUserID, long lChannel, ushort dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, int wInput);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_EX(long lRealHandle, ushort dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, int wInput);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack(long lRealHandle, ushort dwPTZTrackCmd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_Other(long lUserID, long lChannel, ushort dwPTZTrackCmd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_EX(long lRealHandle, ushort dwPTZTrackCmd);
        //带速度的云台控制
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed(long lRealHandle, ushort dwPTZCommand, ushort dwStop, ushort dwSpeed);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_Other(long lUserID, long lChannel, ushort dwPTZCommand, ushort dwStop, ushort dwSpeed);
        //2007-05-11 IP快球
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCruise(long lUserID, long lChannel, long lCruiseRoute, ref NET_DVR_CRUISE_RET lpCruiseRet);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn(long lRealHandle, ref NET_DVR_POINT_FRAME pStruPointFrame);

        //文件回放
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_FindFile(long lUserID, long lChannel, ushort dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_FindNextFile(long lFindHandle, ref NET_DVR_FIND_DATA lpFindData);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose(long lFindHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_FindFileByCard(long lUserID, long lChannel, ushort dwFileType, bool bNeedCardNum, IntPtr sCardNumber, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_PlayBackByName(long lUserID, string sPlayBackFileName, IntPtr hWnd);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_PlayBackByTime(long lUserID, long lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, IntPtr hWnd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl(long lPlayHandle, ushort dwControlCode, ushort dwInValue, ref ushort lpOutValue);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBack(long lPlayHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack(long lPlayHandle, FPlayDataCallBack fPlayDataCallBack, ushort dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackSaveData(long lPlayHandle, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBackSave(long lPlayHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPlayBackOsdTime(long lPlayHandle, ref NET_DVR_TIME lpOsdTime);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackCaptureFile(long lPlayHandle, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_GetFileByName(long lUserID, string sDVRFileName, string sSavedFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_GetFileByTime(long lUserID, long lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, string sSavedFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetFile(long lFileHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadPos(long lFileHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPlayerIndex(long lPlayHandle);

        //恢复默认值
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(long lUserID);
        //保存参数
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveConfig(long lUserID);
        //重启
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(long lUserID);
        //关闭DVR
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(long lUserID);
        //升级
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_Upgrade(long lUserID, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeState(long lUpgradeHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseUpgradeHandle(long lUpgradeHandle);
        //远程格式化硬盘
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_FormatDisk(long lUserID, long lDiskNumber);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(long lFormatHandle, ref long pCurrentFormatDisk, ref long pCurrentDiskPos, ref long pFormatStatic);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(long lFormatHandle);
        //报警
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_SetupAlarmChan(long lUserID);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan(long lAlarmHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut(long lUserID, ref NET_DVR_ALARMOUTSTATUS lpAlarmOutState);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmOut(long lUserID, long lAlarmOutPort, long lAlarmOutStatic);
        //语音对讲
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_StartVoiceCom(long lUserID, FVoiceDataCallBack fVoiceDataCallBack, ushort dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVoiceComClientVolume(long lVoiceComHandle, int wVolume);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopVoiceCom(long lVoiceComHandle);
        //语音广播
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStop();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_AddDVR(long lUserID);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR(long lUserID);
        //语音转发
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_StartVoiceCom_MR(long lUserID, FVoiceDataCallBack fVoiceDataCallBack, ushort dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(long lVoiceComHandle, IntPtr pSendBuf, ushort dwBufSize);
        ////////////////////////////////////////////////////////////
        //透明通道设置
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_SerialStart(long lUserID, long lSerialPort, FSerialDataCallBack fSerialDataCallBack, ushort dwUser);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(long lSerialHandle, long lChannel, IntPtr pSendBuf, ushort dwBufSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(long lSerialHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(long lUserID, IntPtr pSendBuf, ushort dwBufSize);
        //远程控制本地显示
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClickKey(long lUserID, long lKeyIndex);
        //远程控制手动录像
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(long lUserID, long lChannel, long lRecordType);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(long lUserID, long lChannel);
        //解码卡
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDevice_Card(ref long pDeviceTotalChan);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDevice_Card();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDraw_Card(IntPtr hParent, int colorKey);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDraw_Card();
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_RealPlay_Card(long lUserID, ref NET_DVR_CARDINFO lpCardInfo, long lChannelNum);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ResetPara_Card(long lRealHandle, ref NET_DVR_DISPLAY_PARA lpDisplayPara);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshSurface_Card();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClearSurface_Card();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreSurface_Card();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound_Card(long lRealHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound_Card(long lRealHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVolume_Card(long lRealHandle, int wVolume);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_AudioPreview_Card(long lRealHandle, bool bEnable);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture_Card(long lRealHandle, string sPicFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDspErrMsg_Card(uint nMessage, IntPtr hWnd);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ResetDSP_Card(long lChannelNum);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSerialNum_Card(long lChannelNum, ref ushort pDeviceSerialNo);
        [DllImport("HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_GetChanHandle_Card(long lRealHandle);

        //服务器状态
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState(long lUserID, ref NET_DVR_WORKSTATE lpWorkState);
        //日志
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_FindDVRLog(long lUserID, long lSelectMode, ushort dwMajorType, ushort dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_FindNextLog(long lLogHandle, ref NET_DVR_LOG lpLogData);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose(long lLogHandle);
        //参数设置
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRConfig(long lUserID, ushort dwCommand, long lChannel, ref IntPtr lpOutBuffer, ushort dwOutBufferSize, ref ushort lpBytesReturned);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRConfig(long lUserID, ushort dwCommand, long lChannel, ref IntPtr lpInBuffer, ushort dwInBufferSize);

        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat(long lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat(long lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        //解码
        [DllImport("HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_InitG722Decoder(int nBitrate);
        [DllImport("HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Decoder(IntPtr pDecHandle);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG722Frame(IntPtr pDecHandle, IntPtr pInBuffer, IntPtr pOutBuffer);
        //编码
        [DllImport("HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_InitG722Encoder();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG722Frame(IntPtr pEncodeHandle, IntPtr pInBuffer, IntPtr pOutBuffer);
        [DllImport("HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Encoder(IntPtr pEncodeHandle);
        //参数设置
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile(long lUserID, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile(long lUserID, string sFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_EX(long lUserID, string sOutBuffer, ushort dwOutSize);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile_EX(long lUserID, string sInBuffer, ushort dwInSize);
        //6001D/F解码设备接口
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecInfo(long lUserID, long lChannel, ref NET_DVR_DECCFG lpDecoderinfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecInfo(long lUserID, long lChannel, ref NET_DVR_DECCFG lpDecoderinfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecTransPort(long lUserID, ref NET_DVR_PORTCFG lpTransPort);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecTransPort(long lUserID, ref NET_DVR_PORTCFG lpTransPort);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_DecPlayBackCtrl(long lUserID, long lChannel, ushort dwControlCode, ushort dwInValue, ref ushort lpOutValue, ref NET_DVR_PLAYREMOTEFILE lpRemoteFileInfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecSpecialCon(long lUserID, long lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecSpecialCon(long lUserID, long lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlDec(long lUserID, long lChannel, ushort dwControlCode);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlScreen(long lUserID, long lChannel, ushort dwControl);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecCurLinkStatus(long lUserID, long lChannel, ref NET_DVR_DECSTATUS lpDecStatus);

        //JPEG抓图
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture(long lUserID, long lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sPicFileName);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture_NEW(long lUserID, long lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sJpegPicBuffer, ushort dwPicSize, ref ushort lpSizeReturned);

        //设置ATM PORT配置
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetATMPortCFG(long lUserID, int wATMPort);
        //获取ATM PORT配置
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetATMPortCFG(long lUserID, IntPtr lpOutATMPort);

        //增加是否启用缩放接口
        //设置缩放配置
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG(long lUserID, ushort dwScale);
        //获取缩放配置
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG(long lUserID, ref ushort lpOutScale);
        //支持显卡
        //#if (WINVER > 0x0400)
        //Note: These funtion must be builded under win2000 or above with Microsoft Platform sdk.
        //	    You can download the sdk from "http://www.microsoft.com/msdownload/platformsdk/sdkupdate/";
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDrawDevice();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDrawDevice();
        [DllImport("HCNetSDK.dll")]
        public static extern long NET_DVR_GetDDrawDeviceTotalNums();
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDDrawDevice(long lPlayPort, ushort nDeviceNum);
        //#endif

        //多路解码器
        //2007-11-30 V211支持以下接口
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic(long lUserID, ushort dwDecChanNum, ref NET_DVR_MATRIX_DYNAMIC_DEC lpDynamicInfo);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopDynamic(long lUserID, ushort dwDecChanNum);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo(long lUserID, ushort dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_INFO lpInter);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo(long lUserID, ushort dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo(long lUserID, ushort dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanEnable(long lUserID, ushort dwDecChanNum, ushort dwEnable);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanEnable(long lUserID, ushort dwDecChanNum, ref ushort lpdwEnable);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecEnable(long lUserID, ref ushort lpdwEnable);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDecChanEnable(long lUserID, ushort dwDecChanNum, ushort dwEnable);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanEnable(long lUserID, ushort dwDecChanNum, ref ushort lpdwEnable);
        [DllImport("HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanStatus(long lUserID, ushort dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_STATUS lpInter);
    }

    public enum Anonymous_78c36191_33ae_4fb8_ad60_d2d5bbbc8da8
    {

        /// NORMALMODE -> 0
        NORMALMODE = 0,

        OVERLAYMODE,
    }

    public enum Anonymous_adfc8a4b_b39b_4106_82eb_490717b847b3
    {

        PTOPTCPMODE,

        PTOPUDPMODE,

        MULTIMODE,

        RTPMODE,

        AUDIODETACH,

        NOUSEMODE,
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DEVICEINFO
    {

        /// BYTE[48]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sSerialNumber;

        /// BYTE->unsigned char
        public byte byAlarmInPortNum;

        /// BYTE->unsigned char
        public byte byAlarmOutPortNum;

        /// BYTE->unsigned char
        public byte byDiskNum;

        /// BYTE->unsigned char
        public byte byDVRType;

        /// BYTE->unsigned char
        public byte byChanNum;

        /// BYTE->unsigned char
        public byte byStartChan;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DISPLAY_PARA
    {

        /// int
        public int bToScreen;

        /// int
        public int bToVideoOut;

        /// int
        public int nLeft;

        /// int
        public int nTop;

        /// int
        public int nWidth;

        /// int
        public int nHeight;

        /// int
        public int nReserved;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_CLIENTINFO
    {

        /// long->int
        public int lChannel;

        /// long->int
        public int lLinkMode;

        /// HWND->HWND__*
        public System.IntPtr hPlayWnd;

        /// char*
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string sMultiCastIP;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_CARDINFO
    {

        /// long->int
        public int lChannel;

        /// long->int
        public int lLinkMode;

        /// char*
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string sMultiCastIP;

        /// NET_DVR_DISPLAY_PARA->Anonymous_da048e16_9d5f_457b_8511_a789eb69a6ea
        public NET_DVR_DISPLAY_PARA struDisplayPara;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_TIME
    {

        /// ushort->unsigned int
        public uint dwYear;

        /// ushort->unsigned int
        public uint dwMonth;

        /// ushort->unsigned int
        public uint dwDay;

        /// ushort->unsigned int
        public uint dwHour;

        /// ushort->unsigned int
        public uint dwMinute;

        /// ushort->unsigned int
        public uint dwSecond;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_SCHEDTIME
    {

        /// BYTE->unsigned char
        public byte byStartHour;

        /// BYTE->unsigned char
        public byte byStartMin;

        /// BYTE->unsigned char
        public byte byStopHour;

        /// BYTE->unsigned char
        public byte byStopMin;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_HANDLEEXCEPTION
    {

        /// ushort->unsigned int
        public uint dwHandleType;

        /// BYTE[4]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byRelAlarmOut;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_DEVICECFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sDVRName;

        /// ushort->unsigned int
        public uint dwDVRID;

        /// ushort->unsigned int
        public uint dwRecycleRecord;

        /// BYTE[48]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sSerialNumber;

        /// ushort->unsigned int
        public uint dwSoftwareVersion;

        /// ushort->unsigned int
        public uint dwSoftwareBuildDate;

        /// ushort->unsigned int
        public uint dwDSPSoftwareVersion;

        /// ushort->unsigned int
        public uint dwDSPSoftwareBuildDate;

        /// ushort->unsigned int
        public uint dwPanelVersion;

        /// ushort->unsigned int
        public uint dwHardwareVersion;

        /// BYTE->unsigned char
        public byte byAlarmInPortNum;

        /// BYTE->unsigned char
        public byte byAlarmOutPortNum;

        /// BYTE->unsigned char
        public byte byRS232Num;

        /// BYTE->unsigned char
        public byte byRS485Num;

        /// BYTE->unsigned char
        public byte byNetworkPortNum;

        /// BYTE->unsigned char
        public byte byDiskCtrlNum;

        /// BYTE->unsigned char
        public byte byDiskNum;

        /// BYTE->unsigned char
        public byte byDVRType;

        /// BYTE->unsigned char
        public byte byChanNum;

        /// BYTE->unsigned char
        public byte byStartChan;

        /// BYTE->unsigned char
        public byte byDecordChans;

        /// BYTE->unsigned char
        public byte byVGANum;

        /// BYTE->unsigned char
        public byte byUSBNum;

        /// char[3]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 3)]
        public string reservedData;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_ETHERNET
    {

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDVRIP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDVRIPMask;

        /// ushort->unsigned int
        public uint dwNetInterface;

        /// int->unsigned short
        public ushort wDVRPort;

        /// BYTE[6]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byMACAddr;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_NETCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_ETHERNET[2]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_ETHERNET[] struEtherNet;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sManageHostIP;

        /// int->unsigned short
        public ushort wManageHostPort;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sIPServerIP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sMultiCastIP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sGatewayIP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sNFSIP;

        /// BYTE[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sNFSDirectory;

        /// ushort->unsigned int
        public uint dwPPPOE;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sPPPoEUser;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sPPPoEPassint;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sPPPoEIP;

        /// int->unsigned short
        public ushort wHttpPort;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_MOTION
    {

        /// BYTE[396]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 396, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byMotionScope;

        /// BYTE->unsigned char
        public byte byMotionSensitive;

        /// BYTE->unsigned char
        public byte byEnableHandleMotion;

        /// char[2]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 2)]
        public string reservedData;

        /// NET_DVR_HANDLEEXCEPTION->Anonymous_549bf980_460b_45cb_bf32_5c9331c7aa28
        public NET_DVR_HANDLEEXCEPTION strMotionHandleType;

        /// NET_DVR_SCHEDTIME[28]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_SCHEDTIME[] struAlarmTime;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byRelRecordChan;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_HIDEALARM
    {

        /// ushort->unsigned int
        public uint dwEnableHideAlarm;

        /// int->unsigned short
        public ushort wHideAlarmAreaTopLeftX;

        /// int->unsigned short
        public ushort wHideAlarmAreaTopLeftY;

        /// int->unsigned short
        public ushort wHideAlarmAreaWidth;

        /// int->unsigned short
        public ushort wHideAlarmAreaHeight;

        /// NET_DVR_HANDLEEXCEPTION->Anonymous_549bf980_460b_45cb_bf32_5c9331c7aa28
        public NET_DVR_HANDLEEXCEPTION strHideAlarmHandleType;

        /// NET_DVR_SCHEDTIME[28]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_SCHEDTIME[] struAlarmTime;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_VILOST
    {

        /// BYTE->unsigned char
        public byte byEnableHandleVILost;

        /// NET_DVR_HANDLEEXCEPTION->Anonymous_549bf980_460b_45cb_bf32_5c9331c7aa28
        public NET_DVR_HANDLEEXCEPTION strVILostHandleType;

        /// NET_DVR_SCHEDTIME[28]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_SCHEDTIME[] struAlarmTime;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_PICCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sChanName;

        /// ushort->unsigned int
        public uint dwVideoFormat;

        /// BYTE->unsigned char
        public byte byBrightness;

        /// BYTE->unsigned char
        public byte byContrast;

        /// BYTE->unsigned char
        public byte bySaturation;

        /// BYTE->unsigned char
        public byte byHue;

        /// ushort->unsigned int
        public uint dwShowChanName;

        /// int->unsigned short
        public ushort wShowNameTopLeftX;

        /// int->unsigned short
        public ushort wShowNameTopLeftY;

        /// NET_DVR_VILOST->Anonymous_ad99df4b_6ede_45c9_9848_c58cdcaf4cb1
        public NET_DVR_VILOST struVILost;

        /// NET_DVR_MOTION->Anonymous_4fa89551_3041_4de7_b55d_b126fcca808c
        public NET_DVR_MOTION struMotion;

        /// NET_DVR_HIDEALARM->Anonymous_072011e6_2e87_4893_8d6c_60bae74c801b
        public NET_DVR_HIDEALARM struHideAlarm;

        /// ushort->unsigned int
        public uint dwEnableHide;

        /// int->unsigned short
        public ushort wHideAreaTopLeftX;

        /// int->unsigned short
        public ushort wHideAreaTopLeftY;

        /// int->unsigned short
        public ushort wHideAreaWidth;

        /// int->unsigned short
        public ushort wHideAreaHeight;

        /// ushort->unsigned int
        public uint dwShowOsd;

        /// int->unsigned short
        public ushort wOSDTopLeftX;

        /// int->unsigned short
        public ushort wOSDTopLeftY;

        /// BYTE->unsigned char
        public byte byOSDType;

        /// BYTE->unsigned char
        public byte byDispWeek;

        /// BYTE->unsigned char
        public byte byOSDAttrib;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_SHELTER
    {

        /// int->unsigned short
        public ushort wHideAreaTopLeftX;

        /// int->unsigned short
        public ushort wHideAreaTopLeftY;

        /// int->unsigned short
        public ushort wHideAreaWidth;

        /// int->unsigned short
        public ushort wHideAreaHeight;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_PICCFG_EX
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sChanName;

        /// ushort->unsigned int
        public uint dwVideoFormat;

        /// BYTE->unsigned char
        public byte byBrightness;

        /// BYTE->unsigned char
        public byte byContrast;

        /// BYTE->unsigned char
        public byte bySaturation;

        /// BYTE->unsigned char
        public byte byHue;

        /// ushort->unsigned int
        public uint dwShowChanName;

        /// int->unsigned short
        public ushort wShowNameTopLeftX;

        /// int->unsigned short
        public ushort wShowNameTopLeftY;

        /// NET_DVR_VILOST->Anonymous_ad99df4b_6ede_45c9_9848_c58cdcaf4cb1
        public NET_DVR_VILOST struVILost;

        /// NET_DVR_MOTION->Anonymous_4fa89551_3041_4de7_b55d_b126fcca808c
        public NET_DVR_MOTION struMotion;

        /// NET_DVR_HIDEALARM->Anonymous_072011e6_2e87_4893_8d6c_60bae74c801b
        public NET_DVR_HIDEALARM struHideAlarm;

        /// ushort->unsigned int
        public uint dwEnableHide;

        /// NET_DVR_SHELTER[4]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_SHELTER[] struShelter;

        /// ushort->unsigned int
        public uint dwShowOsd;

        /// int->unsigned short
        public ushort wOSDTopLeftX;

        /// int->unsigned short
        public ushort wOSDTopLeftY;

        /// BYTE->unsigned char
        public byte byOSDType;

        /// BYTE->unsigned char
        public byte byDispWeek;

        /// BYTE->unsigned char
        public byte byOSDAttrib;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_COMPRESSION_INFO
    {

        /// BYTE->unsigned char
        public byte byStreamType;

        /// BYTE->unsigned char
        public byte byResolution;

        /// BYTE->unsigned char
        public byte byBitrateType;

        /// BYTE->unsigned char
        public byte byPicQuality;

        /// ushort->unsigned int
        public uint dwVideoBitrate;

        /// ushort->unsigned int
        public uint dwVideoFrameRate;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_COMPRESSION_INFO_EX
    {

        /// BYTE->unsigned char
        public byte byStreamType;

        /// BYTE->unsigned char
        public byte byResolution;

        /// BYTE->unsigned char
        public byte byBitrateType;

        /// BYTE->unsigned char
        public byte byPicQuality;

        /// ushort->unsigned int
        public uint dwVideoBitrate;

        /// ushort->unsigned int
        public uint dwVideoFrameRate;

        /// int->unsigned short
        public ushort wIntervalFrameI;

        /// BYTE->unsigned char
        public byte byIntervalBPFrame;

        /// BYTE->unsigned char
        public byte byRes;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_COMPRESSIONCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_COMPRESSION_INFO->Anonymous_010224a0_49be_4851_8f82_32e23a8e6a50
        public NET_DVR_COMPRESSION_INFO struRecordPara;

        /// NET_DVR_COMPRESSION_INFO->Anonymous_010224a0_49be_4851_8f82_32e23a8e6a50
        public NET_DVR_COMPRESSION_INFO struNetPara;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_COMPRESSIONCFG_EX
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_COMPRESSION_INFO_EX->Anonymous_f53e5770_0702_4180_a95a_61a496a37ee0
        public NET_DVR_COMPRESSION_INFO_EX struRecordPara;

        /// NET_DVR_COMPRESSION_INFO_EX->Anonymous_f53e5770_0702_4180_a95a_61a496a37ee0
        public NET_DVR_COMPRESSION_INFO_EX struNetPara;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_RECORDSCHED
    {

        /// NET_DVR_SCHEDTIME->Anonymous_4cfcd975_9900_40a3_8bb9_53c7050bda82
        public NET_DVR_SCHEDTIME struRecordTime;

        /// BYTE->unsigned char
        public byte byRecordType;

        /// char[3]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 3)]
        public string reservedData;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_RECORDDAY
    {

        /// int->unsigned short
        public ushort wAllDayRecord;

        /// BYTE->unsigned char
        public byte byRecordType;

        /// char
        public byte reservedData;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_RECORD
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// ushort->unsigned int
        public uint dwRecord;

        /// NET_DVR_RECORDDAY[7]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_RECORDDAY[] struRecAllDay;

        /// NET_DVR_RECORDSCHED[28]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_RECORDSCHED[] struRecordSched;

        /// ushort->unsigned int
        public uint dwRecordTime;

        /// ushort->unsigned int
        public uint dwPreRecordTime;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DECODERCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// ushort->unsigned int
        public uint dwBaudRate;

        /// BYTE->unsigned char
        public byte byDataBit;

        /// BYTE->unsigned char
        public byte byStopBit;

        /// BYTE->unsigned char
        public byte byParity;

        /// BYTE->unsigned char
        public byte byFlowcontrol;

        /// int->unsigned short
        public ushort wDecoderType;

        /// int->unsigned short
        public ushort wDecoderAddress;

        /// BYTE[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] bySetPreset;

        /// BYTE[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] bySetCruise;

        /// BYTE[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] bySetTrack;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_PPPCFG
    {

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sRemoteIP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sLocalIP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sLocalIPMask;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sUsername;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sPassint;

        /// BYTE->unsigned char
        public byte byPPPMode;

        /// BYTE->unsigned char
        public byte byRedial;

        /// BYTE->unsigned char
        public byte byRedialMode;

        /// BYTE->unsigned char
        public byte byDataEncrypt;

        /// ushort->unsigned int
        public uint dwMTU;

        /// char[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
        public string sTelephoneNumber;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_RS232CFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// ushort->unsigned int
        public uint dwBaudRate;

        /// BYTE->unsigned char
        public byte byDataBit;

        /// BYTE->unsigned char
        public byte byStopBit;

        /// BYTE->unsigned char
        public byte byParity;

        /// BYTE->unsigned char
        public byte byFlowcontrol;

        /// ushort->unsigned int
        public uint dwWorkMode;

        /// NET_DVR_PPPCFG->Anonymous_cf9919e9_b173_4df1_a5e3_cae5f42f4fd6
        public NET_DVR_PPPCFG struPPPConfig;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_ALARMINCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sAlarmInName;

        /// BYTE->unsigned char
        public byte byAlarmType;

        /// BYTE->unsigned char
        public byte byAlarmInHandle;

        /// NET_DVR_HANDLEEXCEPTION->Anonymous_549bf980_460b_45cb_bf32_5c9331c7aa28
        public NET_DVR_HANDLEEXCEPTION struAlarmHandleType;

        /// NET_DVR_SCHEDTIME[28]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_SCHEDTIME[] struAlarmTime;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byRelRecordChan;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byEnablePreset;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byPresetNo;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byEnableCruise;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byCruiseNo;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byEnablePtzTrack;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byPTZTrack;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_ALARMINFO
    {

        /// ushort->unsigned int
        public uint dwAlarmType;

        /// ushort->unsigned int
        public uint dwAlarmInputNumber;

        /// ushort[4]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwAlarmOutputNumber;

        /// ushort[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwAlarmRelateChannel;

        /// ushort[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwChannel;

        /// ushort[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwDiskNumber;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_ALARMOUTCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sAlarmOutName;

        /// ushort->unsigned int
        public uint dwAlarmOutDelay;

        /// NET_DVR_SCHEDTIME[28]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_SCHEDTIME[] struAlarmOutTime;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_PREVIEWCFG
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// BYTE->unsigned char
        public byte byPreviewNumber;

        /// BYTE->unsigned char
        public byte byEnableAudio;

        /// int->unsigned short
        public ushort wSwitchTime;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] bySwitchSeq;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_VGAPARA
    {

        /// int->unsigned short
        public ushort wResolution;

        /// int->unsigned short
        public ushort wFreq;

        /// ushort->unsigned int
        public uint dwBrightness;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_MATRIXPARA
    {

        /// int->unsigned short
        public ushort wDisplayLogo;

        /// int->unsigned short
        public ushort wDisplayOsd;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_VOOUT
    {

        /// BYTE->unsigned char
        public byte byVideoFormat;

        /// BYTE->unsigned char
        public byte byMenuAlphaValue;

        /// int->unsigned short
        public ushort wScreenSaveTime;

        /// int->unsigned short
        public ushort wVOffset;

        /// int->unsigned short
        public ushort wBrightness;

        /// BYTE->unsigned char
        public byte byStartMode;

        /// char
        public byte reservedData;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_VIDEOOUT
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_VOOUT[2]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_VOOUT[] struVOOut;

        /// NET_DVR_VGAPARA[1]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_VGAPARA[] struVGAPara;

        /// NET_DVR_MATRIXPARA->Anonymous_9d93b02a_11cb_4bec_bdf8_7d28d853cb79
        public NET_DVR_MATRIXPARA struMatrixPara;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_USER_INFO
    {

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sUserName;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sPassint;

        /// ushort[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwLocalRight;

        /// ushort[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwRemoteRight;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sUserIP;

        /// BYTE[6]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byMACAddr;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_USER
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_USER_INFO[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_USER_INFO[] struUser;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_USER_INFO_EX
    {

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sUserName;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] sPassint;

        /// ushort[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwLocalRight;

        /// ushort->unsigned int
        public uint dwLocalPlaybackRight;

        /// ushort[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwRemoteRight;

        /// ushort->unsigned int
        public uint dwNetPreviewRight;

        /// ushort->unsigned int
        public uint dwNetPlaybackRight;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sUserIP;

        /// BYTE[6]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byMACAddr;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_USER_EX
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_USER_INFO_EX[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_USER_INFO_EX[] struUser;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_EXCEPTION
    {

        /// ushort->unsigned int
        public uint dwSize;

        /// NET_DVR_HANDLEEXCEPTION[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_HANDLEEXCEPTION[] struExceptionHandleType;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_FIND_DATA
    {

        /// char[100]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 100)]
        public string sFileName;

        /// NET_DVR_TIME->Anonymous_511f1082_0590_412c_a00c_f609eed9eef4
        public NET_DVR_TIME struStartTime;

        /// NET_DVR_TIME->Anonymous_511f1082_0590_412c_a00c_f609eed9eef4
        public NET_DVR_TIME struStopTime;

        /// ushort->unsigned int
        public uint dwFileSize;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_CHANNELSTATE
    {

        /// BYTE->unsigned char
        public byte byRecordStatic;

        /// BYTE->unsigned char
        public byte bySignalStatic;

        /// BYTE->unsigned char
        public byte byHardwareStatic;

        /// char
        public byte reservedData;

        /// ushort->unsigned int
        public uint dwBitRate;

        /// ushort->unsigned int
        public uint dwLinkNum;

        /// ushort[6]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint[] dwClientIP;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DISKSTATE
    {

        /// ushort->unsigned int
        public uint dwVolume;

        /// ushort->unsigned int
        public uint dwFreeSpace;

        /// ushort->unsigned int
        public uint dwHardDiskStatic;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_WORKSTATE
    {

        /// ushort->unsigned int
        public uint dwDeviceStatic;

        /// NET_DVR_DISKSTATE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_DISKSTATE[] struHardDiskStatic;

        /// NET_DVR_CHANNELSTATE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_CHANNELSTATE[] struChanStatic;

        /// BYTE[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byAlarmInStatic;

        /// BYTE[4]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byAlarmOutStatic;

        /// ushort->unsigned int
        public uint dwLocalDisplay;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HWND__
    {

        /// int
        public int unused;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_REDIRECTORINFO
    {

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sRedirectorIP;

        /// WORD->unsigned short
        public ushort wRedirectorPort;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_ALARMOUTSTATUS
    {

        /// BYTE[]
        public byte[] Output;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_TRADEINFO
    {

        /// USHORT->unsigned short
        public ushort m_Year;

        /// USHORT->unsigned short
        public ushort m_Month;

        /// USHORT->unsigned short
        public ushort m_Day;

        /// USHORT->unsigned short
        public ushort m_Hour;

        /// USHORT->unsigned short
        public ushort m_Minute;

        /// USHORT->unsigned short
        public ushort m_Second;

        /// BYTE[24]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] DeviceName;

        /// DWORD->unsigned int
        public uint dwChannelNumer;

        /// BYTE[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] CardNumber;

        /// char[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 12)]
        public string cTradeType;

        /// DWORD->unsigned int
        public uint dwCash;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_FRAMETYPECODE
    {

        /// BYTE[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] code;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_FRAMEFORMAT
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sATMIP;

        /// DWORD->unsigned int
        public uint dwATMType;

        /// DWORD->unsigned int
        public uint dwInputMode;

        /// DWORD->unsigned int
        public uint dwFrameSignBeginPos;

        /// DWORD->unsigned int
        public uint dwFrameSignLength;

        /// BYTE[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byFrameSignContent;

        /// DWORD->unsigned int
        public uint dwCardLengthInfoBeginPos;

        /// DWORD->unsigned int
        public uint dwCardLengthInfoLength;

        /// DWORD->unsigned int
        public uint dwCardNumberInfoBeginPos;

        /// DWORD->unsigned int
        public uint dwCardNumberInfoLength;

        /// DWORD->unsigned int
        public uint dwBusinessTypeBeginPos;

        /// DWORD->unsigned int
        public uint dwBusinessTypeLength;

        /// NET_DVR_FRAMETYPECODE[10]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public NET_DVR_FRAMETYPECODE[] frameTypeCode;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_DECCHANINFO
    {

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDVRIP;

        /// WORD->unsigned short
        public ushort wDVRPort;

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;

        /// BYTE->unsigned char
        public byte byChannel;

        /// BYTE->unsigned char
        public byte byLinkMode;

        /// BYTE->unsigned char
        public byte byLinkType;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DECINFO
    {

        /// BYTE->unsigned char
        public byte byPoolChans;

        /// NET_DVR_DECCHANINFO[]
        public NET_DVR_DECCHANINFO[] struchanConInfo;

        /// BYTE->unsigned char
        public byte byEnablePoll;

        /// BYTE->unsigned char
        public byte byPoolTime;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DECCFG
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// DWORD->unsigned int
        public uint dwDecChanNum;

        /// NET_DVR_DECINFO[]
        public NET_DVR_DECINFO[] struDecInfo;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_PORTINFO
    {

        /// DWORD->unsigned int
        public uint dwEnableTransPort;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDecoderIP;

        /// WORD->unsigned short
        public ushort wDecoderPort;

        /// WORD->unsigned short
        public ushort wDVRTransPort;

        /// char[4]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 4)]
        public string cReserve;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_PORTCFG
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// NET_DVR_PORTINFO[]
        public NET_DVR_PORTINFO[] struTransPortInfo;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct Anonymous_b71b25fd_06c1_4e1a_af6e_82b0b7906ea7
    {

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;

        /// char[52]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 52)]
        public string cReserve;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_221f8282_3e1d_4517_9d27_874070a62875
    {

        /// BYTE[100]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] fileName;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_SHOWSTRINGINFO
    {

        /// WORD->unsigned short
        public ushort wShowString;

        /// WORD->unsigned short
        public ushort wStringSize;

        /// WORD->unsigned short
        public ushort wShowStringTopLeftX;

        /// WORD->unsigned short
        public ushort wShowStringTopLeftY;

        /// char[44]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 44)]
        public string sString;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_SHOWSTRING
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// NET_DVR_SHOWSTRINGINFO[]
        public NET_DVR_SHOWSTRINGINFO[] struStringInfo;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_FTPCFG
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// DWORD->unsigned int
        public uint dwEnableFTP;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sFTPIP;

        /// DWORD->unsigned int
        public uint dwFTPPort;

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;

        /// DWORD->unsigned int
        public uint dwDirLevel;

        /// WORD->unsigned short
        public ushort wTopDirMode;

        /// WORD->unsigned short
        public ushort wSubDirMode;

        /// BYTE[24]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] reservedData;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_PICTURE_NAME
    {

        /// BYTE[15]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byItemOrder;

        /// BYTE->unsigned char
        public byte byDelimiter;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_JPEGPARA
    {

        /// WORD->unsigned short
        public ushort wPicSize;

        /// WORD->unsigned short
        public ushort wPicQuality;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_SERIAL_CATCHPIC_PARA
    {

        /// BYTE->unsigned char
        public byte byStrFlag;

        /// BYTE->unsigned char
        public byte byEndFlag;

        /// WORD->unsigned short
        public ushort wCardIdx;

        /// DWORD->unsigned int
        public uint dwCardLen;

        /// DWORD->unsigned int
        public uint dwTriggerPicChans;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_JPEGCFG
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// NET_DVR_JPEGPARA[]
        public NET_DVR_JPEGPARA[] struJpegPara;

        /// WORD->unsigned short
        public ushort wBurstMode;

        /// WORD->unsigned short
        public ushort wUploadInterval;

        /// NET_DVR_PICTURE_NAME->Anonymous_825752b6_e5dc_4fb4_81d9_a201855c2783
        public NET_DVR_PICTURE_NAME struPicNameRule;

        /// BYTE->unsigned char
        public byte bySaveToHD;

        /// BYTE->unsigned char
        public byte res1;

        /// WORD->unsigned short
        public ushort wCatchInterval;

        /// BYTE[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] res2;

        /// NET_DVR_SERIAL_CATCHPIC_PARA->Anonymous_5276fc02_c0dd_4e05_ba43_8b414f57f140
        public NET_DVR_SERIAL_CATCHPIC_PARA struRs232Cfg;

        /// NET_DVR_SERIAL_CATCHPIC_PARA->Anonymous_5276fc02_c0dd_4e05_ba43_8b414f57f140
        public NET_DVR_SERIAL_CATCHPIC_PARA struRs485Cfg;

        /// DWORD[]
        public uint[] dwTriggerPicTimes;

        /// DWORD[]
        public uint[] dwAlarmInPicChanTriggered;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_GETJPEG
    {

        /// DWORD->unsigned int
        public uint dwReturn;

        /// DWORD->unsigned int
        public uint dwUploadFtp;

        /// DWORD->unsigned int
        public uint dwSaveHd;

        /// char[]
        //[System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 200)]
        public string sImageName;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sOsdStr;

        /// WORD->unsigned short
        public ushort wOsdLen;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_AUXOUTCFG
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// DWORD->unsigned int
        public uint dwAlarmOutChan;

        /// DWORD->unsigned int
        public uint dwAlarmChanSwitchTime;

        /// DWORD[]
        public uint[] dwAuxSwitchTime;

        /// BYTE[]
        public byte[] byAuxOrder;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_a6dfe295_05da_4535_9cc7_c64f25099481
    {

        /// BYTE->unsigned char
        public byte PresetNum;

        /// BYTE->unsigned char
        public byte Dwell;

        /// BYTE->unsigned char
        public byte Speed;

        /// BYTE->unsigned char
        public byte Reserve;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_99f3df1c_0b7f_4209_b477_a010da70749f
    {

        /// BYTE->unsigned char
        public byte PresetNum;

        /// BYTE->unsigned char
        public byte Dwell;

        /// BYTE->unsigned char
        public byte Speed;

        /// BYTE->unsigned char
        public byte Reserve;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_CRUISE_RET
    {

        /// Anonymous_99f3df1c_0b7f_4209_b477_a010da70749f[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
        public Anonymous_99f3df1c_0b7f_4209_b477_a010da70749f[] struCruisePoint;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_POINT_FRAME
    {

        /// int
        public int xTop;

        /// int
        public int yTop;

        /// int
        public int xBottom;

        /// int
        public int yBottom;

        /// int
        public int bCounter;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_LOG
    {
        NET_DVR_TIME strLogTime;
        /// DWORD->unsigned int
        public uint dwMajorType;

        /// DWORD->unsigned int
        public uint dwMinorType;

        /// BYTE[]
        public byte[] sPanelUser;

        /// BYTE[]
        public byte[] sNetUser;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sRemoteHostAddr;

        /// DWORD->unsigned int
        public uint dwParaType;

        /// DWORD->unsigned int
        public uint dwChannel;

        /// DWORD->unsigned int
        public uint dwDiskNumber;

        /// DWORD->unsigned int
        public uint dwAlarmInPort;

        /// DWORD->unsigned int
        public uint dwAlarmOutPort;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_b30accb8_f8e8_42b3_8469_a68aa3f1c236
    {

        /// DWORD->unsigned int
        public uint dwChannel;

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;

        NET_DVR_TIME struStartTime;	/* 按时间回放的开始时间 */
        NET_DVR_TIME struStopTime;	/* 按时间回放的结束时间 */
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_9583279b_0567_45b2_b77a_6bcce8ebb498
    {

        /// BYTE[100]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public byte[] byFile;

        /// Anonymous_b30accb8_f8e8_42b3_8469_a68aa3f1c236
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_b30accb8_f8e8_42b3_8469_a68aa3f1c236 bytime;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_PLAYREMOTEFILE
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDecoderIP;

        /// WORD->unsigned short
        public ushort wDecoderPort;

        /// WORD->unsigned short
        public ushort wLoadMode;

        /// Anonymous_9583279b_0567_45b2_b77a_6bcce8ebb498
        public Anonymous_9583279b_0567_45b2_b77a_6bcce8ebb498 mode_size;
    }


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct Anonymous_a5aca173_eb28_4fde_8e63_1881a6f19b2b
    {

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;

        /// char[52]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 52)]
        public string cReserve;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_ef9ea98a_157b_493e_bde0_1dbfb9da379e
    {

        /// BYTE[100]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] fileName;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Anonymous_828b7109_6502_4253_9039_8fe11b476fb9
    {

        /// DWORD->unsigned int
        public uint dwChannel;

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;

        NET_DVR_TIME struStartTime;		/* 按时间回放的开始时间 */
        NET_DVR_TIME struStopTime;		/* 按时间回放的结束时间 */
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct Anonymous_fec9d284_712c_4402_8cfd_c5275cd9c980
    {

        /// Anonymous_a5aca173_eb28_4fde_8e63_1881a6f19b2b
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_a5aca173_eb28_4fde_8e63_1881a6f19b2b userInfo;

        /// Anonymous_ef9ea98a_157b_493e_bde0_1dbfb9da379e
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_ef9ea98a_157b_493e_bde0_1dbfb9da379e fileInfo;

        /// Anonymous_828b7109_6502_4253_9039_8fe11b476fb9
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_828b7109_6502_4253_9039_8fe11b476fb9 timeInfo;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_DECCHANSTATUS
    {

        /// DWORD->unsigned int
        public uint dwWorkType;

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDVRIP;

        /// WORD->unsigned short
        public ushort wDVRPort;

        /// BYTE->unsigned char
        public byte byChannel;

        /// BYTE->unsigned char
        public byte byLinkMode;

        /// DWORD->unsigned int
        public uint dwLinkType;

        /// Anonymous_fec9d284_712c_4402_8cfd_c5275cd9c980
        public Anonymous_fec9d284_712c_4402_8cfd_c5275cd9c980 objectInfo;
    }


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_DECSTATUS
    {
        NET_DVR_DECCHANSTATUS[] struDecState;
        /// DWORD->unsigned int
        public uint dwSize;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_MATRIX_DECINFO
    {

        /// char[16]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sDVRIP;

        /// WORD->unsigned short
        public ushort wDVRPort;

        /// BYTE->unsigned char
        public byte byChannel;

        /// BYTE->unsigned char
        public byte byTransProtocol;

        /// BYTE->unsigned char
        public byte byTransMode;

        /// BYTE[3]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
        public byte[] byRes;

        /// BYTE[]
        public byte[] sUserName;

        /// BYTE[]
        public byte[] sPassword;
    }


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_MATRIX_DYNAMIC_DEC
    {
        NET_DVR_MATRIX_DECINFO struDecChanInfo;		/* 动态解码通道信息 */
        /// DWORD->unsigned int
        public uint dwSize;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_MATRIX_DEC_CHAN_INFO
    {

        /// DWORD->unsigned int
        public uint dwSize;

        NET_DVR_MATRIX_DECINFO struDecChanInfo;		/* 解码通道信息 */

        /// DWORD->unsigned int
        public uint dwDecState;

        NET_DVR_TIME StartTime;		/* 按时间回放开始时间 */
        NET_DVR_TIME StopTime;		/* 按时间回放停止时间 */

        /// char[128]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
        public string sFileName;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_MATRIX_DECCHANINFO
    {

        /// DWORD->unsigned int
        public uint dwEnable;

        NET_DVR_MATRIX_DECINFO struDecChanInfo;		/* 轮循解码通道信息 */
    }


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct NET_DVR_MATRIX_LOOP_DECINFO
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// DWORD->unsigned int
        public uint dwPoolTime;

        NET_DVR_MATRIX_DECCHANINFO[] struchanConInfo;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct NET_DVR_MATRIX_DEC_CHAN_STATUS
    {

        /// DWORD->unsigned int
        public uint dwSize;

        /// DWORD->unsigned int
        public uint dwIsLinked;

        /// DWORD->unsigned int
        public uint dwStreamCpRate;

        /// char[64]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cRes;
    }
}
