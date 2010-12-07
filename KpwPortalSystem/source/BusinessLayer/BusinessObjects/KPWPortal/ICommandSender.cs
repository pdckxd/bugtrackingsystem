using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Nairc.KPWPortal.BusinessLayer.BusinessObjects
{
    [ServiceContract(Namespace = "Nairc.KPWPortal.BusinessLayer.BusinessObjects")]
    public interface ICommandSender
    {
        [OperationContract]
        void Send(CommandMessage command);
    }
}
