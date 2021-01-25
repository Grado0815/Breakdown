/* Copyright (c) 2020 ExT (V.Sigalkin) */

using System;

namespace extOSC.Mapping
{
	[Serializable]
	public class OSCMapValue
	{
		#region Public Vars

		public OSCMapType Type;

		public float InputMin;

		public float InputMax = 1;

		public float OutputMin;

		public float OutputMax = 1;

		public bool Clamp = true;

		public float Value = 1;

		public float FalseValue;

		public float TrueValue = 1;

		public OSCMapLogic Logic;

		#endregion

		#region Public Methods

		public OscValue Map(OscValue value)
		{
			//FLOAT MAP
			if (Type == OSCMapType.Float)
			{
				value.floatValue = OSCUtilities.Map(value.floatValue, InputMin, InputMax, OutputMin, OutputMax, Clamp);
			}

			// FLOAT TO BOOL MAP
			else if (Type == OSCMapType.FloatToBool)
			{
				if (Logic == OSCMapLogic.GreaterOrEquals)
					return OscValue.Bool(value.floatValue >= Value);
				if (Logic == OSCMapLogic.Greater)
					return OscValue.Bool(value.floatValue > Value);
				if (Logic == OSCMapLogic.LessOrEquals)
					return OscValue.Bool(value.floatValue <= Value);
				if (Logic == OSCMapLogic.Less)
					return OscValue.Bool(value.floatValue < Value);
				if (Logic == OSCMapLogic.Equals)
					return OscValue.Bool(Math.Abs(value.floatValue - Value) <= float.Epsilon);
			}

			// BOOL TO FLOAT MAP
			else if (Type == OSCMapType.BoolToFloat)
			{
				return OscValue.Float(value.boolValue ? TrueValue : FalseValue);
			}

			// INT MAP
			else if (Type == OSCMapType.Int)
			{
				value.intValue = (int) OSCUtilities.Map(value.intValue, InputMin, InputMax, OutputMin, OutputMax, Clamp);
			}

			// INT TO BOOL MAP
			else if (Type == OSCMapType.IntToBool)
			{
				if (Logic == OSCMapLogic.GreaterOrEquals)
					return OscValue.Bool(value.intValue >= Value);
				if (Logic == OSCMapLogic.Greater)
					return OscValue.Bool(value.intValue > Value);
				if (Logic == OSCMapLogic.LessOrEquals)
					return OscValue.Bool(value.intValue <= Value);
				if (Logic == OSCMapLogic.Less)
					return OscValue.Bool(value.intValue < Value);
				if (Logic == OSCMapLogic.Equals)
					return OscValue.Bool(Math.Abs(value.intValue - Value) <= float.Epsilon);
			}

			// BOOL TO INT MAP
			else if (Type == OSCMapType.BoolToInt)
			{
				return OscValue.Int((int) (value.boolValue ? TrueValue : FalseValue));
			}

			return value;
		}

		#endregion
	}
}