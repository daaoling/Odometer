using UnityEngine;
using System.Collections;

public class OdometerExample : MonoBehaviour {

	public Odometer odometer;
	bool autoStarted = false;
   float flipTime =.5f;
	float lastFlipTime;
	// Use this for initialization
	void Start () {
			
	}

	public void SetSlipSpeed(){
		flipTime = (1-UIProgressBar.current.value)*.5f;
	}

	// Update is called once per frame
	void Update () {
	  if(autoStarted){
			if(Time.time-lastFlipTime > flipTime){
				lastFlipTime = Time.time;
				odometer.SetValue(odometer.getValue()+1);
			}
		}
	}

	public void RandomValue(){
		if(autoStarted){
			autoStarted = false;
		}
		odometer.SetValue(Random.Range(0,odometer.maxValue));
	}

	public void MaximumValue(){
		if(autoStarted){
			autoStarted = false;
		}
		odometer.SetValue(odometer.maxValue);
	}

	public void IncrementOnce(){
		if(autoStarted){
			autoStarted = false;
		}
		odometer.SetValue(odometer.getValue()+1);
	}

	public void IncrementTenJump(){
		if(autoStarted){
			autoStarted = false;
		}
		odometer.IncrementJump(10);
	}
	public void IncrementTenSmooth(){
		if(autoStarted){
			autoStarted = false;
		}
		odometer.IncrementSmooth(10,.1f);
	}

	public void ToggleAuto(){
		autoStarted = !autoStarted;
		lastFlipTime = Time.time;
	}


}
