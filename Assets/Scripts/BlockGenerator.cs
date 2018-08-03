﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockGenerator : MonoBehaviour {

	public int maxGo;
	public int maxNoGo;

	private List<bool> generatedBlock;

	void Start(){

		GenerateBlock ();

		for (int i = 0; i < generatedBlock.Count; i++){
			print (generatedBlock [i]);
		}
	}

	public void GenerateBlock(){

		List<bool> trials = new List<bool>(); //List where all trials are stored.

		for (int i = 0; i < maxGo; i++){	//generate all 'trues' and add to list
			trials.Add (true);
		}

		for (int i = 0; i < maxNoGo; i++){	//generate all 'falses' and add to list.
			trials.Add (false);
		}

		for (int i = 0; i < trials.Count; i++) {	//shuffle trues and falses.
			bool temp = trials [i];
			int randomIndex = Random.Range (i, trials.Count);
			trials [i] = trials [randomIndex];
			trials [randomIndex] = temp;
		}

		StartCoroutine (CheckBlockValidity (trials));
	}

	IEnumerator CheckBlockValidity(List<bool> block){

		if(block[0] == false || block[block.Count - 1] == false){ //Check if first or last trial is false.
			print ("false detected at start or end of block, recreating");
			GenerateBlock ();
			yield break;
		}

		for (int i = 0; i < block.Count; i++) {
			if (i == 0 || i == block.Count - 1) {	//skip first and last values of list
				continue;
			} else if (block [i] == false && (block [i + 1] == false)) { //Check if two nogos are together.
				print ("two falses together, recreating block");
				GenerateBlock ();
				yield break;
			}
		}
			
		print ("block generated successfully");
		generatedBlock = block;

		yield return null;
	}
}
