using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WorldGeneration : MonoBehaviour {

	Mesh mesh;

	Vector3[] vertices;
	int[] triangles;
	Color[] colors;

	public int xSize = 20;
	public int zSize = 20;

	public Gradient gradient;

	float minTerranHeight;
	float maxTerranHeight;

	private void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		//StartCoroutine(CreateShape());
		CreateShape();
	}
	private void Update()
	{
		UpdateMesh();
	}

	void CreateShape()
	{
		vertices = new Vector3[(xSize + 1) * (zSize + 1)];
		for (int z = 0, i = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				float y = Mathf.PerlinNoise(x * .05f, z * .05f) * 6f;
				vertices[i] = new Vector3(x, y, z);

				if (y > maxTerranHeight)
				{
					maxTerranHeight = y;
				}
				if (y < minTerranHeight)
				{
					minTerranHeight = y;
				}
				i++;
			}
		}

		triangles = new int[zSize * zSize * 6];

		int vert = 0;
		int tris = 0;

		for (int z = 0; z < zSize; z++)
		{
			for (int x = 0; x < xSize; x++)
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + zSize + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + xSize + 1;
				triangles[tris + 5] = vert + xSize + 2;

				vert++;
				tris += 6;
				//yield return new WaitForSeconds(.001f);
			}
			vert++;
		}

		colors = new Color[vertices.Length];
		for (int z = 0, i = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				float height = Mathf.InverseLerp(minTerranHeight, maxTerranHeight, vertices[i].y);
				colors[i] = gradient.Evaluate(height);
				i++;
			}
		}

	}

	void UpdateMesh()
	{
		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = colors;

		mesh.RecalculateNormals();
	}

/*	private void OnDrawGizmos()
	{
		if (vertices == null)
		{
			return;
		}
		for (int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawSphere(vertices[i], .1f);
		}
	}*/
}
