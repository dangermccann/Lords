using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Primatives {
		public float Food { get; set; }
		public float Housing { get; set; }
		public float Productivity { get; set; }
		public float Health { get; set; }
		public float Security { get; set; }
		public float Beauty { get; set; }
		public float Faith { get; set; }
		public float Entertainment { get; set; }
		public float Literacy { get; set; }

		public Primatives() {}

		public Primatives(float initial) {
			Food = initial;
			Housing = initial;
			Productivity = initial;
			Health = initial;
			Security = initial;
			Beauty = initial;
			Faith = initial;
			Entertainment = initial;
			Literacy = initial;
		}

		static string[] properties;
		static Primatives() {
			// cache properties by name so we can access them dynamically 
			PropertyInfo[] infos = new Primatives().GetType().GetProperties();
			List<string> propNames = new List<string>();
			
			for(int i = 0; i < infos.Length; i++) {
				
				// exclude the square bracket accessor
				if(infos[i].Name.Length > 0 && infos[i].Name != "Item") {
					propNames.Add(infos[i].Name);
				}
			}
			properties = propNames.ToArray();
		}

		public override string ToString() {
			return String.Format("Food: {0} | Housing: {1} | Productivity: {2} | Health: {3} | " +
				"Security: {4} | Beauty: {5} | Faith: {6} | Entertainment: {7} | Literacy: {8}", 
			    Food, Housing, Productivity, Health, Security, Beauty, Faith, Entertainment, Literacy);
		}

		public static Primatives operator +(Primatives p1, Primatives p2) {
			Primatives result = new Primatives();
			result.Food = p1.Food + p2.Food;
			result.Housing = p1.Housing + p2.Housing;
			result.Productivity = p1.Productivity + p2.Productivity;
			result.Health = p1.Health + p2.Health;
			result.Security = p1.Security + p2.Security;
			result.Beauty = p1.Beauty + p2.Beauty;
			result.Faith = p1.Faith + p2.Faith;
			result.Entertainment = p1.Entertainment + p2.Entertainment;
			result.Literacy = p1.Literacy + p2.Literacy;
			return result;
		}

		public static Primatives operator +(float f, Primatives p1) {
			Primatives result = new Primatives();
			result.Food = p1.Food + f;
			result.Housing = p1.Housing + f;
			result.Productivity = p1.Productivity + f;
			result.Health = p1.Health + f;
			result.Security = p1.Security + f;
			result.Beauty = p1.Beauty + f;
			result.Faith = p1.Faith + f;
			result.Entertainment = p1.Entertainment + f;
			result.Literacy = p1.Literacy + f;
			return result;
		}

		public static Primatives operator *(Primatives p1, float f) {
			Primatives result = new Primatives();
			result.Food = p1.Food * f;
			result.Housing = p1.Housing * f;
			result.Productivity = p1.Productivity * f;
			result.Health = p1.Health * f;
			result.Security = p1.Security * f;
			result.Beauty = p1.Beauty * f;
			result.Faith = p1.Faith * f;
			result.Entertainment = p1.Entertainment * f;
			result.Literacy = p1.Literacy * f;
			return result;
		}

		public static Primatives operator /(Primatives p1, float f) {
			Primatives result = new Primatives();
			result.Food = p1.Food / f;
			result.Housing = p1.Housing / f;
			result.Productivity = p1.Productivity / f;
			result.Health = p1.Health / f;
			result.Security = p1.Security / f;
			result.Beauty = p1.Beauty / f;
			result.Faith = p1.Faith / f;
			result.Entertainment = p1.Entertainment / f;
			result.Literacy = p1.Literacy / f;
			return result;
		}

		public static Primatives operator *(Primatives p1, Primatives p2) {
			Primatives result = new Primatives();
			result.Food = p1.Food * p2.Food;
			result.Housing = p1.Housing * p2.Housing;
			result.Productivity = p1.Productivity * p2.Productivity;
			result.Health = p1.Health * p2.Health;
			result.Security = p1.Security * p2.Security;
			result.Beauty = p1.Beauty * p2.Beauty;
			result.Faith = p1.Faith * p2.Faith;
			result.Entertainment = p1.Entertainment * p2.Entertainment;
			result.Literacy = p1.Literacy * p2.Literacy;
			return result;
		}

		public static string[] Properties() {
			return properties;
		}

		/// <summary>
		/// Retrieves a property of this Primative by name.  
		/// </summary>
		/// <param name="prop">Property.</param>
		public float this[string prop] {
			get { 
				return (float) this.GetType().GetProperty(prop).GetValue(this, null); 
			}
		}

		public override bool Equals(System.Object obj) {
			// If parameter cannot be cast to Primatives return false
			Primatives p = obj as Primatives;
			if ((object)p == null) {
				return false;
			}

			return _Equals(p);
		}

		bool _Equals(Primatives p) {
			return  p.Food == Food && 
					p.Housing == Housing &&
					p.Productivity == Productivity &&
					p.Health == Health &&
					p.Security == Security &&
					p.Beauty == Beauty &&
					p.Faith == Faith &&
					p.Entertainment == Entertainment &&
					p.Literacy == Literacy;
		}

		public override int GetHashCode() {
			int hash = 13;
			hash = (hash * 7) + Food.GetHashCode();
			hash = (hash * 7) + Housing.GetHashCode();
			hash = (hash * 7) + Productivity.GetHashCode();
			hash = (hash * 7) + Health.GetHashCode();
			hash = (hash * 7) + Security.GetHashCode();
			hash = (hash * 7) + Beauty.GetHashCode();
			hash = (hash * 7) + Faith.GetHashCode();
			hash = (hash * 7) + Entertainment.GetHashCode();
			hash = (hash * 7) + Literacy.GetHashCode();
			return hash;
		}
	}

	public class PrimativeValues {
		public const string Food = "Food";
		public const string Housing = "Housing";
		public const string Productivity = "Productivity";
		public const string Health = "Health";
		public const string Security = "Security";
		public const string Beauty = "Beauty";
		public const string Faith = "Faith";
		public const string Entertainment = "Entertainment";
		public const string Literacy = "Literacy";
	}
}