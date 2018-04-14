/// Date	: 13/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzer
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace FantasticYes.Tools
{
	public static partial class GUIControls
	{
		#region Fields
		private static Color s_ToggleColor;
		private static Color s_ContentColor;
		private static Dictionary<string, GUIStyle> s_Styles;
		#endregion

		#region GUI Controls
		/// <summary>
		/// Creates a toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <returns>The button state.</returns>
		public static bool Toggle (bool state, GUIContent content)
		{
			PopulateStyles();
			return DoToggle (EditorGUILayout.GetControlRect (), state, content, s_Styles ["ToggleOn"], s_Styles ["ToggleOff"]);
		}

		/// <summary>
		/// Creates a toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <param name="options">GUILayout options.</param>
		/// <returns></returns>
		public static bool Toggle (bool state, GUIContent content, params GUILayoutOption [] options)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (options), state, content, s_Styles ["ToggleOn"], s_Styles ["ToggleOff"]);
		}

		/// <summary>
		/// Creates a middle toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <returns>The button state.</returns>
		public static bool ToggleMid (bool state, GUIContent content)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (), state, content, s_Styles ["ToggleMidOn"], s_Styles ["ToggleMidOff"]);
		}

		/// <summary>
		/// Creates a middle toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <param name="options">GUILayout options.</param>
		/// <returns></returns>
		public static bool ToggleMid (bool state, GUIContent content, params GUILayoutOption [] options)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (options), state, content, s_Styles ["ToggleMidOn"], s_Styles ["ToggleMidOff"]);
		}

		/// <summary>
		/// Creates a left toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <returns>The button state.</returns>
		public static bool ToggleLeft (bool state, GUIContent content)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (), state, content, s_Styles ["ToggleLeftOn"], s_Styles ["ToggleLeftOff"]);
		}

		/// <summary>
		/// Creates a left toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <param name="options">GUILayout options.</param>
		/// <returns></returns>
		public static bool ToggleLeft (bool state, GUIContent content, params GUILayoutOption [] options)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (options), state, content, s_Styles ["ToggleLeftOn"], s_Styles ["ToggleLeftOff"]);
		}

		/// <summary>
		/// Creates a right toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <returns>The button state.</returns>
		public static bool ToggleRight (bool state, GUIContent content)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (), state, content, s_Styles ["ToggleRightOn"], s_Styles ["ToggleRightOff"]);
		}

		/// <summary>
		/// Creates a right toggle button control.
		/// </summary>
		/// <param name="state">The toggle state.</param>
		/// <param name="content">GUIContent label.</param>
		/// <param name="options">GUILayout options.</param>
		/// <returns></returns>
		public static bool ToggleRight (bool state, GUIContent content, params GUILayoutOption [] options)
		{
			PopulateStyles ();
			return DoToggle (EditorGUILayout.GetControlRect (options), state, content, s_Styles ["ToggleRightOn"], s_Styles ["ToggleRightOff"]);
		}

		/// <summary>
		/// Creates a toggle button group.
		/// </summary>
		/// <param name="index">The selected button index, or -1.</param>
		/// <param name="content">The GUIContent array for the all buttons.</param>
		/// <returns>The selected index, or -1 when unselected.</returns>
		public static int ToggleGroup (int index, GUIContent [] content)
		{
			PopulateStyles ();
			int count = content.Length;
			Rect rect = EditorGUILayout.GetControlRect ();
			rect.width /= count;

			for (int i = 0; i < count; i++)
			{
				rect.x = 4 + i * rect.width;
				bool isSelected = (i == index);

				if (i == 0)
				{
					if (DoToggle (rect, isSelected, content [i], s_Styles ["ToggleLeftOn"], s_Styles ["ToggleLeftOff"]) != isSelected)
					{
						index = isSelected ? -1 : i;
					}
				}
				else if (i == count - 1)
				{
					if (DoToggle (rect, isSelected, content [i], s_Styles ["ToggleRightOn"], s_Styles ["ToggleRightOff"]) != isSelected)
					{
						index = isSelected ? -1 : i;
					}
				}
				else
				{
					if (DoToggle (rect, isSelected, content [i], s_Styles ["ToggleMidOn"], s_Styles ["ToggleMidOff"]) != isSelected)
					{
						index = isSelected ? -1 : i;
					}
				}
			}

			return index;
		}
		#endregion

		private static bool DoToggle (Rect rect, bool state, GUIContent content, GUIStyle on, GUIStyle off)
		{
			if (!state)
			{
				s_ContentColor = GUI.contentColor;
				GUI.contentColor = s_ToggleColor;

				state = GUI.Toggle (rect, state, content, state ? on : off);
				GUI.contentColor = s_ContentColor;

				return state;
			}

			return GUI.Toggle (rect, state, content, state ? on : off);
		}

		private static void PopulateStyles ()
		{
			if (s_Styles == null)
			{
				s_ToggleColor = EditorGUIUtility.isProSkin ? new Color (0.75f, 0.75f, 0.75f) : new Color (0.1f, 0.1f, 0.1f);

				s_Styles = new Dictionary<string, GUIStyle>
				{
					{ "ToggleOn",       new GUIStyle (EditorStyles.miniButton)      { padding = new RectOffset (2,2,2,2) } },
					{ "ToggleOff",      new GUIStyle (EditorStyles.miniButton)      { padding = new RectOffset (2,2,2,2) } },
					{ "ToggleMidOn",    new GUIStyle (EditorStyles.miniButtonMid)   { padding = new RectOffset (2,2,2,2), margin = new RectOffset (0,0,2,2) } },
					{ "ToggleMidOff",   new GUIStyle (EditorStyles.miniButtonMid)   { padding = new RectOffset (2,2,2,2), margin = new RectOffset (0,0,2,2) } },
					{ "ToggleLeftOn",   new GUIStyle (EditorStyles.miniButtonLeft)  { padding = new RectOffset (2,2,2,2), margin = new RectOffset (4,0,2,2) } },
					{ "ToggleLeftOff",  new GUIStyle (EditorStyles.miniButtonLeft)  { padding = new RectOffset (2,2,2,2), margin = new RectOffset (4,0,2,2) } },
					{ "ToggleRightOn",  new GUIStyle (EditorStyles.miniButtonRight) { padding = new RectOffset (2,2,2,2), margin = new RectOffset (0,4,2,2) } },
					{ "ToggleRightOff", new GUIStyle (EditorStyles.miniButtonRight) { padding = new RectOffset (2,2,2,2), margin = new RectOffset (0,4,2,2) } },
				};

				s_Styles ["ToggleOn"].normal.background = s_Styles ["ToggleOn"].active.background;
				s_Styles ["ToggleMidOn"].normal.background = s_Styles ["ToggleMidOn"].active.background;
				s_Styles ["ToggleLeftOn"].normal.background = s_Styles ["ToggleLeftOn"].active.background;
				s_Styles ["ToggleRightOn"].normal.background = s_Styles ["ToggleRightOn"].active.background;
			}
		}
	}
}
