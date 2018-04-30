using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace FantasticYes.Rendering
{
	public static partial class ShaderUtility
	{
		private static readonly GUIContent [] s_CullOptions = new GUIContent []
		{
			new GUIContent ("Off",              "Culls faces pointing away from camera"),
			new GUIContent ("Front",            "Culls faces pointing towards camera"),
			new GUIContent ("Back",             "Renders front and backfaces"),
		};

		private static readonly GUIContent [] s_ZTestOptions = new GUIContent []
		{
			new GUIContent ("Disabled",         "No depth testing"),
			new GUIContent ("Never",            "Never rendered"),
			new GUIContent ("Less",             "Rendered if depth is less"),
			new GUIContent ("Equal",            "Rendered if depth is equal"),
			new GUIContent ("LEqual",           "Rendered if depth is less or equal"),
			new GUIContent ("Greater",          "Rendered if depth is greater"),
			new GUIContent ("NotEqual",         "Rendered if depth is not equal"),
			new GUIContent ("GEqual",           "Rendered if depth is greater or equal"),
			new GUIContent ("Always",           "Rendered always on top"),
		};

		private static readonly GUIContent [] s_BlendOptions = new GUIContent []
		{
			new GUIContent ("None",             "Renders opaque geometry, no blending"),
			new GUIContent ("Alpha",            "Renders transparent alpha blended"),
			new GUIContent ("Additive",         "Renders transparent additive"),
			new GUIContent ("Multiplicative",   "Renders transparent multiplicative"),
		};

		private static readonly GUIContent [] s_ColorMaskOptions = new GUIContent []
		{
			new GUIContent ("RGBA",             "Renders to the framebuffer RGBA channels"),
			new GUIContent ("RGB",              "Renders to the framebuffer RGB channels"),
			new GUIContent ("R",                "Renders to the framebuffer Red channel"),
			new GUIContent ("G",                "Renders to the framebuffer Green channel"),
			new GUIContent ("B",                "Renders to the framebuffer Blue channel"),
			new GUIContent ("A",                "Renders to the framebuffer Alpha channel"),
		};

		public static void CullProperty (this MaterialEditor materialEditor, MaterialProperty property)
		{
			property.floatValue = EditorGUILayout.Popup (new GUIContent ("Culling"), (int) property.floatValue, s_CullOptions);
		}

		public static void ZTestProperty (this MaterialEditor materialEditor, MaterialProperty property)
		{
			property.floatValue = EditorGUILayout.Popup (new GUIContent ("ZTest"), (int) property.floatValue, s_ZTestOptions);
		}

		public static void BlendProperty (this MaterialEditor materialEditor, MaterialProperty property)
		{
			EditorGUI.BeginChangeCheck ();
			property.floatValue = EditorGUILayout.Popup (new GUIContent ("Blend"), (int) property.floatValue, s_BlendOptions);

			if (EditorGUI.EndChangeCheck ())
			{
				Material material = (Material) materialEditor.target;

				switch ((int) property.floatValue)
				{
					case 0: // None
					{
						material.SetInt ("_ZWrite", 1);
						material.SetInt ("_BlendSrc", (int) BlendMode.One);
						material.SetInt ("_BlendDst", (int) BlendMode.Zero);
						material.SetOverrideTag ("RenderType", "Opaque");
						material.renderQueue = (int) RenderQueue.Geometry;
						break;
					}

					case 1: // Alpha
					{
						material.SetInt ("_ZWrite", 0);
						material.SetFloat ("_BlendSrc", (int) BlendMode.SrcAlpha);
						material.SetFloat ("_BlendDst", (int) BlendMode.OneMinusSrcAlpha);
						material.SetOverrideTag ("RenderType", "Transparent");
						material.renderQueue = (int) RenderQueue.Transparent;
						break;
					}

					case 2: // Additive
					{
						material.SetInt ("_ZWrite", 0);
						material.SetFloat ("_BlendSrc", (int) BlendMode.One);
						material.SetFloat ("_BlendDst", (int) BlendMode.One);
						material.SetOverrideTag ("RenderType", "Transparent");
						material.renderQueue = (int) RenderQueue.Transparent;
						break;
					}

					case 3: // Multiplicative
					{
						material.SetInt ("_ZWrite", 0);
						material.SetFloat ("_BlendSrc", (int) BlendMode.DstColor);
						material.SetFloat ("_BlendDst", (int) BlendMode.Zero);
						material.SetOverrideTag ("RenderType", "Transparent");
						material.renderQueue = (int) RenderQueue.Transparent;
						break;
					}
				}
			}
		}

		public static void ColorMaskProperty (this MaterialEditor materialEditor, MaterialProperty property)
		{
			property.floatValue = EditorGUILayout.Popup (new GUIContent ("ColorMask"), (int) property.floatValue, s_ColorMaskOptions);

			if (EditorGUI.EndChangeCheck ())
			{
				Material material = (Material) materialEditor.target;

				switch ((int) property.floatValue)
				{
					case 0:
					{
						material.SetInt ("_ColorWriteMask", (int) ColorWriteMask.All);
						break;
					}

					case 1:
					{
						material.SetInt ("_ColorWriteMask", (int) (ColorWriteMask.Red | ColorWriteMask.Green | ColorWriteMask.Blue));
						break;
					}

					case 2:
					{
						material.SetInt ("_ColorWriteMask", (int) ColorWriteMask.Red);
						break;
					}

					case 3:
					{
						material.SetInt ("_ColorWriteMask", (int) ColorWriteMask.Green);
						break;
					}

					case 4:
					{
						material.SetInt ("_ColorWriteMask", (int) ColorWriteMask.Blue);
						break;
					}

					case 5:
					{
						material.SetInt ("_ColorWriteMask", (int) ColorWriteMask.Alpha);
						break;
					}
				}
			}
		}
	}
}