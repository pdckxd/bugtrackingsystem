using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Nairc.KpwFramework.DataModel
{
    [ServiceContract(Namespace = "Nairc.KpwFramework.TelescopeControler")]
    public interface ICommandSender
    {
        [OperationContract]
        void Send(CommandMessage command, params string[] list);
    }
}
