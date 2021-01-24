/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.Serialization;

namespace extOSC.Examples.Scripts
{
	public class SimpleMessageReceiver : MonoBehaviour
	{
		#region Public Vars

		[FormerlySerializedAs("Address")] public string address = "/*/*";

		[FormerlySerializedAs("Receiver")] [Header("OSC Settings")]
		public OSCReceiver receiver;

		#endregion

		#region Unity Methods

		private void Start()
		{
			receiver.Bind(address, ReceivedMessage);
		}

		#endregion

		#region Private Methods

		private void ReceivedMessage(OSCMessage message)
		{
			Debug.LogFormat("Received: {0}", message);
		}

		#endregion
	}
}