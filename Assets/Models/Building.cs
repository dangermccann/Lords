using System;
using System.Collections.Generic;

namespace Lords {
	public class Building {
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

		public Tile Tile { get; protected set; }
		public BuildingType Type { get;  protected set; }

		public Building (Tile tile, BuildingType type) {
			this.Tile = tile;
			this.Type = type;
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

