using System;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class PrimativesTest {
		[Test]
		public void TestAddition() {
			Primatives p1 = new Primatives() {
				Food = 1, Housing = 1, Productivity = 1,
				Health = 1, Security = 1, Beauty = 1,
				Faith = 1, Entertainment = 1, Literacy = 1
			};
			Primatives p2 = new Primatives() {
				Food = 2, Housing = 2, Productivity = 2,
				Health = 2, Security = 2, Beauty = 2,
				Faith = 2, Entertainment = 2, Literacy = 2
			};

			Primatives p3 = p1 + p2;
			Assert.AreEqual(3, p3.Food);
			Assert.AreEqual(3, p3.Housing);
			Assert.AreEqual(3, p3.Productivity);
			Assert.AreEqual(3, p3.Health);
			Assert.AreEqual(3, p3.Security);
			Assert.AreEqual(3, p3.Beauty);
			Assert.AreEqual(3, p3.Faith);
			Assert.AreEqual(3, p3.Entertainment);
			Assert.AreEqual(3, p3.Literacy);
		}

		[Test]
		public void TestMultiplication() {
			Primatives p1 = new Primatives() {
				Food = 1, Housing = 1, Productivity = 1,
				Health = 1, Security = 1, Beauty = 1,
				Faith = 1, Entertainment = 1, Literacy = 1
			};
			p1 *= 3;
			Assert.AreEqual(3, p1.Food);
			Assert.AreEqual(3, p1.Housing);
			Assert.AreEqual(3, p1.Productivity);
			Assert.AreEqual(3, p1.Health);
			Assert.AreEqual(3, p1.Security);
			Assert.AreEqual(3, p1.Beauty);
			Assert.AreEqual(3, p1.Faith);
			Assert.AreEqual(3, p1.Entertainment);
			Assert.AreEqual(3, p1.Literacy);
		}

		[Test]
		public void TestPropGet() {
			Primatives p1 = new Primatives() {
				Food = 1, Housing = 1, Productivity = 1,
				Health = 1, Security = 1, Beauty = 1,
				Faith = 1, Entertainment = 1, Literacy = 1
			};
			Assert.AreEqual(1, p1["Food"]);
			Assert.AreEqual(1, p1["Housing"]);
		}
	}
}

