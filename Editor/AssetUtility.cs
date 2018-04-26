/// Date	: 13/04/2018
/// Company	: Fantastic, yes
/// Author	: Maximilian Rötzler
/// License	: This code is licensed under MIT license

using UnityEngine;
using UnityEditor;

namespace FantasticYes.Tools
{
	public static class AssetUtility
	{
		/// <summary>
		/// Finds the specified asset in the project. Optional filter parameter allows to restrict the search.
		/// </summary>
		/// <param name="name">The asset or folder name.</param>
		/// <param name="filter">An optional search filter.</param>
		/// <returns>The asset project path.</returns>
		public static string FindAssetPath (string name, string filter = "")
		{
			string [] guids = AssetDatabase.FindAssets (filter + " " + name);

			if (guids.Length > 0)
			{
				if (guids.Length > 1)
				{
					Debug.LogWarning ("More than one instance of " + name + " exists! Using the first occurance.");
				}

				return AssetDatabase.GUIDToAssetPath (guids [0]);
			}

			Debug.LogError ("File not found " + name);
			return string.Empty;
		}
	}
}