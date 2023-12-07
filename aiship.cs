using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiship : MonoBehaviour {
	GameObject target;
	public AudioSource gunshot;
	public AudioSource hit;
	public GameObject cannonball;
	public int range = 200;
	public float speed=3.12f;
	public float turn=1.0f;
	private float drag=0.1f;
	public int team;
	public unitcontrol Unitcontrol;
	static int idle=0; static int shooting=1; static int attacking=2; private static int move=4;
	//[HideInInspector]
	public int state=idle;private int variance; private bool acting=false;   private int chosendir=0;
	private float timeline=0.0f; private int dice;  private bool going=false; private bool zooting=false;
	public Transform port1R;public Transform port2R;public Transform port3R;public Transform port4R;public Transform port5R;
	public Transform port1L;public Transform port2L;public Transform port3L;public Transform port4L;public Transform port5L;
	private float chosentime1;private float chosentime2;private float chosentime3;private float chosentime4;
	private float chosentime5;  public GameObject smoke; public GameObject smoke2; public GameObject smoke3;
	// Use this for initialization
	void Start () {
		state=idle;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0.0f)return;
		if(state==idle)
			DetectEnemies();
		if(state==attacking)
			Attack();
		if(Unitcontrol.Unit=="ship"){
		if(Unitcontrol.health<100 && !Unitcontrol.dead)
			smoke.GetComponent<ParticleEmitter>().emit=true;
		if(Unitcontrol.health<50 && !Unitcontrol.dead)
			smoke2.GetComponent<ParticleEmitter>().emit=true;
		if( Unitcontrol.health<25 && !Unitcontrol.dead)
			smoke3.GetComponent<ParticleEmitter>().emit=true;
		}
		if(state==move){
			Move(); DetectEnemies();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))chosendir=1;if(Input.GetKeyDown(KeyCode.LeftArrow))chosendir=2;
		if(Input.GetKeyDown(KeyCode.UpArrow))chosendir=3;if(Input.GetKeyDown(KeyCode.DownArrow))chosendir=4;
	}

	public void Attack(){
		if(target==null){acting=false;state=idle;return;}
		if(target.GetComponent<unitcontrol>().dead){target=null;acting=false;state=idle;return;}

		if(Vector3.Distance(transform.position,target.transform.position)>Unitcontrol.reach || going)
		{  acting=false; zooting=false;
			if(Unitcontrol.Unit=="ship" && Vector3.Angle(transform.forward,target.transform.position-transform.position)<45)
			rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
			else if(Unitcontrol.Unit!="ship")
				rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
			 variance=Random.Range(15,60); dice=0;
		RotateTo(target);going=true;  if(Vector3.Distance(transform.position,target.transform.position)<Unitcontrol.reach-8)going=false;
		}
		else /*if(Vector3.Distance(transform.position,target.transform.position)<=Unitcontrol.reach)*/{  //in range
			if(dice<60)dice+=1; if(dice>=variance && Vector3.Angle(transform.right,target.transform.position-transform.position)<15 &&
			                       Unitcontrol.Unit=="ship")
				Shoot();
			if(dice>=variance && Vector3.Angle(transform.forward,target.transform.position-transform.position)<15 &&
			   Unitcontrol.Unit=="sub")
				Shoot();
			if(dice>=variance && Unitcontrol.Unit=="battleship")
				Shoot();
			if(Unitcontrol.Unit=="ship")
				RotateToSide(target);
			if(!zooting && Unitcontrol.Unit=="sub")
				RotateTo(target);
		}


	}//

	void Shoot(){
		if(timeline==0){chosentime1=Random.Range(0.0f,1.5f);chosentime2=Random.Range(0.0f,1.5f);chosentime3=Random.Range(0.0f,1.5f);
			chosentime4=Random.Range(0.0f,1.5f);chosentime5=Random.Range(0.0f,1.5f);acting=true;}
		if(SignedAngle(transform.forward,target.transform.position-transform.position)>0 && Unitcontrol.Unit=="ship"){
		if(timeline>=chosentime1 && timeline<=chosentime1+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port1R.transform.position+port1R.transform.forward*5,port1R.transform.rotation);gunshot.Play();}
			if(timeline>=chosentime2 && timeline<=chosentime2+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port2R.transform.position+port2R.transform.forward*5,port2R.transform.rotation);gunshot.Play();}		
			if(timeline>=chosentime3 && timeline<=chosentime3+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port3R.transform.position+port3R.transform.forward*5,port3R.transform.rotation);gunshot.Play();}
			if(timeline>=chosentime4 && timeline<=chosentime4+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port4R.transform.position+port4R.transform.forward*5,port4R.transform.rotation);gunshot.Play();}
			if(timeline>=chosentime5 && timeline<=chosentime5+Time.deltaTime*1.1 && port5R!=null){	 
				Instantiate(cannonball,port5R.transform.position+port5R.transform.forward*5,port5R.transform.rotation);gunshot.Play();}
			  rigidbody.constraints=RigidbodyConstraints.FreezeRotation;
		}
		if(SignedAngle(transform.forward,target.transform.position-transform.position)<0 && Unitcontrol.Unit=="ship"){
			if(timeline>=chosentime1 && timeline<=chosentime1+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port1L.transform.position+port1L.transform.forward*5,port1L.transform.rotation);gunshot.Play();}
			if(timeline>=chosentime2 && timeline<=chosentime2+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port2L.transform.position+port2L.transform.forward*5,port2L.transform.rotation);gunshot.Play();}		
			if(timeline>=chosentime3 && timeline<=chosentime3+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port3L.transform.position+port3L.transform.forward*5,port3L.transform.rotation);gunshot.Play();}
			if(timeline>=chosentime4 && timeline<=chosentime4+Time.deltaTime*1.1){	 
				Instantiate(cannonball,port4L.transform.position+port4L.transform.forward*5,port4L.transform.rotation);gunshot.Play();}
			if(timeline>=chosentime5 && timeline<=chosentime5+Time.deltaTime*1.1 && port5L!=null){	 
				Instantiate(cannonball,port5L.transform.position+port5L.transform.forward*5,port5L.transform.rotation);gunshot.Play();}
			rigidbody.constraints=RigidbodyConstraints.FreezeRotation;
		}
		if(timeline>=0.1 && timeline<=0.1+Time.deltaTime*1.1){ //ship or sub fire projectile
			if(Unitcontrol.Unit=="battleship"){
				var missile=Instantiate(cannonball,transform.position+transform.up*24.6f+transform.forward*3.5f,Quaternion.LookRotation(Vector3.up)) as GameObject;
				missile.GetComponent<missile>().target=target;}
			if(Unitcontrol.Unit=="sub"){
				var water=GameObject.Find("water");
				if(water!=null){
				var missile=Instantiate(cannonball,transform.position+transform.forward*34.4f,Quaternion.LookRotation(transform.forward)) as GameObject;
					missile.transform.position=new Vector3(missile.transform.position.x,water.transform.position.y-1.7f,missile.transform.position.z);} }
		}
		if(timeline>=1.0f && timeline<=5.0f)
			zooting=true;
		if(timeline>5.0f)
			zooting=false;
		if(timeline>=7.0f && Unitcontrol.Unit=="ship"){
			acting=false;timeline=0;return;
		}
		if(timeline>=12.0f && Unitcontrol.Unit!="ship"){
			acting=false;timeline=0;return;
		}
		timeline+=Time.deltaTime;
	}

	 void RotateToSide(GameObject enemy){
		
		var pos = enemy.transform.position;//y pos same as player's
		//var dir = pos - transform.position; 
		float singleStep;
		var dir  = Vector3.Cross(enemy.transform.up,transform.position-pos); //right of enemy
			singleStep = turn * Time.deltaTime;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		
	}

	void RotateTo(GameObject enemy){
		
		var pos = enemy.transform.position;//y pos same as player's
		var dir = pos - transform.position; float singleStep;
			singleStep = turn * Time.deltaTime;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		
	}

