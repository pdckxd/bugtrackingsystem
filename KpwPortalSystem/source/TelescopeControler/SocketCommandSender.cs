using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class SocketCommandSender:ICommandSender
    {
        #region ICommandSender Members

        public void Send(CommandMessage command, params string[] list)
        {
            KpwRuntime.Instance.CommandSender.SendMsg(KpwCommands.GetCommandString(command.ToString(), list));
        }

        #endregion
    }
}
