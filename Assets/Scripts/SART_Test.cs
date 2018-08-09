using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

	IEnumerator ShowTrial(bool trial){

		if(trial == true){
			ShowGo ();
		} else if (trial == false){
			ShowNoGo ();
		}
			
		yield return new WaitForSeconds (waitTime);

		if(allTrialsDone){
			MainMenuManager.testCompleted = true;
			SceneManager.LoadScene ("SART_MainMenu");
		} else {
			StartCoroutine (WaitForNextStimulus ());
		}
	}

	IEnumerator WaitForNextStimulus(){
		
		Reset ();
		yield return new WaitForSeconds (waitTime);
		StartCoroutine (ShowTrial (SelectTrial ()));
	}

	bool SelectTrial(){
		List<bool> block = blockGenerator.allBlocks [currentBlock];
		bool trial = block [currentTrial];

		print ("block: " + currentBlock + " trial: " + currentTrial);

		currentTrial++;

		if(currentTrial == block.Count && currentBlock == blockGenerator.allBlocks.Length - 1){
			print ("end of test");
			allTrialsDone = true;
		}

		if(currentTrial == block.Count){
			print ("end of block");
			currentTrial = 0;
			currentBlock++;
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
