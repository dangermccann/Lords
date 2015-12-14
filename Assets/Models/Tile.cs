using System;

namespace Lords {
	public class Tile {
		public Hex Position { get; set; }
		public City City { get; protected set; }
		public TileType Type { get; protected set; }

		public Tile(City city, Hex position) {
			City = city;
			Position = position;
			Type = TileType.Empty;
		}
	}

	public enum TileType {
		Empty
	}
}
