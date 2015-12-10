using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Odometer : MonoBehaviour
{
		public GameObject digitPrefab;
		public int maxValue = 99999;
		public bool rollover = true;
		public bool useGrid = true;
		public bool autoPopulate = true;
		public bool autoSize = true;
		public Color backgroundColor = Color.black;
		public Color fontColor = Color.white;
		public Color backBackgroundColor = Color.black;
		public bool useBorder = false;
		public Font font;
		public float springiness = 8.0f;
		public int digitHeight = 100;
		public int fontSize = 100;
		int value;
		int numDigits;
		int incrementValue;
		float delayTime;
		GameObject[] digits;
		UIGrid grid;
		GameObject border;
		float borderPadding = .1f;

		float heightRatio = .8f;

		// Use this for initialization
		void Start ()
		{
				border = transform.FindChild ("Border").gameObject;
				//	numDigits = (int)(Mathf.Floor (Mathf.Log10 (maxValue) + 1));  
				numDigits = maxValue.ToString ().Length;
				if (useGrid) {
						grid = transform.FindChild ("DigitGrid").GetComponent<UIGrid> ();

						if (autoSize) {
								grid.cellWidth = digitHeight * heightRatio;
								grid.cellHeight = digitHeight;
						}
						grid.enabled = true;
						if (autoPopulate) {
								for (int i=0; i<numDigits; i++) {
										GameObject digit = Instantiate (digitPrefab, Vector3.zero, Quaternion.identity) as GameObject;
										digit.transform.parent = grid.transform;
										digit.transform.localScale = Vector3.one;
										digit.name = "Digit" + i;
								}
						}

						if (autoSize) {
								foreach (Transform child in grid.transform) {	
										if (child.gameObject.GetComponent<OdometerDigit> () != null) {
												OdometerDigit d = child.GetComponent<OdometerDigit> ();
												d.GetComponent<UISprite> ().width = (int)(digitHeight * heightRatio);
												d.GetComponent<UISprite> ().height = (int)(digitHeight * heightRatio);
												d.centerer.transform.GetComponent<UIWrapContent> ().itemSize = digitHeight;
												foreach (Transform c in d.centerer.transform) {
														c.GetComponent<UISprite> ().height = digitHeight;
														c.GetComponent<UISprite> ().width = digitHeight;
														c.FindChild ("Label").GetComponent<UILabel> ().width = (int)(digitHeight * heightRatio);
														c.FindChild ("Label").GetComponent<UILabel> ().height = (int)(digitHeight * heightRatio);
														c.FindChild ("Label").localPosition = new Vector3 (0, -borderPadding * digitHeight * heightRatio, 0);
														c.FindChild ("Label").GetComponent<UILabel> ().fontSize = fontSize;
														if (font != null) {
																c.FindChild ("Label").GetComponent<UILabel> ().trueTypeFont = font;
														}
												}
										}
								}
						}

						if (useGrid) {
								grid.repositionNow = true;
								grid.Reposition();
						}
				}
				
				List<GameObject> digitsList = new List<GameObject> ();
				foreach (Transform child in grid.transform.Cast<Transform>().OrderBy(t=>t.name)) {
						if (child.gameObject.GetComponent<OdometerDigit> () != null && !child.gameObject.GetComponent<OdometerDigit>().notADigit) {
								digitsList.Add (child.gameObject);
						}
				}
				digits = digitsList.ToArray ();
				if (digits.Length != numDigits) {
						Debug.LogError ("ERROR!! max value number of digits must equal number of odometer digits!!! " + digits.Length + "  " + numDigits);
				}
				if (border != null && useBorder) {
						UISprite borderSprite = border.GetComponent<UISprite> ();
						borderSprite.enabled = true;
						borderSprite.width = (int)(grid.cellWidth * grid.transform.childCount + grid.cellWidth * borderPadding);
						borderSprite.height = (int)(grid.cellWidth + grid.cellWidth * borderPadding);
						border.transform.localPosition = new Vector3 (borderSprite.width / 2 - grid.cellWidth / 2 - grid.cellWidth * borderPadding / 2, 0, 0);
				}

				UpdateOdometerSpringiness (springiness);
				UpdateColors ();
		}

		public int getValue ()
		{
				return value;
		}

		public void IncrementJump (int val)
		{
				SetValue (value + val);
		}

		public void IncrementSmooth (int val, float delay)
		{
				incrementValue = val;
				delayTime = delay;
				StartCoroutine ("InternalIncrementSmooth");
		}

		IEnumerator InternalIncrementSmooth ()
		{
				int currentValue = value;
				for (int i=0; i<incrementValue; i++) {
						SetValue (currentValue + i + 1);
						yield return new WaitForSeconds (delayTime);
				}
		}

		public void SetValue (int val)
		{
				if (val > maxValue && rollover) {
						val = val - maxValue - 1;
				}
				if (val > maxValue && !rollover) {
						val = maxValue;
				}
				value = val;
				UpdateOdometer ();
		}

		void UpdateOdometer ()
		{
				int sum = 0;
				for (int i=0; i<numDigits; i++) {
						int digitBaseValue = (int)(Mathf.Pow (10, numDigits - i - 1));
						int digitValue = (value - sum) / digitBaseValue;
						sum += digitValue * digitBaseValue;
						digits [i].GetComponent<OdometerDigit> ().newValue = digitValue;
				}
		}

		public void UpdateOdometerSpringiness (float springValue)
		{
				springValue = Mathf.Clamp (springValue, 1f, 10f);
				foreach (GameObject digit in digits) {
						digit.GetComponent<OdometerDigit> ().centerer.springStrength = springValue;
				}
		}

		public void UpdateColors ()
		{
				foreach (GameObject digit in digits) {
						OdometerDigit od = digit.GetComponent<OdometerDigit> ();
						if (!od.overrideColors) {
								od.backBackgroundColor = backBackgroundColor;
								od.fontColor = fontColor;
								od.backgroundColor = backgroundColor;
								od.UpdateColors ();
						} 
				}
		}
		
		void Update ()
		{
	  
		}
}
