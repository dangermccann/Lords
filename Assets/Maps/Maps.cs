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
	}
}
