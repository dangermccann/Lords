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
using System.Collections.Generic;
using NUnit.Framework;

namespace Lords {
	/// <summary>
	/// Unit test coverage for the functions in the Hex class.
	/// </summary>

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

		[Test]
		public void TestAddition() {
			Hex h1 = new Hex(1, 0);
			Hex h2 = new Hex(2, 1);
			Hex sum = h1 + h2;
			Assert.AreEqual(h1.ToCube() + h2.ToCube(), sum.ToCube());
		}

		[Test]
		public void TestRange1() {
			List<Hex> result = Hex.HexesInRange(new Hex(0, 0), 1);
			Assert.AreEqual(7, result.Count);
			Assert.Contains(new Hex(0, 0), result);
			Assert.Contains(new Hex(1, 0), result);
			Assert.Contains(new Hex(0, 1), result);
			Assert.Contains(new Hex(-1, 1), result);
			Assert.That( result.Contains(new Hex(1, 1)) == false );
		}

		[Test]
		public void TestRange2() {
			List<Hex> result = Hex.HexesInRange(new Hex(-2, 2), 3);
			Assert.AreEqual(37, result.Count);
			Assert.Contains(new Hex(-2, 2), result);
			Assert.Contains(new Hex(0, 0), result);
			Assert.Contains(new Hex(0, -1), result);
			Assert.Contains(new Hex(1, 2), result);
			Assert.Contains(new Hex(-3, 3), result);
			Assert.That( result.Contains(new Hex(2, -1)) == false );
			Assert.That( result.Contains(new Hex(2, 1)) == false );
		}

	}
}
