using UnityEngine;


[ExecuteInEditMode]
public class VertexStream : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private Mesh m_mesh;

	/// <summary>
	/// Copy the lightmap uvs from the specified mesh and apply the lightmap uv offset.
	/// </summary>
	/// <param name="mesh"></param>
	/// <param name="uvScaleOffset"></param>
	private void SetLightmapUVs (Mesh mesh, Vector4 lightmapScaleOffset)
	{
		if (m_mesh.uv2 != null)
		{
			for (int i = 0; i < m_mesh.vertexCount; i++)
			{
				Vector2 uv = mesh.uv2 [i];

				uv.x = uv.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
				uv.y = uv.y * lightmapScaleOffset.y + lightmapScaleOffset.w;

				m_mesh.uv2 [i] = uv;
			}
		}
	}

	private void Start ()
	{
		MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
		meshRenderer.additionalVertexStreams = m_mesh;

		m_mesh.UploadMeshData (true);
	}

#if UNITY_EDITOR

	#region Editor Methods
	[SerializeField, HideInInspector]
	private int instanceID = 0;

	/// <summary>
	/// Gets and sets the vertex normals.
	/// </summary>
	public Vector3 [] Normals
	{
		get
		{
			if (m_mesh.normals.Length == 0)
			{
				m_mesh.normals = new Vector3 [m_mesh.vertexCount];
			}

			return m_mesh.normals;
		}
		set
		{
			if (value.Length == m_mesh.vertexCount)
			{
				m_mesh.normals = value;
			}
		}
	}

	/// <summary>
	/// Gets and sets the vertex tangents.
	/// </summary>
	public Vector4 [] Tangents
	{
		get
		{
			if (m_mesh.tangents.Length == 0)
			{
				m_mesh.tangents = new Vector4 [m_mesh.vertexCount];
			}

			return m_mesh.tangents;
		}
		set
		{
			if (value.Length == m_mesh.vertexCount)
			{
				m_mesh.tangents = value;
			}
		}
	}

	/// <summary>
	/// Gets and sets the vertex colors.
	/// </summary>
	public Color [] Colors
	{
		get
		{
			if (m_mesh.colors.Length == 0)
			{
				m_mesh.colors = new Color [m_mesh.vertexCount];
			}

			return m_mesh.colors;
		}
		set
		{
			if (value.Length == m_mesh.vertexCount)
			{
				m_mesh.colors = value;
			}
		}
	}

	/// <summary>
	/// Gets and sets the vertex UV coordinates.
	/// </summary>
	public Vector2 [] UV
	{
		get
		{
			if (m_mesh.uv.Length == 0)
			{
				m_mesh.uv = new Vector2 [m_mesh.vertexCount];
			}

			return m_mesh.uv;
		}
		set
		{
			if (value.Length == m_mesh.vertexCount)
			{
				m_mesh.uv = value;
			}
		}
	}

	/// <summary>
	/// Gets and sets the vertex UV2 coordinates.
	/// </summary>
	public Vector2 [] UV2
	{
		get
		{
			if (m_mesh.uv2.Length == 0)
			{
				m_mesh.uv2 = new Vector2 [m_mesh.vertexCount];
			}

			return m_mesh.uv2;
		}
		set
		{
			if (value.Length == m_mesh.vertexCount)
			{
				m_mesh.uv2 = value;
			}
		}
	}

	/// <summary>
	/// Gets and sets the vertex UV3 coordinates.
	/// </summary>
	public Vector2 [] UV3
	{
		get
		{
			if (m_mesh.uv3.Length == 0)
			{
				m_mesh.uv3 = new Vector2 [m_mesh.vertexCount];
			}

			return m_mesh.uv3;
		}
		set
		{
			if (value.Length == m_mesh.vertexCount)
			{
				m_mesh.uv3 = value;
			}
		}
	}

	/// <summary>
	/// Gets the VertexStream component from the specified GameObject.
	/// </summary>
	/// <param name="gameObject">The GameObject.</param>
	/// <returns></returns>
	public static VertexStream GetComponent (GameObject gameObject)
	{
		VertexStream vertexStream = gameObject.GetComponent<VertexStream> ();

		if (vertexStream == null)
		{
			vertexStream = gameObject.AddComponent<VertexStream> ();
			vertexStream.hideFlags = HideFlags.HideInInspector;
			vertexStream.CreateNewMesh ();
		}

		return vertexStream;
	}

	/// <summary>
	///  Creates a new mesh.
	/// </summary>
	private void CreateNewMesh ()
	{
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter> ();
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer> ();

		m_mesh = new Mesh
		{
			vertices = meshFilter.sharedMesh.vertices
		};

		m_mesh.MarkDynamic ();
		m_mesh.UploadMeshData (false);

		meshRenderer.additionalVertexStreams = m_mesh;
	}

	private void CopyNewMesh ()
	{
		Mesh copy = m_mesh;

		m_mesh = new Mesh
		{
			vertices = copy.vertices,
			tangents = copy.tangents,
			normals = copy.normals,
			colors = copy.colors,
			uv3 = copy.uv3,
			uv2 = copy.uv2,
			uv = copy.uv
		};

		m_mesh.MarkDynamic ();
		m_mesh.UploadMeshData (false);
	}

	/// <summary>
	/// Check if the object was duplicated, break the mesh reference.
	/// </summary>
	private void Awake ()
	{
		if (instanceID != 0)
		{
			CopyNewMesh ();
		}

		instanceID = gameObject.GetInstanceID ();
	}

	private void Update ()
	{
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer> ();
		meshRenderer.additionalVertexStreams = m_mesh;
	}
	#endregion

#endif
}

