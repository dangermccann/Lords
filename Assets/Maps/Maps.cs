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

		// standard
		public static MapConfiguration Standard = new MapConfiguration() {
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

		// mountains and forest
		public static MapConfiguration MountainsForest = new MapConfiguration() {
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

		// marsh and water
		public static MapConfiguration MarshWater = new MapConfiguration() {
			Radius = 9,
			Seed = 3333,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Marsh,
				TileType.Water,
				TileType.Marsh,
				TileType.Grass,
				TileType.Water,
				TileType.Dirt,
				TileType.Mountains,
			}
		};

		// mountains
		public static MapConfiguration Mountains = new MapConfiguration() {
			Radius = 9,
			Seed = 11111,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Marsh,
				TileType.Mountains,
				TileType.Forest,
				TileType.Mountains,
				TileType.Grass,
				TileType.Water,
				TileType.Mountains,
			}
		};

		// smaller with sand
		public static MapConfiguration SmallerSand = new MapConfiguration() {
			Radius = 8,
			Seed = 1,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Sand,
				TileType.Grass,
				TileType.Sand,
				TileType.Water,
				TileType.Forest,
				TileType.Tundra,
				TileType.Mountains,
			}
		};

		// tundra
		public static MapConfiguration Tundra = new MapConfiguration() {
			Radius = 10,
			Seed = 123,
			PerlinOctive = 4,
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

		// smaller
		public static MapConfiguration Smaller = new MapConfiguration() {
			Radius = 7,
			Seed = 222,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Mountains,
				TileType.Water,
				TileType.Grass,
				TileType.Forest,
				TileType.Mountains,
				TileType.Forest,
				TileType.Tundra,
				TileType.Mountains,
				TileType.Mountains,
			}
		};

		// smaller lake (10)
		public static MapConfiguration SmallLake = new MapConfiguration() {
			Radius = 7,
			Seed = 33,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Mountains,
				TileType.Water,
				TileType.Water,
				TileType.Sand,
				TileType.Dirt,
				TileType.Grass,
				TileType.Forest,
				TileType.Mountains,
				TileType.Mountains,
			}
		};

		// large easy
		public static MapConfiguration LargeEasy = new MapConfiguration() {
			Radius = 10,
			Seed = 1,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Mountains,
				TileType.Water,
				TileType.Water,
				TileType.Sand,
				TileType.Dirt,
				TileType.Grass,
				TileType.Forest,
				TileType.Mountains,
				TileType.Mountains,
			}
		};

		// tiny with river
		public static MapConfiguration TinyRiver = new MapConfiguration() {
			Radius = 6,
			Seed = 11,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Mountains,
				TileType.Water,
				TileType.Water,
				TileType.Sand,
				TileType.Dirt,
				TileType.Grass,
				TileType.Forest,
				TileType.Mountains,
				TileType.Mountains,
			}
		};

		// tiny and hard
		public static MapConfiguration TinyHard = new MapConfiguration() {
			Radius = 6,
			Seed = 44,
			PerlinOctive = 4,
			TileConfiguration = new List<TileType>() {
				TileType.Sand,
				TileType.Dirt,
				TileType.Marsh,
				TileType.Water,
				TileType.Grass,
				TileType.Sand,
				TileType.Forest,
			}
		};

		// huge and sandy
		public static MapConfiguration HugeSand = new MapConfiguration() {
			Radius = 12,
			Seed = 111,
			PerlinOctive = 5,
			TileConfiguration = new List<TileType>() {
				TileType.Sand,
				TileType.Dirt,
				TileType.Forest,
				TileType.Mountains,
				TileType.Grass,
				TileType.Sand,
				TileType.Water,
				TileType.Water,
				TileType.Sand,
			}
		};

		// medium and cold
		public static MapConfiguration MediumCold = new MapConfiguration() {
			Radius = 9,
			Seed = 2,
			PerlinOctive = 5,
			TileConfiguration = new List<TileType>() {
				TileType.Sand,
				TileType.Dirt,
				TileType.Forest,
				TileType.Mountains,
				TileType.Tundra,
				TileType.Forest,
				TileType.Mountains,
				TileType.Snow,
				TileType.Snow,
			}
		};
	}
}
