/// Date	: 27/03/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzer
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;

namespace FantasticYes.Tools
{
	public abstract class SceneWindow : ScriptableObject
	{
		#region Fields
		[SerializeField]
		private int m_id;
		[SerializeField]
		private string m_title;
		[SerializeField]
		private Vector2 m_size = new Vector2 (200, 100);

		private Rect m_rect = Rect.zero;
		#endregion

		#region Properties
		/// <summary>
		/// Get and set the size of the scene view window.
		/// </summary>
		public Vector2 Size
		{
			get
			{
				return m_size;
			}
			set
			{
				m_size = value;
			}
		}

		/// <summary>
		/// Get and set the window rect.
		/// </summary>
		public Rect Rect
		{
			get
			{
				return m_rect;
			}
			set
			{
				m_rect = value;
			}
		}

		/// <summary>
		/// Get and set the window title content.
		/// </summary>
		public string Title
		{
			get
			{
				return m_title;
			}
		}

		/// <summary>
		/// Get and set the window ID.
		/// </summary>
		public int Id
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}
		#endregion

		/// <summary>
		/// Show this window in the scene viewport.
		/// </summary>
		public void Show ()
		{
			m_size = GetWindowSize ();
			m_title = GetWindowTitle ();

			SceneWindowLayout.Add (this);
			SceneView.onSceneGUIDelegate += OnSceneGUI;
		}

		/// <summary>
		/// Closes this window and removes it from scene viewport.
		/// </summary>
		public void Close ()
		{
			SceneWindowLayout.Remove (this);
			SceneView.onSceneGUIDelegate -= OnSceneGUI;
		}

		protected void OnDisable ()
		{
			Close ();
		}

		protected void OnSceneGUI (SceneView sceneView)
		{
			m_rect = GUI.Window (m_id, m_rect, WindowGUI, m_title);
		}

		protected virtual string GetWindowTitle ()
		{
			return GetType ().Name;
		}

		protected abstract Vector2 GetWindowSize ();
		public abstract void WindowGUI (int windowID);
	}
}