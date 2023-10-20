using UnityEngine;
using System.Collections;

public class unitcontrol : MonoBehaviour {
	public MonoBehaviour ai;
	public MonoBehaviour thirdpersoncontroller;
	public CharacterController charactercontroller;
	//public CapsuleCollider capsulecollider;
	public MonoBehaviour player;
	public Transform target;
	public string unitType="orbit";
	public int team;
	[HideInInspector]
	public bool damaging;
	public int health=100;
	public AudioSource death1;
	public AudioSource death2;
	public AudioSource death3;
	public AudioSource death4;
	[HideInInspector]
	public bool dead=false;
	public GameObject joint1;
	public GameObject joint2;
	public GameObject joint6;
	public GameObject joint10;
	public GameObject joint3;
	public GameObject joint7;
	public GameObject joint11;
	public GameObject joint15;
	public GameObject joint18;
	public GameObject joint12;
	public GameObject joint16;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<ai>().enabled)
			damaging=GetComponent<ai>().damaging;
		if(GetComponent<player>().enabled)
			damaging=GetComponent<player>().damaging;

		if(health<=0 && !dead)
		{
			var dice=Random.Range(0,4);
			if(dice==0)
				death1.Play();
			if(dice==1)
				death2.Play();
			if(dice==2)
				death3.Play();
			if(dice==3)
				death4.Play();
			dead=true; animation.Stop();
			ai.enabled=false;
			thirdpersoncontroller.enabled=false;
			if(player.enabled){
				EndControl();
				var control=GameObject.Find("Control").GetComponent<control>();
				control.ChooseUnit();}
			player.enabled=false;

			EnableRagDoll();
		}
		if(!dead)
			transform.eulerAngles=new Vector3(0,transform.eulerAngles.y,0);
	}

	public void ControlUnit(){
		ai.enabled=false;
		if(thirdpersoncontroller!=null)
		thirdpersoncontroller.enabled=true;
		//capsulecollider.enabled=true;
		charactercontroller.enabled=true;
		player.enabled=true;
		GameObject.Find("CamControl").GetComponent<CameraController>().enabled=false;
		if(unitType=="orbit"){
			Camera.main.GetComponent<MouseOrbit>().enabled=true;
			Camera.main.GetComponent<MouseOrbit>().target=target;}
		else
		{Camera.main.GetComponent<SmoothFollow>().enabled=true;
			Camera.main.GetComponent<SmoothFollow>().target=target;}
	}

	public void EndControl(){
		if(!dead)
		ai.enabled=true;
		if(thirdpersoncontroller!=null)
		thirdpersoncontroller.enabled=false;
		//capsulecollider.enabled=false;
			charactercontroller.enabled=false;
		player.enabled=false;
		GameObject.Find("CamControl").GetComponent<CameraController>().enabled=true;
		if(unitType=="orbit"){
			Camera.main.GetComponent<MouseOrbit>().enabled=false;}
		else
		{Camera.main.GetComponent<SmoothFollow>().enabled=false;}
	}

	public void EnableRagDoll(){ GetComponent<CharacterController>().enabled=false; rigidbody.isKinematic=false;
		joint1.GetComponent<Rigidbody>().isKinematic=false;joint1.GetComponent<Rigidbody>().useGravity=true;
		joint2.GetComponent<Rigidbody>().isKinematic=false;joint2.GetComponent<Rigidbody>().useGravity=true;
		joint6.GetComponent<Rigidbody>().isKinematic=false;joint6.GetComponent<Rigidbody>().useGravity=true;
		joint10.GetComponent<Rigidbody>().isKinematic=false;joint10.GetComponent<Rigidbody>().useGravity=true;
		joint3.GetComponent<Rigidbody>().isKinematic=false;joint3.GetComponent<Rigidbody>().useGravity=true;
		joint7.GetComponent<Rigidbody>().isKinematic=false;joint7.GetComponent<Rigidbody>().useGravity=true;
		joint11.GetComponent<Rigidbody>().isKinematic=false;joint11.GetComponent<Rigidbody>().useGravity=true;
		joint15.GetComponent<Rigidbody>().isKinematic=false;joint15.GetComponent<Rigidbody>().useGravity=true;
		joint18.GetComponent<Rigidbody>().isKinematic=false;joint18.GetComponent<Rigidbody>().useGravity=true;
		joint12.GetComponent<Rigidbody>().isKinematic=false;joint12.GetComponent<Rigidbody>().useGravity=true;
		joint16.GetComponent<Rigidbody>().isKinematic=false;joint16.GetComponent<Rigidbody>().useGravity=true;
	}

}//x



