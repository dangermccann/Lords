using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lords {
	public class TradeOverlay : Dialog {

		public GameObject importsRowPrefab;
		public float speed = 0.75f;

		GameObject importsTable, exportsTable, noTradingPost;
		UILabel exportsTotal;
		GameObject[] rows;
		Dictionary<Imports, UISlider> importSliders;
		UIPanel panel;
		Coroutine changeCoroutine;

		protected override void Start() {
			base.Start();

			exportsTable = transform.FindChild("Background/Exports/Table").gameObject;
			importsTable = transform.FindChild("Background/Imports/Table").gameObject;
			exportsTotal = transform.FindChild("Background/Exports/Total").gameObject.GetComponent<UILabel>();
			noTradingPost = transform.FindChild("Background/Exports/NoTradingPost").gameObject;
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
				SetColumnText(row, "Yield", Strings.FormatImportYield(import));
				SetColumnText(row, "Cost", Strings.FormatCurrency(Game.CurrentLevel.importPrices[import]));
				GameObject slider = row.transform.FindChild("slider-2").gameObject;
				slider.name = import.ToString() + "_slider-2";

				sliders[i] = slider;
				sliderValues[i] = Game.CurrentCity.ImportAllocation[import];
			}

			importsTable.GetComponent<UITable>().Reposition();
			importsTable.GetComponent<MultiSlider>().SetSliders(sliders);
			importsTable.GetComponent<MultiSlider>().BulkUpdate(sliderValues);

			importSliders = new Dictionary<Imports, UISlider>();
			
			for(int i = 0; i < imports.Length; i++) {
				Imports import = imports[i];
				string sliderPath = import.ToString() + "_row/" + import.ToString() + "_slider-2";
				UISlider slider = importsTable.transform.FindChild(sliderPath).gameObject.GetComponent<UISlider>();
				importSliders.Add(import, slider);
				slider.onDragFinished += OnSliderDragFinished;
			}
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

		void OnSliderDragFinished() {
			if(changeCoroutine != null) {
				StopCoroutine(changeCoroutine);
				changeCoroutine = null;
			}

			Dictionary<Imports, float> from = new Dictionary<Imports, float>();
			Dictionary<Imports, float> to = new Dictionary<Imports, float>();

			foreach(Imports import in importSliders.Keys) {
				UISlider slider = importSliders[import];
				from.Add(import, Game.CurrentCity.ImportAllocation[import]);
				to.Add(import, slider.value);
			}

			changeCoroutine = StartCoroutine(ImportChangeRoutine(from, to));
		}

		IEnumerator ImportChangeRoutine(Dictionary<Imports, float> from, Dictionary<Imports, float> to) {
			bool done = false;

			while(!done) {
				bool changedSomething = false;

				foreach(Imports import in from.Keys) {
					float delta = (to[import] - from[import]) * Time.deltaTime * speed;

					if(delta == 0) {
						continue;
					}

					// make sure we don't overshoot our target
					if((delta > 0 && Game.CurrentCity.ImportAllocation[import] + delta > to[import]) || 
					    delta < 0 && Game.CurrentCity.ImportAllocation[import] + delta < to[import]) {
						delta = to[import] - Game.CurrentCity.ImportAllocation[import];
					}

					if(delta != 0) {
						changedSomething = true;
					}

					Game.CurrentCity.ImportAllocation[import] += delta;
				}

				if(changedSomething) {
					yield return null;
				}
				else {
					done = true;
				}
			}
		}

		public override void FadeIn () {
			base.FadeIn();
			PopulateImports();
			UpdateExports();

			if(Game.CurrentCity.Buildings[BuildingType.Trading_Post].Count == 0) {
				noTradingPost.SetActive(true);
			}
			else {
				noTradingPost.SetActive(false);
			}
		}

		public override void FadeOut (bool dismissShade) {
			base.FadeOut(dismissShade);
		}


	}
}
