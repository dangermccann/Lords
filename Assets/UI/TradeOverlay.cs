using UnityEngine;
using System;

namespace Lords {
	public class TradeOverlay : Dialog {

		public GameObject importsRowPrefab;

		GameObject importsTable, exportsTable;
		UILabel exportsTotal;
		GameObject[] rows;
		UIPanel panel;

		protected override void Start() {
			base.Start();

			exportsTable = transform.FindChild("Background/Exports/Table").gameObject;
			importsTable = transform.FindChild("Background/Imports/Table").gameObject;
			exportsTotal = transform.FindChild("Background/Exports/Total").gameObject.GetComponent<UILabel>();
			panel = GetComponent<UIPanel>();
		}

		void PopulateImports() {
			Imports[] imports;
			GameObject[] sliders;
			float[] sliderValues;

			if(rows != null && rows.Length > 0) {
				// It's already populated, let's make sure we sync the sliders with the allocation values
				imports = (Imports[]) Enum.GetValues(typeof(Imports));
				sliderValues = new float[imports.Length];

				for(int i = 0; i < imports.Length; i++) {
					sliderValues[i] = Game.CurrentCity.ImportAllocation[imports[i]];
				}

				importsTable.GetComponent<MultiSlider>().BulkUpdate(sliderValues);
				return; 
			}

			imports = (Imports[]) Enum.GetValues(typeof(Imports));
			sliders = new GameObject[imports.Length];
			sliderValues = new float[imports.Length];
			rows = new GameObject[imports.Length];

			for(int i = 0; i < imports.Length; i++) {
				Imports import = imports[i];

				GameObject row = (GameObject) GameObject.Instantiate(importsRowPrefab);
				row.transform.SetParent(importsTable.transform, false);
				row.name = import.ToString() + "_row";
				rows[i] = row;

				SetColumnText(row, "Name", import.ToString());
				SetColumnText(row, "Yield", Strings.FormatImportYield(import, 1));
				SetColumnText(row, "Cost", Strings.FormatCurrency(Game.CurrentLevel.importPrices[import]));
				GameObject slider = row.transform.FindChild("slider-2").gameObject;
				slider.name = import.ToString() + "_slider-2";

				sliders[i] = slider;
				sliderValues[i] = Game.CurrentCity.ImportAllocation[import];
			}

			importsTable.GetComponent<UITable>().Reposition();
			importsTable.GetComponent<MultiSlider>().SetSliders(sliders);
			importsTable.GetComponent<MultiSlider>().BulkUpdate(sliderValues);
		}

		void UpdateExports() {
			foreach(Exports export in Enum.GetValues(typeof(Exports))) {
				SetColumnText(exportsTable, export + "_Qty", Mathf.Floor(Game.CurrentCity.Exports[export]).ToString());
				SetColumnText(exportsTable, export + "_Price", Strings.FormatCurrency(Game.CurrentLevel.exportPrices[export]));
			}

			exportsTotal.text = "Total: " + Strings.FormatCurrency(Game.CurrentCity.ExportTotal);

		}

		void SetColumnText(GameObject row, string columnName, string text) {
			row.transform.FindChild(columnName).GetComponent<UILabel>().text = text;
		}

		void FixedUpdate() {
			if(panel.alpha == 1) {
				UpdateExports();
			}
		}

		public override void FadeIn () {
			base.FadeIn();
			PopulateImports();
			UpdateExports();
		}

		public override void FadeOut (bool dismissShade) {
			base.FadeOut(dismissShade);

			Imports[] imports = (Imports[]) Enum.GetValues(typeof(Imports));

			for(int i = 0; i < imports.Length; i++) {
				Imports import = imports[i];
				string sliderPath = import.ToString() + "_row/" + import.ToString() + "_slider-2";
				UISlider slider = importsTable.transform.FindChild(sliderPath).gameObject.GetComponent<UISlider>();
				Game.CurrentCity.ImportAllocation[import] = slider.value;
			}
		}


	}
}
