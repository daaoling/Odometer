using UnityEngine;
using System.Collections;

public class mBgControl : MonoBehaviour {


    void OnPress(bool isDown)
    {
        Debug.Log(" OnPress : " + isDown);
    }

    void OnDrag(Vector2 delta)
    {
        Debug.Log(" OnDrag delta : " + delta);
    }
}
