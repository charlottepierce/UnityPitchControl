using UnityEngine;
using System.Collections;
using Pitch;

public class Test : MonoBehaviour {
	PitchTracker _pitchTracker;
	AudioClip _micInput;
	float[] _samples;

	void Start() {
		int minFreq, maxFreq;

		foreach (string device in Microphone.devices) {
			Debug.Log("Name: " + device);
			Microphone.GetDeviceCaps(device, out minFreq, out maxFreq);
			Debug.Log("- " + minFreq + " -> " + maxFreq);
		}

		Microphone.GetDeviceCaps("Built-In Microphone", out minFreq, out maxFreq);
		_micInput = Microphone.Start("Built-In Microphone", true, 1, maxFreq);

		_samples = new float[_micInput.samples * _micInput.channels];
		Debug.Log(_micInput.samples + " samples (" + _micInput.channels + " channel)");

		_pitchTracker = new PitchTracker();
		_pitchTracker.SampleRate = _micInput.samples;
		_pitchTracker.PitchDetected += new PitchTracker.PitchDetectedHandler(PitchDetectedListener);
	}

	void Update() {
		_micInput.GetData(_samples, 0);
		Debug.Log("Samples length: " + _samples.Length);

		float sum = 0;
		foreach (float s in _samples) sum += s;
		float avg = sum / _samples.Length;

		Debug.Log("Samples avg: " + avg.ToString("0.0000000000"));

		_pitchTracker.ProcessBuffer(_samples);
	}

	private void PitchDetectedListener(PitchTracker sender, PitchTracker.PitchRecord pitchRecord) {
		Debug.Log("Pitch detected (" + pitchRecord.Pitch + ")");
	}
}
