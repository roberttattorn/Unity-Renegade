using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class functions : MonoBehaviour {
	public AudioSource click;
	public GameObject ExitPrompt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadMainMenu(){
		click.Play();
		Application.LoadLevel("mainmenu");
	}

	public void LoadOptions(){
		click.Play();
		Application.LoadLevel("options");
	}

	public void LoadMenu(){
		click.Play();
		Application.LoadLevel("menu");
	}

	public void LoadSkirmish(){
		click.Play();
		Application.LoadLevel("skirmish");
	}

	public void SetHard(){
		click.Play();
		if(GameObject.Find("hard").GetComponent<Toggle>().isOn)
		global.difficulty=1;
	}

	public void SetEasy(){
		click.Play();
		if(GameObject.Find("easy").GetComponent<Toggle>().isOn)
		global.difficulty=0;
	}

	public void SetMusicVolume(){
		global.musicVolume=GameObject.Find("Slider").GetComponent<Slider>().value;
		GameObject.Find("Theme").GetComponent<AudioSource>().volume=global.musicVolume;
	}

	public void SetAutoSave(){
		click.Play();
		if(GameObject.Find("Autosave").GetComponent<Toggle>().isOn)
			global.autosave=true;
		else
			global.autosave=false;
	}

	public void OpenExitPrompt(){
		click.Play();
		ExitPrompt.SetActive(true);
	}

	public void CloseExitPrompt(){
		click.Play();
		ExitPrompt.SetActive(false);
	}

	public void Quit(){
		click.Play();
		Application.Quit();
	}
}////////////////

/*if(Input.GetMouseButtonDown(0))
{
	//create a ray cast and set it to the mouses cursor position in game
	Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	RaycastHit hit;
	if (Physics.Raycast (ray, out hit, distance)) 
	{
		//draw invisible ray cast/vector
		Debug.DrawLine (ray.origin, hit.point);
		//log hit area to the console
		Debug.Log(hit.point);
		
	}    
} */
