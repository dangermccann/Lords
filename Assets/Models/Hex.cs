using UnityEngine;
using System;

namespace Lords {
	public class Hex {
		public const float HEX_SIZE = 1f;
		public float q, r;

		public Hex(float q, float r) {
			this.q = q;
			this.r = r;
		}

		public Hex() : this(0, 0) { }

		public Vector3 ToCube() {
			return HexToCube(this);
		}

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
		

		public static Vector3 HexToWorld(Hex hex) {
			Vector3 result = new Vector3();
			
			result.x = hex.q * 1.5f;
			result.y = Mathf.Sqrt(3) * (hex.r + hex.q / 2);
			result.z = 0;
			
			return result;
		}
		
		public static Hex WorldToHex(Vector3 world) {
			Hex result = new Hex();
			
			result.q = world.x * 2/3 / HEX_SIZE;
			result.r = (-1 * world.x / 3 + Mathf.Sqrt(3) / 3 * world.y) / HEX_SIZE;
			
			return HexRound(result);
		}
	}
}
