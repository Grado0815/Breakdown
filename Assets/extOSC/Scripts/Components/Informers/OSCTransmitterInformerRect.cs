/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;

namespace extOSC.Components.Informers
{
	[AddComponentMenu("extOSC/Components/Transmitter/Rect Informer")]
	public class OSCTransmitterInformerRect : OSCTransmitterInformer<Rect>
	{
		#region Protected Methods

		protected override void FillMessage(OSCMessage message, Rect value)
		{
			message.AddValue(OscValue.Float(value.x));
			message.AddValue(OscValue.Float(value.y));
			message.AddValue(OscValue.Float(value.width));
			message.AddValue(OscValue.Float(value.height));
		}

		#endregion
	}
}