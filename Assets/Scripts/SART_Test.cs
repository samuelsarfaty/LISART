using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SART_Test : MonoBehaviour {

	public GameObject door;
	public GameObject go;
	public GameObject noGo;
	public Text counter;
	public float waitTime;

	private BlockGenerator blockGenerator;
	private bool allTrialsDone;
	private int currentBlock;
	private int currentTrial;
	private bool showingTrial = false;
	private Coroutine lastRoutine;
	private float lastPressTime = 0;

	void Awake(){
		blockGenerator = GameObject.FindObjectOfType<BlockGenerator> ();

		currentBlock = 0;
		currentTrial = 0;
	}

	void Start(){

		Reset ();
		lastRoutine = StartCoroutine(StartGame());
	}

	void Update(){

		if(showingTrial == true){
			if(HitButton.buttonPressed == true && (Time.time - lastPressTime > 0.3f)){
				lastPressTime = Time.time;
				HitButton.buttonPressed = false;
				StopCoroutine (lastRoutine);
				StartCoroutine (WaitForNextStimulus ());

				if(allTrialsDone){
					MainMenuManager.testCompleted = true;
					SceneManager.LoadScene ("SART_MainMenu");
				}
			}

		}
	}

	IEnumerator StartGame(){
		for (int i = 3; i > 0; i--){
			counter.text = i.ToString ();
			yield return new WaitForSeconds (1);
		}

		counter.gameObject.SetActive (false);

		StartCoroutine (WaitForNextStimulus ());
	}

	IEnumerator ShowTrial(bool trial){

		showingTrial = true;

		if(trial == true){
			ShowGo ();
		} else if (trial == false){
			ShowNoGo ();
		}
			
		yield return new WaitForSeconds (waitTime);
		showingTrial = false;

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
		lastRoutine = StartCoroutine (ShowTrial (SelectTrial ()));
	}

	bool SelectTrial(){
		List<bool> block = blockGenerator.allBlocks [currentBlock];
		bool trial = block [currentTrial];

		//print ("block: " + currentBlock + " trial: " + currentTrial);

		currentTrial++;

		if(currentTrial == block.Count && currentBlock == blockGenerator.allBlocks.Length - 1){
			//print ("end of test");
			allTrialsDone = true;
		}

		if(currentTrial == block.Count){
			//print ("end of block");
			currentTrial = 0;
			currentBlock++;
		}
	
		return trial;
			
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
