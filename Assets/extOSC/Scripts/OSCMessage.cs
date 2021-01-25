/* Copyright (c) 2020 ExT (V.Sigalkin) */

using System;
using System.Net;
using System.Collections.Generic;

using extOSC.Core;

namespace extOSC
{
	public class OSCMessage : IOSCPacket
	{
		#region Static Public Methods

		public static OSCMessage Create(string address, params OscValue[] values)
		{
			return new OSCMessage(address, values);
		}

		#endregion

		#region Public Vars

		public string Address { get; set; }

		public IPAddress Ip { get; set; }

		public int Port { get; set; }

		public List<OscValue> Values { get; } = new List<OscValue>();

		#endregion

		#region Public Methods

		public OSCMessage(string address)
		{
			Address = address;
		}

		public OSCMessage(string address, params OscValue[] values)
		{
			Address = address;
			AddRange(values);
		}

		public void AddValue(OscValue value)
		{
			if (value == null)
				throw new NullReferenceException(nameof(value));

			Values.Add(value);
		}

		public void AddRange(IEnumerable<OscValue> values)
		{
			if (values == null)
				throw new NullReferenceException(nameof(values));

			Values.AddRange(values);
		}

		public OscValue[] FindValues(params OSCValueType[] types)
		{
			var tempValues = new List<OscValue>();

			foreach (var value in Values)
			{
				foreach (var type in types)
				{
					if (value.type == type)
					{
						tempValues.Add(value);
					}
				}
			}

			return tempValues.ToArray();
		}

		public bool IsBundle() => false;

		public IOSCPacket Copy()
		{
			var valuesCount = Values.Count;
			var values = new OscValue[valuesCount];

			for (var i = 0; i < valuesCount; ++i)
			{
				values[i] = Values[i].Copy();
			}

			return new OSCMessage(Address, values);
		}

		public override string ToString()
		{
			var stringValues = string.Empty;

			if (Values.Count > 0)
			{
				foreach (var value in Values)
				{
					stringValues += $"{value.GetType().Name}({value.type}) : \"{value.value}\", ";
				}

				stringValues = $"({stringValues.Remove(stringValues.Length - 2)})";
			}

			return $"<{GetType().Name}:\"{Address}\"> : {(string.IsNullOrEmpty(stringValues) ? "null" : stringValues)}";
		}

		// OBSOLETE
		[Obsolete("Use FindValues method.")]
		public OscValue[] GetValues(params OSCValueType[] types)
		{
			return FindValues(types);
		}

		#endregion
	}
}