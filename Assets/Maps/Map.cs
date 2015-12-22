using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	public class Map {
		int radius;
		float[][] data;
		List<TileType> tileConfiguration;

		public Map(int radius, float[][] data, List<TileType> tileConfiguration) {
			this.radius = radius;
			this.data = data;
			this.tileConfiguration = tileConfiguration;
		}


		public TileType GetTileTypeAt(Hex position) {
			Vector2 offset = Hex.CubeToAddQOffset(Hex.HexToCube(position));
			float value = data[(int)offset.x+radius][(int)offset.y+radius];
			return tileConfiguration[(int)(value * tileConfiguration.Count) % tileConfiguration.Count];
		}

	}
}

