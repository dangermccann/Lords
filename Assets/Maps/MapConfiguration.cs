using System;
using System.Collections.Generic;

namespace Lords {
	[Serializable]
	public class MapConfiguration {
		public int Radius;
		public int Seed;
		public int PerlinOctive;
		public List<TileType> TileConfiguration;

		public MapConfiguration() {
			TileConfiguration = new List<TileType>();
		}
	}
}
