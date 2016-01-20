using System;

namespace Lords {
	public class SelectionController {
		public static Action<Selection> SelectionChanged;

		static SelectionController() {
			selection = new Selection();
			selection.OperationChanged += (Selection s) => {
				Changed();
			};
			selection.BuildingTypeChanged += (Selection s) => {
				Changed();
			};
			selection.TileChanged += (Selection s) => {
				Changed();
			};

			Reset();
		}


		public static Selection selection;

		public static void Reset() {
			selection.Operation = Operation.Info;
			selection.BuildingType = BuildingType.Villa;
			selection.Tile = null;
		}

		public static void Changed() {
			if(SelectionChanged != null) {
				SelectionChanged(selection);
			}
		}
	}

	public class Selection {
		public Action<Selection> OperationChanged;
		public Action<Selection> BuildingTypeChanged;
		public Action<Selection> TileChanged;

		protected Operation operation;
		public Operation Operation {
			get {
				return operation;
			}
			set {
				operation = value;
				if(OperationChanged != null)
					OperationChanged(this);
			}
		}

		protected BuildingType buildingType;
		public BuildingType BuildingType {
			get {
				return buildingType;
			}
			set {
				buildingType = value;
				if(BuildingTypeChanged != null)
					BuildingTypeChanged(this);
			}
		}

		protected Tile tile;
		public Tile Tile {
			get {
				return tile;
			}
			set {
				tile = value;
				if(TileChanged != null) 
					TileChanged(this);
			}
		}
	}

	public enum Operation {
		Build,
		Destroy,
		Info
	}
}
