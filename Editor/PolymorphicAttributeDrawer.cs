using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using MicKami.PolymorphicSerialization;

namespace MicKami.PolymorphicSerialization.Editor
{
	[CustomPropertyDrawer(typeof(PolymorphicAttribute))]
	public class PolymorphicAttributeDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (property.hasMultipleDifferentValues)
				return 0;

			return EditorGUI.GetPropertyHeight(property);
		}

		List<Type> GetDerivedClasses(Type baseType)
		{
			return Assembly.GetAssembly(baseType).GetTypes().Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t)).ToList();
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.hasMultipleDifferentValues)
			{
				return;
			}

			var fieldType = fieldInfo.FieldType;
			if (fieldType.IsGenericType)
				fieldType = fieldType.GetGenericArguments()[0];

			var derivedClasses = GetDerivedClasses(fieldType);
			if (property.managedReferenceValue == null)
				SetPropertyInstanceFromType(property, derivedClasses.FirstOrDefault());

			Rect popupRect = position;
			popupRect.width -= EditorGUIUtility.labelWidth;
			popupRect.x += EditorGUIUtility.labelWidth;
			popupRect.height = EditorGUIUtility.singleLineHeight;

			string typeName = property.managedReferenceValue.GetType().Name;
			GUI.SetNextControlName("popup");
			if (EditorGUI.DropdownButton(popupRect, new GUIContent(typeName), FocusType.Passive))
			{
				GUI.FocusControl("popup");
				GenericMenu menu = new();
				foreach (var type in derivedClasses)
				{
					menu.AddItem(new GUIContent(type.Name), typeName == type.Name, () =>
					{
						SetPropertyInstanceFromType(property, type);
					});
				}
				menu.ShowAsContext();
			}
			EditorGUI.PropertyField(position, property, label, true);
		}

		private static void SetPropertyInstanceFromType(SerializedProperty property, Type type)
		{
			property.managedReferenceValue = type.GetConstructor(Type.EmptyTypes).Invoke(null);
			property.serializedObject.ApplyModifiedProperties();
			property.serializedObject.Update();
		}
	}
}