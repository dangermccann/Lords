using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class HexTest {
		[Test]
		public void TestOverrides() {
			Hex h1 = new Hex(3, 4);
			Hex h2 = new Hex(3, 4);
			Hex h3 = new Hex(-3, -4);

			Assert.AreEqual(h1, h2);
			Assert.AreEqual(h1.GetHashCode(), h2.GetHashCode());
			Assert.AreNotEqual(h1, h3);
			Assert.AreNotEqual(h1.GetHashCode(), h3.GetHashCode());
		}

		[Test]
		public void TestWorldConversion() {
			Hex hex = new Hex(3, 4);
			Assert.AreEqual(hex, Hex.WorldToHex(Hex.HexToWorld(hex)));
		}

		[Test]
		public void TestCubeConversion() {
			Hex hex = new Hex(5, 6);
			Assert.AreEqual(hex, Hex.CubeToHex(Hex.HexToCube(hex)));
		}

		[Test]
		public void TestOddQConversion() {
			Hex hex = new Hex(-3, -4);
			Vector3 cube = Hex.HexToCube(hex);
			Assert.AreEqual(cube, Hex.OddQOffsetToCube(Hex.CubeToAddQOffset(cube)));
		}

		[Test]
		public void TestDistance() {
			Hex h1 = new Hex(1, -2);
			Hex h2 = new Hex(-2, 1);
			Assert.AreEqual(3, Hex.Distance(h1, h2));
		}

		[Test]
		public void TestDistance2() {
			Hex h1 = new Hex(0, -1);
			Hex h2 = new Hex(1, -1);
			Assert.AreEqual(1, Hex.Distance(h1, h2));
		}
	}
}
