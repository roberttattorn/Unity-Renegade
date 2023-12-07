using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerplane : MonoBehaviour {
	bool acting=false;
	//public float speed=3.12f;
	//public float turn=1.0f;
	public GameObject missile;
	static int idle=0; static int attacking=1; static int dead=3; static int shooting=4; static int targeting=2; static int aiming=5;
	int state=idle;  
	private float timeline=0.0f;
	public int team;
	public unitcontrol Unitcontrol;
	private GameObject reticule;
	public int range=600;
	[HideInInspector]
	public GameObject target;  private int aimtimeout=0;
	// Use this for initialization
	void Start () {
		reticule=Camera.main.transform.Find("reticule").gameObject;
	}
	void OnDisable(){
		if(reticule!=null)reticule.SetActive(false);
	}
	void OnEnable(){
		state=idle; rigidbody.useGravity=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1) && !acting)
		{state=aiming;reticule.SetActive(true);LockedOnTarget();}
		if(state==aiming )
		{ }

		if(Input.GetButtonDown("Fire") && !acting)
		{Shoot();state=shooting;}
		if(state==shooting)
			Shoot();

		if(target!=null)
		{   
			Vector2 targetonscreen = Camera.main.WorldToScreenPoint(target.transform.position);
			Ray ray = Camera.main.ScreenPointToRay(targetonscreen);
			reticule.transform.position=Camera.main.transform.position+ray.direction*7.6f;
			reticule.transform.rotation=Quaternion.LookRotation(Camera.main.transform.forward);
			if(target.GetComponent<unitcontrol>().dead)
			{target=null;reticule.SetActive(false);}
		}
	
	}

	public void Shoot(){
		if(timeline==0){
			var m=Instantiate(missile,transform.position+transform.forward*5+transform.right*5,Quaternion.LookRotation(transform.forward)) as GameObject;
			if(target!=null)
			m.GetComponent<missile>().target=target; acting=true;
		}
		if(timeline>=7.0f)
		{timeline=0.0f;acting=false;state=idle;return;}
		timeline+=Time.deltaTime;
	}

	public void LockedOnTarget(){
		List<GameObject> units = new List<GameObject>();  
		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){  //add filter to make sure unit is in player's side
			if(item.GetComponent<aiplane>()!=null){
				if(item.GetComponent<aiplane>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range && 
				   item.GetComponent<unitcontrol>().dead==false  && Vector3.Angle(transform.forward,item.transform.position-transform.position)<45)
				{units.Add(item);}
			}
			else if(item.GetComponent<vehicleai>()!=null){
				if(item.GetComponent<vehicleai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range && 
				   item.GetComponent<unitcontrol>().dead==false  && Vector3.Angle(transform.forward,item.transform.position-transform.position)<45)
				{units.Add(item);}
			}
			else if(item.GetComponent<ai>()!=null){
				if(item.GetComponent<ai>().team!=team && Vector3.Distance(transform.position,item.transform.position)<range && 
				   item.GetComponent<unitcontrol>().dead==false && Vector3.Angle(transform.forward,item.transform.position-transform.position)<45)
				{units.Add(item);}}
		}
		if(units.Count!=0)
		{
			int[] unitdist=new int[units.Count]; int i=0; GameObject[] distunit= new GameObject[units.Count];
			foreach(GameObject unit in units){
				var pos=Camera.main.WorldToScreenPoint(unit.transform.position);
				unitdist[i]=(int)Vector2.Distance(pos,new Vector2(Screen.width/2,Screen.height/2));
				distunit[i]=unit;
				i+=1;
			}   var nearest=0;
			for(i=0;i<units.Count-1;i++){
				if(unitdist[i+1]<unitdist[i])
					nearest=i+1;
			}
			target=distunit[nearest];
		}//units
	}
}//x
