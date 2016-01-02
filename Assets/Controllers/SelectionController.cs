using System;

namespace Lords {
	public class SelectionController {
		public static Action<Selection> SelectionChanged;

		static SelectionController() {
			selection = new Selection();
			selection.OperationChanged += (Selection s) => {
				if(SelectionChanged != null) {
					SelectionChanged(selection);
				}
			};
			selection.BuildingTypeChanged += (Selection s) => {
				if(SelectionChanged != null) {
					SelectionChanged(selection);
				}
			};
			selection.Operation = Operation.Build;
			selection.BuildingType = BuildingType.Villa;
		}


		public static Selection selection;

	}

	public class Selection {
		public Action<Selection> OperationChanged;
		public Action<Selection> BuildingTypeChanged;

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
	}

	public enum Operation {
		Build,
		Destroy
	}
}
