/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEngine;

using System;
using System.Collections.Generic;

namespace extOSC
{
	public class OscValue
	{
		#region Static Public Methods

		public static OscValue Long(long value) => new OscValue(OSCValueType.Long, value);

		public static OscValue Char(char value) => new OscValue(OSCValueType.Char, value);

		public static OscValue Color(Color value) => new OscValue(OSCValueType.Color, value);

		public static OscValue Blob(byte[] value) => new OscValue(OSCValueType.Blob, value);

		public static OscValue Int(int value) => new OscValue(OSCValueType.Int, value);

		public static OscValue Bool(bool value) => new OscValue(value ? OSCValueType.True : OSCValueType.False, value);

		public static OscValue Float(float value) => new OscValue(OSCValueType.Float, value);

		public static OscValue Double(double value) => new OscValue(OSCValueType.Double, value);

		public static OscValue String(string value) => new OscValue(OSCValueType.String, value == null ? string.Empty : value);

		public static OscValue Null() => new OscValue(OSCValueType.Null, null);

		public static OscValue Impulse() => new OscValue(OSCValueType.Impulse, null);

		public static OscValue TimeTag(DateTime value) => new OscValue(OSCValueType.TimeTag, value);

		public static OscValue Midi(OSCMidi value) => new OscValue(OSCValueType.Midi, value);

		public static OscValue Array(params OscValue[] values) => new OscValue(OSCValueType.Array, new List<OscValue>(values));

		[Obsolete]
		public static char GetTag(Type type)
		{
			return GetTag(GetValueType(type));
		}

		public static char GetTag(OSCValueType valueType)
		{
			switch (valueType)
			{
				case OSCValueType.Unknown:
					return 'N';
				case OSCValueType.Int:
					return 'i';
				case OSCValueType.Long:
					return 'h';
				case OSCValueType.True:
					return 'T';
				case OSCValueType.False:
					return 'F';
				case OSCValueType.Float:
					return 'f';
				case OSCValueType.Double:
					return 'd';
				case OSCValueType.String:
					return 's';
				case OSCValueType.Null:
					return 'N';
				case OSCValueType.Impulse:
					return 'I';
				case OSCValueType.Blob:
					return 'b';
				case OSCValueType.Char:
					return 'c';
				case OSCValueType.Color:
					return 'r';
				case OSCValueType.TimeTag:
					return 't';
				case OSCValueType.Midi:
					return 'm';
				case OSCValueType.Array:
					return 'N';
				default:
					return 'N';
			}
		}

		public static OSCValueType GetValueType(char valueTag)
		{
			switch (valueTag)
			{
				case 'i':
					return OSCValueType.Int;
				case 'h':
					return OSCValueType.Long;
				case 'T':
					return OSCValueType.True;
				case 'F':
					return OSCValueType.False;
				case 'f':
					return OSCValueType.Float;
				case 'd':
					return OSCValueType.Double;
				case 's':
					return OSCValueType.String;
				case 'N':
					return OSCValueType.Null;
				case 'I':
					return OSCValueType.Impulse;
				case 'b':
					return OSCValueType.Blob;
				case 'c':
					return OSCValueType.Char;
				case 'r':
					return OSCValueType.Color;
				case 't':
					return OSCValueType.TimeTag;
				case 'm':
					return OSCValueType.Midi;
				case '[':
					return OSCValueType.Array;
				//case ']':
				//	return OSCValueType.Array;
				default:
					return OSCValueType.Unknown;
			}
		}

		public static Type GetType(OSCValueType valueType)
		{
			if (valueType == OSCValueType.Unknown)
				return null;
			if (valueType == OSCValueType.Int)
				return typeof(int);
			if (valueType == OSCValueType.Long)
				return typeof(long);
			if (valueType == OSCValueType.True)
				return typeof(bool);
			if (valueType == OSCValueType.False)
				return typeof(bool);
			if (valueType == OSCValueType.Float)
				return typeof(float);
			if (valueType == OSCValueType.Double)
				return typeof(double);
			if (valueType == OSCValueType.String)
				return typeof(string);
			if (valueType == OSCValueType.Null)
				return null;
			if (valueType == OSCValueType.Impulse)
				return null;
			if (valueType == OSCValueType.Blob)
				return typeof(byte[]);
			if (valueType == OSCValueType.Char)
				return typeof(char);
			if (valueType == OSCValueType.Color)
				return typeof(Color);
			if (valueType == OSCValueType.TimeTag)
				return typeof(DateTime);
			if (valueType == OSCValueType.Midi)
				return typeof(OSCMidi);
			if (valueType == OSCValueType.Array)
				return typeof(List<OscValue>);

			return null;
		}

