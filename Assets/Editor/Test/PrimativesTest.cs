using System;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class PrimativesTest {
		[Test]
		public void TestConstructorsEndEquals() {
			Primatives p1 = new Primatives(1.0f);
			Primatives p2 = new Primatives(1.0f);
			Primatives p3 = new Primatives(3.0f);

			Assert.AreEqual(p1.Security, 1.0f);
			Assert.AreEqual(p3.Security, 3.0f);
			Assert.AreEqual(p1, p2);
			Assert.AreNotEqual(p1, p3);
		}

		[Test]
		public void TestHashCodes() {
			Primatives p1 = new Primatives(1.0f);
			Primatives p2 = new Primatives(1.0f);
			Primatives p3 = new Primatives(3.0f);
			
			Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
			Assert.AreNotEqual(p1.GetHashCode(), p3.GetHashCode());
		}

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
		public void TestFloatAddition() {
			Primatives p1 = new Primatives() {
				Food = 1, Housing = 1, Productivity = 1,
				Health = 1, Security = 1, Beauty = 1,
				Faith = 1, Entertainment = 1, Literacy = 1
			};
			
			Primatives p3 = 2.0f + p1;
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
				Food = 3, Housing = 3, Productivity = 3,
				Health = 3, Security = 3, Beauty = 3,
				Faith = 3, Entertainment = 3, Literacy = 3
			};
			Primatives p2 = new Primatives() {
				Food = 2, Housing = 2, Productivity = 2,
				Health = 2, Security = 2, Beauty = 2,
				Faith = 2, Entertainment = 2, Literacy = 2
			};
			
			Primatives p3 = p1 * p2;
			Assert.AreEqual(6, p3.Food);
			Assert.AreEqual(6, p3.Housing);
			Assert.AreEqual(6, p3.Productivity);
			Assert.AreEqual(6, p3.Health);
			Assert.AreEqual(6, p3.Security);
			Assert.AreEqual(6, p3.Beauty);
			Assert.AreEqual(6, p3.Faith);
			Assert.AreEqual(6, p3.Entertainment);
			Assert.AreEqual(6, p3.Literacy);
		}


		[Test]
		public void TestFloatMultiplication() {
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

