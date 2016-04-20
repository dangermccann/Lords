using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	public class City {

		static float MAX_FUNDS = 10000;
		static float MAX_RAW_MATERIALS = 10000;

		public static Dictionary<Imports, float> DEFAULT_IMPORT_ALLOCATION = new Dictionary<Imports, float>() {
			{ Imports.Medicine, 1 / Trade.ImportItemCount },
			{ Imports.Spirits,  1 / Trade.ImportItemCount },
			{ Imports.Books,    1 / Trade.ImportItemCount },
			{ Imports.Incense,  1 / Trade.ImportItemCount },
			{ Imports.Jewlery,  1 / Trade.ImportItemCount },
			{ Imports.Weapons,  1 / Trade.ImportItemCount }
		};

		public Dictionary<Hex, Tile> Tiles { get; protected set; }
		public Level Level { get; protected set; }
		public Primatives Primatives { get; protected set; }
		public Aggregates Score { get; protected set; }
		public Dictionary<BuildingType, List<Building>> Buildings { get; protected set; }
		public float RawMaterials { get; protected set; }
		public float Funds { get; protected set; }
		public float ElapsedTime { get; protected set; }
		public Dictionary<Exports, float> Exports { get; protected set; }
		public float ExportTotal { get; protected set; }
		public Dictionary<Imports, float> ImportAllocation { get; protected set; }

		public float FoodLevel() {
			if(Primatives.Housing > 0) {
				return Math.Min(1.0f, Primatives.Food * Building.FARM_OUTPUT / Primatives.Housing);
			}
			else return 0;
		}

		public float FoodPerCapita() {
			if(Primatives.Housing > 0) {
				return Primatives.Food * Building.FARM_OUTPUT / Primatives.Housing;
			}
			else return 0;
		}

		public City(Level level) {
			this.Level = level;
			Tiles = new Dictionary<Hex, Tile>();
			Primatives = new Primatives();
			Score = new Aggregates();
			Exports = new Dictionary<Exports, float>();
			ImportAllocation = DEFAULT_IMPORT_ALLOCATION;

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
					result += EffectiveYield(building);
				}
			}

			Primatives = result;
		}

		public void UpdateExports() {
			ExportTotal = 0;


			foreach(Exports export in Trade.ExportLookupTable.Keys) {
				float count = 0;

				// Must have a Trading Post to trade
				if(Buildings[BuildingType.Trading_Post].Count > 0) {
					foreach(Building b in Buildings[Trade.ExportLookupTable[export]]) {
						count += Trade.EXPORT_QTY_PER_BUILDING * BuildingEffectiveness(b);
					}
				}

				this.Exports[export] = count;
				ExportTotal += count * Game.CurrentLevel.exportPrices[export];
			}

		}

		public void UpdateImports() {
			foreach(Imports import in Trade.ImportLookupTable.Keys) {
				float amount = ExportTotal / Level.importPrices[import] * ImportAllocation[import];

				// Map the value from the Imports enum to the Primative field and increment the amount
				Primatives[Trade.ImportLookupTable[import]] += amount;
			}
		}

		public Primatives EffectiveYield(Building building) {
			Primatives buildingYeild = Building.Yields[building.Type];
			
			buildingYeild *= TimeMultiplier(building);
			buildingYeild *= PopulationMultiplier(building);
			buildingYeild *= TileModifier(building);
			buildingYeild *= NearbyBuildingModifier(building);
			return buildingYeild;
		}

		public float TimeMultiplier(Building building) {
			return Math.Min(1, (Time.fixedTime - building.CreateTime) / Building.BUILD_TIME);
		}
		
		public float PopulationMultiplier(Building building) {
			float popFactor = 1.0f;
			float minimumPop = Building.RequiredNearbyPeople[building.Type];
			if(minimumPop > 0) {
				popFactor = Math.Min(1, PopulationNearby(building.Tile) / minimumPop);
			}
			return popFactor;
		}

		public Primatives TileModifier(Building building) {
			if(Building.Modifiers[building.Type].ContainsKey(building.Tile.Type)) {
				return (1.0f + Building.Modifiers[building.Type][building.Tile.Type]);
			}
			return new Primatives(1.0f);
		}

		public Primatives NearbyBuildingModifier(Building building) {
			Primatives modifier = new Primatives(1.0f);
			foreach(BuildingType nearbyBuildingType in Building.NearbyModifiers[building.Type].Keys) {
				Primatives nearbyModifier = Building.NearbyModifiers[building.Type][nearbyBuildingType];
				
				// find all nearby buildings of this type
				foreach(Building nearbyBuilding in Buildings[nearbyBuildingType]) {
					if(nearbyBuilding != building) {
						float distance = Hex.Distance(building.Tile.Position, nearbyBuilding.Tile.Position);
						if(distance < 5) {
							Primatives thisBuildingModifier = nearbyModifier / Mathf.Pow(distance, 1.5f);
							modifier *= (1.0f + thisBuildingModifier);
						}
					}
				}
			}

			return modifier;
		}

		public float BuildingEffectiveness(Building building) {
			float effectiveness = 1.0f;
			string positiveYield = Building.PositiveYields[building.Type];

			effectiveness *= TimeMultiplier(building) * PopulationMultiplier(building);
			effectiveness *= TileModifier(building)[positiveYield];
			effectiveness *= NearbyBuildingModifier(building)[positiveYield];

			return effectiveness;
		}

		public void UpdateScore() {
			Score.Population = Math.Min(Primatives.Housing, Primatives.Food * Building.FARM_OUTPUT);
			Score.Happiness = Primatives.Entertainment + Primatives.Health + Primatives.Security;
			Score.Prosperity = Primatives.Productivity + Primatives.Literacy;
			Score.Culture = Primatives.Faith + Primatives.Beauty;
		}

		public void UpdateFunds(float deltaTime) {
			if(Score.Population == 0) 
				return;

			Funds += Level.additionalFundsPerSecond * deltaTime;

			float foodLevel = FoodLevel();

			foreach(List<Building> buildings in Buildings.Values) {
				foreach(Building building in buildings) {
					if(Building.TaxRates.ContainsKey(building.Type)) {
						float taxRate = Building.TaxRates[building.Type];
						Funds += foodLevel * taxRate * deltaTime;
					}
				}
			}
			Funds = Math.Min(Funds, MAX_FUNDS);
		}

		public void UpdateRawMaterials(float deltaTime) {
			if(Score.Population == 0) 
				return;

			RawMaterials += Level.additionalRawMaterialsPerSecond * deltaTime;

			float foodLevel = FoodLevel();

			foreach(List<Building> buildings in Buildings.Values) {
				foreach(Building building in buildings) {
					if(Building.RawMaterialProduction.ContainsKey(building.Type)) {
						float production = Building.RawMaterialProduction[building.Type];
						RawMaterials += foodLevel * production * deltaTime;
					}
				}
			}

			RawMaterials = Math.Min(RawMaterials, MAX_RAW_MATERIALS);
		}

		public void UpdateEverything(float deltaTime) {
			ElapsedTime += deltaTime;
			UpdatePrimatives();
			UpdateExports();
			UpdateImports();
			UpdateFunds(deltaTime);
			UpdateRawMaterials(deltaTime);
			UpdateScore();
		}

		float PopulationNearby(Tile tile) {
			return PrimativeValueNearby(tile, 
					new BuildingType[] { BuildingType.Villa, BuildingType.Cottages, BuildingType.Slums }, 
					PrimativeValues.Housing) * FoodLevel();
		}

		// TODO: This cuts my framerate in half on large maps!!!!
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
			if(Building.BuildingLimits.ContainsKey(type)) {
				if(Buildings[type].Count >= Building.BuildingLimits[type]) {
					return false;
				}
			}

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
			if(Score.Culture <= Level.failureConditions.Culture ||
			   Score.Happiness <= Level.failureConditions.Happiness ||
			   Score.Prosperity <= Level.failureConditions.Prosperity) {
				return true;
			}

			return (ElapsedTime > Level.maxElapsedTime);
		}

		public SavedCity SaveCity() {
			SavedCity saved = new SavedCity();
			saved.rawMaterials = this.RawMaterials;
			saved.funds = this.Funds;
			saved.level = this.Level.name;
			saved.elapsedTime = this.ElapsedTime;
			saved.importAllocation = this.ImportAllocation;
			
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
			city.ElapsedTime = saved.elapsedTime;
			city.ImportAllocation = saved.importAllocation;

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
