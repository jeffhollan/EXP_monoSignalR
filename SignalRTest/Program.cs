using System;
using System.Threading;

namespace SignalRTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var connector = new SocketConnector ();
			connector.Listen ();
			while (true) {
			}
		}
	}
}
