﻿using UnityEngine;
using Lords;

public class StatsPanel : MonoBehaviour {
	
	UILabel population, happinessValue, properityValue, cultureValue; 
	Lords.Slider happiness, prosperity, culture;

	void Start() {
		population = GetUIText("Table/PopulationValue");
		happiness = GetSlider("Table/HappinessSlider");
		prosperity = GetSlider("Table/ProsperitySlider");
		culture = GetSlider("Table/CultureSlider");
		happinessValue = GetUIText("Table/HappinessValue");
		properityValue = GetUIText("Table/ProsperityValue");
		cultureValue = GetUIText("Table/CultureValue");
	}

	UILabel GetUIText(string name) {
		return transform.FindChild(name).gameObject.GetComponent<UILabel>();
	}

	Lords.Slider GetSlider(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Lords.Slider>();
	}
	
	void FixedUpdate() {
		population.text = Mathf.Floor(Game.CurrentCity.Score.Population).ToString();
		happiness.Value = SliderValue(Game.CurrentLevel.failureConditions.Happiness, Game.CurrentLevel.victoryConditions.Happiness, Game.CurrentCity.Score.Happiness);
		prosperity.Value = SliderValue(Game.CurrentLevel.failureConditions.Prosperity, Game.CurrentLevel.victoryConditions.Prosperity, Game.CurrentCity.Score.Prosperity);
		culture.Value = SliderValue(Game.CurrentLevel.failureConditions.Culture, Game.CurrentLevel.victoryConditions.Culture, Game.CurrentCity.Score.Culture);

		happinessValue.text = Strings.FormatScore(Game.CurrentCity.Score.Happiness);
		properityValue.text = Strings.FormatScore(Game.CurrentCity.Score.Prosperity);
		cultureValue.text = Strings.FormatScore(Game.CurrentCity.Score.Culture);
	}

	float SliderValue(float min, float max, float actual) {
		return (actual - min) / (max - min);
	}
}
