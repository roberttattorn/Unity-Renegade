using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeTeam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SwitchTeam1(){
		var toggle=GetComponent<Toggle>();
		if(toggle.isOn)
			global.chosenteam=1;}
	public void SwitchTeam2(){
		var toggle=GetComponent<Toggle>();
		if(toggle.isOn)
		global.chosenteam=2;}
}
