using System;
using System.Collections.Generic;

namespace Lords {
	public class MapConfiguration {
		public int Radius { get; set; }
		public int Seed { get; set; }
		public int PerlinOctive { get; set; }
		public List<TileType> TileConfiguration { get; set; }
	}
}
