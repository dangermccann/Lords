using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Building {
		public const float FARM_OUTPUT = 20;
		public const float BUILD_TIME = 10;

		public static Dictionary<BuildingType, TileType> BuildingTileMap = new Dictionary<BuildingType, TileType> {
			{ BuildingType.Villa, 			TileType.Sand },
			{ BuildingType.Slums, 			TileType.Sand },
			{ BuildingType.Cottages,		TileType.Sand },
			{ BuildingType.School, 			TileType.Sand },
			{ BuildingType.Vegetable_Farm, 	TileType.Grass },
			{ BuildingType.Wheat_Farm, 		TileType.Grass },
			{ BuildingType.Tavern, 			TileType.Sand },
			{ BuildingType.Amphitheater,	TileType.Sand },
			{ BuildingType.Trading_Post, 	TileType.Sand },
			{ BuildingType.Fort, 			TileType.Sand },
			{ BuildingType.Hospital, 		TileType.Sand },
			{ BuildingType.Garden,			TileType.Sand },
			{ BuildingType.Church,			TileType.Grass },
			{ BuildingType.Workshop,		TileType.Sand },
		};

		public static Dictionary<BuildingType, Primatives> Yields = new Dictionary<BuildingType, Primatives> {
			{ BuildingType.Villa, 			new Primatives { Housing = 10, Literacy = 1 } },
			{ BuildingType.Slums, 			new Primatives { Housing = 100, Security = -4 } },
			{ BuildingType.Cottages, 		new Primatives { Housing = 25, Security = -1 } },
			{ BuildingType.School, 			new Primatives { Literacy = 3, Entertainment = -1 } },
			{ BuildingType.Vegetable_Farm,	new Primatives { Food = 3, Literacy = -1 } },
			{ BuildingType.Wheat_Farm, 		new Primatives { Food = 5, Literacy = -2 } },
			{ BuildingType.Tavern, 			new Primatives { Entertainment = 2, Security = -1 } },
			{ BuildingType.Amphitheater, 	new Primatives { Entertainment = 4, Productivity = -2 } },
			{ BuildingType.Trading_Post, 	new Primatives { Productivity = 2, Beauty = -1 } },
			{ BuildingType.Fort, 			new Primatives { Security = 4, Beauty = -1 } },
			{ BuildingType.Hospital, 		new Primatives { Health = 3, Faith = -1 } },
			{ BuildingType.Garden, 			new Primatives { Beauty = 3, Productivity = -1 } },
			{ BuildingType.Church, 			new Primatives { Faith = 2, Literacy = -1 } },
			{ BuildingType.Workshop, 		new Primatives { Productivity = 4, Health = -1, Entertainment = -1 } },
		};

		public static Dictionary<BuildingType, Dictionary<TileType, Primatives>> Modifiers = new Dictionary<BuildingType, Dictionary<TileType, Primatives>> {
			{ BuildingType.Villa, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Slums, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Cottages, new Dictionary<TileType, Primatives>() },
			{ BuildingType.School, new Dictionary<TileType, Primatives>() },
			{
				BuildingType.Vegetable_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow, new Primatives{ Food = -2 } },
					{ TileType.Tundra, new Primatives{ Food = -1 } },
					{ TileType.Marsh, new Primatives{ Food = -2 } },
				}
			},
			{
				BuildingType.Wheat_Farm, new Dictionary<TileType, Primatives>() { 
					{ TileType.Snow, new Primatives{ Food = -3 } },
					{ TileType.Tundra, new Primatives{ Food = -2 } },
					{ TileType.Marsh, new Primatives{ Food = -3 } },
				}
			},
			{ BuildingType.Tavern, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Amphitheater, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Trading_Post, new Dictionary<TileType, Primatives>() },
			{ 
				BuildingType.Fort, new Dictionary<TileType, Primatives>() {
					{ TileType.Forest, new Primatives{ Security = 2 } },
				}
			},
			{ BuildingType.Hospital, new Dictionary<TileType, Primatives>() },
			{ 
				BuildingType.Garden, new Dictionary<TileType, Primatives>() {
					{ TileType.Marsh, new Primatives{ Beauty = -1 } },
				}
			},
			{ BuildingType.Church, new Dictionary<TileType, Primatives>() },
			{ BuildingType.Workshop, new Dictionary<TileType, Primatives>() },
		};

		public static Dictionary<BuildingType, float> PopulationMinimums = new Dictionary<BuildingType, float> {
			{ BuildingType.Villa, 			0 },
			{ BuildingType.Slums, 			0 },
			{ BuildingType.Cottages, 		0 },
			{ BuildingType.School, 			500 },
			{ BuildingType.Vegetable_Farm,	0 },
			{ BuildingType.Wheat_Farm, 		0 },
			{ BuildingType.Tavern, 			350 },
			{ BuildingType.Amphitheater, 	450 },
			{ BuildingType.Trading_Post, 	200 },
			{ BuildingType.Fort, 			100 },
			{ BuildingType.Hospital, 		100 },
			{ BuildingType.Garden, 			500 },
			{ BuildingType.Church, 			500 },
			{ BuildingType.Workshop, 		200 },
		};

		public Tile Tile { get; protected set; }
		public BuildingType Type { get;  protected set; }
		public float CreateTime { get; protected set; }

		public Building (Tile tile, BuildingType type) {
			this.Tile = tile;
			this.Type = type;
			CreateTime = Time.fixedTime;
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

