using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Nairc.KpwFramework.TelescopeControler;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using log4net;
using Nairc.KpwFramework.DataModel;
using Nairc.KpwDataAccess;

namespace WebApplication
{
    /// <summary>
    /// Summary description for KPWPortalWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class KPWPortalWebService : System.Web.Services.WebService
    {
        private static ILog logger = log4net.LogManager.GetLogger(typeof(KPWPortalWebService));


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool SendCommand(string Name)
        {
            try
            {
                int command = int.Parse(Name);

                SocketCommandSender fss = new SocketCommandSender();

                CommandMessage message = (CommandMessage)command;
                logger.Debug("Send Command. Msg:" + message.ToString());

                fss.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Send command failed. Msg:" + Name, ex);
                return false;
            }
        }

        [WebMethod]
        public string GetServerDataTime()
        {
            return DateTime.Now.ToShortTimeString();
        }

        //RA:0486000 DEC:+108000
        [WebMethod]
        public bool FindStarByPosition(string RaH,string RaM,string RaS,string DecFlag,string DecD,string DecM,string DecS)
        {
            try
            {
                int Ra = (int.Parse(RaH) * 3600 + int.Parse(RaM) * 60 + int.Parse(RaS)) * 15;
                int Dec = int.Parse(DecD) * 3600 + int.Parse(DecM) * 60 + int.Parse(DecS);
                if (DecFlag == "-") Dec = -Dec;
                
                SocketCommandSender fss = new SocketCommandSender();

                CommandMessage message = CommandMessage.FindStarByPosition;
                logger.Debug("Send Command. Msg:" + message.ToString());
                logger.Debug(string.Format("Ra:{0},Dec:{1}.", Ra.ToString("0000000"), DecFlag + Dec.ToString("000000")));
                fss.Send(message, new string[] { Ra.ToString("0000000"), DecFlag + Dec.ToString("000000") });
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Send command failed. Msg:" + CommandMessage.FindStarByPosition.ToString(), ex);
                return false;
            }
        }

        [WebMethod]
        public bool FindStarByName(string Name)
        {
            try
            {
                SocketCommandSender fss = new SocketCommandSender();

                CommandMessage message = CommandMessage.FindStarByName;
                logger.Debug("Send Command. Msg:" + message.ToString());

                fss.Send(message, new string[] { Name});
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Send command failed. Msg:" + CommandMessage.FindStarByName.ToString(), ex);
                return false;
            }
        }

        [WebMethod]
        public bool StopFindStar()
        {
            try
            {
                SocketCommandSender fss = new SocketCommandSender();

                CommandMessage message = CommandMessage.StopFindStar;
                logger.Debug("Send Command. Msg:" + message.ToString());

                fss.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Send command failed. Msg:" + CommandMessage.StopFindStar.ToString(), ex);
                return false;
            }
        }

        [WebMethod]
        public ImageInfo TakePicture(int grabTime)
        {
            System.Threading.Thread.Sleep(grabTime * 1000);

            return new ImageInfo();

            ImageInfoDB db = new ImageInfoDB();

            int id = db.AddImageInfo(new ImageInfo()
            {
                
            });

            return db.GetImageByID(id);
        }

    }
}
