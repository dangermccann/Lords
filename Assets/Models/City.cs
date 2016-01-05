using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	public class City {

		static float MAX_FUNDS = 10000;
		static float MAX_RAW_MATERIALS = 10000;

		public string Name { get; protected set; }
		public Dictionary<Hex, Tile> Tiles { get; protected set; }
		public int Radius { get; protected set; }
		public Primatives Primatives { get; protected set; }
		public Aggregates Score { get; protected set; }
		public Dictionary<BuildingType, List<Building>> Buildings { get; protected set; }
		public float RawMaterials { get; protected set; }
		public float Funds { get; protected set; }

		public float FoodLevel() {
			if(Primatives.Housing > 0) {
				return Math.Min(1.0f, Primatives.Food * Building.FARM_OUTPUT / Primatives.Housing);
			}
			else return 0;
		}

		public City(int radius, float initialFunds, float initialMaterials) {
			this.Radius = radius;
			Tiles = new Dictionary<Hex, Tile>();
			Primatives = new Primatives();
			Score = new Aggregates();

			Buildings = new Dictionary<BuildingType, List<Building>>();
			foreach(BuildingType type in Building.Types) {
				Buildings[type] = new List<Building>();
			}

			Funds = initialFunds;
			RawMaterials = initialMaterials;

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
					float effectiveness = BuildingEffectiveness(building);

					Primatives buildingYeild = Building.Yields[building.Type];
					if(Building.Modifiers[building.Type].ContainsKey(building.Tile.Type)) {
						buildingYeild += Building.Modifiers[building.Type][building.Tile.Type];
					}
					buildingYeild *= effectiveness;
					result += buildingYeild;
				}
			}

			Primatives = result;
		}

		public float BuildingEffectiveness(Building building) {
			float popFactor = 1.0f;
			float minimumPop = Building.PopulationMinimums[building.Type];
			if(minimumPop > 0) {
				popFactor = Math.Min(1, PopulationNearby(building.Tile) / minimumPop);
			}
			
			float timeFactor = Math.Min(1, (Time.fixedTime - building.CreateTime) / Building.BUILD_TIME);
			return popFactor * timeFactor;
		}
		
		public void UpdateScore() {
			Score.Population = Math.Min(Primatives.Housing, Primatives.Food * Building.FARM_OUTPUT);
			Score.Happiness = Primatives.Entertainment + Primatives.Health + Primatives.Security;
			Score.Prosperity = Primatives.Productivity + Primatives.Literacy;
			Score.Culture = Primatives.Faith + Primatives.Beauty;
		}

		public void UpdateFunds(float deltaTime) {
			float foodLevel = FoodLevel();

			foreach(List<Building> buildings in Buildings.Values) {
				foreach(Building building in buildings) {
					if(Building.TaxRates.ContainsKey(building.Type)) {
						float housingYield = Building.Yields[building.Type].Housing;
						float taxRate = Building.TaxRates[building.Type];
						Funds += foodLevel * housingYield * taxRate * deltaTime;
					}
				}
			}

			Funds = Math.Min(Funds, MAX_FUNDS);
		}

		public void UpdateRawMaterials(float deltaTime) {
			float foodLevel = FoodLevel();

			foreach(List<Building> buildings in Buildings.Values) {
				foreach(Building building in buildings) {
					if(Building.RawMaterialProduction.ContainsKey(building.Type)) {
						float housingYield = Building.Yields[building.Type].Housing;
						float production = Building.RawMaterialProduction[building.Type];
						RawMaterials += foodLevel * housingYield * production * deltaTime;
					}
				}
			}

			foreach(Building workshop in Buildings[BuildingType.Workshop]) {
				float popFactor = 1.0f;
				float minimumPop = Building.PopulationMinimums[BuildingType.Workshop];
				if(minimumPop > 0) {
					popFactor = Math.Min(1, PopulationNearby(workshop.Tile) / minimumPop);
				}

				RawMaterials += popFactor * Building.WORKSHOP_BONUS * deltaTime;
			}

			RawMaterials = Math.Min(RawMaterials, MAX_RAW_MATERIALS);
		}

		public void UpdateEverything(float deltaTime) {
			UpdatePrimatives();
			UpdateFunds(deltaTime);
			UpdateRawMaterials(deltaTime);
			UpdateScore();
		}

		float PopulationNearby(Tile tile) {
			float population = 0;

			List<BuildingType> types = new List<BuildingType> { BuildingType.Villa, BuildingType.Cottages, BuildingType.Slums };
			foreach(BuildingType type in types) {
				List<Building> buildings = Buildings[type];
				foreach(Building building in buildings) {
					float distance = Hex.Distance(tile.Position, building.Tile.Position);
					population += Building.Yields[building.Type].Housing * FoodLevel() / distance;
				}
			}

			return population;
		}

		public void AddBuilding(Building b) {
			Buildings[b.Type].Add(b);
			Funds -= Building.RequiredFunds[b.Type];
			RawMaterials -= Building.RequiredMaterials[b.Type];
		}

		public void RemoveBuilding(Building b) {
			Buildings[b.Type].Remove(b);
		}

		public Boolean CanBuild(BuildingType type) {
			return Funds >= Building.RequiredFunds[type] && 
				RawMaterials >= Building.RequiredMaterials[type] &&
				Score.Population >= Building.PopulationMinimums[type];
		}

		public bool MeetsVictoryConditions(Aggregates victoryConditions) {
			return Score.Population >= victoryConditions.Population &&
				Score.Happiness >= victoryConditions.Happiness &&
				Score.Prosperity >= victoryConditions.Prosperity &&
				Score.Culture >= victoryConditions.Culture;
		}

		public bool MeetsFailureConditions() {


			return false;
		}
	}
}
