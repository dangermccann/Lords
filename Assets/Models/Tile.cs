using System;

namespace Lords {
	public class Tile {

		public Action<Tile> TypeChanged;
		public Action<Tile> BuildingChanged;

		public Hex Position { get; set; }
		public City City { get; protected set; }


		protected TileType type;
		public TileType Type {
			get {
				return type;
			}
			set {
				type = value;
				if(TypeChanged != null)
					TypeChanged(this);
			}
		}

		protected Building building = null;
		public Building Building {
			get {
				return building;
			}
			set {
				building = value;
				if(BuildingChanged != null)
					BuildingChanged(this);
			}
		}

		public Tile(City city, Hex position) {
			City = city;
			Position = position;
			Type = TileType.Empty;
		}

		public bool CanBuildOn() {
			if(Building != null) return false;
			return (Type != TileType.Water && Type != TileType.Mountains);
		}
	}

	public enum TileType {
		Empty,
		Sand,
		Dirt,
		Snow,
		Grass,
		Tundra,
		Mountains,
		Water,
		Marsh,
		Forest
	}
}
