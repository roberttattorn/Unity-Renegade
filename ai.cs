using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ai : MonoBehaviour {
	public int team;
	public int range=30;
	public float speed=3.0f;
	private int idle=0; private static int attacking=1; private static int guarding=2; private static int dead=3; 
	[HideInInspector]
	public int state;
	public AudioSource whip;
	public AudioSource metal;
	public AudioSource wood;
	public AudioSource hit;
	public AudioSource footsteps;
	[HideInInspector]
	public GameObject target;
	[HideInInspector]
	public bool damaging=false;
	private float timeline=0.0f;
	bool acting=false;
	private int damagetime=0;
	[HideInInspector]
	public bool slashing=false;
	private float guardtime=0.0f;
	// Use this for initialization
	void Start () {
		state=idle;
		team=GetComponent<unitcontrol>().team;
	}

	void OnEnable(){   //function executes when script is re enables
		animation.Play("idlewithshield");
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0.0f)return;
	if(state==idle){
		if(!animation.IsPlaying("idlewithshield"))
				animation.Play("idlewithshield");animation["idle"].speed=0.3f;
			DetectEnemies();
		}

    if(state==attacking)
			Attack();
	if(state==guarding)
			Guard();

	
	}//

	public void DetectEnemies(){
		List<GameObject> units = new List<GameObject>();
		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){  //add filter to make sure unit is in player's side
			if(item.GetComponent<ai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range+1)
			units.Add(item);}
		if(units.Count>0){
			int dice = Random.Range(0,units.Count);
			target=units[dice];
			state=attacking;
	  }
	}

	public void Attack(){
		if(target.GetComponent<unitcontrol>().dead){target=null;state=idle;return;}
		if(Vector3.Distance(transform.position,target.transform.position)>1.5)
		{rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
			if(!animation.IsPlaying("runwithshield"))
			animation.Play("runwithshield");if(!footsteps.isPlaying)footsteps.Play();}
		else{
			Melee();  footsteps.Stop();
		}
		if(!acting)
		RotateTo(target);
	}

	public void Melee(){

		if(!slashing && !acting){
			if(target.GetComponent<ai>().enabled)
			{if(target.GetComponent<ai>().slashing && Random.Range(0,3)==1)
				{state=guarding;acting=true;}
			}
			if(target.GetComponent<player>().enabled)
			{if(target.GetComponent<player>().slashing && Random.Range(0,3)==1)
				{state=guarding;acting=true;}
			}

			if(Random.Range(0,20)==1 && !slashing)
				slashing=true;
		}
			if(slashing){
		if(timeline==0)
		{acting=true;whip.Play();
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
		if(timeline>0.1 && timeline<0.5)
			{damaging=true;DamageEnemy();}
		if(timeline>=0.5)
			damaging=false;
		if(timeline>0.7)
			{acting=false;timeline=0;slashing=false;return;}

			timeline+=Time.deltaTime;}
	}

	void RotateTo(GameObject enemy){
			
		var pos = enemy.transform.position;//y pos same as player's
				var dir = pos - transform.position;
				var singleStep = 4.5f * Time.deltaTime;
				var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
				transform.rotation = Quaternion.LookRotation(Direction);
		
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

	void Guard(){
		 guardtime+=Time.deltaTime;
		animation.Play("guard"); animation["guard"].speed=0.0f;
		if(guardtime>1.0)
		{guardtime=0;acting=false;state=attacking;}
	}

	void FixedUpdate(){
		if(damagetime>0)
			damagetime--;
	}

	void DamageEnemy(){
		if(target!=null && damaging && damagetime==0){
			if(Vector3.Distance(transform.position,target.transform.position)<1.5){
				if(target.GetComponent<ai>()!=null){
					if(target.GetComponent<ai>().slashing==false && target.GetComponent<ai>().state!=guarding )
					{

						hit.Play();target.GetComponent<unitcontrol>().health-=20;
						damagetime=7;
					}
				}
			}//
		}
	}

}//x