void DetectEnemies(){
	List<GameObject> units = new List<GameObject>();
	GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
	foreach( GameObject item in items){  //add filter to make sure unit is in player's side
		if(item.GetComponent<aiship>()!=null){
			if(item.GetComponent<aiship>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range+1 && 
			   item.GetComponent<unitcontrol>().dead==false )
				units.Add(item);
		}
		/*else if(item.GetComponent<vehicleai>()!=null){
			if(item.GetComponent<vehicleai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range+1 && 
			   item.GetComponent<unitcontrol>().dead==false )
				units.Add(item);
		}*/
	}
	if(units.Count>0){
		int dice = Random.Range(0,units.Count);
		target=units[dice];
			state=attacking;going=true;  variance=Random.Range(0,10);
	}
}

float SignedAngle(Vector3 a,Vector3 b){

		// angle in [0,180]               n is the normal of the angle
		var n=Vector3.up;//var n=Vector3.Cross(a, b);
		var angle = Vector3.Angle(a,b);
		var sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		// angle in [0,360] (not used but included here for completeness)
		//float angle360 =  (signed_angle + 180) % 360;
		return signed_angle;
}

	void OnCollisionEnter(Collision other){  
		if(Unitcontrol.Unit=="ship"){
		if(other.gameObject.name=="cannonball" && other.rigidbody.velocity.magnitude>1.0f)
			hit.Play();
		if(other.gameObject.name=="cannonball" && Unitcontrol.health<100 && !Unitcontrol.dead)
			smoke.GetComponent<ParticleEmitter>().emit=true;
		if(other.gameObject.name=="cannonball" && Unitcontrol.health<50 && !Unitcontrol.dead)
			smoke2.GetComponent<ParticleEmitter>().emit=true;
		if(other.gameObject.name=="cannonball" && Unitcontrol.health<25 && !Unitcontrol.dead)
				smoke3.GetComponent<ParticleEmitter>().emit=true;}
		if(Unitcontrol.Unit!="ship"){
		if(other.gameObject.tag=="projectile1" && Unitcontrol.health<200 && !Unitcontrol.dead){
				var fumes=Instantiate(smoke,other.transform.position,Quaternion.identity) as GameObject;
				fumes.transform.parent=transform;}
		}
	}

	void Move(){
		var dir = Vector3.zero; float singleStep; if(team==1)dir=Vector3.right;if(team==2)dir=-Vector3.right;
		if(Application.loadedLevelName=="battleships"){if(global.team==1)dir=-Vector3.right;else dir=Vector3.right;}
		if(chosendir==1)dir=Vector3.right;if(chosendir==2)dir=-Vector3.right;if(chosendir==3)dir=Vector3.forward;if(chosendir==4)dir=-Vector3.forward;
		singleStep = turn * Time.deltaTime;  if(dir==Vector3.zero)Debug.Log("zero vector on ship "+name);
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
		if(Input.GetKeyDown(KeyCode.H) || global.stop){state=idle;chosendir=0;}

	}

}//x
