using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace OdyLibrary
{
    public class OneServiceRemoteProvider : MarshalByRefObject
    {

        public event Action<string, string, string> OnReceivedMsg;
        public void SendMsg(string type, string name = "", string value = "")
        {
            OnReceivedMsg(type, name, value);
        }

        public override object InitializeLifetimeService()
        {
            //ILease currentLease = (ILease)base.InitializeLifetimeService();
            //if (currentLease.CurrentState == LeaseState.Initial)
            //{
            //    currentLease.InitialLeaseTime = TimeSpan.FromSeconds(5);
            //    currentLease.RenewOnCallTime = TimeSpan.FromSeconds(1);
            //}

            //return currentLease;
            return null;
        }
    }
}
