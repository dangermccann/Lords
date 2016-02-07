/// 
/// This work is licensed under the Creative Commons Attribution 4.0 International License. 
/// To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/.
/// 
/// The work below was created for Lord Mayor, a realtime strategy game.  Find out more at:
/// https://lordmayorgame.tumblr.com/
/// 
/// Special thanks to Red Blob Games for authoring a blog post that was the inspiration for
/// the game and this code: http://www.redblobgames.com/grids/hexagons/
/// 
using UnityEngine;
using System;

namespace Lords {
	/// <summary>
	/// Represents a hexagon in Axial coordinates.  
	/// </summary>
	[System.Serializable] 
	public class Hex {
		public const float HEX_SIZE = 0.5f;
		public float q, r;

		public Hex(float q, float r) {
			this.q = q;
			this.r = r;
		}

		public Hex() : this(0, 0) { }

		/// <summary>
		/// Returns the Cube coordinates of the Hex.
		/// </summary>
		/// <returns>The Hex's location in Cube coordinates.</returns>
		public Vector3 ToCube() {
			return HexToCube(this);
		}

		/// <summary>
		/// Returns the location of the Hex in World coordinates.
		/// </summary>
		/// <returns>The location of the Hex in World coordinates.</returns>
		public Vector3 ToWorld() {
			return HexToWorld(this);
		}

		public override bool Equals(System.Object obj) {
			if (obj == null) {
				return false;
			}

			Hex h = obj as Hex;
			if ((System.Object)h == null) {
				return false;
			}

			return (q == h.q) && (r == h.r);
		}
		
		public bool Equals(Hex h) {
			if ((object)h == null) {
				return false;
			}

			return (q == h.q) && (r == h.r);
		}
		
		public override int GetHashCode() {
			return q.GetHashCode() ^ r.GetHashCode() << 2;
		}

		public override string ToString () {
			return String.Format("({0}, {1})", q, r);
		}

		public static Vector2 CubeToAddQOffset(Vector3 cube) {
			Vector2 result = new Vector2();
			result.x = cube.x;
			result.y = cube.z + (cube.x - ((int)cube.x & 1)) / 2;
			return result;
		}

		public static Vector3 OddQOffsetToCube(Vector2 offset) {
			Vector3 result = new Vector3();
			result.x = offset.x;
			result.z = offset.y - (offset.x - ((int)offset.x & 1)) / 2;
			result.y = -result.x - result.z;
			return result;
		}
		
		public static Hex CubeToHex(Vector3 cube) {
			return new Hex(cube.x, cube.z);
		}
		
		public static Vector3 HexToCube(Hex hex) {
			return new Vector3(hex.q, -1 * hex.q - hex.r, hex.r);
		}
		
		public static Vector3 CubeRound(Vector3 cube) {
			float rx = Mathf.Round(cube.x);
			float ry = Mathf.Round(cube.y);
			float rz = Mathf.Round(cube.z);
			
			float diff_x = Mathf.Abs(rx - cube.x);
			float diff_y = Mathf.Abs(ry - cube.y);
			float diff_z = Mathf.Abs(rz - cube.z);
			
			if(diff_x > diff_y && diff_x > diff_z) {
				rx = -ry - rz;
			}
			else if(diff_y > diff_z) {
				ry = -rx - rz;
			}
			else {
				rz = -rx - ry;
			}
			return new Vector3(rx, ry, rz);
		}
		
		public static Hex HexRound(Hex hex) {
			return CubeToHex(CubeRound(HexToCube(hex)));
		}
		
		/// <summary>
		/// Converts a hex into world coordinates.
		/// </summary>
		/// <returns>The world coordinates of the Hex</returns>
		/// <param name="hex">The Hex to convert.</param>
		public static Vector3 HexToWorld(Hex hex) {
			Vector3 result = new Vector3();
			
			result.x = HEX_SIZE * Mathf.Sqrt(3) * (hex.q + hex.r/2);
			result.y = HEX_SIZE * 3/2 * hex.r;
			result.z = 0;
			
			return result;
		}

		/// <summary>
		/// Converts a point in world coordinates into a Hex addressed in Axial coordinates.
		/// </summary>
		/// <returns>The converted Hex</returns>
		/// <param name="world">A point in world coordinates</param>
		public static Hex WorldToHex(Vector3 world) {
			Hex result = new Hex();
			
			result.q = (world.x * Mathf.Sqrt(3)/3 - world.y / 3) / HEX_SIZE;
			result.r = world.y * 2/3 / HEX_SIZE;
			
			return HexRound(result);
		}

		/// <summary>
		/// Calculates the distance between two hexes.
		/// </summary>
		/// <param name="a">The first hex.</param>
		/// <param name="b">The second hex.</param>
		public static float Distance(Hex a, Hex b) {
			Vector3 ac = HexToCube(a);
			Vector3 bc = HexToCube(b);
			return Mathf.Max(Math.Abs(ac.x - bc.x), Math.Abs(ac.y - bc.y), Math.Abs(ac.z - bc.z));
		}
	}
}
