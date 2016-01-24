using UnityEngine;
using System;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class BuildingTest {
		[Test]
		public void TestStaticStuff() {
			Assert.AreEqual(Enum.GetValues(typeof(BuildingType)).Length, Building.Types.Count);
			Assert.AreEqual(Building.Types.Count, Building.Yields.Count);
			Assert.AreEqual(Building.Types.Count, Building.NearbyModifiers.Count);
			Assert.AreEqual(Building.Types.Count, Building.Modifiers.Count);
			Assert.AreEqual(Building.Types.Count, Building.PopulationMinimums.Count);
			Assert.AreEqual(Building.Types.Count, Building.RequiredFunds.Count);
			Assert.AreEqual(Building.Types.Count, Building.RequiredMaterials.Count);
			Assert.AreEqual(Building.Types.Count, Building.PositiveYields.Count);
		}
	}
}
