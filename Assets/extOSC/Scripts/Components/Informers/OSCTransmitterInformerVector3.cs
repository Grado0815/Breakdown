/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;

namespace extOSC.Components.Informers
{
	[AddComponentMenu("extOSC/Components/Transmitter/Vector3 Informer")]
	public class OSCTransmitterInformerVector3 : OSCTransmitterInformer<Vector3>
	{
		#region Protected Methods

		protected override void FillMessage(OSCMessage message, Vector3 value)
		{
			message.AddValue(OscValue.Float(value.x));
			message.AddValue(OscValue.Float(value.y));
			message.AddValue(OscValue.Float(value.z));
		}

		#endregion
	}
}