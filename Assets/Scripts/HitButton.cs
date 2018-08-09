using UnityEngine;
using System.Collections;

public class HitButton : MonoBehaviour {

	public static bool buttonPressed = false;


	void Update(){
		/*if(buttonPressed == true){
			buttonPressed = false;
		}*/
	}

	public void Press(){
		buttonPressed = true;
		print ("button pressed =" + buttonPressed);
	}
}
