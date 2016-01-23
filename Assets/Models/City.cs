using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	public class City {

		static float MAX_FUNDS = 10000;
		static float MAX_RAW_MATERIALS = 10000;

		public Dictionary<Hex, Tile> Tiles { get; protected set; }
		public Level Level { get; protected set; }
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

		public City(Level level) {
			this.Level = level;
			Tiles = new Dictionary<Hex, Tile>();
			Primatives = new Primatives();
			Score = new Aggregates();

			Buildings = new Dictionary<BuildingType, List<Building>>();
			foreach(BuildingType type in Building.Types) {
				Buildings[type] = new List<Building>();
			}

			Funds = Level.initialFunds;
			RawMaterials = Level.initialRawMaterials;

			CreateTiles();
		}

		void CreateTiles() {
			int radius = Level.mapConfiguration.Radius;
			for(int x = -1 * radius; x <= radius; x++) {
				for(int y = -1 * radius; y <= radius; y++) {
					if(Math.Abs(x + y) <= radius) {
						Hex hex = new Hex(x, y);
						Tiles.Add(hex, new Tile(this, hex));
					}
				}
			}
		}

		public void SetMap(Map map) {
			foreach(Tile tile in Tiles.Values) {
				tile.Type = map.GetTileTypeAt(tile.Position);
			}
		}

		public void UpdatePrimatives() {
			Primatives result = new Primatives();
			foreach(List<Building> buildings in Buildings.Values) {
				foreach(Building building in buildings) {
					Primatives buildingYeild = Building.Yields[building.Type];
					float effectiveness = BuildingEffectiveness(building);

					// tile modifiers
					if(Building.Modifiers[building.Type].ContainsKey(building.Tile.Type)) {
						buildingYeild *= (1.0f + Building.Modifiers[building.Type][building.Tile.Type]);
					}

					// nearby building modifiers
					foreach(BuildingType nearbyBuildingType in Building.NearbyModifiers[building.Type].Keys) {
						Primatives nearbyModifier = Building.NearbyModifiers[building.Type][nearbyBuildingType];

						// find all nearby buildings of this type
						foreach(Building nearbyBuilding in Buildings[nearbyBuildingType]) {
							Debug.Log("this: " + building.Type + " that: " + nearbyBuilding.Type);

							float distance = Hex.Distance(building.Tile.Position, nearbyBuilding.Tile.Position);
							Primatives thisBuildingModifier = nearbyModifier / distance;
							buildingYeild *= (1.0f + thisBuildingModifier);
							Debug.Log("buildingYeild: " + buildingYeild);
						}
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
			return PrimativeValueNearby(tile, 
					new BuildingType[] { BuildingType.Villa, BuildingType.Cottages, BuildingType.Slums }, 
					PrimativeValues.Housing) * FoodLevel();
		}

		float PrimativeValueNearby(Tile tile, BuildingType[] types, string field) {
			float value = 0;

			foreach(BuildingType type in types) {
				List<Building> buildings = Buildings[type];
				foreach(Building building in buildings) {
					float distance = Hex.Distance(tile.Position, building.Tile.Position);
					value += Building.Yields[building.Type][field] / distance;
				}
			}
			
			return value;
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

		public bool MeetsVictoryConditions() {
			return Score.Population >= Level.victoryConditions.Population &&
				Score.Happiness >= Level.victoryConditions.Happiness &&
				Score.Prosperity >= Level.victoryConditions.Prosperity &&
				Score.Culture >= Level.victoryConditions.Culture;
		}

		public bool MeetsFailureConditions() {


			return false;
		}

		public SavedCity SaveCity() {
			SavedCity saved = new SavedCity();
			saved.rawMaterials = this.RawMaterials;
			saved.funds = this.Funds;
			saved.level = this.Level.name;
			
			foreach(List<Building> buildings in this.Buildings.Values) {
				foreach(Building building in buildings) {
					SavedBuilding savedBuilding = new SavedBuilding();
					savedBuilding.position = building.Tile.Position;
					savedBuilding.type = building.Type;
					saved.buildings.Add(savedBuilding);
				}
			}
			
			return saved;
		}

		public static City LoadCity(SavedCity saved) {
			City city = new City(Levels.GetLevel(saved.level));
			city.RawMaterials = saved.rawMaterials;
			city.Funds = saved.funds;

			foreach(SavedBuilding savedBuilding in saved.buildings) {
				Building building = new Building(city.Tiles[savedBuilding.position], savedBuilding.type);
				building.ResetCreateTime();
				city.Buildings[building.Type].Add(building);
				city.Tiles[savedBuilding.position].Building = building;
			}

			return city;
		}
	}
}
