/* Copyright (c) 2020 ExT (V.Sigalkin) */

using UnityEditor;
using UnityEngine;

using System.Collections.Generic;

using extOSC.Core;

namespace extOSC.Editor.Drawers
{
	public class OSCPacketEditableDrawer
	{
		#region Static Private Vars

		private static readonly GUIContent _bundleEmptyContent = new GUIContent("Bundle is empty!");

		private static readonly GUIContent _addressContent = new GUIContent("Address:");

		private static readonly GUIContent _addBundleContent = new GUIContent("Add Bundle");

		private static readonly GUIContent _addMessageContent = new GUIContent("Add Message");

		private static readonly GUIContent _addValueContent = new GUIContent("Add Value");

		private static readonly GUIContent _beginArrayContent = new GUIContent("Begin Array");

		private static readonly GUIContent _endArrayContent = new GUIContent("End Array");

		private static readonly GUIContent _addToArrayContent = new GUIContent("Array:");

		private static readonly GUIContent _closeContent = new GUIContent("x");

		#endregion

		#region Private Vars

		private static readonly Dictionary<object, OSCValueType> _valueTypeTemp = new Dictionary<object, OSCValueType>();

		#endregion

		#region Public Methods

		public void DrawLayout(IOSCPacket packet)
		{
			if (packet.IsBundle())
			{
				DrawBundle((OSCBundle) packet);
			}
			else
			{
				DrawMessage((OSCMessage) packet);
			}
		}

		#endregion

		#region Private Methods

		private void DrawBundle(OSCBundle bundle)
		{
			var defaultColor = GUI.color;

			if (bundle.Packets.Count > 0)
			{
				var removePacket = (IOSCPacket) null;

				foreach (var bundlePacket in bundle.Packets)
				{
					using (new GUILayout.HorizontalScope())
					{
						EditorGUILayout.LabelField($"{bundlePacket.GetType().Name}:", EditorStyles.boldLabel);

						GUI.color = Color.red;
						if (GUILayout.Button(_closeContent, GUILayout.Height(EditorGUIUtility.singleLineHeight), GUILayout.Width(EditorGUIUtility.singleLineHeight)))
						{
							removePacket = bundlePacket;
						}

						GUI.color = defaultColor;
					}

					using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
					{
						DrawLayout(bundlePacket);
					}

					GUILayout.Space(10);
				}

				if (removePacket != null)
				{
					bundle.Packets.Remove(removePacket);

					if (_valueTypeTemp.ContainsKey(removePacket))
						_valueTypeTemp.Remove(removePacket);
				}
			}
			else
			{
				using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
				{
					EditorGUILayout.LabelField(_bundleEmptyContent, OSCEditorStyles.CenterLabel);
				}
			}

			// ADD PACKET
			using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
			{
				GUI.color = Color.green;
				if (GUILayout.Button(_addBundleContent))
				{
					bundle.AddPacket(new OSCBundle());
				}

				if (GUILayout.Button(_addMessageContent))
				{
					bundle.AddPacket(new OSCMessage("/address"));
				}

				GUI.color = defaultColor;
			}
		}

		private void DrawMessage(OSCMessage message)
		{
			var removeValue = (OscValue) null;

			EditorGUILayout.LabelField(_addressContent, EditorStyles.boldLabel);
			using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
			{
				message.Address = EditorGUILayout.TextField(message.Address, GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight));
			}

			EditorGUILayout.LabelField($"Values ({OSCEditorUtils.GetValueTags(message)}):", EditorStyles.boldLabel);
			using (new GUILayout.VerticalScope())
			{
				foreach (var value in message.Values)
				{
					DrawValue(value, ref removeValue);
				}
			}

			var includeValue = CreateValueButton(message);

			// ACTIONS
			if (removeValue != null)
			{
				message.Values.Remove(removeValue);
			}

			if (includeValue != null)
			{
				message.AddValue(includeValue);
			}
		}

		private void DrawArray(OscValue value, ref OscValue removeValue)
		{
			var defaultColor = GUI.color;
			var removeArrayValue = (OscValue) null;
			var includeArrayValue = (OscValue) null;

			using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
			{
				using (new GUILayout.HorizontalScope())
				{
					using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
					{
						EditorGUILayout.LabelField(_beginArrayContent, OSCEditorStyles.CenterBoldLabel);
					}

					using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
					{
						GUI.color = Color.red;
						if (GUILayout.Button(_closeContent, GUILayout.Height(EditorGUIUtility.singleLineHeight), GUILayout.Width(EditorGUIUtility.singleLineHeight)))
						{
							removeValue = value;
						}

						GUI.color = defaultColor;
					}
				}

				foreach (var arrayValues in value.arrayValue)
				{
					DrawValue(arrayValues, ref removeArrayValue);
				}

				using (new GUILayout.HorizontalScope())
				{
					using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
					{
						EditorGUILayout.LabelField(_addToArrayContent, GUILayout.Width(40));
					}

					includeArrayValue = CreateValueButton(value);
				}

				using (new GUILayout.HorizontalScope())
				{
					using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
					{
						EditorGUILayout.LabelField(_endArrayContent, OSCEditorStyles.CenterBoldLabel);
					}
				}
			}

			if (includeArrayValue != null)
			{
				value.arrayValue.Add(includeArrayValue);
			}

			if (removeArrayValue != null)
			{
				value.arrayValue.Remove(removeArrayValue);
			}
		}

