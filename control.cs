using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class control : MonoBehaviour {
	public AudioSource calmMusic1;
	public AudioSource calmMusic2;
	public AudioSource calmMusic3;
	public AudioSource Music1;
	public AudioSource Music2;
	public AudioSource Music3;
	public AudioSource Music4;
	public GameObject controlbutton;
	public GameObject endctrlbutton;
	//public GameObject playbutton;
	public Text playbuttontext;
	public AudioSource Click;
	private GameObject unit;
	// Use this for initialization

	void Start () {
	if(Application.loadedLevelName=="skirmish")
			Time.timeScale=0.0f; 
	}
	
	// Update is called once per frame
	void Update () {
		Click=GetComponent<AudioSource>();
	}

	public void ChooseUnit(){ if(Time.timeScale==0.0f)return;
		List<GameObject> units = new List<GameObject>();
		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){  //add filter to make sure unit is in player's side
			if(item.GetComponent<unitcontrol>().team==global.team && item.GetComponent<unitcontrol>().dead==false)
			units.Add(item);}
		if(units.Count>0){
		int dice = Random.Range(0,units.Count);
			unit=units[dice];
			unit.GetComponent<unitcontrol>().ControlUnit();
			controlbutton.SetActive(false);
			endctrlbutton.SetActive(true);
			CameraController.dist=Vector3.Distance(Camera.main.transform.position,CameraController.pos);
			Click.Play();
		}
	}
   public void StartSkirmish(){
		Click.Play();
		if(Time.timeScale==0.0f){
		Time.timeScale=1.0f;
			playbuttontext.text="Stop";return;}
		else{
			Time.timeScale=0.0f;
			playbuttontext.text="Play";
		}
	}
   public void StopSkirmish(){
		Click.Play();
		Time.timeScale=0.0f;
		playbuttontext.text="Play";
	}
   public void ExitUnit(){  //end unit control
		if(unit!=null){
			unit.GetComponent<unitcontrol>().EndControl(); 
			unit=null;
			Vector3 vector=Camera.main.transform.position-CameraController.pos;
			Camera.main.transform.position=CameraController.pos+Vector3.Normalize(vector)*CameraController.dist;
			/*ChooseUnit();*/}
		if(unit==null){
		controlbutton.SetActive(true);
			endctrlbutton.SetActive(false);}
	}

	public void SwitchTeam1(){
		global.chosenteam=1;}
	public void SwitchTeam2(){
		global.chosenteam=2;}

}//x




