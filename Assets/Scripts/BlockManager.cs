using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour {

	public int maxBlocks;
	public int currentBlock;

	public int maxTrials;
	public int currentTrial;

	public int maxGo;
	public int goCount;
	public int maxNoGo;
	public int noGoCount;



	void Start(){
		GenerateBlock ();
	}

	public List<bool> GenerateBlock(){

		List<bool> trials = new List<bool>();

		for (int i = 0; i < maxGo; i++){	//generate 'true' and add to list
			trials.Add (true);
		}

		for (int i = 0; i < maxNoGo; i++){	//generate 'false' and add to list.
			trials.Add (false);
		}


		for (int i = 0; i < trials.Count; i++) {	//shuffle goes and nogos.
			bool temp = trials [i];
			int randomIndex = Random.Range (i, trials.Count);
			trials [i] = trials [randomIndex];
			trials [randomIndex] = temp;
		}
			
		for (int i = 0; i < trials.Count; i++){ //print all values of block in order.
			print (trials [i]);
		}

		for (int i = 0; i < trials.Count; i++){
			if (i == 0 || i == trials.Count -1){
				continue;
			} else {
				if (trials [i] == false && (trials[i+1] == false || trials[i-1] == false)){
					print ("recreating block");
					GenerateBlock ();
					trials.Clear ();
				}
			}
		}
			
		return trials;
	}
}
