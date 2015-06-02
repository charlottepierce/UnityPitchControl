using UnityEngine;
using UnityEditor;
using UnityPitchControl.Input;
using System.Collections.Generic;

namespace UnityPitchControl.Editor {
	public class KeyMapEditor : EditorWindow {
		private InputManager _inputManager;
//		private Vector2 _scrollPos = Vector2.zero;
		
		[MenuItem("Pitch Input/Edit Key Mappings")]
		public static void ShowWindow() {
			EditorWindow win = EditorWindow.GetWindow(typeof(KeyMapEditor)); // show editor window; create it if it doesn't exist
			win.title = "Pitch Controls";
		}
		
		public void OnEnable() {
			_inputManager = UnityEngine.Object.FindObjectOfType(typeof(InputManager)) as InputManager;
			if (_inputManager == null) {
				// try to load prefab
				Object managerPrefab = Resources.Load("InputManager"); // looks inside all 'Resources' folders in 'Assets'
				if (managerPrefab != null) {
					Object prefab = Instantiate(managerPrefab);
					prefab.name = "InputManager"; // otherwise creates a game object with "(Clone)" appended to the name
				} else if (UnityEngine.Object.FindObjectOfType(typeof(InputManager)) == null) {
					// no prefab found, create new input manager
					GameObject gameObject = new GameObject("InputManager");
					gameObject.AddComponent<InputManager>();
					DontDestroyOnLoad(gameObject);
				}
				_inputManager = UnityEngine.Object.FindObjectOfType(typeof(InputManager)) as InputManager;
			}
		}
		
		public void OnDisable() {
			SavePrefab();
		}
		
		public void OnGUI() {
//			if (_inputManager == null) {
//				OnEnable(); // reload input manager; required when editor window opens with unity (instead of being opened from the menu) and no prefab exists
//			}
//			
//			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
//			if (_inputManager.KeyMappings.Mappings.Count > 0) {
//				EditorGUILayout.LabelField("Key Mappings: ", EditorStyles.boldLabel);
//				
//				for (int i = _inputManager.KeyMappings.Mappings.Count - 1; i >= 0; --i) {
//					KeyMapping m = _inputManager.KeyMappings.Mappings[i];
//					
//					GUILayout.BeginHorizontal();
//					EditorGUIUtility.labelWidth = 90;
//					m.trigger = EditorGUILayout.IntField(" Note Number:", m.trigger, GUILayout.MaxWidth(130));
//					EditorGUIUtility.labelWidth = 60;
//					m.key = EditorGUILayout.TextField("Triggers:", m.key, GUILayout.MaxWidth(130));
//					
//					if (GUILayout.Button("-", GUILayout.MaxWidth(25))) {
//						_inputManager.RemoveMapping(m.trigger, m.key);
//					}
//					GUILayout.EndHorizontal();
//				}
//			}
//			
//			if (_inputManager.ControlMappings.Mappings.Count > 0) {
//				EditorGUILayout.LabelField("Control Mappings: ", EditorStyles.boldLabel);
//				
//				for (int i = _inputManager.ControlMappings.Mappings.Count - 1; i >= 0; --i) {
//					ControlMapping m = _inputManager.ControlMappings.Mappings[i];
//					
//					GUILayout.BeginHorizontal();
//					EditorGUIUtility.labelWidth = 40;
//					m.control = EditorGUILayout.IntField(" if CC", m.control, GUILayout.MaxWidth(75));
//					EditorGUIUtility.labelWidth = 15;
//					m.minVal = EditorGUILayout.IntField(">", m.minVal, GUILayout.MaxWidth(50));
//					EditorGUIUtility.labelWidth = 50;
//					m.maxVal = EditorGUILayout.IntField("and <=", m.maxVal, GUILayout.MaxWidth(85));
//					EditorGUIUtility.labelWidth = 50;
//					m.key = EditorGUILayout.TextField("trigger:", m.key, GUILayout.MaxWidth(120)); // TODO: validate that this is a real key
//					
//					if (GUILayout.Button("-", GUILayout.MaxWidth(25))) {
//						_inputManager.RemoveMapping(m.control, m.minVal, m.maxVal, m.key);
//					}
//					GUILayout.EndHorizontal();
//				}
//			}
//			EditorGUILayout.EndScrollView();
//			
//			GUILayout.BeginHorizontal();
//			if (GUILayout.Button("New Key Mapping", GUILayout.MaxWidth(182))) {
//				_inputManager.MapKey(-1, "");
//			}
//			if (GUILayout.Button("New Control Mapping", GUILayout.MaxWidth(182))) {
//				_inputManager.MapControl(-1, -1, -1, "");
//			}
//			GUILayout.EndHorizontal();
//			
//			if (GUILayout.Button("Save Mappings", GUILayout.MaxWidth(369))) {
//				SavePrefab();
//			}
		}
		
		private void SavePrefab() {
			if (!System.IO.Directory.Exists("Assets/UnityPitchControl/Resources")) {
				System.IO.Directory.CreateDirectory("Assets/UnityPitchControl/Resources");
			}
			
			GameObject inputManager = GameObject.Find("InputManager");
			PrefabUtility.CreatePrefab("Assets/UnityPitchControl/Resources/InputManager.prefab", inputManager);
			AssetDatabase.Refresh();
		}
	}
}