/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;

using System.Collections.Generic;

namespace extOSC.Components.ReceiverReflections
{
	[AddComponentMenu("extOSC/Components/Receiver/Array Reflection")]
	public class OSCReceiverReflectionArray : OSCReceiverReflection<List<OscValue>>
	{
		#region Protected Methods

		protected override bool ProcessMessage(OSCMessage message, out List<OscValue> value) => message.ToArray(out value);

		#endregion
	}
}