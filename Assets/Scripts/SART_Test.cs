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
	private CSV_Maker csvMaker;

	private string blockType = "Test";
	private bool allTrialsDone;
	private int currentBlock;
	private int currentTrial;
	private bool showingTrial = false;
	private Coroutine lastRoutine;
	private float lastButtonPressTime = 0;

	//For writing CSV
	private float testStartTime;
	private float presentationTime;
	private int blockToWrite;
	private int trialToWrite;
	private float cummulativeActionTime;
	private float lastActionTime;


	void Awake(){
		blockGenerator = GameObject.FindObjectOfType<BlockGenerator> ();
		csvMaker = GameObject.FindObjectOfType<CSV_Maker> ();

		currentBlock = 0;
		currentTrial = 0;

		lastActionTime = 0;
		cummulativeActionTime = 0;

	}

	void Start(){

		Reset ();
		testStartTime = Time.time;
		lastRoutine = StartCoroutine(StartGame());
	}

	void Update(){

		/*if(showingTrial == true){
			if(HitButton.buttonPressed == true && (Time.time - lastButtonPressTime > 0.3f)){
				lastButtonPressTime = Time.time;
				HitButton.buttonPressed = false;
				StopCoroutine (lastRoutine);
				StartCoroutine (WaitForNextStimulus ());

				if(allTrialsDone){
					MainMenuManager.testCompleted = true;
					SceneManager.LoadScene ("SART_MainMenu");
				}

			} else if (HitButton.buttonPressed == true && (Time.time - lastButtonPressTime < 0.3f)){ //Check for second press within same trial
				print ("second push at: " + Time.time);
				HitButton.buttonPressed = false;
			}
		}*/

		if(showingTrial == true){
			if(HitButton.buttonPressed == true && (Time.time - lastButtonPressTime > 0.3f)){
				lastButtonPressTime = Time.time;
				print ("first push at: " + lastButtonPressTime);
				HitButton.buttonPressed = false;
				StopCoroutine (lastRoutine);
				StartCoroutine (WaitForNextStimulus ());

				if(allTrialsDone){
					MainMenuManager.testCompleted = true;
					SceneManager.LoadScene ("SART_MainMenu");
				}

			} else if (HitButton.buttonPressed == true && (Time.time - lastButtonPressTime < 0.3f)){ //Check for second press within same trial
				print ("second push at: " + Time.time);
				HitButton.buttonPressed = false;
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

		presentationTime = Time.time - testStartTime;

		showingTrial = true;

		if(trial == true){
			ShowGo ();
		} else if (trial == false){
			ShowNoGo ();
		}
			
		yield return new WaitForSeconds (waitTime);

		csvMaker.Write (blockType, blockToWrite, trialToWrite, trial, presentationTime, 0, 0, false);

		showingTrial = false;

		if(allTrialsDone){
			MainMenuManager.testCompleted = true;
			csvMaker.CreateCSVFile (csvMaker.path, csvMaker.dataCollected);
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

		blockToWrite = currentBlock;
		trialToWrite = currentTrial;

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
