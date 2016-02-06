using System;
using System.Collections.Generic;

namespace Lords {
	public class Maps {

		public static MapConfiguration Tutorial = new MapConfiguration() {
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

		public static MapConfiguration PortHenry = new MapConfiguration() {
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

		public static MapConfiguration Galloway = new MapConfiguration() {
			Radius = 10,
			Seed = 701,
			PerlinOctive = 5,
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

		public static MapConfiguration Greencastle = new MapConfiguration() {
			Radius = 10,
			Seed = 670,
			PerlinOctive = 5,
			TileConfiguration = new List<TileType>() {
				TileType.Snow,
				TileType.Tundra,
				TileType.Forest,
				TileType.Mountains,
				TileType.Tundra,
				TileType.Grass,
				TileType.Grass,
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
