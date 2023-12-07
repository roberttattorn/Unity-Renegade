using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PutUnits : MonoBehaviour {
	public GameObject unit1;
	public GameObject unit2;
	public GameObject unit3;
	public GameObject unit4;
	public GameObject unit5;
	public GameObject unit6;
	public GameObject unit7;
	public GameObject unit8;
	public GameObject unit9;
	public GameObject unit10;
	public GameObject unit11;
	const int swordsman=1; const int archer=2;const int knight=3;const int pikemman=4;const int cavalry=5;const int redcoat=6;
	const int cannon=7; const int soldier=8;const int tank=9;const int apc=10;const int manpad=11;
	public GameObject englishswordsman;
	public GameObject englisharcher;
	public GameObject englishpikeman;public GameObject englishknight; public GameObject britcavalry;public GameObject britredcoat;
	public GameObject britcannon; public GameObject ussoldier; public GameObject ustank; public GameObject bradley;
	public GameObject nlaw;
	public GameObject frenchswordsman;
	public GameObject crossbowman;
	public GameObject frenchpikeman;public GameObject frenchknight; public GameObject contcavalry;public GameObject continental;
	public GameObject contcannon; public GameObject sovsoldier; public GameObject sovtank; public GameObject bmp;
	public GameObject rpg;
	public GameObject Genglishswordsman;
	public GameObject Genglisharcher;
	public GameObject Genglishpikeman;public GameObject Genglishknight; public GameObject Gbritcavalry;public GameObject Gbritredcoat;
	public GameObject Gbritcannon; public GameObject Gussoldier; public GameObject Gustank; public GameObject Gbradley;
	public GameObject Gnlaw;
	public GameObject Gfrenchswordsman;
	public GameObject Gcrossbowman;
	public GameObject Gfrenchpikeman;public GameObject Gfrenchknight; public GameObject Gcontcavalry;public GameObject Gcontinental;
	public GameObject Gcontcannon; public GameObject Gsovsoldier; public GameObject Gsovtank; public GameObject Gbmp;
	public GameObject Grpg;
	public GameObject button1text; public GameObject button2text; public GameObject button3text; public GameObject button4text;
	public GameObject button5text; public GameObject button6text; public GameObject button7text; public GameObject button8text;
	public GameObject button9text; public GameObject button10text; public GameObject button11text;
	public bool isMain=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 if(isMain)
		{if(Input.GetMouseButton(1))
			{global.chosenunit=0;RemoveGhosts();}
		if(global.chosenteam==1)
			{AssignTeam1();
				CheckTeamUnit();}
			else
			{AssignTeam2();
				CheckTeamUnit();}
		 if(Input.GetMouseButtonDown(0) && global.chosenunit>0)
				PutUnit();
		}

	}

	public void AssignTeam1(){
		unit1=englishswordsman; unit2=englisharcher; unit3=englishpikeman; unit4=englishknight; unit5=britcavalry;
		unit6=britredcoat;unit7=britcannon; unit8=ussoldier; unit9=ustank; unit10=bradley; unit11=nlaw;
		button2text.GetComponent<Text>().text="archer"; button6text.GetComponent<Text>().text="redcoat";
		button11text.GetComponent<Text>().text="manpad";
	}

	public void AssignTeam2(){
		unit1=frenchswordsman; unit2=crossbowman; unit3=frenchpikeman; unit4=frenchknight; unit5=contcavalry;
		unit6=continental;unit7=contcannon; unit8=sovsoldier; unit9=sovtank; unit10=bmp; unit11=rpg;
		button2text.GetComponent<Text>().text="crossbowman"; button6text.GetComponent<Text>().text="continental";
		button11text.GetComponent<Text>().text="rpg";
	}

	public void CheckTeamUnit(){ if(Time.timeScale==1.0f)return;
		RaycastHit hit;
		//ray shooting out of the camera from where the mouse is
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out hit))
		{
			if(hit.collider.tag=="Untagged"){
			  if(global.chosenteam==1){
				if(global.chosenunit==1)
					Genglishswordsman.transform.position=hit.point;
				if(global.chosenunit==2)
						Genglisharcher.transform.position=hit.point;
				if(global.chosenunit==3)
						Genglishpikeman.transform.position=hit.point;
				if(global.chosenunit==4)
						Genglishknight.transform.position=hit.point;
				if(global.chosenunit==5)
						Gbritcavalry.transform.position=hit.point;
				if(global.chosenunit==6)
						Gbritredcoat.transform.position=hit.point;
				if(global.chosenunit==7)
						Gbritcannon.transform.position=hit.point;
				if(global.chosenunit==8)
						Gussoldier.transform.position=hit.point;
				if(global.chosenunit==9)
						Gustank.transform.position=hit.point;
				if(global.chosenunit==10)
						Gbradley.transform.position=hit.point;
				if(global.chosenunit==11)
						Gnlaw.transform.position=hit.point;
					if(global.chosenunit==0)
						RemoveGhosts();
				}//
				else if(global.chosenteam==2){
					if(global.chosenunit==1)
						Gfrenchswordsman.transform.position=hit.point;
					if(global.chosenunit==2)
						Gcrossbowman.transform.position=hit.point;
					if(global.chosenunit==3)
						Gfrenchpikeman.transform.position=hit.point;
					if(global.chosenunit==4)
						Gfrenchknight.transform.position=hit.point;
					if(global.chosenunit==5)
						Gcontcavalry.transform.position=hit.point;
					if(global.chosenunit==6)
						Gcontinental.transform.position=hit.point;
					if(global.chosenunit==7)
						Gcontcannon.transform.position=hit.point;
					if(global.chosenunit==8)
						Gsovsoldier.transform.position=hit.point;
					if(global.chosenunit==9)
						Gsovtank.transform.position=hit.point;
					if(global.chosenunit==10)
						Gbmp.transform.position=hit.point;
					if(global.chosenunit==11)
						Grpg.transform.position=hit.point;
					if(global.chosenunit==0)
						RemoveGhosts();
				}//
			}
		}

	}

	public void SelectUnit1(){ if(Time.timeScale==1.0f)return;  //selection by button
		global.chosenunit=1;
	}
	public void SelectUnit2(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=2;
	}
	public void SelectUnit3(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=3;
	}
	public void SelectUnit4(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=4;
	}
	public void SelectUnit5(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=5;
	}
	public void SelectUnit6(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=6;
	}
	public void SelectUnit7(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=7;
	}
	public void SelectUnit8(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=8;
	}
	public void SelectUnit9(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=9;
	}
	public void SelectUnit10(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=10;
	}
	public void SelectUnit11(){ if(Time.timeScale==1.0f)return;
		global.chosenunit=11;
	}

  public void RemoveGhosts(){
		Genglishswordsman.transform.position=Vector3.zero-Vector3.up*100;
		Genglisharcher.transform.position=Vector3.zero-Vector3.up*100;
		Genglishknight.transform.position=Vector3.zero-Vector3.up*100;
		Genglishpikeman.transform.position=Vector3.zero-Vector3.up*100;
		Gbritredcoat.transform.position=Vector3.zero-Vector3.up*100;
		Gbritcannon.transform.position=Vector3.zero-Vector3.up*100;
		Gbritcavalry.transform.position=Vector3.zero-Vector3.up*100;
		Gussoldier.transform.position=Vector3.zero-Vector3.up*100;
		Gustank.transform.position=Vector3.zero-Vector3.up*100;
		Gbradley.transform.position=Vector3.zero-Vector3.up*100;
		Gnlaw.transform.position=Vector3.zero-Vector3.up*100;
		Gfrenchswordsman.transform.position=Vector3.zero-Vector3.up*100;
		Gcrossbowman.transform.position=Vector3.zero-Vector3.up*100;
		Gfrenchpikeman.transform.position=Vector3.zero-Vector3.up*100;
		Gfrenchknight.transform.position=Vector3.zero-Vector3.up*100;
		Gcontinental.transform.position=Vector3.zero-Vector3.up*100;
		Gcontcavalry.transform.position=Vector3.zero-Vector3.up*100;
		Gcontcannon.transform.position=Vector3.zero-Vector3.up*100;
		Gsovsoldier.transform.position=Vector3.zero-Vector3.up*100;
		Gsovtank.transform.position=Vector3.zero-Vector3.up*100;
		Gbmp.transform.position=Vector3.zero-Vector3.up*100;
		Grpg.transform.position=Vector3.zero-Vector3.up*100;

	}

	public void PutUnit(){  if(Time.timeScale==1.0f)return;
		RaycastHit hit;
		//ray shooting out of the camera from where the mouse is
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out hit))
		{
			if(hit.collider.tag=="Untagged"){
		if(global.chosenteam==1){
			if(global.chosenunit==1)
						Instantiate(unit1,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==2)
						Instantiate(unit2,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==3)
						Instantiate(unit3,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==4)
						Instantiate(unit4,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==5)
						Instantiate(unit5,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==6)
						Instantiate(unit6,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==7)
						Instantiate(unit7,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==8)
						Instantiate(unit8,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==9)
						Instantiate(unit9,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==10)
						Instantiate(unit10,hit.point,Quaternion.LookRotation(-Vector3.right));
					if(global.chosenunit==11)
						Instantiate(unit11,hit.point,Quaternion.LookRotation(-Vector3.right)); global.chosenunit=0;
		}
		else if(global.chosenteam==2){
					if(global.chosenunit==1)
						Instantiate(unit1,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==2)
						Instantiate(unit2,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==3)
						Instantiate(unit3,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==4)
						Instantiate(unit4,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==5)
						Instantiate(unit5,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==6)
						Instantiate(unit6,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==7)
						Instantiate(unit7,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==8)
						Instantiate(unit8,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==9)
						Instantiate(unit9,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==10)
						Instantiate(unit10,hit.point,Quaternion.LookRotation(Vector3.right));
					if(global.chosenunit==11)
						Instantiate(unit11,hit.point,Quaternion.LookRotation(Vector3.right)); global.chosenunit=0;
		}
		RemoveGhosts();
			}
			}
	}//

}
