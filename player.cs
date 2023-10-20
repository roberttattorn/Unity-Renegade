using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {
	public AudioSource whip;
	public AudioSource metal;
	public AudioSource wood;
	public AudioSource hit;
	public ThirdPersonController thirdpersoncontroller;
	bool acting=false;
	static int idle=0; static int attacking=1; static int guarding=2;  static int dead=3;
	int state=idle;
	private float timeline=0.0f;
	[HideInInspector]
	public bool damaging=false;
	public int team;
	[HideInInspector]
	public bool slashing=false;
	private int damagetime=0;
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Attack") && !acting)
			state=attacking;
		
		if(state==attacking)
			Attack ();
		if(Input.GetButton("Guard") && !acting)
			state=guarding;
		if(state==guarding)
			Guard();
		if(Input.GetButtonUp("Guard") && state==guarding)
		    {state=idle;acting=false;}
	}

	public void Attack(){
		if(timeline==0)
		{ acting=true; thirdpersoncontroller.enabled=false; slashing=true;
			int dice=Random.Range(0,3);
			switch (dice){
			case 0:
				animation.Play("attack");animation["attack"].speed=1.0f;
				break;
			case 1:
				animation.Play("attack2");animation["attack2"].speed=1.0f;
				break;
			case 2:
				animation.Play("attack3");animation["attack3"].speed=1.0f;
				break;
			}
		}
		if(animation["attack"].normalizedTime>=0.99)animation.CrossFade("idlewithshield");
		if(animation["attack2"].normalizedTime>=0.99)animation.CrossFade("idlewithshield");
		if(animation["attack3"].normalizedTime>=0.99)animation.CrossFade("idlewithshield");
		if(timeline>=0.1 && timeline<=0.1+Time.deltaTime*1.1)
			whip.Play();
		if(timeline>0.1 && timeline<0.5)
		{damaging=true;DamageEnemy();}
		if(timeline>=0.5)
			damaging=false;
		if(timeline>0.7)
		{acting=false;state=idle;timeline=0; thirdpersoncontroller.enabled=true;slashing=false;return;}
		timeline+=Time.deltaTime;
	}

	public void Guard(){
		acting=true;
	
			animation.Play("guard"); animation["guard"].speed=0.0f;
	}

	void OnCollisionStay(Collision other){
		if(other.gameObject.tag=="Player"){
			if(other.gameObject.GetComponent<unitcontrol>().team!=team && other.gameObject.GetComponent<unitcontrol>().damaging &&
			   damagetime==0  )
			{
				if(damaging && Vector3.Angle(transform.forward,other.transform.position-transform.position)<45)
					metal.Play();
				else if(state==guarding && Vector3.Angle(transform.forward,other.transform.position-transform.position)<45)
					wood.Play();
				else
				{hit.Play();GetComponent<unitcontrol>().health-=20;}
				damagetime=7;
			}
		}
	}
	
	void FixedUpdate(){
		if(damagetime>0)
			damagetime--;
	}

	void DamageEnemy(){
		List<GameObject> enemiess = new List<GameObject>();
		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){
			if(item.GetComponent<unitcontrol>().team!=team){
				if(damaging && damagetime==0){
					if(Vector3.Distance(transform.position,item.transform.position)<1.5){
						if(item.GetComponent<ai>()!=null){
							if(item.GetComponent<ai>().slashing==false && item.GetComponent<ai>().state!=guarding )
							{
								
								hit.Play();item.GetComponent<unitcontrol>().health-=20;
								damagetime=7;
							}
						}
					}//
				}
			}
		}

			
		
	}//

}//x
