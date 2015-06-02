Use instruments as game controllers!

This Unity interface allows you to map audio inputs to keyboard buttons - play a pitch from the mapped range on your instrument to trigger the corresponding key press.
The interface currently supports `GetKey`, `GetKeyDown` and `GetKeyUp` events for all keyboard buttons.
Direct button presses (i.e., using the keyboard rather than a mapped instrument) will still be detected.

UnityPitchControl was tested on Unity version __4.6.2f1__.

This project is based on [Realtime C# Pitch Tracker](https://pitchtracker.codeplex.com/).

## Use: ##

1. Copy the contents of __Assets__ into the _Assets_ folder of your project
2. In Unity, click __Pitch Input > Edit Key Mappings__ to open the editor GUI
3. Select the desired audio input device
4. Add key mappings as desired
	* mappings can be removed using the '-' buttons
5. Click __Save Mappings__
6. In your game code, replace calls to `Input.GetKey`, `Input.GetKeyDown` and `Input.GetKeyUp` with `UnityPitchControl.Input.InputManager.GetKey`, `UnityPitchControl.Input.InputManager.GetKeyDown` and `UnityPitchControl.Input.InputManager.GetKeyUp`, respectively
7. Before running your project, ensure your audio device is connected and switched on

## Example Use: ##

The following note mappings trigger:

* the 't' key when a pitch between 80 and 120 is played
* the 'u' key when a pitch between 130 and 140 is played
* the 'a' key when a pitch between 145 and 155 is played

by using input from a guitar attached using a [Rocksmith USB Guitar Adapter](http://rocksmith.ubi.com/rocksmith/en-au/home/index.aspx).

![Example key mappings](https://bitbucket.org/charlottepierce/unitypitchcontrol/raw/master/example_mappings.png)

These keypresses may be detected programmatically using the following code:

	if (UnityMidiControl.Input.InputManager.GetKeyDown("t")) {
		Debug.Log("'x' down");
	}
	if (UnityMidiControl.Input.InputManager.GetKeyDown("u")) {
		Debug.Log("'d' down");
	}
	if (UnityMidiControl.Input.InputManager.GetKeyDown("a")) {
		Debug.Log("'a' down");
	}
	
Using key codes rather than string arguments will also work:

	if (UnityMidiControl.Input.InputManager.GetKeyUp(KeyCode.T)) {
		Debug.Log("'x' up");
	}
	if (UnityMidiControl.Input.InputManager.GetKeyUp(KeyCode.U)) {
		Debug.Log("'d' up");
	}
	if (UnityMidiControl.Input.InputManager.GetKeyUp(KeyCode.A)) {
		Debug.Log("'a' up");
	}