		public static OSCValueType GetValueType(Type type)
		{
			if (type == typeof(int))
				return OSCValueType.Int;
			if (type == typeof(long))
				return OSCValueType.Long;
			if (type == typeof(bool))
				return OSCValueType.False;
			if (type == typeof(float))
				return OSCValueType.Float;
			if (type == typeof(double))
				return OSCValueType.Double;
			if (type == typeof(string))
				return OSCValueType.String;
			if (type == typeof(byte[]))
				return OSCValueType.Blob;
			if (type == typeof(char))
				return OSCValueType.Char;
			if (type == typeof(Color))
				return OSCValueType.Color;
			if (type == typeof(DateTime))
				return OSCValueType.TimeTag;
			if (type == typeof(OSCMidi))
				return OSCValueType.Midi;
			if (type == typeof(List<OscValue>))
				return OSCValueType.Array;

			return OSCValueType.Unknown;
		}

		#endregion

		#region Public Vars

		public object value => m_Value;

		public OSCValueType type => m_Type;

		public string tag => m_Type == OSCValueType.Array ? GetArrayTag() : GetTag(m_Type).ToString();

		public long longValue
		{
			get => GetValue<long>(OSCValueType.Long);
			set => m_Value = value;
		}

		public char charValue
		{
			get => GetValue<char>(OSCValueType.Char);
			set => m_Value = value;
		}

		public Color colorValue
		{
			get => GetValue<Color>(OSCValueType.Color);
			set => m_Value = value;
		}

		public byte[] blobValue
		{
			get => GetValue<byte[]>(OSCValueType.Blob);
			set => m_Value = value;
		}

		public int intValue
		{
			get => GetValue<int>(OSCValueType.Int);
			set => m_Value = value;
		}

		public bool boolValue
		{
			get => m_Type == OSCValueType.True;
			set => m_Type = value ? OSCValueType.True : OSCValueType.False;
		}

		public float floatValue
		{
			get => GetValue<float>(OSCValueType.Float);
			set => m_Value = value;
		}

		public float doubleValue
		{
			get => GetValue<float>(OSCValueType.Double);
			set => m_Value = value;
		}

		public string stringValue
		{
			get => GetValue<string>(OSCValueType.String);
			set => m_Value = value;
		}

		public bool isNull => m_Type == OSCValueType.Null;

		public bool isImpulse => m_Type == OSCValueType.Impulse;

		public DateTime timeTagValue
		{
			get => GetValue<DateTime>(OSCValueType.TimeTag);
			set => m_Value = value;
		}

		public OSCMidi midiValue
		{
			get => GetValue<OSCMidi>(OSCValueType.Midi);
			set => m_Value = value;
		}

		public List<OscValue> arrayValue
		{
			get => GetValue<List<OscValue>>(OSCValueType.Array);
			set => m_Value = value;
		}

		#endregion

		#region Private Vars

		private object m_Value;

		private OSCValueType m_Type;

		#endregion

		#region Public Methods

		public OscValue(OSCValueType type, object value)
		{
			m_Value = value;
			m_Type = type;
		}

		public void AddValue(OscValue arrayValue)
		{
			if (m_Type != OSCValueType.Array)
				throw new Exception("OSCValue must be \"Array\" type.");

			if (arrayValue == this)
				throw new Exception("OSCValue with \"Array\" type cannot store itself.");

			this.arrayValue.Add(arrayValue);
		}

		public OscValue Copy()
		{
			return new OscValue(type, value);
		}

		public override string ToString()
		{
			if (m_Type == OSCValueType.True || m_Type == OSCValueType.False || m_Type == OSCValueType.Null || m_Type == OSCValueType.Impulse)
			{
				return $"<OSCValue {tag}>";
			}

			if (m_Type == OSCValueType.Array)
			{
				var stringValues = string.Empty;

				if (arrayValue.Count > 0)
				{
					foreach (var arrayValue in arrayValue)
					{
						stringValues += arrayValue + ", ";
					}

					if (stringValues.Length > 2)
						stringValues = stringValues.Remove(stringValues.Length - 2);
				}

				return $"<OSCValue Array [{stringValues}]>";
			}

			return $"<OSCValue {tag}: {m_Value}>";
		}

		#endregion

		#region Private Methods

		private T GetValue<T>(OSCValueType requiredType)
		{
			if (requiredType == m_Type)
			{
				return (T) m_Value;
			}

			return default;
		}

		private string GetArrayTag()
		{
			var arrayTag = "[";

			foreach (var arrayValue in arrayValue)
			{
				arrayTag += arrayValue.tag;
			}

			arrayTag += "]";

			return arrayTag;
		}

		#endregion
	}
}