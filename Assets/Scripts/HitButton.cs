using UnityEngine;
using System.Collections;

public class HitButton : MonoBehaviour {

	public static bool buttonPressed = false;

	public void Press(){
		buttonPressed = true;
		//print ("button pressed =" + buttonPressed);
	}

	public void Release(){
		buttonPressed = false;
		//print ("button pressed =" + buttonPressed);
	}
}
