using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class BuildingTest {
		[Test]
		public void TestStaticStuff() {
			Assert.AreEqual(Building.Types.Count, Building.Yields.Count);
			Assert.AreEqual(Building.Types.Count, Building.NearbyModifiers.Count);
			Assert.AreEqual(Building.Types.Count, Building.Modifiers.Count);
			Assert.AreEqual(Building.Types.Count, Building.PopulationMinimums.Count);
			Assert.AreEqual(Building.Types.Count, Building.RequiredFunds.Count);
			Assert.AreEqual(Building.Types.Count, Building.RequiredMaterials.Count);
		}
	}
}
