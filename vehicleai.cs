using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vehicleai : MonoBehaviour {
	GameObject target;
	public AudioSource gunshot;
	public GameObject cannonball;
	public GameObject grapeshot;
	public float speed=1.12f;
	public float turn=1.0f;
	public GameObject man1;
	public GameObject man2;
	public Transform wheels1;
	public Transform wheels2;
	private float velocity;
	private Vector3 posprev;
	public int team=1;
	public int range=85;
	public unitcontrol Unitcontrol;
	static int idle=0; static int shooting=1; static int attacking=2;  private static int move=4;
	[HideInInspector]
	public int state=idle; private int variance; private bool acting=false;  private int chosendir=0;
	private float timeline=0.0f; private int dice; private int ammo=45;
	public Transform turret;  public int damage=100;
	// Use this for initialization
	void Start () {
		if(man1!=null)
		{man1.animation.Play("idle");man1.animation["idle"].speed=0.3f;man2.animation.Play("idle");man2.animation["idle"].speed=0.3f;}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0.0f)return;

		if(state==idle)
			DetectEnemies();
		if(state==attacking)
			Attack();
		if(state==move){
			Move(); DetectEnemies();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))chosendir=1;if(Input.GetKeyDown(KeyCode.LeftArrow))chosendir=2;
		if(Input.GetKeyDown(KeyCode.UpArrow))chosendir=3;if(Input.GetKeyDown(KeyCode.DownArrow))chosendir=4;
	}

	void Attack(){
		if(target==null){acting=false;state=idle;return;}
		if(target.GetComponent<unitcontrol>().dead){target=null;acting=false;state=idle;return;}
		if(Vector3.Distance(transform.position,target.transform.position)>Unitcontrol.reach && !acting)
		{ if(Unitcontrol.Unit=="cannon")
			rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
			else
				rigidbody.AddForce(transform.forward*speed*350000*Time.deltaTime);
			if(man1!=null){
			if(!man2.animation.IsPlaying("walk"))
				man2.animation.Play("walk");
				if(!man1.animation.IsPlaying("walk"))
					man1.animation.Play("walk");} variance=Random.Range(15,60); dice=0;
			if(wheels1!=null)
			wheels1.Rotate(rigidbody.velocity.magnitude*8,0,0);
			}
		else if(Vector3.Distance(transform.position,target.transform.position)<=Unitcontrol.reach){  //in range
			if(dice<60)dice+=1; if(dice>=variance && Vector3.Angle(transform.forward,target.transform.position-transform.position)<10)
					Shoot();
			if(Unitcontrol.Unit=="cannon"){
			if(!man2.animation.IsPlaying("idle"))
				man2.animation.Play("idle");
			if(!man1.animation.IsPlaying("idle") && !acting)
					man1.animation.Play("idle");}
			}
			

		if(!acting)
			RotateTo(target);
		if(Unitcontrol.Unit!="cannon")
			RotateTurret(target);
	}//

	void Shoot(){
		if(timeline==0){
			acting=true; 
			if(Unitcontrol.Unit=="tank")
			Instantiate(cannonball,transform.position+turret.transform.forward*12+Vector3.up*3,turret.transform.rotation);
			else if(Unitcontrol.Unit=="apc")
				Instantiate(cannonball,turret.transform.position+turret.transform.forward*9,turret.transform.rotation);
			else
				Instantiate(cannonball,transform.position+transform.forward*3+Vector3.up,transform.rotation);
			gunshot.Play();  rigidbody.constraints=RigidbodyConstraints.FreezeRotation;
		}
		if(timeline>=0.15f && timeline<=0.15f+Time.deltaTime*1.1 && Unitcontrol.Unit=="apc" && ammo>0){
			Lazer();ammo-=1;timeline=0;return;
		}
		if(timeline>=0.6f && timeline<=0.6f+Time.deltaTime*1.1 && Unitcontrol.Unit=="cannon"){
			man1.animation.Play("loadcannon");man1.animation["loadcannon"].speed=0.3f;
		}
		if(timeline>=5.0f && timeline<=5.0f+Time.deltaTime*1.1)
		{if(Unitcontrol.Unit=="cannon")man1.animation.Play("idle");acting=false;rigidbody.constraints=RigidbodyConstraints.None;}
		if(timeline>=7.0f){
			timeline=0;ammo=45;return;
		}
			timeline+=Time.deltaTime;
	}

	void RotateTo(GameObject enemy){
		
		var pos = enemy.transform.position;//y pos same as player's
		var dir = pos - transform.position; float singleStep;
		if(Unitcontrol.Unit=="cannon")
			 singleStep = turn * Time.deltaTime;
			else
		 singleStep = turn * Time.deltaTime*rigidbody.velocity.magnitude;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		
	}

	void RotateTurret(GameObject enemy){
		var pos = enemy.transform.position;//y pos same as player's
		var dir = pos - transform.position; float singleStep;
			singleStep = 1.0f * Time.deltaTime;
		var Direction = Vector3.RotateTowards(turret.transform.forward, dir, singleStep, 0.0f);
		turret.transform.rotation = Quaternion.LookRotation(Direction);
		if(turret.localEulerAngles.x>20f && turret.localEulerAngles.x<180)
			turret.localEulerAngles=new Vector3(20f,turret.localEulerAngles.y,turret.localEulerAngles.z);
		if(turret.localEulerAngles.x>180f && turret.localEulerAngles.x<340)
			turret.localEulerAngles=new Vector3(340f,turret.localEulerAngles.y,turret.localEulerAngles.z);
	}

	void DetectEnemies(){
		List<GameObject> units = new List<GameObject>();
		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){  //add filter to make sure unit is in player's side
			if(item.GetComponent<ai>()!=null){
			if(item.GetComponent<ai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range+1 && 
			   item.GetComponent<unitcontrol>().dead==false )
				units.Add(item);
			}
			else if(item.GetComponent<vehicleai>()!=null){
				if(item.GetComponent<vehicleai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range+1 && 
				   item.GetComponent<unitcontrol>().dead==false )
					units.Add(item);
			}
		}
		if(units.Count>0){
			int dice = Random.Range(0,units.Count);
			target=units[dice];
			state=attacking;  variance=Random.Range(0,10);
		}
	}

	void Lazer(){
		RaycastHit hit;
		//ray shooting out of the camera from where the mouse is
		var aim = turret.transform.forward;
		if (Physics.Raycast(turret.transform.position+turret.transform.forward*9,aim,out hit,155))
			
		{
			if(hit.collider.tag=="Player"){ 
				if(hit.collider.GetComponent<unitcontrol>().armoured){
					if(Random.Range(0,2)==1)
						hit.collider.gameObject.GetComponent<unitcontrol>().health-=damage/20;
					else
						hit.collider.gameObject.GetComponent<unitcontrol>().health-=damage/10;
				}
				else{ var dice=Random.Range(0,3);
					if(dice==1)
						hit.collider.gameObject.GetComponent<unitcontrol>().health-=damage/2;
					else if(dice==0)
						hit.collider.gameObject.GetComponent<unitcontrol>().health-=damage;
				}
			}
		}
	}

	void Move(){
		float singleStep;
		if(Unitcontrol.Unit=="cannon")
			singleStep = turn * Time.deltaTime;
		else
			singleStep = turn * Time.deltaTime*rigidbody.velocity.magnitude; Vector3 dir=Vector3.zero;
		if(team==1)dir=Vector3.right;if(team==2)dir=-Vector3.right;
		if(chosendir==1)dir=Vector3.right;if(chosendir==2)dir=-Vector3.right;if(chosendir==3)dir=Vector3.forward;if(chosendir==4)dir=-Vector3.forward;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		if(Unitcontrol.Unit=="cannon")
			rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
		else
			rigidbody.AddForce(transform.forward*speed*350000*Time.deltaTime);
		if(man1!=null){
			if(!man2.animation.IsPlaying("walk"))
				man2.animation.Play("walk");
			if(!man1.animation.IsPlaying("walk"))
				man1.animation.Play("walk");}
		if(Input.GetKeyDown(KeyCode.H) || global.stop){state=idle;chosendir=0;}
	}

}
