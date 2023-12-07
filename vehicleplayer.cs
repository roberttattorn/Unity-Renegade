using UnityEngine;
using System.Collections;

public class vehicleplayer : MonoBehaviour {
	public AudioSource gunshot;
	public GameObject cannonball;
	public GameObject grapeshot;
	public float speed=0.12f;
	public float turn=1.0f;
	public GameObject man1;
	public GameObject man2;
	public Transform wheels1;
	public Transform wheels2;
	private float velocity;
	private Vector3 posprev;
	private bool acting;
	private float timeline;
	static int idle=0; static int shooting=1; 
	int state=idle;
	private bool useGrapeshot=false;
	public Transform turret;
	public unitcontrol Unitcontrol; private int ammo=45; public int damage=100; private int timeout=0;
	// Use this for initialization
	void Start () {
		if(Unitcontrol.Unit=="cannon")
		{man1.animation.Play("idle");man1.animation["idle"].speed=0.3f;man2.animation.Play("idle");man2.animation["idle"].speed=0.3f;}
	}

	void OnEnable(){
		if(Unitcontrol.crosshairs!=null)
			Unitcontrol.crosshairs.SetActive(true);
	}
	void OnDisable(){
		timeout=0;
	}
	
	// Update is called once per frame
	void Update () { if(Time.timeScale==0.0f || Unitcontrol.dead)return;
	 if(Input.GetAxis("Horizontal")!=0 && !acting && Unitcontrol.Unit=="cannon")//move object, rtte with arrows animate men
			Rotate();
		else if(Input.GetAxis("Horizontal")!=0 && !acting && Unitcontrol.Unit!="cannon")
			RotateMove();
		if(Input.GetAxis("Vertical")!=0 && !acting && Unitcontrol.Unit=="cannon"){ 
		rigidbody.MovePosition(transform.position+transform.forward*Input.GetAxis("Vertical")*speed*Time.deltaTime);
			if(!man1.animation.IsPlaying("walk"))man1.animation.Play("walk");
			if(!man2.animation.IsPlaying("walk"))man2.animation.Play("walk");}
		else if(Input.GetAxis("Vertical")!=0 && !acting && Unitcontrol.Unit!="cannon"){
			rigidbody.AddForce(transform.forward*Input.GetAxis("Vertical")*speed*350000*Time.deltaTime);
		}
		else if(Input.GetAxis("Vertical")==0 && !acting && Unitcontrol.Unit=="cannon"){
			if(!man1.animation.IsPlaying("idle"))man1.animation.Play("idle");
			if(!man2.animation.IsPlaying("idle"))man2.animation.Play("idle");
		}
		if(wheels1!=null)
		wheels1.Rotate(rigidbody.velocity.magnitude*8,0,0);
		//turn wheels
		//shoot & reload  shoot graeshot if right click
		if(state==shooting)
			Shoot();
		if(Input.GetButtonUp("Fire") && state==shooting && Unitcontrol.Unit!="cannon")
		{state=idle;timeline=0;acting=false;}
		if(Input.GetButtonDown("Fire") && !acting)
			Shoot();
		if(Input.GetMouseButtonDown(1) && !acting && Unitcontrol.Unit=="cannon")
		{useGrapeshot=true; Shoot ();}
		if(Unitcontrol.Unit!="cannon")
		RotateTurret();
		//die if health low put men flat
		//animate man to reload
		if(timeout<3)timeout++; if(timeout==1 || timeout==2)Unitcontrol.crosshairs.SetActive(true);
	}

	void RotateTurret(){
		var singleStep = 1.5f * Time.deltaTime; var dir= Camera.main.transform.forward;
		var Direction = Vector3.RotateTowards(turret.transform.forward, dir, singleStep, 0.0f);
		turret.rotation = Quaternion.LookRotation(Direction);
		if(turret.localEulerAngles.x>20f && turret.localEulerAngles.x<180f)
			turret.localEulerAngles=new Vector3(20f,turret.localEulerAngles.y,turret.localEulerAngles.z);
		if(turret.localEulerAngles.x>180f && turret.localEulerAngles.x<340f)
			turret.localEulerAngles=new Vector3(340f,turret.localEulerAngles.y,turret.localEulerAngles.z);
	}

	void Shoot(){
		if(timeline==0){
			if(useGrapeshot)
			{Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);
				Instantiate(grapeshot,transform.position+transform.forward*3+Vector3.up,transform.rotation);}
				else{
				if(Unitcontrol.Unit=="cannon")
				Instantiate(cannonball,transform.position+transform.forward*3+Vector3.up,transform.rotation);
			else
				{	Instantiate(cannonball,turret.transform.position+turret.transform.forward*12,turret.transform.rotation);
					}
			}
			gunshot.Play(); acting=true; state=shooting;
		}
		if(timeline>=0.15f && timeline<=0.15f+Time.deltaTime*1.1 && Unitcontrol.Unit=="apc" && ammo>0)
		{Lazer();ammo-=1;timeline=0;return;}
		if(timeline>=0.5f && timeline<=0.5f+Time.deltaTime*1.1 && Unitcontrol.Unit=="cannon")
		{man1.animation.Play("loadcannon");}
		if(timeline>=3.5f && timeline<=3.5f+Time.deltaTime*1.1 && Unitcontrol.Unit=="cannon")
			man1.animation.Play("idle");
		if(timeline>=7.0f)
		{acting=false;timeline=0;state=idle;ammo=45;return;}
		timeline+=Time.deltaTime;
	}

	void Rotate(){
		//transform.Rotate(0,Input.GetAxis("Horizontal")*turn,0);
		Vector3 eulerAngleVelocity = new Vector3(0,Input.GetAxis("Horizontal")*turn, 0);
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
	}

	void RotateMove(){
		Vector3 eulerAngleVelocity = new Vector3(0,Input.GetAxis("Horizontal")*turn, 0);
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime*rigidbody.velocity.magnitude);
		rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
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
}
