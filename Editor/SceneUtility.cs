/// Date	: 13/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzer
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace FantasticYes.Tools
{
	public static partial class SceneUtility
	{
		#region Fields
		private static MethodInfo s_PickGameObject;
		private static MethodInfo s_IntersectRayMesh;
		private static MethodInfo s_SelectGameObject;
		#endregion

		[InitializeOnLoadMethod]
		static void Setup ()
		{
			s_IntersectRayMesh = typeof (HandleUtility).GetMethod ("IntersectRayMesh", BindingFlags.Static | BindingFlags.NonPublic);
			s_SelectGameObject = typeof (HandleUtility).GetMethod ("PickRectObjects", BindingFlags.Static | BindingFlags.Public, null, new [] { typeof (Rect), typeof (bool) }, null);
			s_PickGameObject = typeof (HandleUtility).GetMethod ("PickGameObject", BindingFlags.Static | BindingFlags.Public, null, new [] { typeof (Vector2), typeof (bool) }, null);
		}

		#region Scene View Methods
		/// <summary>
		/// Computes a ray mesh intersection in the scene view without colliders.
		/// </summary>
		/// <param name="ray">The intersection ray.</param>
		/// <param name="mesh">The mesh to raycast against.</param>
		/// <param name="matrix">The local to world matrix.</param>
		/// <param name="hit">The raycast hit.</param>
		/// <returns>True if the raycast hit the object.</returns>
		public static bool IntersectRayMesh (Ray ray, Mesh mesh, Matrix4x4 matrix, out RaycastHit hit)
		{
			object [] parameters = new object []
			{
			ray,
			mesh,
			matrix,
			null,
			};

			bool result = (bool) s_IntersectRayMesh.Invoke (null, parameters);
			hit = (RaycastHit) parameters [3];

			return result;
		}

		/// <summary>
		/// Pick a GameObject using screen space coordinates.
		/// </summary>
		/// <param name="position">The screen space position.</param>
		/// <returns>The picked GameObject or null.</returns>
		public static GameObject PickGameObject (Vector2 position)
		{
			return (GameObject) s_PickGameObject.Invoke (null, new object [] { position, false });
		}

		/// <summary>
		/// Select all GameObjects within the screen space rectangle.
		/// </summary>
		/// <param name="rect">The screen space rectangle.</param>
		/// <returns>An array of GameObjects.</returns>
		public static GameObject [] SelectGameObjects (Rect rect)
		{
			return (GameObject []) s_SelectGameObject.Invoke (null, new object [] { rect, false });
		}
		#endregion
	}
}