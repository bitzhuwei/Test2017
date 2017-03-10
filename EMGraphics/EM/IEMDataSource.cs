using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EMGraphics
{
	public interface IEMDataSource
	{
		#region whole model

		/// <summary>
		/// whole model's vertex positions.
		/// </summary>
		vec3[] VertexPositions { get; }

		/// <summary>
		/// whole model's vertex normals.
		/// </summary>
		vec3[] VertexNormals { get; }

		/// <summary>
		/// whole model's triangles.
		/// </summary>
		Triangle[] Triangles { get; }

		/// <summary>
		/// whole model's triangles' positions(center of triangle).
		/// </summary>
		vec3[] FaceNormalPositions { get; }

		/// <summary>
		/// whole model's triangles' normals' directions.
		/// </summary>
		vec3[] FaceNormalDirections { get; }

		/// <summary>
		/// whole model's triangles' normals' lengths.
		/// </summary>
		float[] FaceNormalLengths { get; }

		#endregion whole model

		#region parted faces

		/// <summary>
		/// list of faces.
		/// </summary>
		IList<EMGrid> GridList { get; }

		/// <summary>
		/// list of normals of faces.
		/// </summary>
		IList<NormalLineModel> NormalList { get; }

		#endregion parted faces

		/// <summary>
		/// Whole model's position and size.
		/// </summary>
		BoundingBox Box { get; }

	}
}
