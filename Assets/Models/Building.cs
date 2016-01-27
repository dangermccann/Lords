using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Building {
		public const float FARM_OUTPUT = 20;
		public const float BUILD_TIME = 10;
		public const float WORKSHOP_BONUS = 0.1f;

		public static HashSet<BuildingType> Types = new HashSet<BuildingType> () {
			BuildingType.Villa,
			BuildingType.Slums,
			BuildingType.Cottages,
			BuildingType.School,
			BuildingType.Vegetable_Farm,
			BuildingType.Wheat_Farm,
			BuildingType.Tavern,
			BuildingType.Amphitheater,
			BuildingType.Trading_Post,
			BuildingType.Fort,
			BuildingType.Hospital,
			BuildingType.Garden,
			BuildingType.Church,
			BuildingType.Workshop,
		};

		public static Dictionary<BuildingType, string> PositiveYields = new Dictionary<BuildingType, string> {
			{ BuildingType.Villa, 			PrimativeValues.Housing },
			{ BuildingType.Slums, 			PrimativeValues.Housing },
			{ BuildingType.Cottages, 		PrimativeValues.Housing },
			{ BuildingType.School, 			PrimativeValues.Housing },
			{ BuildingType.Vegetable_Farm,	PrimativeValues.Food },
			{ BuildingType.Wheat_Farm, 		PrimativeValues.Food },
			{ BuildingType.Tavern, 			PrimativeValues.Entertainment },
			{ BuildingType.Amphitheater, 	PrimativeValues.Entertainment },
			{ BuildingType.Trading_Post, 	PrimativeValues.Productivity },
			{ BuildingType.Fort, 			PrimativeValues.Security },
			{ BuildingType.Hospital, 		PrimativeValues.Health },
			{ BuildingType.Garden, 			PrimativeValues.Beauty },
			{ BuildingType.Church, 			PrimativeValues.Faith },
			{ BuildingType.Workshop, 		PrimativeValues.Productivity },
		};

		public static Dictionary<BuildingType, Primatives> Yields = new Dictionary<BuildingType, Primatives> {
			{ BuildingType.Villa, 			new Primatives { Housing = 20, Literacy = 2, Beauty = 1 } },
			{ BuildingType.Slums, 			new Primatives { Housing = 100, Security = -4 } },
			{ BuildingType.Cottages, 		new Primatives { Housing = 45, Security = -2 } },
			{ BuildingType.School, 			new Primatives { Literacy = 30, Entertainment = -4 } },
			{ BuildingType.Vegetable_Farm,	new Primatives { Food = 7, Literacy = -3 } },
			{ BuildingType.Wheat_Farm, 		new Primatives { Food = 10, Literacy = -5 } },
			{ BuildingType.Tavern, 			new Primatives { Entertainment = 10, Security = -5 } },
			{ BuildingType.Amphitheater, 	new Primatives { Entertainment = 20, Productivity = -5 } },
			{ BuildingType.Trading_Post, 	new Primatives { Productivity = 20, Beauty = -4 } },
			{ BuildingType.Fort, 			new Primatives { Security = 20, Beauty = -5 } },
			{ BuildingType.Hospital, 		new Primatives { Health = 30, Faith = -10 } },
			{ BuildingType.Garden, 			new Primatives { Beauty = 15, Productivity = -3 } },
			{ BuildingType.Church, 			new Primatives { Faith = 20, Literacy = -5 } },
			{ BuildingType.Workshop, 		new Primatives { Productivity = 20, Health = -5, Entertainment = -5 } },
		};

		public static Dictionary<BuildingType, Dictionary<BuildingType, Primatives>> NearbyModifiers = new Dictionary<BuildingType, Dictionary<BuildingType, Primatives>> {
			{ 
				BuildingType.Villa, new Dictionary<BuildingType, Primatives>() {
					// Villas grouped together add to the city's Beauty score
					{ BuildingType.Villa, 	new Primatives{ Beauty = 0.05f } }
				}
			},
			{ 
				BuildingType.Slums, new Dictionary<BuildingType, Primatives>() {
					// Slums grouped together increase the negative Security effect
					{ BuildingType.Slums, 	new Primatives{ Security = 0.05f } }
				}
			},
			{ BuildingType.Cottages, new Dictionary<BuildingType, Primatives>() },
			{ BuildingType.School, new Dictionary<BuildingType, Primatives>() },
			{ 
				BuildingType.Vegetable_Farm, new Dictionary<BuildingType, Primatives>() {
					// Farms grouped together are more effective - 5% higher Food yield per nearby farm
					{ BuildingType.Vegetable_Farm, 	new Primatives{ Food = 0.05f } }
				}
			},
			{ 
				BuildingType.Wheat_Farm, new Dictionary<BuildingType, Primatives>() {
					// Farms grouped together are more effective - 5% higher Food yield per nearby farm
					{ BuildingType.Wheat_Farm, 	new Primatives{ Food = 0.05f } }
				}
			},
			{ 
				BuildingType.Tavern, new Dictionary<BuildingType, Primatives>() {
					// Nearby forts reduce a tavern's negative Security yield
					{ BuildingType.Fort, 	new Primatives{ Security = -0.20f } },

					// Don't work well when grouped together
					{ BuildingType.Tavern, 	new Primatives{ Entertainment = -0.30f } }
				}
			},
			{ 
				BuildingType.Amphitheater, new Dictionary<BuildingType, Primatives>() {
					// Amphitheaters with nearby tavers yield higher Entertainment value
					{ BuildingType.Tavern, 	new Primatives{ Entertainment = 0.20f } },

					// Don't work well when grouped together
					{ BuildingType.Amphitheater, 	new Primatives{ Entertainment = -0.30f } }
				}
			},
			{ 
				BuildingType.Trading_Post, new Dictionary<BuildingType, Primatives>() {
					// Trading posts work better with a nearby workshop
					{ BuildingType.Workshop, 	new Primatives{ Productivity = 0.20f } },

					// Don't work well when grouped together
					{ BuildingType.Trading_Post, 	new Primatives{ Productivity = -0.30f } }
				}
			},
			{ 
				BuildingType.Fort, new Dictionary<BuildingType, Primatives>() {
					// Don't work well when grouped together
					{ BuildingType.Fort, 	new Primatives{ Security = -0.30f } }
				}
			},
			{ 
				BuildingType.Hospital, new Dictionary<BuildingType, Primatives>() {
					// Don't work well when grouped together
					{ BuildingType.Hospital, 	new Primatives{ Health = -0.30f } }
				}
			},
			{
				BuildingType.Garden, new Dictionary<BuildingType, Primatives>() {
					// Nearby workshops reduce a garden's Beauty yield
					{ BuildingType.Workshop, 	new Primatives{ Beauty = -0.35f } }
				}
			},
			{ 
				BuildingType.Church, new Dictionary<BuildingType, Primatives>() {
					// Churches are more effective with a garden nearby
					{ BuildingType.Garden, 	new Primatives{ Faith = 0.20f } },

					// Don't work well when grouped together
					{ BuildingType.Church, 	new Primatives{ Faith = -0.30f } }
				}
			},
			{ 
				BuildingType.Workshop, new Dictionary<BuildingType, Primatives>() {
					// Workshop's negative Health yield is offset by nearby hospital
					{ BuildingType.Hospital, 	new Primatives{ Health = -0.35f } },

					// Don't work well when grouped together
					{ BuildingType.Workshop, 	new Primatives{ Productivity = -0.30f } }
				}
			},
		};

		public static Dictionary<BuildingType, Dictionary<TileType, Primatives>> Modifiers = new Dictionary<BuildingType, Dictionary<TileType, Primatives>> {
			{ BuildingType.Villa, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Slums, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Cottages, new Dictionary<TileType, Primatives>() },
			{ BuildingType.School, new Dictionary<TileType, Primatives>() },
			{
				BuildingType.Vegetable_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow, new Primatives{ Food = -0.15f } },
					{ TileType.Sand, new Primatives{ Food = -0.15f } },
					{ TileType.Tundra, new Primatives{ Food = -0.10f } },
					{ TileType.Marsh, new Primatives{ Food = -0.15f } },
				}
			},
			{
				BuildingType.Wheat_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow, new Primatives{ Food = -0.15f } },
					{ TileType.Sand, new Primatives{ Food = -0.15f } },
					{ TileType.Tundra, new Primatives{ Food = -0.10f } },
					{ TileType.Marsh, new Primatives{ Food = -0.15f } },
				}
			},
			{ BuildingType.Tavern, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Amphitheater, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Trading_Post, new Dictionary<TileType, Primatives>() },
			{ 
				BuildingType.Fort, new Dictionary<TileType, Primatives>() {
					{ TileType.Forest, new Primatives{ Security = 0.30f } },
					{ TileType.Sand, new Primatives{ Security = -0.10f } },
				}
			},
			{ BuildingType.Hospital, new Dictionary<TileType, Primatives>() },
			{ 
				BuildingType.Garden, new Dictionary<TileType, Primatives>() {
					{ TileType.Marsh, new Primatives{ Beauty = -0.30f } },
				}
			},
			{ BuildingType.Church, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Workshop, new Dictionary<TileType, Primatives>() },
		};

		public static Dictionary<BuildingType, float> PopulationMinimums = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			0 },
			{ BuildingType.Slums, 			0 },
			{ BuildingType.Cottages, 		0 },
			{ BuildingType.School, 			200 },
			{ BuildingType.Vegetable_Farm,	0 },
			{ BuildingType.Wheat_Farm, 		0 },
			{ BuildingType.Tavern, 			350 },
			{ BuildingType.Amphitheater, 	650 },
			{ BuildingType.Trading_Post, 	400 },
			{ BuildingType.Fort, 			450 },
			{ BuildingType.Hospital, 		500 },
			{ BuildingType.Garden, 			400 },
			{ BuildingType.Church, 			300 },
			{ BuildingType.Workshop, 		350 },
		};

		public static Dictionary<BuildingType, float> RequiredFunds = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			0 },
			{ BuildingType.Slums, 			0 },
			{ BuildingType.Cottages, 		0 },
			{ BuildingType.School, 			500 },
			{ BuildingType.Vegetable_Farm,	250 },
			{ BuildingType.Wheat_Farm, 		250 },
			{ BuildingType.Tavern, 			300 },
			{ BuildingType.Amphitheater, 	600 },
			{ BuildingType.Trading_Post, 	200 },
			{ BuildingType.Fort, 			550 },
			{ BuildingType.Hospital, 		500 },
			{ BuildingType.Garden, 			200 },
			{ BuildingType.Church, 			300 },
			{ BuildingType.Workshop, 		350 },
		};

		public static Dictionary<BuildingType, float> RequiredMaterials = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			200 },
			{ BuildingType.Slums, 			350 },
			{ BuildingType.Cottages, 		200 },
			{ BuildingType.School, 			500 },
			{ BuildingType.Vegetable_Farm,	0 },
			{ BuildingType.Wheat_Farm, 		0 },
			{ BuildingType.Tavern, 			600 },
			{ BuildingType.Amphitheater, 	1000 },
			{ BuildingType.Trading_Post, 	450 },
			{ BuildingType.Fort, 			750 },
			{ BuildingType.Hospital, 		1100 },
			{ BuildingType.Garden, 			400 },
			{ BuildingType.Church, 			350 },
			{ BuildingType.Workshop, 		450 },
		};

		public static Dictionary<BuildingType, float> TaxRates = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 		0.3f },
			{ BuildingType.Slums, 		0.03f },
			{ BuildingType.Cottages, 	0.075f },
		};

		public static Dictionary<BuildingType, float> RawMaterialProduction = new Dictionary<BuildingType, float> {
			{ BuildingType.Slums, 		0.2f },
			{ BuildingType.Cottages, 	0.2f },
		};

		public Tile Tile { get; protected set; }
		public BuildingType Type { get;  protected set; }
		public float CreateTime { get; protected set; }

		public Building (Tile tile, BuildingType type) {
			this.Tile = tile;
			this.Type = type;
			CreateTime = Time.fixedTime;

			this.Tile.Building = this;
		}

		public Building ResetCreateTime() {
			CreateTime = -1000;
			return this;
		}


	}

	public enum BuildingType {
		Villa,
		Cottages,
		Slums,
		School,
		Vegetable_Farm,
		Wheat_Farm,
		Tavern,
		Amphitheater,
		Trading_Post,
		Fort,
		Hospital,
		Garden,
		Church,
		Workshop
	}
}