		private void DrawValue(OscValue value, ref OscValue removeValue)
		{
			var firstColumn = 40f;
			var secondColumn = 60f;
			var defaultValue = GUI.color;

			if (value.type == OSCValueType.Array)
			{
				DrawArray(value, ref removeValue);
				return;
			}

			using (new GUILayout.HorizontalScope())
			{
				using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
				{
					EditorGUILayout.LabelField($"Tag: {value.tag}", OSCEditorStyles.CenterLabel, GUILayout.Width(firstColumn));
				}

				using (new GUILayout.HorizontalScope())
				{
					if (value.type == OSCValueType.Blob ||
						value.type == OSCValueType.Impulse ||
						value.type == OSCValueType.Null)
					{
						using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
						{
							EditorGUILayout.LabelField(value.type.ToString(), OSCEditorStyles.CenterLabel);
						}
					}
					else
					{
						using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
						{
							EditorGUILayout.LabelField($"{value.type}:", GUILayout.Width(secondColumn));
						}

						using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
						{
							switch (value.type)
							{
								case OSCValueType.Color:
									value.colorValue = EditorGUILayout.ColorField(value.colorValue);
									break;
								case OSCValueType.True:
								case OSCValueType.False:
									value.boolValue = EditorGUILayout.Toggle(value.boolValue);
									break;
								case OSCValueType.Char:
									var rawString = EditorGUILayout.TextField(value.charValue.ToString());
									value.charValue = rawString.Length > 0 ? rawString[0] : ' ';
									break;
								case OSCValueType.Int:
									value.intValue = EditorGUILayout.IntField(value.intValue);
									break;
								case OSCValueType.Double:
									value.doubleValue = (float) EditorGUILayout.DoubleField(value.doubleValue);
									break;
								case OSCValueType.Long:
									value.longValue = EditorGUILayout.LongField(value.longValue);
									break;
								case OSCValueType.Float:
									value.floatValue = EditorGUILayout.FloatField(value.floatValue);
									break;
								case OSCValueType.String:
									value.stringValue = EditorGUILayout.TextField(value.stringValue);
									break;
								case OSCValueType.Midi:
									var midi = value.midiValue;
									midi.Channel = (byte) Mathf.Clamp(EditorGUILayout.IntField(midi.Channel), 0, 255);
									midi.Status = (byte) Mathf.Clamp(EditorGUILayout.IntField(midi.Status), 0, 255);
									midi.Data1 = (byte) Mathf.Clamp(EditorGUILayout.IntField(midi.Data1), 0, 255);
									midi.Data2 = (byte) Mathf.Clamp(EditorGUILayout.IntField(midi.Data2), 0, 255);
									value.midiValue = midi;
									break;
								default:
									EditorGUILayout.SelectableLabel(value.value.ToString(), GUILayout.Height(EditorGUIUtility.singleLineHeight));
									break;
							}
						}
					}
				}

				using (new GUILayout.VerticalScope(OSCEditorStyles.Box))
				{
					GUI.color = Color.red;
					if (GUILayout.Button(_closeContent, GUILayout.Height(EditorGUIUtility.singleLineHeight), GUILayout.Width(EditorGUIUtility.singleLineHeight)))
					{
						removeValue = value;
					}

					GUI.color = defaultValue;
				}
			}
		}

		private OscValue CreateValueButton(object sender)
		{
			var defaultColor = GUI.color;

			using (new GUILayout.HorizontalScope(OSCEditorStyles.Box))
			{
				if (!_valueTypeTemp.ContainsKey(sender))
				{
					_valueTypeTemp.Add(sender, OSCValueType.Float);
				}

				_valueTypeTemp[sender] = (OSCValueType) EditorGUILayout.EnumPopup(_valueTypeTemp[sender]);

				GUI.color = Color.green;
				if (GUILayout.Button(_addValueContent, GUILayout.Height(EditorGUIUtility.singleLineHeight)))
				{
					var value = OSCEditorUtils.CreateValue(_valueTypeTemp[sender]);
					if (value != null)
					{
						return value;
					}

					Debug.LogError($"[extOSC] You can't add this ({_valueTypeTemp[sender]}) value type!");
				}

				GUI.color = defaultColor;
			}

			return null;
		}

		#endregion
	}
}