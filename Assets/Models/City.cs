using System;
using System.Collections.Generic;

namespace Lords {
	public class City {
		const float FOOD_MULTIPLIER = 100;

		public string Name { get; protected set; }
		public Dictionary<Hex, Tile> Tiles { get; protected set; }
		public int Radius { get; protected set; }
		public Primatives Primatives { get; protected set; }
		public Aggregates Score { get; protected set; }
		public Aggregates VictoryConditions { get; protected set; }
		public Dictionary<BuildingType, List<Building>> Buildings { get; protected set; }

		public City(int radius) {
			this.Radius = radius;
			Tiles = new Dictionary<Hex, Tile>();
			Primatives = new Primatives();
			Score = new Aggregates();
			VictoryConditions = new Aggregates();

			Buildings = new Dictionary<BuildingType, List<Building>>();
			foreach(BuildingType type in Enum.GetValues(typeof(BuildingType))) {
				Buildings[type] = new List<Building>();
			}

			CreateTiles();
		}

		void CreateTiles() {
			for(int x = -1 * Radius; x <= Radius; x++) {
				for(int y = -1 * Radius; y <= Radius; y++) {
					if(Math.Abs(x + y) <= Radius) {
						Hex hex = new Hex(x, y);
						Tiles.Add(hex, new Tile(this, hex));
					}
				}
			}
		}

		public void UpdatePrimatives() {
			Primatives result = new Primatives();
			foreach(List<Building> buildings in Buildings.Values) {
				foreach(Building building in buildings) {
					float popFactor = 1.0f;
					float minimumPop = Building.PopulationMinimums[building.Type];
					if(minimumPop > 0) {
						popFactor = Math.Min(1, PopulationNearby(building.Tile) / minimumPop);
					}

					Primatives buildingYeild = Building.Yields[building.Type] * popFactor;
					result += buildingYeild;
				}
			}

			Primatives = result;
		}

		public void UpdateScore() {
			Score.Population = Math.Min(Primatives.Housing, Primatives.Food * FOOD_MULTIPLIER);
			Score.Happiness = Math.Max(0, Primatives.Entertainment + Primatives.Health + Primatives.Security);
			Score.Prosperity = Math.Max(0, Primatives.Productivity + Primatives.Literacy);
			Score.Culture = Math.Max(0, Primatives.Faith + Primatives.Beauty);
		}

		float PopulationNearby(Tile tile) {
			float foodLevel = Math.Min(1.0f, Primatives.Food * FOOD_MULTIPLIER / Primatives.Housing);
			float population = 0;

			List<Building> b = Buildings[BuildingType.Amphitheater];

			List<BuildingType> types = new List<BuildingType> { BuildingType.Villa, BuildingType.Cottages, BuildingType.Slums };
			foreach(BuildingType type in types) {
				List<Building> buildings = Buildings[type];
				foreach(Building building in buildings) {
					float distance = Hex.Distance(tile.Position, building.Tile.Position);
					population += Building.Yields[building.Type].Housing * foodLevel / distance;
				}
			}

			return population;
		}

		public void AddBuilding(Building b) {
			Buildings[b.Type].Add(b);
		}

		public void RemoveBuilding(Building b) {
			Buildings[b.Type].Remove(b);
		}
	}
	
	public class Aggregates {
		public float Population { get; set; }
		public float Happiness { get; set; }
		public float Prosperity { get; set; }
		public float Culture { get; set; }

		public override string ToString() {
			return String.Format("Population: {0} | Happiness: {1} | Prosperity: {2} | Culture: {3}", 
			                     Population, Happiness, Prosperity, Culture);
		}
	}
}
