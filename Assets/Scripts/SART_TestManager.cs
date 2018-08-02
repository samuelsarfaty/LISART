using UnityEngine;
using System.Collections;

public class SART_TestManager : MonoBehaviour {

	public int blockNumber;
	public int goCount;
	public int noGoCount;

	public float stimulusWaitTime;
	public float delayBetweenStimuli;
	public float cummulativeTime;

	private float lastActionTime;

	public GameObject go;
	public GameObject noGo;
	public GameObject door;

	private bool isWaiting;


	void Awake(){
		isWaiting = false;
		lastActionTime = Time.time;
	}

	void Start(){
		Reset ();
	}

	void FixedUpdate(){
		if(!isWaiting){
			if(Time.time - lastActionTime >= delayBetweenStimuli){
				lastActionTime = Time.time;
				ShowGo ();
			}
		} else if (isWaiting){
			if(Time.time - lastActionTime >= stimulusWaitTime){
				lastActionTime = Time.time;
				Reset ();
			}
		}



		print (Time.time);
	}

	void ShowGo(){
		print ("Shown!");

		isWaiting = true;
		door.SetActive (false);
		noGo.SetActive (false);
		go.SetActive (true);
		goCount++;
	}

	void ShowNoGo(){
		isWaiting = true;
		door.SetActive (false);
		go.SetActive (false);
		noGo.SetActive (true);
		noGoCount++;
	}

	void Reset(){
		print ("Reset");

		isWaiting = false;
		go.SetActive (false);
		noGo.SetActive (false);
		door.SetActive (true);
	}
		
}
