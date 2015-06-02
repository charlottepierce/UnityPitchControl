using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityPitchControl.Input {
	[Serializable]
	public class PitchMappings {
		public List<PitchMapping> Mappings = new List<PitchMapping>();
		
		public void ClearMappings() {
			Mappings = new List<PitchMapping>();
		}
		
		public void RemoveMapping(int minVal, int maxVal, string key) {
			for (int i = Mappings.Count - 1; i >= 0; --i) {
				PitchMapping m = Mappings[i];
				if ((m.minVal == minVal) && (m.maxVal == maxVal) && (m.key == key)) {
					Mappings.RemoveAt(i);
					return; // if there are multiple mappings with the same settings, only the first will be removed
				}
			}
		}
		
		public void MapPitch(int minVal, int maxVal, string key) {
			Mappings.Insert(0, new PitchMapping(minVal, maxVal, key));
		}
		
		public bool MapsKey(string key) {
			foreach (PitchMapping m in Mappings) {
				if (m.key == key) return true;
			}
			
			return false;
		}
		
		public List<PitchMapping> GetMappings(string key) {
			List<PitchMapping> mappings = new List<PitchMapping>();
			foreach (PitchMapping m in Mappings) {
				if (m.key == key) mappings.Add(m);
			}
			
			return mappings;
		}
	}
	
	[Serializable]
	public class PitchMapping {
		public int minVal; // exclusive - value must be greater than this to trigger the key
		public int maxVal; // inclusive - value must be less than or equal to this to trigger the key
		public string key; // key activated (e.g., "x")
		
		public PitchMapping(int minVal, int maxVal, string key) {
			this.minVal = minVal;
			this.maxVal = maxVal;
			this.key = key;
		}
	}
}