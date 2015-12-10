using UnityEngine;
using System.Collections;

public class mTest : MonoBehaviour {

	public mOdometer odometer;

	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
		{
			odometer.AddValue(5);
		}
	}
}
