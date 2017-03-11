﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
	/// <summary>
	/// information(vertex positions, triangles etc) laoded from a *.nas file.
	/// </summary>
	public static class NASParser
	{
		public static NASDataSource Parse(this NASFile file)
		{
			if (file == null) { throw new ArgumentNullException(); }

			vec3[] allVertexPositions = file.VertexPositions;
			BoundingBox box = allVertexPositions.Move2Center();

			var gridGroups = from item in file.Triangles
							 group item by item.FaceLabel;

			IList<EMGrid> gridList = FindGridList(file.VertexPositions, file.VertexNormals, gridGroups);

			IList<NormalLineModel> normalLineModelList = FindNormaLineModelList(
				file.FaceNormalPositions,
				file.FaceNormalDirections,
				file.FaceNormalLengths, 
				gridGroups);

			return new NASDataSource(
				file.VertexPositions, file.VertexNormals, file.Triangles,
				file.FaceNormalPositions, file.FaceNormalDirections, file.FaceNormalLengths,
				gridList, normalLineModelList, box);
		}

		private static List<NormalLineModel> FindNormaLineModelList(
			vec3[] allFaceNormalPositions,
			vec3[] allFaceNormalDirections,
			float[] allFaceNormalLengths,
			IEnumerable<IGrouping<string, Triangle>> gridGroups)
		{
			var list = new List<NormalLineModel>();

			foreach (var group in gridGroups)
			{
				var dict = new Dictionary<int, int>();// index of all -> index of this grid
				string label = group.Key;
				int count = group.Count();

				var faceNormalPositions = new vec3[count];
				var faceNormalDirections = new vec3[count];
				var faceNormalLengths = new float[count];

				int i = 0;
				foreach (var triangle in group)
				{
					int index = triangle.IndexOfTriangles;// triangles.IndexOf(triangle);
					faceNormalPositions[i] = allFaceNormalPositions[index];
					faceNormalDirections[i] = allFaceNormalDirections[index];
					faceNormalLengths[i] = allFaceNormalLengths[index];
					i++;
				}

				list.Add(new NormalLineModel(faceNormalPositions, faceNormalDirections, faceNormalLengths, label));
			}

			return list;
		}

		private static List<EMGrid> FindGridList(
			vec3[] allVertexPositions, vec3[] allVertexNormals,
			IEnumerable<IGrouping<string, Triangle>> gridGroups)
		{
			var list = new List<EMGrid>();

			foreach (var group in gridGroups)
			{
				var dict = new Dictionary<int, int>();// index of all -> index of this grid
				string label = group.Key;
				int count = group.Count();
				var positions = new List<vec3>();
				var normals = new List<vec3>();
				var triangles = new Triangle[count];
				int i = 0;
				foreach (var triangle in group)
				{
					int index1, index2, index3;
					if (dict.ContainsKey(triangle.Num1)) { index1 = dict[triangle.Num1]; }
					else
					{
						index1 = positions.Count;
						positions.Add(allVertexPositions[triangle.Num1]);
						normals.Add(allVertexNormals[triangle.Num1]);
						dict.Add(triangle.Num1, index1);
					}

					if (dict.ContainsKey(triangle.Num2)) { index2 = dict[triangle.Num2]; }
					else
					{
						index2 = positions.Count;
						positions.Add(allVertexPositions[triangle.Num2]);
						normals.Add(allVertexNormals[triangle.Num2]);
						dict.Add(triangle.Num2, index2);
					}

					if (dict.ContainsKey(triangle.Num3)) { index3 = dict[triangle.Num3]; }
					else
					{
						index3 = positions.Count;
						positions.Add(allVertexPositions[triangle.Num3]);
						normals.Add(allVertexNormals[triangle.Num3]);
						dict.Add(triangle.Num3, index3);
					}

					triangles[i++] = new Triangle(index1, index2, index3, label);
				}
				if (i != count) { throw new Exception(); }
				list.Add(new EMGrid(positions.ToArray(), normals.ToArray(), triangles, label));
			}

			return list;
		}


	}
}
