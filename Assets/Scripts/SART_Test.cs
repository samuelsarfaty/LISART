using UnityEngine;
using System.Collections;

public class SART_Test : MonoBehaviour {

	private BlockGenerator blockGenerator;

	void Awake(){
		blockGenerator = GameObject.FindObjectOfType<BlockGenerator> ();
	}
		
}
