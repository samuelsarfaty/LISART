using UnityEngine;
using System.Collections;

public class SART_TestManager : MonoBehaviour {

	public int blockNumber;
	public int goCount;
	public int noGoCount;

	public float stimulusWaitTime;
	public float delayBetweenStimuli;
	public float cummulativeTime;

	public GameObject go;
	public GameObject noGo;
	public GameObject door;

	void Start(){
		Reset ();
	}

	void Update(){
		
	}

	void ShowGo(){
		door.SetActive (false);
		go.SetActive (true);
	}

	void ShowNoGo(){
		door.SetActive (false);
		noGo.SetActive (false);
	}

	void Reset(){
		go.SetActive (false);
		noGo.SetActive (false);
		door.SetActive (true);
	}
		
}
