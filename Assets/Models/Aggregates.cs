using System;

namespace Lords {
	public class Aggregates {
		public float Population { get; set; }
		public float Happiness { get; set; }
		public float Prosperity { get; set; }
		public float Culture { get; set; }
		
		public Aggregates() {}
		
		public override string ToString() {
			return String.Format("Population: {0} | Happiness: {1} | Prosperity: {2} | Culture: {3}", 
			                     Population, Happiness, Prosperity, Culture);
		}
	}
}
