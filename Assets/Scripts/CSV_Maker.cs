using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
public class CSV_Maker : MonoBehaviour {

	private static CSV_Maker maker;
	private List<string[]> dataCollected = new List<string[]> ();
	private string path = "E:/Samuel/Goldsmiths/Research projects/Listen-In SART/Assets/Resources/SART_Data.csv"; //Choose path to output data

	void Start(){
		maker = this;
		dataCollected = CreateHeader ();
	}

	public List<string[]> CreateHeader(){
		List<string[]> rowData = new List<string[]> ();
		string[] temp = new string[7];

		temp [0] = "TrialNumber";
		temp [1] = "BlockNumber";
		temp [2] = "Type";
		temp [3] = "isGo?";
		temp [4] = "PresentationTime";
		temp [5] = "ReactionTime";
		temp [6] = "Hit";

		rowData.Add (temp);
		return rowData;
	}

	public static void Write(int trial, int block, string type, bool isGo, float presentationTime, float reactionTime, bool hit){
		string[] rowDataTemp = new string[7];

		rowDataTemp [0] = trial.ToString ();
		rowDataTemp [1] = block.ToString ();
		rowDataTemp [2] = type;
		rowDataTemp [3] = isGo.ToString ();
		rowDataTemp [4] = presentationTime.ToString ();
		rowDataTemp [5] = reactionTime.ToString ();
		rowDataTemp [6] = hit.ToString ();

		maker.dataCollected.Add (rowDataTemp);
	}

	public static void CreateCSVFile(string directory, List<string[]> data){
		string[][] output = new string[data.Count][];

		for (int i = 0; i < output.Length; i++){
			output [i] = data [i];
		}

		int lenght = output.GetLength (0);
		string delimeter = ",";

		StringBuilder sb = new StringBuilder ();

		for (int index = 0; index < lenght; index++){
			sb.AppendLine (string.Join (delimeter, output [index]));
		}

		StreamWriter outStream = System.IO.File.CreateText (directory);
		outStream.WriteLine (sb);
		outStream.Close ();
	}

	void OnApplicationQuit(){
		CreateCSVFile (path, dataCollected);
	}


}
