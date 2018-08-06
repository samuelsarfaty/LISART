using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SART_Test : MonoBehaviour {

	public GameObject door;
	public GameObject go;
	public GameObject noGo;
	public float waitTime;

	private BlockGenerator blockGenerator;


	private bool allTrialsDone;

	private int currentBlock;
	private int currentTrial;

	void Awake(){
		blockGenerator = GameObject.FindObjectOfType<BlockGenerator> ();

		currentBlock = 0;
		currentTrial = 0;

	}

	void Start(){
		StartCoroutine(WaitForNextStimulus());
	}

	void Update(){
		if(allTrialsDone){
			Application.Quit ();
		}
	}

	IEnumerator ShowTrial(bool trial){

		if(trial == true){
			ShowGo ();
		} else if (trial == false){
			ShowNoGo ();
		}
			
		yield return new WaitForSeconds (waitTime);
		StartCoroutine (WaitForNextStimulus ());
	}

	IEnumerator WaitForNextStimulus(){
		Reset ();
		yield return new WaitForSeconds (waitTime);
		StartCoroutine (ShowTrial (SelectTrial ()));
	}

	bool SelectTrial(){
		List<bool> block = blockGenerator.allBlocks [currentBlock];
		bool trial = block [currentTrial];

		if (currentTrial < block.Count && currentBlock < blockGenerator.allBlocks.Length){ //TODO possible error here
			currentTrial++;
		}

		if (currentTrial >= block.Count && currentBlock < blockGenerator.allBlocks.Length) {
			currentTrial = 0;
			currentBlock++;
		}

		if(currentTrial >= block.Count && currentBlock >= blockGenerator.allBlocks.Length){
			allTrialsDone = true;
		}

		return trial;
	}

	void CheckForInput(){
		if(Input.GetKeyDown("space")){
			print ("pressed");
		}
	}

	void ShowGo(){
		door.SetActive (false);
		noGo.SetActive (false);
		go.SetActive (true);
	}

	void ShowNoGo(){
		door.SetActive (false);
		go.SetActive (false);
		noGo.SetActive (true);
	}

	void Reset(){
		go.SetActive (false);
		noGo.SetActive (false);
		door.SetActive (true);
	}
}
