using System;
using System.Collections.Generic;

namespace Lords {
	public class Maps {

		public static MapConfiguration Basic = new MapConfiguration() {
			Radius = 10,
			Seed = 1111,
			PerlinOctive = 5,
			TileConfiguration = new List<TileType>() {
				TileType.Snow,
				TileType.Snow,
				TileType.Dirt,
				TileType.Grass,
				TileType.Sand,
				TileType.Grass,
				TileType.Dirt,
				TileType.Snow,
				TileType.Snow,
			}
		};

		public static MapConfiguration Wet = new MapConfiguration() {
			Radius = 10,
			Seed = 1000,
			PerlinOctive = 5,
			TileConfiguration = new List<TileType>() {
				TileType.Water,
				TileType.Water,
				TileType.Sand,
				TileType.Grass,
				TileType.Dirt,
				TileType.Sand,
				TileType.Water,
				TileType.Water,
				TileType.Water,
			}
		};

		public static MapConfiguration Mountains = new MapConfiguration() {
			Radius = 10,
			Seed = 701,
			PerlinOctive = 3,
			TileConfiguration = new List<TileType>() {
				TileType.Water,
				TileType.Tundra,
				TileType.Mountains,
				TileType.Tundra,
				TileType.Mountains,
				TileType.Forest,
				TileType.Snow,
				TileType.Mountains,
				TileType.Forest,
			}
		};

		public static MapConfiguration Tundra = new MapConfiguration() {
			Radius = 10,
			Seed = 8888,
			PerlinOctive = 5,
			TileConfiguration = new List<TileType>() {
				TileType.Marsh,
				TileType.Water,
				TileType.Grass,
				TileType.Forest,
				TileType.Tundra,
				TileType.Snow,
				TileType.Tundra,
				TileType.Mountains,
				TileType.Mountains,
			}
		};
	}
}
