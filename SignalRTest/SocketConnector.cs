using System;
using Microsoft.AspNet.SignalR.Client;
using NLog;

namespace SignalRTest
{
	public class SocketConnector
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		private HubConnection _hubConnection;
		private IHubProxy _messageProxy;
		private IHubProxy MessageProxy
		{
			get 
			{
				if (_messageProxy != null && _hubConnection.State == ConnectionState.Connected) {
					return _messageProxy;
				}
				_hubConnection = new HubConnection ("http://jeffhollansignalrtutorial.azurewebsites.net");
				_messageProxy = _hubConnection.CreateHubProxy ("ChatHub");

				_hubConnection.Start ().Wait ();
				return _messageProxy;
			}
		}

		private void Invoke(string method, params object[] args)
		{
			try
			{
//				Logger.Trace("Invoking method {0}", method);
				MessageProxy.Invoke(method, args);
			}
			catch (Exception ex) {
//				Logger.ErrorException (string.Format ("Invoke {0} error.", method), ex);
			}
		}

		public void Listen()
		{
			MessageProxy.On<string, string> ("broadcastMessage", (name, message) => {
				DoThis (name, message);
			});
		}

		public void DoThis(string name, string message)
		{
			Console.WriteLine ("Name: {0} - Message {1}", name, message);
		}

//		public void UpdateNotificationsCount(long memberKey)
//		{
//			Invoke ("UpdateNotificationsCount", memberKey, 10);
//		}
	}
}

