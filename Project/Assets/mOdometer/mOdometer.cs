using UnityEngine;
using System.Collections;

public class mOdometer : MonoBehaviour {

	private int _curValue = 0;
	public int curValue
	{
		get{
            return this._curValue;
        }
		set{ 
			this._curValue = value;

			OnCurValueChanged();
		}
	}

	void Start()
	{

	}

	public int incrementValue;
	public float delay;
	public void AddValue(int incrementValue)
	{
		this.incrementValue = incrementValue;
		this.delay = 1.0f / this.incrementValue;
		StartCoroutine(InternalIncrementSmooth());
	}




	public IEnumerator InternalIncrementSmooth()
	{
		for (int i = 0; i < this.incrementValue; i++) 
		{
			this.curValue += 1;
			yield return new WaitForSeconds(this.delay);
		}
	}

	public UILabel curLabel;
	public void OnCurValueChanged()
	{
		this.curLabel.text = this.curValue.ToString();
	}
}