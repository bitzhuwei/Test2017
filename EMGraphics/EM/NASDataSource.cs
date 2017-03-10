using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EMGraphics
{
	public class NASDataSource : IEMDataSource
	{
		#region whole model

		public vec3[] VertexPositions { get; private set; }
		public vec3[] VertexNormals { get; private set; }
		public Triangle[] Triangles { get; private set; }

		public vec3[] FaceNormalPositions { get; private set; }
		public vec3[] FaceNormalDirections { get; private set; }
		public float[] FaceNormalLengths { get; private set; }

		#endregion whole model

		#region parted faces

		public IList<EMGrid> GridList { get; private set; }
		public IList<NormalLineModel> NormalList { get; private set; }

		#endregion parted faces

		/// <summary>
		/// Whole model's position and size.
		/// </summary>
		public BoundingBox Box { get; private set; }

		public NASDataSource(
			vec3[] vertexPositions, vec3[] vertexNormals, Triangle[] triangles,
			vec3[] faceNormalPositions, vec3[] faceNormalDirections, float[] faceNormalLengths,
			IList<EMGrid> gridList, IList<NormalLineModel> normalList, BoundingBox box)
		{
			if (vertexPositions == null || vertexNormals == null || triangles == null)
			{
				throw new ArgumentNullException();
			}
			if (faceNormalPositions == null || faceNormalDirections == null || faceNormalLengths == null)
			{
				throw new ArgumentNullException();
			}
			if (gridList == null || normalList == null || box == null)
			{
				throw new ArgumentNullException();
			}

			this.VertexPositions = vertexPositions;
			this.VertexNormals = vertexNormals;
			this.Triangles = triangles;

			this.FaceNormalPositions = faceNormalPositions;
			this.FaceNormalDirections = faceNormalDirections;
			this.FaceNormalLengths = faceNormalLengths;

			this.GridList = gridList;
			this.NormalList = normalList;

			this.Box = box;
		}

		public override string ToString()
		{
			return string.Format("{0} grid(s); {1} normal(s), {2}",
				this.GridList.Count, this.NormalList.Count, this.Box);
		}
	}
}
