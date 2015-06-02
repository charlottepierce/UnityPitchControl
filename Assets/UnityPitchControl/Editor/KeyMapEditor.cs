using UnityEngine;
using UnityEditor;
using UnityPitchControl.Input;
using System.Collections.Generic;

namespace UnityPitchControl.Editor {
	public class KeyMapEditor : EditorWindow {
		private InputManager _inputManager;
		private Vector2 _scrollPos = Vector2.zero;
		
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
			if (_inputManager == null) {
				OnEnable(); // reload input manager; required when editor window opens with unity (instead of being opened from the menu) and no prefab exists
			}
			
//			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
			if (_inputManager.PitchMappings.Mappings.Count > 0) {
				EditorGUILayout.LabelField("Pitch Mappings: ", EditorStyles.boldLabel);
				
				for (int i = _inputManager.PitchMappings.Mappings.Count - 1; i >= 0; --i) {
					PitchMapping m = _inputManager.PitchMappings.Mappings[i];
					
					GUILayout.BeginHorizontal();
					EditorGUIUtility.labelWidth = 40;
					m.control = EditorGUILayout.IntField(" if Pitch (Hz)", m.control, GUILayout.MaxWidth(75));
					EditorGUIUtility.labelWidth = 15;
					m.minVal = EditorGUILayout.IntField(">", m.minVal, GUILayout.MaxWidth(50));
					EditorGUIUtility.labelWidth = 50;
					m.maxVal = EditorGUILayout.IntField("and <=", m.maxVal, GUILayout.MaxWidth(85));
					EditorGUIUtility.labelWidth = 50;
					m.key = EditorGUILayout.TextField("trigger:", m.key, GUILayout.MaxWidth(120)); // TODO: validate that this is a real key
					
					if (GUILayout.Button("-", GUILayout.MaxWidth(25))) {
						_inputManager.RemoveMapping(m.control, m.minVal, m.maxVal, m.key);
					}
					GUILayout.EndHorizontal();
				}
			}
//			EditorGUILayout.EndScrollView();

			if (GUILayout.Button("New Pitch Mapping", GUILayout.MaxWidth(369))) {
				_inputManager.MapPitch(-1, -1, "");
			}
			if (GUILayout.Button("Save Mappings", GUILayout.MaxWidth(369))) {
				SavePrefab();
			}
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