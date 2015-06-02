using UnityEngine;
using UnityEditor;
using UnityPitchControl.Input;
using System.Collections.Generic;

namespace UnityPitchControl.Editor {
	public class KeyMapEditor : EditorWindow {
		private InputManager _inputManager;

		private string[] audioInputs = Microphone.devices; // names of possible audio inputs
		private int audioInput = 0; // index of currently used audio input

		private Vector2 _mappingsScrollPos = Vector2.zero;
		
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

			// make sure audioInput index matches the currently selected audio input
			for (int i = 0; i < audioInputs.Length; ++i) {
				if (audioInputs[i] == _inputManager.AudioInput) {
					audioInput = i;
				}
			}
		}
		
		public void OnDisable() {
			SavePrefab();
		}
		
		public void OnGUI() {
			if (_inputManager == null) {
				OnEnable(); // reload input manager; required when editor window opens with unity (instead of being opened from the menu) and no prefab exists
			}
			EditorGUILayout.LabelField("Audio Settings: ", EditorStyles.boldLabel);
			int selection = GUILayout.SelectionGrid(audioInput, audioInputs, 1, EditorStyles.radioButton);
			if (selection != audioInput) {
				audioInput = selection;
				_inputManager.AudioInput = audioInputs[audioInput];
			}
			
			_mappingsScrollPos = EditorGUILayout.BeginScrollView(_mappingsScrollPos);
			if (_inputManager.PitchMappings.Mappings.Count > 0) {
				EditorGUILayout.LabelField("Pitch Mappings: ", EditorStyles.boldLabel);
				
				for (int i = _inputManager.PitchMappings.Mappings.Count - 1; i >= 0; --i) {
					PitchMapping m = _inputManager.PitchMappings.Mappings[i];
					
					GUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("if pitch (Hz)", GUILayout.MaxWidth(70));
					EditorGUIUtility.labelWidth = 15;
					m.minVal = EditorGUILayout.IntField(">", m.minVal, GUILayout.MaxWidth(50));
					EditorGUIUtility.labelWidth = 50;
					m.maxVal = EditorGUILayout.IntField("and <=", m.maxVal, GUILayout.MaxWidth(85));
					EditorGUIUtility.labelWidth = 50;
					m.key = EditorGUILayout.TextField("trigger:", m.key, GUILayout.MaxWidth(120)); // TODO: validate that this is a real key
					
					if (GUILayout.Button("-", GUILayout.MaxWidth(25))) {
						_inputManager.RemoveMapping(m.minVal, m.maxVal, m.key);
					}
					GUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndScrollView();

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