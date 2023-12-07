using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class functions : MonoBehaviour {
	public AudioSource click;
	public GameObject ExitPrompt;
	public GameObject Sideprompt;
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

	public void OpenSkirmishSide(){
		click.Play();
		Sideprompt.SetActive(true);
	}

	public void CloseSidePrompt(){
		click.Play();
		Sideprompt.SetActive(false);
	}

	public void LoadSkirmishTeam1(){
		click.Play();
		global.team=1;
		Application.LoadLevel("skirmish");
	}
	public void LoadSkirmishTeam2(){
		click.Play();
		global.team=2;
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

	public void LoadStory(){
		click.Play(); 
		if(global.storylevel==0 || global.storylevel==1)
		{if(global.storylevel==0)global.storylevel+=1;Application.LoadLevel("medievalChoice");}
		else if(global.storylevel==2)
			Application.LoadLevel("RevolutionaryChoice");
		else if(global.storylevel==3)
			Application.LoadLevel("armadaChoice");
		else if(global.storylevel==4)
			Application.LoadLevel("civilwarChoice");
		else if(global.storylevel==5)
			Application.LoadLevel("isandlwanaChoice");
		else if(global.storylevel==6)
			Application.LoadLevel("battleshipsChoice");
		else if(global.storylevel==7)
			Application.LoadLevel("dogfightChoice");
	}
	public void ChooseFrench(){
		global.team=2; click.Play();
		Application.LoadLevel("medieval");
	}
	public void ChooseEnglish(){
		global.team=1; click.Play();
		Application.LoadLevel("medieval");
	}
	public void ChooseReccoats(){
		global.team=1; click.Play();
		Application.LoadLevel("revolutionarywar");
	}
	public void ChooseContinental(){
		global.team=2; click.Play();
		Application.LoadLevel("revolutionarywar");
	}
	public void ChooseSpanish(){
		global.team=1; click.Play();
		Application.LoadLevel("armada");
	}
	public void ChooseEnglishNavy(){
		global.team=2; click.Play();
		Application.LoadLevel("armada");
	}
	public void ChooseUnion(){
		global.team=1; click.Play();
		Application.LoadLevel("civilwar");
	}
	public void ChooseConfed(){
		global.team=2; click.Play();
		Application.LoadLevel("civilwar");
	}
	public void ChooseBritish(){
		global.team=1; click.Play();
		Application.LoadLevel("isandlwana");
	}
	public void ChooseZulu(){
		global.team=2; click.Play();
		Application.LoadLevel("isandlwana");
	}
	public void ChooseShip(){
		global.team=1; click.Play();
		Application.LoadLevel("battleships");
	}
	public void ChooseSub(){
		global.team=2; click.Play();
		Application.LoadLevel("battleships");
	}
	public void Choosef16(){
		global.team=1; click.Play();
		Application.LoadLevel("dogfight");
	}
	public void Choosemig29(){
		global.team=2; click.Play();
		Application.LoadLevel("dogfight");
	}
	public void LoadFinal(){
		global.team=1; click.Play();
		Application.LoadLevel("final");
	}

	public void StoryLoad(){
		//global.load=true;
		var worldwide=GameObject.Find("Global").GetComponent<global>();
		worldwide.Load();
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
