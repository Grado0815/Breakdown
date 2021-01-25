/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;
using UnityEngine.UI;

namespace extOSC.Examples
{
	public class MatchPatternExample : MonoBehaviour
	{
		#region Public Vars

		[Header("OSC Settings")]
		public OSCReceiver Receiver;

		public OSCTransmitter Transmitter;

		[Header("Correct Message UI Settings")]
		public Text CorrectMessageAddress;

		[Header("Wrong Message UI Settings")]
		public Text WrongMessageAddress;

		#endregion

		#region Private Vars

		private const string _address = "/example/13/";

		#endregion

		#region Unity Methods

		public void Start()
		{
			CorrectMessageAddress.text = _address;
			WrongMessageAddress.text = _address;

			Receiver.Bind(_address, ReceiveMessage);
		}

		#endregion

		#region Public Methods

		public void SendCorrectMessage()
		{
			var message = new OSCMessage(_address);
			message.AddValue(OscValue.String("Correct Message")); // String
			message.AddValue(OscValue.Int(137));                  // Int
			message.AddValue(OscValue.Bool(true));                // Bool
			message.AddValue(OscValue.Bool(true));                // Bool

			Transmitter.Send(message);
		}

		public void SendWrongMessage()
		{
			var message = new OSCMessage(_address);
			message.AddValue(OscValue.Int(137));                              // Int
			message.AddValue(OscValue.String("Wrong Message"));               // String
			message.AddValue(OscValue.Blob(new byte[] {0x1, 0x3, 0x3, 0x7})); // Byte
			message.AddValue(OscValue.Bool(true));                            // Bool

			Transmitter.Send(message);
		}

		public void ReceiveMessage(OSCMessage message)
		{
			// Create match pattern (For bool values you can use True or False ValueType)
			var matchPattern = new OSCMatchPattern(OSCValueType.String, // String
												   OSCValueType.Int,    // Int
												   OSCValueType.True,   // Bool
												   OSCValueType.False); // Bool

			// Check match pattern
			if (message.IsMatch(matchPattern))
			{
				// Correct message
				Debug.Log("We got a correct message! Yeah! Maybe you want parse it?");
			}
			else
			{
				// Wrong message
				Debug.Log("Oh, no! It's a wrong message!");
			}
		}

		#endregion
	}
}