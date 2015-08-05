using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Quobject.SocketIoClientDotNet.Client;

namespace WebSocketDemo.Droid
{
	[Activity (Label = "WebSocketDemo.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private Socket socket;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.bSend);
			EditText edit = FindViewById<EditText> (Resource.Id.etMessage);

			button.Click += delegate {
				var message = edit.Text;
				socket.Emit("message", message);
			};

			socket = IO.Socket(Constants.HOST);
			socket.On ("message", delegate(object obj) {
				RunOnUiThread(() => {
					var message = obj.ToString ();
					var alert = Toast.MakeText (this, message, ToastLength.Short);
					alert.Show ();
				});
			});
		}
	}
}