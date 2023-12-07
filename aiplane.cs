using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiplane : MonoBehaviour {
	public GameObject missile;
	public float speed=3.12f;
	public float turn=1.0f;
	public int team=1;
	public int minAltitude=160;
	public int range=600;
	private bool acting;
	private float timeline;
	private GameObject target;
	static int idle=0; static int shooting=2; static int attacking=1; static int move=4; private float climbtime=0.0f;
	int state=idle;    GameObject reticule; private bool targeting=false; private int chosendir=0; private float angularSpeed;
	public unitcontrol Unitcontrol;  private float angleprev;
	// Use this for initialization
	void Start () {
		reticule=Camera.main.transform.Find("reticule").gameObject;
	}
	void OnDisable(){
		targeting=false;if(reticule!=null)reticule.transform.position=Vector3.zero-Vector3.up*1000;
	}
	void  OnEnable(){
		rigidbody.useGravity=false;
	}
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0.0f || Unitcontrol.dead)return;

	  if(state==idle)
		{Rotate();
			rigidbody.MovePosition(transform.position+transform.forward*speed*2/3*Time.deltaTime);
			DetectEnemies();
		}
		if(state==attacking)
			Attack();

		if(state==move){
			Move(); DetectEnemies();
		}
		if(Input.GetKeyDown(KeyCode.RightArrow))chosendir=1;if(Input.GetKeyDown(KeyCode.LeftArrow))chosendir=2;
		if(Input.GetKeyDown(KeyCode.UpArrow))chosendir=3;if(Input.GetKeyDown(KeyCode.DownArrow))chosendir=4;
		angleprev=transform.eulerAngles.y;
	}//x

	void Attack(){
		if(target==null){acting=false;state=idle;return;}
		if(target.GetComponent<unitcontrol>().dead){target=null;acting=false;state=idle;return;}
		if(Vector3.Distance(transform.position,target.transform.position)>Unitcontrol.reach){
		if(target!=null)
				RotateTo(target);}
		if(Vector3.Distance(transform.position,target.transform.position)<=Unitcontrol.reach && !acting)
			Shoot();
		if(Vector3.Distance(transform.position,target.transform.position)<100)
			Rotate ();
		rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);

	}

	void Shoot(){
		if(target!=null){if(Vector3.Angle(transform.forward,target.transform.position-transform.position)>80)return;}
		if(timeline==0){
			var m=Instantiate(missile,transform.position+transform.forward*5+transform.right*5,Quaternion.LookRotation(transform.forward)) as GameObject;
			m.GetComponent<missile>().target=target; acting=true;
		}
		if(timeline>=7.0f)
		{timeline=0.0f;acting=false;return;}
		timeline+=Time.deltaTime;
	}

	void Move(){
		var singleStep = turn * Time.deltaTime; Vector3 direction=Vector3.zero; if(team==1)direction=Vector3.forward;if(team==2)direction=-Vector3.forward;
		if(chosendir==1)direction=Vector3.right;if(chosendir==2)direction=-Vector3.right;
		if(chosendir==3)direction=Vector3.forward;if(chosendir==4)direction=-Vector3.forward;
		var Direction = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		rigidbody.MovePosition(transform.position+transform.forward*speed*3/4*Time.deltaTime);
		if(Input.GetKeyDown(KeyCode.H) || global.stop){state=idle;chosendir=0;}
	}

		void Rotate(){
		 Vector3 dir;
		  if(transform.position.y<minAltitude)
			climbtime=Random.Range(7.0f,14.0f);
		   if(climbtime>0){
			dir=transform.forward+Vector3.up*0.5f; climbtime-=Time.deltaTime;}
		   else
			dir = transform.right;   if(climbtime<0)climbtime=0;
			var singleStep = turn * Time.deltaTime;
			var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
			transform.rotation = Quaternion.LookRotation(Direction);
		var angle=transform.eulerAngles.y;  angularSpeed=angle-angleprev; 
		transform.eulerAngles=new Vector3(transform.eulerAngles.x,angularSpeed*5,transform.eulerAngles.z);
		}

	void RotateTo(GameObject enemy){

		var pos = enemy.transform.position;//y pos same as player's
		Vector3 dir;
		if(transform.position.y<minAltitude)
			climbtime=Random.Range(7.0f,14.0f);
		if(climbtime>0){
			dir=transform.forward+Vector3.up*0.5f; climbtime-=Time.deltaTime;}
		else
			dir = pos - transform.position;   if(climbtime<0)climbtime=0; 
		var singleStep = turn * Time.deltaTime;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
		var angle=transform.eulerAngles.y;  angularSpeed=angle-angleprev; 
		transform.eulerAngles=new Vector3(transform.eulerAngles.x,angularSpeed*5,transform.eulerAngles.z);
	}

	public void DetectEnemies(){
		List<GameObject> units = new List<GameObject>();
		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){  //add filter to make sure unit is in player's side
			if(item.GetComponent<ai>()!=null){
				if((item.GetComponent<ai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range+1 && 
				    item.GetComponent<unitcontrol>().dead==false) )
					units.Add(item);
			}
			else if(item.GetComponent<vehicleai>()!=null){
				if(item.GetComponent<vehicleai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range && 
				   item.GetComponent<unitcontrol>().dead==false )
					units.Add(item);
			}
			else if(item.GetComponent<aiplane>()!=null){
				if(item.GetComponent<aiplane>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range && 
				   item.GetComponent<unitcontrol>().dead==false )
					units.Add(item);
			}
		}
		if(units.Count>0){
			int dice = Random.Range(0,units.Count);
			target=units[dice];
			state=attacking;  
		}
	}
}//x
