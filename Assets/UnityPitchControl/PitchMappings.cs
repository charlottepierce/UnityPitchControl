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
		
		public void RemoveMapping(int control, int minVal, int maxVal, string key) {
//			for (int i = Mappings.Count - 1; i >= 0; --i) {
//				PitchMapping m = Mappings[i];
//				if ((m.control == control) && (m.minVal == minVal) && (m.maxVal == maxVal) && (m.key == key)) {
//					Mappings.RemoveAt(i);
//					return; // if there are multiple mappings with the same settings, only the first will be removed
//				}
//			}
		}
		
		public void MapControl(int control, int minVal, int maxVal, string key) {
//			Mappings.Insert(0, new PitchMapping(control, minVal, maxVal, key));
		}
		
		public bool MapsKey(string key) {
//			foreach (PitchMapping m in Mappings) {
//				if (m.key == key) return true;
//			}
			
			return false;
		}
		
		public List<PitchMapping> GetMappings(string key) {
//			List<PitchMapping> mappings = new List<PitchMapping>();
//			foreach (PitchMapping m in Mappings) {
//				if (m.key == key) mappings.Add(m);
//			}
			
			return new List<PitchMapping>;
		}
	}
	
	[Serializable]
	public class PitchMapping {
		public int control;
		public int minVal; // exclusive - value must be greater than this to trigger the key
		public int maxVal; // inclusive - value must be less than or equal to this to trigger the key
		public string key; // key activated (e.g., "x")
		
		// used to determine the exact key event that should be triggered, and then update this next frame
		public bool conditionMet;
		public bool keyDown;
		public bool keyUp;
		
		public PitchMapping(int control, int minVal, int maxVal, string key) {
			this.control = control;
			this.minVal = minVal;
			this.maxVal = maxVal;
			this.key = key;
			
			conditionMet = false;
			keyDown = false;
			keyUp = false;
		}
	}
}