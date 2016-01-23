using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class CityTest {
		[Test]
		public void TestBasicPrimatives() {
			City city = new City(Levels.Tutorial);
			Building villa = new Building(city.Tiles[new Hex(0, 0)], BuildingType.Villa).ResetCreateTime();
			Building farm = new Building(city.Tiles[new Hex(0, 1)], BuildingType.Wheat_Farm).ResetCreateTime();

			city.AddBuilding(villa);
			city.AddBuilding(farm);
			city.UpdatePrimatives();
			city.UpdateScore();

			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Primatives.Housing);
			Assert.AreEqual(Building.Yields[BuildingType.Wheat_Farm].Food, city.Primatives.Food);
			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Score.Population);
		}

		[Test]
		public void TestTerrainModifier() {
			City city = new City(Levels.Tutorial);
			city.Tiles[new Hex(0, 1)].Type = TileType.Snow;

			Building villa = new Building(city.Tiles[new Hex(0, 0)], BuildingType.Villa).ResetCreateTime();
			Building farm = new Building(city.Tiles[new Hex(0, 1)], BuildingType.Wheat_Farm).ResetCreateTime();
			
			city.AddBuilding(villa);
			city.AddBuilding(farm);
			city.UpdatePrimatives();
			city.UpdateScore();

			float modifierValue = (1.0f + Building.Modifiers[BuildingType.Wheat_Farm][TileType.Snow]).Food;

			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Primatives.Housing);
			Assert.AreEqual(Building.Yields[BuildingType.Wheat_Farm].Food * modifierValue, city.Primatives.Food);
			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Score.Population);
		}

		[Test]
		public void TestDistanceModifier() {
			City city = new City(Levels.Tutorial);
			Building villa = new Building(city.Tiles[new Hex(0, 0)], BuildingType.Villa).ResetCreateTime();
			Building farm = new Building(city.Tiles[new Hex(0, 1)], BuildingType.Wheat_Farm).ResetCreateTime();
			Building tavern = new Building(city.Tiles[new Hex(0, 3)], BuildingType.Tavern).ResetCreateTime();
			
			city.AddBuilding(villa);
			city.AddBuilding(farm);
			city.AddBuilding(tavern);
			city.UpdatePrimatives();
			city.UpdatePrimatives();
			city.UpdateScore();
			
			float modifierValue = 1 / Hex.Distance(villa.Tile.Position, tavern.Tile.Position); 
			modifierValue *= Building.Yields[BuildingType.Villa].Housing / Building.PopulationMinimums[BuildingType.Tavern];

			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Primatives.Housing);
			Assert.AreEqual(Building.Yields[BuildingType.Wheat_Farm].Food, city.Primatives.Food);
			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Score.Population);
			Assert.AreEqual(Roundish(Building.Yields[BuildingType.Tavern].Entertainment * modifierValue), Roundish(city.Primatives.Entertainment));
		}

		[Test]
		public void TestNearbyModifier() {
			City city = new City(Levels.Tutorial);
			Building villa = new Building(city.Tiles[new Hex(0, 0)], BuildingType.Villa).ResetCreateTime();
			Building farm = new Building(city.Tiles[new Hex(0, 1)], BuildingType.Wheat_Farm).ResetCreateTime();
			Building tavern = new Building(city.Tiles[new Hex(0, -1)], BuildingType.Tavern).ResetCreateTime();
			Building fort = new Building(city.Tiles[new Hex(1, -1)], BuildingType.Fort).ResetCreateTime();
			
			city.AddBuilding(villa);
			city.AddBuilding(farm);
			city.AddBuilding(tavern);
			city.AddBuilding(fort);
			city.UpdatePrimatives();
			city.UpdatePrimatives();
			city.UpdateScore();
			
			float expected = Building.Yields[BuildingType.Tavern].Security * Building.Yields[BuildingType.Villa].Housing / Building.PopulationMinimums[BuildingType.Tavern];
			expected *= (1.0f + Building.NearbyModifiers[BuildingType.Tavern][BuildingType.Fort]).Security;
			expected += Building.Yields[BuildingType.Fort].Security * Building.Yields[BuildingType.Villa].Housing / Building.PopulationMinimums[BuildingType.Fort];

			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Primatives.Housing);
			Assert.AreEqual(Building.Yields[BuildingType.Wheat_Farm].Food, city.Primatives.Food);
			Assert.AreEqual(Building.Yields[BuildingType.Villa].Housing, city.Score.Population);
			Assert.AreEqual(Roundish(expected), Roundish(city.Primatives.Security));
		}

		float Roundish(float val) {
			return Mathf.Round(val * 10000f) / 10000f;
		}
	}
}
