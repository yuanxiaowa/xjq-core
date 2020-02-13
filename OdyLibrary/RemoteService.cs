using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Remoting.Channels.Ipc;

namespace OdyLibrary
{
    public class RemoteService
    {
        public OneServiceRemoteProvider remoteProvider;
        public RemoteService(string name, string port)
        {

            remoteProvider = new OneServiceRemoteProvider();
            // 将 remoteProvider/OneServiceRemoteProvider 设置到这个路由，你还可以设置其它的 MarshalByRefObject 到不同的路由。
            //RemotingServices.Marshal(remoteProvider, "one");
            //ChannelServices.RegisterChannel(new IpcChannel(ServiceIpcPortName), false);
            //remoteProvider.onReceived += RemoteProvider_onReceived;

            var channel = CreateChannel(name);
            //向系统的信道服务注册信道
            ChannelServices.RegisterChannel(channel, false);
            RemotingServices.Marshal(remoteProvider, port);
            //new Thread(() =>
            //{
            //    new UpdateUIDelegate
            //}).Start();

            //IpcChannel serverchannel = new IpcChannel("testchannel");
            //ChannelServices.RegisterChannel(serverchannel, false);
            //RemotingConfiguration.RegisterWellKnownServiceType(typeof(OneServiceRemoteProvider), "test", WellKnownObjectMode.Singleton);

            //var pipeServer = new NamedPipeServerStream("ccc", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            //pipeServer.BeginWaitForConnection(PipeCallback, pipeServer);
        }

        //private void PipeCallback(IAsyncResult ar)
        //{
        //    var pipeServer = (NamedPipeServerStream)ar.AsyncState;
        //    pipeServer.EndWaitForConnection(ar);
        //    var data = new byte[1024];
        //    var count = pipeServer.Read(data, 0, 1024);
        //    if (count > 0)
        //    {
        //        string message = Encoding.UTF8.GetString(data, 0, count);
        //        //TODO:
        //    }
        //    pipeServer.Disconnect();//一定要断开链接
        //    pipeServer.BeginWaitForConnection(PipeCallback, pipeServer);
        //}

        public void DisConnect()
        {
            RemotingServices.Disconnect(remoteProvider);
        }

        public static T GetRemoteObject<T>(string name, string path)
        {
            return (T)Activator.GetObject(
                typeof(T),
                String.Format("ipc://{0}/{1}", name, path)
            );
        }

        public static IpcChannel CreateChannel(string name = "")
        {
            var serverProvider = new BinaryServerFormatterSinkProvider { TypeFilterLevel = TypeFilterLevel.Full };
            var clientProvider = new BinaryClientFormatterSinkProvider();
            var properties = new Hashtable();    //指定信道的端口
            properties["portName"] = name;
            var channel = new IpcChannel(properties, clientProvider, serverProvider);
            return channel;
        }
    }
}
