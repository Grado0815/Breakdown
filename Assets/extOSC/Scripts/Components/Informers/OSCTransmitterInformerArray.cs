/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;

using System.Collections.Generic;

namespace extOSC.Components.Informers
{
	[AddComponentMenu("extOSC/Components/Transmitter/Array Informer")]
	public class OSCTransmitterInformerArray : OSCTransmitterInformer<List<OscValue>>
	{
		#region Protected Methods

		protected override void FillMessage(OSCMessage message, List<OscValue> value) => message.AddValue(OscValue.Array(value.ToArray()));

		#endregion
	}
}