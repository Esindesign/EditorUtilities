/// Date	: 13/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzler
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace FantasticYes.Tools
{
	public static class SceneWindowLayout
	{
		private static List<SceneWindow> m_windows = new List<SceneWindow> ();

		/// <summary>
		/// Add the specified SceneWindow to the layout.
		/// </summary>
		/// <param name="window"></param>
		public static void Add (SceneWindow window)
		{
			m_windows.Add (window);
			UpdateLayout ();
		}

		/// <summary>
		/// Removes the specified SceneWindow from the layout.
		/// </summary>
		/// <param name="window"></param>
		public static void Remove (SceneWindow window)
		{
			m_windows.Remove (window);
			UpdateLayout ();
		}

		/// <summary>
		/// Position windows in scene view and assign window ids.
		/// </summary>
		private static void UpdateLayout ()
		{
			float maxWidth = 0f;
			Vector2 position = new Vector2 (10, 25);

			// Find max width across all windows
			foreach (SceneWindow window in m_windows)
			{
				if (window.Size.x > maxWidth)
				{
					maxWidth = window.Size.x;
				}
			}

			// Iterate over windows, assign window id, position and width
			for (int i = 0; i < m_windows.Count; i++)
			{
				SceneWindow window = m_windows [i];

				window.Id = i;
				window.Rect = new Rect (position.x, position.y, maxWidth, window.Size.y);

				position.y += window.Size.y + 10;
			}

			SceneView.RepaintAll ();
		}
	}
}