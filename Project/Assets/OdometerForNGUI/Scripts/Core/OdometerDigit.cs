using UnityEngine;
using System.Collections;

public class OdometerDigit : MonoBehaviour
{
		public Transform zero;
		public UICenterOnChild centerer;
		public int value;
		public int newValue;
		public bool notADigit = false;
		public bool overrideColors = false;
		public Color backgroundColor = Color.black;
		public Color fontColor = Color.white;
		public Color backBackgroundColor = Color.black;
		void Start ()
		{
		if(overrideColors){
			UpdateColors ();
		}
			ZeroDigit ();

		}

		void Update ()
		{
				if (newValue > 9) {
						newValue = 9;
						//just in case something odd gets passed in, we don't want an endless loop
				}

				if (newValue != value) {
						value++; 
						if (value > 9) {
								value = 0;
						}
						centerer.CenterOn (centerer.transform.GetChild (value));
				}
		}

		public void ZeroDigit ()
		{
				centerer.CenterOn (zero);
				value = 0;
				newValue = 0;
		}

		public void UpdateColors(){
	
			GetComponent<UISprite>().color = backBackgroundColor;
			foreach(Transform child in centerer.transform){
			child.GetComponent<UISprite>().color = backgroundColor;
			child.FindChild("Label").GetComponent<UILabel>().color = fontColor;
		}
	}

}
