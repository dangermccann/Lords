using System;
using System.Collections.Generic;

namespace Lords {
	public class City {
		public Dictionary<Hex, Tile> Tiles { get; protected set; }
		public int Radius { get; protected set; }

		public City(int radius) {
			this.Radius = radius;
			Tiles = new Dictionary<Hex, Tile>();
			CreateTiles();
		}

		void CreateTiles() {
			for(int x = -1 * Radius; x <= Radius; x++) {
				for(int y = -1 * Radius; y <= Radius; y++) {
					if(Math.Abs(x + y) <= Radius) {
						Hex hex = new Hex(x, y);
						Tiles.Add(hex, new Tile(this, hex));
					}
				}
			}
		}
	}
}
