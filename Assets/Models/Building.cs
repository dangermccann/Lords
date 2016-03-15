using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Building {
		public const float FARM_OUTPUT = 20;
		public const float BUILD_TIME = 10;
		public const float WORKSHOP_BONUS = 0.1f;

		public static HashSet<BuildingType> PrimativeTypes = new HashSet<BuildingType> () {
			BuildingType.Villa,
			BuildingType.Slums,
			BuildingType.Cottages,
			BuildingType.School,
			BuildingType.Wheat_Farm,
			BuildingType.Tavern,
			BuildingType.Amphitheater,
			BuildingType.Trading_Post,
			BuildingType.Constabulary,
			BuildingType.Clinic,
			BuildingType.Garden,
			BuildingType.Church,
			BuildingType.Workshop,
		};

		public static HashSet<BuildingType> Types = new HashSet<BuildingType> () {
			BuildingType.Villa,
			BuildingType.Cottages,
			BuildingType.Slums,
			BuildingType.School,
			BuildingType.Upper_School,
			BuildingType.University,
			BuildingType.Vegetable_Farm,
			BuildingType.Wheat_Farm,
			BuildingType.Berry_Farm,
			BuildingType.Tavern,
			BuildingType.Hotel,
			BuildingType.Amphitheater,
			BuildingType.Coloseum,
			BuildingType.Trading_Post,
			BuildingType.Constabulary,
			BuildingType.Military_Fort,
			BuildingType.Clinic,
			BuildingType.Dispensary,
			BuildingType.Hospital,
			BuildingType.Garden,
			BuildingType.Plaza,
			BuildingType.Statue,
			BuildingType.Church,
			BuildingType.Cathedral,
			BuildingType.Monestary,
			BuildingType.Workshop,
			BuildingType.Blacksmith,
			BuildingType.Factory
		};

		// Used as a hint in the UI only -- not important functionally
		public static Dictionary<BuildingType, string> PositiveYields = new Dictionary<BuildingType, string> {
			{ BuildingType.Villa, 			PrimativeValues.Housing },
			{ BuildingType.Slums, 			PrimativeValues.Housing },
			{ BuildingType.Cottages, 		PrimativeValues.Housing },
			{ BuildingType.School, 			PrimativeValues.Housing },
			{ BuildingType.Upper_School,	PrimativeValues.Housing },
			{ BuildingType.University,		PrimativeValues.Housing },
			{ BuildingType.Vegetable_Farm,	PrimativeValues.Food },
			{ BuildingType.Wheat_Farm, 		PrimativeValues.Food },
			{ BuildingType.Berry_Farm, 		PrimativeValues.Food },
			{ BuildingType.Tavern, 			PrimativeValues.Entertainment },
			{ BuildingType.Hotel, 			PrimativeValues.Entertainment },
			{ BuildingType.Amphitheater, 	PrimativeValues.Entertainment },
			{ BuildingType.Coloseum, 		PrimativeValues.Entertainment },
			{ BuildingType.Trading_Post, 	PrimativeValues.Productivity },
			{ BuildingType.Constabulary, 	PrimativeValues.Security },
			{ BuildingType.Military_Fort, 	PrimativeValues.Security },
			{ BuildingType.Clinic,	 		PrimativeValues.Health },
			{ BuildingType.Dispensary, 		PrimativeValues.Health },
			{ BuildingType.Hospital, 		PrimativeValues.Health },
			{ BuildingType.Garden, 			PrimativeValues.Beauty },
			{ BuildingType.Plaza, 			PrimativeValues.Beauty },
			{ BuildingType.Statue, 			PrimativeValues.Beauty },
			{ BuildingType.Church, 			PrimativeValues.Faith },
			{ BuildingType.Monestary,		PrimativeValues.Faith },
			{ BuildingType.Cathedral,		PrimativeValues.Faith },
			{ BuildingType.Workshop, 		PrimativeValues.Productivity },
			{ BuildingType.Blacksmith, 		PrimativeValues.Productivity },
			{ BuildingType.Factory, 		PrimativeValues.Productivity },
		};

		public static Dictionary<BuildingType, Primatives> Yields = new Dictionary<BuildingType, Primatives> {
			{ BuildingType.Villa, 			new Primatives { Housing = 20,			Literacy = 0.1f, 		Beauty = 0.1f	} },
			{ BuildingType.Slums, 			new Primatives { Housing = 100,			Security = -4 							} },
			{ BuildingType.Cottages, 		new Primatives { Housing = 45,			Security = -1 							} },
			{ BuildingType.School, 			new Primatives { Literacy = 10,			Entertainment = -6 						} },
			{ BuildingType.Upper_School,	new Primatives { Literacy = 20,			Entertainment = -8	 					} },
			{ BuildingType.University,		new Primatives { Literacy = 30,			Entertainment = -10 					} },
			{ BuildingType.Wheat_Farm, 		new Primatives { Food = 10,				Literacy = -5 							} },
			{ BuildingType.Vegetable_Farm,	new Primatives { Food = 10,				Literacy = -3 							} },
			{ BuildingType.Berry_Farm, 		new Primatives { Food = 10,				Literacy = -2 							} },
			{ BuildingType.Tavern, 			new Primatives { Entertainment = 10,	Security = -6							} },
			{ BuildingType.Hotel, 			new Primatives { Entertainment = 15,	Security = -7 							} },
			{ BuildingType.Amphitheater, 	new Primatives { Entertainment = 20,	Productivity = -12 						} },
			{ BuildingType.Coloseum, 		new Primatives { Entertainment = 30,	Productivity = -14 						} },
			{ BuildingType.Trading_Post, 	new Primatives { Productivity = 20,		Beauty = -12 							} },
			{ BuildingType.Constabulary,	new Primatives { Security = 15,			Beauty = -9 							} },
			{ BuildingType.Military_Fort,	new Primatives { Security = 25,			Beauty = -11 							} },
			{ BuildingType.Clinic, 			new Primatives { Health = 20,			Faith = -12 							} },
			{ BuildingType.Dispensary, 		new Primatives { Health = 20,			Faith = -10 							} },
			{ BuildingType.Hospital, 		new Primatives { Health = 30,			Faith = -14 							} },
			{ BuildingType.Garden, 			new Primatives { Beauty = 15,			Productivity = -9 						} },
			{ BuildingType.Plaza, 			new Primatives { Beauty = 20,			Entertainment = -10 					} },
			{ BuildingType.Statue, 			new Primatives { Beauty = 20,			Faith = -10								} },
			{ BuildingType.Church, 			new Primatives { Faith = 10,			Literacy = -6 							} },
			{ BuildingType.Monestary,		new Primatives { Faith = 20,			Literacy = -8 							} },
			{ BuildingType.Cathedral,		new Primatives { Faith = 30,			Literacy = -10 							} },
			{ BuildingType.Workshop, 		new Primatives { Productivity = 20,		Health = -6, 		Entertainment = -6	} },
			{ BuildingType.Blacksmith, 		new Primatives { Productivity = 20,		Health = -4, 		Entertainment = -4 	} },
			{ BuildingType.Factory, 		new Primatives { Productivity = 30,		Health = -5, 		Entertainment = -5	} },
		};

		public static Dictionary<BuildingType, Dictionary<BuildingType, Primatives>> NearbyModifiers = new Dictionary<BuildingType, Dictionary<BuildingType, Primatives>> {
			// -- Housing --
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

			// -- Schools --
			{ 
				BuildingType.School, new Dictionary<BuildingType, Primatives>() {
					{ BuildingType.School, 	new Primatives{ Literacy = -0.30f } }
				}
			},
			{ 
				BuildingType.Upper_School, new Dictionary<BuildingType, Primatives>() {
					{ BuildingType.Upper_School, 	new Primatives{ Literacy = -0.30f } }
				}
			},
			{ 
				BuildingType.University, new Dictionary<BuildingType, Primatives>() {
					{ BuildingType.University, 	new Primatives{ Literacy = -0.30f } }
				}
			},

			// -- Farms --
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
				BuildingType.Berry_Farm, new Dictionary<BuildingType, Primatives>() {
					// Farms grouped together are more effective - 5% higher Food yield per nearby farm
					{ BuildingType.Berry_Farm, 	new Primatives{ Food = 0.05f } }
				}
			},

			// -- Tavern / Hotel --
			{ 
				BuildingType.Tavern, new Dictionary<BuildingType, Primatives>() {
					// Nearby police or forts reduce a tavern's negative Security yield
					{ BuildingType.Constabulary, 	new Primatives{ Security = -0.20f } },
					{ BuildingType.Military_Fort, 	new Primatives{ Security = -0.20f } },

					// Don't work well when grouped together
					{ BuildingType.Tavern, 	new Primatives{ Entertainment = -0.30f } }
				}
			},
			{ 
				BuildingType.Hotel, new Dictionary<BuildingType, Primatives>() {
					// Nearby police or forts reduce a tavern's negative Security yield
					{ BuildingType.Constabulary, 	new Primatives{ Security = -0.20f } },
					{ BuildingType.Military_Fort, 	new Primatives{ Security = -0.20f } },
					
					// Don't work well when grouped together
					{ BuildingType.Hotel, 	new Primatives{ Entertainment = -0.30f } }
				}
			},

			// -- Entertainment Buildings -- 
			{ 
				BuildingType.Amphitheater, new Dictionary<BuildingType, Primatives>() {
					// Amphitheaters with nearby tavers yield higher Entertainment value
					{ BuildingType.Tavern, 	new Primatives{ Entertainment = 0.20f } },
					{ BuildingType.Hotel, 	new Primatives{ Entertainment = 0.20f } },

					// Don't work well when grouped together
					{ BuildingType.Amphitheater, 	new Primatives{ Entertainment = -0.30f } }
				}
			},
			{ 
				BuildingType.Coloseum, new Dictionary<BuildingType, Primatives>() {
					// Amphitheaters with nearby tavers yield higher Entertainment value
					{ BuildingType.Tavern, 	new Primatives{ Entertainment = 0.25f } },
					{ BuildingType.Hotel, 	new Primatives{ Entertainment = 0.25f } },
					
					// Don't work well when grouped together
					{ BuildingType.Coloseum, 	new Primatives{ Entertainment = -0.30f } }
				}
			},

			// -- Trading Post --
			{ 
				BuildingType.Trading_Post, new Dictionary<BuildingType, Primatives>() {
					// Trading posts work better with a nearby productivity buildings
					{ BuildingType.Workshop, 	new Primatives{ Productivity = 0.10f } },
					{ BuildingType.Blacksmith, 	new Primatives{ Productivity = 0.10f } },
					{ BuildingType.Factory, 	new Primatives{ Productivity = 0.10f } },

					// Don't work well when grouped together
					{ BuildingType.Trading_Post, 	new Primatives{ Productivity = -0.30f } }
				}
			},

			// -- Security Buildings -- 
			{ 
				BuildingType.Constabulary, new Dictionary<BuildingType, Primatives>() {
					// Don't work well when grouped together
					{ BuildingType.Constabulary, 	new Primatives{ Security = -0.30f } }
				}
			},
			{ 
				BuildingType.Military_Fort, new Dictionary<BuildingType, Primatives>() {
					// Don't work well when grouped together
					{ BuildingType.Military_Fort, 	new Primatives{ Security = -0.30f } }
				}
			},

			// -- Health Buildings -- 
			{ 
				BuildingType.Clinic, new Dictionary<BuildingType, Primatives>() {
					// Taverns make them less effective
					{ BuildingType.Tavern, 	new Primatives{ Health = -0.10f } },

					// Don't work well when grouped together
					{ BuildingType.Clinic, 	new Primatives{ Health = -0.30f } }
				}
			},
			{ 
				BuildingType.Dispensary, new Dictionary<BuildingType, Primatives>() {
					// Don't work well when grouped together
					{ BuildingType.Dispensary, 	new Primatives{ Health = -0.30f } }
				}
			},
			{ 
				BuildingType.Hospital, new Dictionary<BuildingType, Primatives>() {
					// Don't work well when grouped together
					{ BuildingType.Hospital, 	new Primatives{ Health = -0.30f } }
				}
			},

			// -- Beauty Buildings -- 
			{
				BuildingType.Garden, new Dictionary<BuildingType, Primatives>() {
					// Nearby productivity buildings reduce a garden's Beauty yield
					{ BuildingType.Workshop, 	new Primatives{ Beauty = -0.35f } },
					{ BuildingType.Blacksmith, 	new Primatives{ Beauty = -0.35f } },
					{ BuildingType.Factory, 	new Primatives{ Beauty = -0.35f } }
				}
			},
			{
				BuildingType.Plaza, new Dictionary<BuildingType, Primatives>() {
					// Nearby productivity buildings reduce a garden's Beauty yield
					{ BuildingType.Workshop, 	new Primatives{ Beauty = -0.35f } },
					{ BuildingType.Blacksmith, 	new Primatives{ Beauty = -0.35f } },
					{ BuildingType.Factory, 	new Primatives{ Beauty = -0.35f } },

					// Hotel will offset the entertainment penalty
					{ BuildingType.Hotel, 	new Primatives{ Entertainment = -0.35f } },
				}
			},
			{
				BuildingType.Statue, new Dictionary<BuildingType, Primatives>() {
					// Nearby productivity buildings reduce a garden's Beauty yield
					{ BuildingType.Workshop, 	new Primatives{ Beauty = -0.35f } },
					{ BuildingType.Blacksmith, 	new Primatives{ Beauty = -0.35f } },
					{ BuildingType.Factory, 	new Primatives{ Beauty = -0.35f } },

					// Cathedral or monstary will offset the faith penalty
					{ BuildingType.Cathedral, 	new Primatives{ Faith = -0.35f } },
					{ BuildingType.Monestary, 	new Primatives{ Faith = -0.35f } },
				}
			},

			// -- Faith Buildings -- 
			{ 
				BuildingType.Church, new Dictionary<BuildingType, Primatives>() {
					// Churches are more effective with a garden nearby
					{ BuildingType.Garden, 	new Primatives{ Faith = 0.20f } },

					// Don't work well when grouped together or near cathedrals
					{ BuildingType.Church, 	new Primatives{ Faith = -0.30f } },
					{ BuildingType.Cathedral, 	new Primatives{ Faith = -0.30f } }
				}
			},
			{ 
				BuildingType.Monestary, new Dictionary<BuildingType, Primatives>() {
					// Churches are more effective with a church nearby
					{ BuildingType.Church, 	new Primatives{ Faith = 0.20f } },
					
					// Don't work well when grouped together
					{ BuildingType.Monestary, 	new Primatives{ Faith = -0.30f } }
				}
			},
			{ 
				BuildingType.Cathedral, new Dictionary<BuildingType, Primatives>() {
					// Churches are more effective with a statue nearby
					{ BuildingType.Statue, 	new Primatives{ Faith = 0.20f } },
					
					// Don't work well when grouped together or near cathedrals
					{ BuildingType.Church, 	new Primatives{ Faith = -0.30f } },
					{ BuildingType.Cathedral, 	new Primatives{ Faith = -0.30f } }
				}
			},

			// -- Productivity Buildings -- 
			{ 
				BuildingType.Workshop, new Dictionary<BuildingType, Primatives>() {
					// Workshop's negative Health yield is offset by nearby hospital
					{ BuildingType.Hospital, 	new Primatives{ Health = -0.25f } },

					// Don't work well when grouped together
					{ BuildingType.Workshop, 	new Primatives{ Productivity = -0.30f } }
				}
			},
			{ 
				BuildingType.Blacksmith, new Dictionary<BuildingType, Primatives>() {
					// Workshop's negative Health yield is offset by nearby hospital
					{ BuildingType.Hospital, 	new Primatives{ Health = -0.35f } },
					
					// Don't work well when grouped together
					{ BuildingType.Blacksmith, 	new Primatives{ Productivity = -0.30f } }
				}
			},
			{ 
				BuildingType.Factory, new Dictionary<BuildingType, Primatives>() {
					// Workshop's negative Health yield is offset by nearby hospital
					{ BuildingType.Hospital, 	new Primatives{ Health = -0.35f } },
					
					// Don't work well when grouped together
					{ BuildingType.Factory, 	new Primatives{ Productivity = -0.30f } }
				}
			},
		};

		public static Dictionary<BuildingType, Dictionary<TileType, Primatives>> Modifiers = new Dictionary<BuildingType, Dictionary<TileType, Primatives>> {
			{ BuildingType.Villa,		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Slums,		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Cottages,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.School,		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Upper_School,new Dictionary<TileType, Primatives>() },
			{ BuildingType.University, 	new Dictionary<TileType, Primatives>() },
			{
				BuildingType.Vegetable_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow,	new Primatives { Food = -0.15f } },
					{ TileType.Sand,	new Primatives { Food = -0.15f } },
					{ TileType.Tundra,	new Primatives { Food = -0.10f } },
					{ TileType.Marsh,	new Primatives { Food = -0.15f } },
					{ TileType.Grass,	new Primatives { Food =  0.05f } },
				}
			},
			{
				BuildingType.Wheat_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow,	new Primatives { Food = -0.15f } },
					{ TileType.Sand,	new Primatives { Food = -0.15f } },
					{ TileType.Tundra,	new Primatives { Food = -0.10f } },
					{ TileType.Marsh,	new Primatives { Food = -0.15f } },
					{ TileType.Grass,	new Primatives { Food =  0.05f } },
				}
			},
			{
				BuildingType.Berry_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow,	new Primatives { Food = -0.15f } },
					{ TileType.Sand,	new Primatives { Food = -0.15f } },
					{ TileType.Tundra,	new Primatives { Food = -0.10f } },
					{ TileType.Marsh,	new Primatives { Food = -0.15f } },
					{ TileType.Grass,	new Primatives { Food =  0.05f } },
				}
			},
			{ BuildingType.Tavern, 		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Hotel,		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Amphitheater,new Dictionary<TileType, Primatives>() },
			{ BuildingType.Coloseum,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.Trading_Post,new Dictionary<TileType, Primatives>() },
			{ BuildingType.Constabulary,new Dictionary<TileType, Primatives>() },
			{ 
				BuildingType.Military_Fort, new Dictionary<TileType, Primatives>() {
					{ TileType.Forest,	new Primatives { Security = 0.30f } },
					{ TileType.Sand,	new Primatives { Security = -0.10f } },
				}
			},
			{ BuildingType.Clinic,		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Dispensary,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.Hospital,	new Dictionary<TileType, Primatives>() },
			{ 
				BuildingType.Garden, new Dictionary<TileType, Primatives>() {
					{ TileType.Marsh, new Primatives { Beauty = -0.30f } },
				}
			},
			{ 
				BuildingType.Plaza, new Dictionary<TileType, Primatives>() {
					{ TileType.Marsh, new Primatives { Beauty = -0.30f } },
				}
			},
			{ 
				BuildingType.Statue, new Dictionary<TileType, Primatives>() {
					{ TileType.Marsh, new Primatives { Beauty = -0.30f } },
				}
			},
			{ BuildingType.Church,		new Dictionary<TileType, Primatives>() },
			{ BuildingType.Monestary,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.Cathedral,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.Workshop,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.Blacksmith,	new Dictionary<TileType, Primatives>() },
			{ BuildingType.Factory,		new Dictionary<TileType, Primatives>() },
		};

		public static Dictionary<BuildingType, float> PopulationMinimums = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			0 },
			{ BuildingType.Slums, 			0 },
			{ BuildingType.Cottages, 		0 },
			{ BuildingType.School, 			200 },
			{ BuildingType.Upper_School,	800 },
			{ BuildingType.University, 		1200 },
			{ BuildingType.Vegetable_Farm,	200 },
			{ BuildingType.Wheat_Farm, 		0 },
			{ BuildingType.Berry_Farm, 		1000 },
			{ BuildingType.Tavern, 			350 },
			{ BuildingType.Hotel, 			1500 },
			{ BuildingType.Amphitheater, 	650 },
			{ BuildingType.Coloseum,	 	2000 },
			{ BuildingType.Trading_Post, 	400 },
			{ BuildingType.Constabulary, 	450 },
			{ BuildingType.Military_Fort, 	850 },
			{ BuildingType.Clinic,	 		500 },
			{ BuildingType.Dispensary, 		1500 },
			{ BuildingType.Hospital, 		2500 },
			{ BuildingType.Garden, 			400 },
			{ BuildingType.Plaza, 			800 },
			{ BuildingType.Statue, 			800 },
			{ BuildingType.Church, 			300 },
			{ BuildingType.Monestary, 		1300 },
			{ BuildingType.Cathedral, 		2000 },
			{ BuildingType.Workshop, 		350 },
			{ BuildingType.Blacksmith, 		1350 },
			{ BuildingType.Factory, 		2050 },
		};

		public static Dictionary<BuildingType, float> RequiredFunds = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			0 },
			{ BuildingType.Slums, 			0 },
			{ BuildingType.Cottages, 		0 },
			{ BuildingType.School, 			500 },
			{ BuildingType.Upper_School,	1000 },
			{ BuildingType.University,		2000 },
			{ BuildingType.Vegetable_Farm,	250 },
			{ BuildingType.Wheat_Farm, 		250 },
			{ BuildingType.Berry_Farm, 		250 },
			{ BuildingType.Tavern, 			300 },
			{ BuildingType.Hotel, 			1000 },
			{ BuildingType.Amphitheater, 	600 },
			{ BuildingType.Coloseum, 		2000 },
			{ BuildingType.Trading_Post, 	200 },
			{ BuildingType.Constabulary, 	550 },
			{ BuildingType.Military_Fort, 	550 },
			{ BuildingType.Clinic,	 		500 },
			{ BuildingType.Dispensary, 		1500 },
			{ BuildingType.Hospital, 		2500 },
			{ BuildingType.Garden, 			200 },
			{ BuildingType.Plaza, 			800 },
			{ BuildingType.Statue, 			800 },
			{ BuildingType.Church, 			300 },
			{ BuildingType.Monestary, 		1500 },
			{ BuildingType.Cathedral, 		2500 },
			{ BuildingType.Workshop, 		350 },
			{ BuildingType.Blacksmith, 		900 },
			{ BuildingType.Factory, 		1200 },
		};

		public static Dictionary<BuildingType, float> RequiredMaterials = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			200 },
			{ BuildingType.Slums, 			350 },
			{ BuildingType.Cottages, 		200 },
			{ BuildingType.School, 			500 },
			{ BuildingType.Upper_School,	1500 },
			{ BuildingType.University, 		2000 },
			{ BuildingType.Vegetable_Farm,	0 },
			{ BuildingType.Wheat_Farm, 		0 },
			{ BuildingType.Berry_Farm, 		100 },
			{ BuildingType.Tavern, 			600 },
			{ BuildingType.Hotel, 			1000 },
			{ BuildingType.Amphitheater, 	1000 },
			{ BuildingType.Coloseum, 		1400 },
			{ BuildingType.Trading_Post, 	450 },
			{ BuildingType.Constabulary, 	750 },
			{ BuildingType.Military_Fort, 	1050 },
			{ BuildingType.Clinic, 			700 },
			{ BuildingType.Dispensary, 		800 },
			{ BuildingType.Hospital, 		1100 },
			{ BuildingType.Garden, 			400 },
			{ BuildingType.Plaza, 			400 },
			{ BuildingType.Statue, 			400 },
			{ BuildingType.Church, 			350 },
			{ BuildingType.Monestary, 		900 },
			{ BuildingType.Cathedral, 		1200 },
			{ BuildingType.Workshop, 		450 },
			{ BuildingType.Blacksmith, 		600 },
			{ BuildingType.Factory, 		800 },
		};

		public static Dictionary<BuildingType, float> TaxRates = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 		0.3f },
			{ BuildingType.Slums, 		0.03f },
			{ BuildingType.Cottages, 	0.075f },
		};

		public static Dictionary<BuildingType, float> RawMaterialProduction = new Dictionary<BuildingType, float> {
			{ BuildingType.Slums, 		0.2f },
			{ BuildingType.Villa, 		0.1f },
			{ BuildingType.Cottages, 	0.2f },
		};

		public static Dictionary<BuildingType, List<BuildingType>> Upgrades = new Dictionary<BuildingType, List<BuildingType>>() {
			{ BuildingType.School, 		new List<BuildingType>() { BuildingType.Upper_School,	BuildingType.University } },
			{ BuildingType.Wheat_Farm, 	new List<BuildingType>() { BuildingType.Vegetable_Farm,	BuildingType.Berry_Farm } },
			{ BuildingType.Tavern, 		new List<BuildingType>() { BuildingType.Hotel 									} },
			{ BuildingType.Amphitheater,new List<BuildingType>() { BuildingType.Coloseum 								} },
			{ BuildingType.Constabulary,new List<BuildingType>() { BuildingType.Military_Fort 							} },
			{ BuildingType.Clinic,		new List<BuildingType>() { BuildingType.Dispensary,		BuildingType.Hospital 	} },
			{ BuildingType.Garden,		new List<BuildingType>() { BuildingType.Plaza,			BuildingType.Statue 	} },
			{ BuildingType.Church,		new List<BuildingType>() { BuildingType.Monestary,		BuildingType.Cathedral 	} },
			{ BuildingType.Workshop,	new List<BuildingType>() { BuildingType.Blacksmith,		BuildingType.Factory	} },
		};

		public static BuildingType FindUpgradeBaseType(BuildingType type) {
			if(Upgrades.ContainsKey(type)) {
				return type;
			}
			else {
				foreach(BuildingType t in Upgrades.Keys) {
					if(Upgrades[t].Contains(type)) {
						return t;
					}
				}
			}

			Debug.LogWarning("Unable to find base type for " + type.ToString());
			return BuildingType.Villa;
		}

		public static bool CanUpgrade(BuildingType type) {
			if(type == BuildingType.Villa || type == BuildingType.Slums || type == BuildingType.Cottages ||
			   type == BuildingType.Trading_Post) {
				return false;
			}
			else {
				return true;
			}
		}

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

		public void ChangeType(BuildingType newtype) {
			Type = newtype;

			if(Tile != null) {
				Tile.Building = this;	// trigger a changed event
			}
		}


	}

	public enum BuildingType {
		Villa,
		Cottages,
		Slums,
		School,
		Upper_School,
		University,
		Vegetable_Farm,
		Wheat_Farm,
		Berry_Farm,
		Tavern,
		Hotel,
		Amphitheater,
		Coloseum,
		Trading_Post,
		Constabulary,
		Military_Fort,
		Clinic,
		Dispensary,
		Hospital,
		Garden,
		Plaza,
		Statue,
		Church,
		Cathedral,
		Monestary,
		Workshop,
		Blacksmith,
		Factory
	}
}

