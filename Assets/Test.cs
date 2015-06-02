using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	void Update () {
		if (UnityPitchControl.Input.InputManager.GetKeyDown(KeyCode.A)) {
			Debug.Log("'a' down");
		}
		if (UnityPitchControl.Input.InputManager.GetKeyDown(KeyCode.B)) {
			Debug.Log("'b' down");
		}
		if (UnityPitchControl.Input.InputManager.GetKeyDown(KeyCode.C)) {
			Debug.Log("'c' down");
		}
		if (UnityPitchControl.Input.InputManager.GetKeyDown(KeyCode.D)) {
			Debug.Log("'d' down");
		}
		
//		if (UnityPitchControl.Input.InputManager.GetKeyUp(KeyCode.A)) {
//			Debug.Log("'a' up");
//		}
//		if (UnityPitchControl.Input.InputManager.GetKeyUp(KeyCode.B)) {
//			Debug.Log("'b' up");
//		}
//		if (UnityPitchControl.Input.InputManager.GetKeyUp(KeyCode.C)) {
//			Debug.Log("'c' up");
//		}
//		if (UnityPitchControl.Input.InputManager.GetKeyUp(KeyCode.D)) {
//			Debug.Log("'d' up");
//		}
	}
}