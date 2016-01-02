using UnityEngine;
using Lords;

public class StatsPanel : MonoBehaviour {
	
	UILabel population; 
	Lords.Slider happiness, prosperity, culture;

	void Start() {
		population = GetUIText("Table/PopulationValue");
		happiness = GetSlider("Table/HappinessSlider");
		prosperity = GetSlider("Table/ProsperitySlider");
		culture = GetSlider("Table/CultureSlider");
	}

	UILabel GetUIText(string name) {
		return transform.FindChild(name).gameObject.GetComponent<UILabel>();
	}

	Lords.Slider GetSlider(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Lords.Slider>();
	}
	
	void FixedUpdate() {
		population.text = Mathf.Floor(Game.CurrentCity.Score.Population).ToString();
		happiness.Value = Game.CurrentCity.Score.Happiness;
		prosperity.Value = Game.CurrentCity.Score.Prosperity;
		culture.Value = Game.CurrentCity.Score.Culture;
	}
}
