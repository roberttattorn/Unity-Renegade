using UnityEngine;
using System.Collections;

public class playership : MonoBehaviour {
	public AudioSource gunshot;
	public AudioSource hit;
	public GameObject cannonball;
	public float speed=3000.12f;
	public float turn=1000.0f;
	private bool acting;
	private float timeline;
	static int idle=0; static int shooting=1; 
	int state=idle;    GameObject reticule; private bool targeting=false;  public GameObject targetbeam;
	public unitcontrol Unitcontrol;  public int damage=100; private int timeout=0;
	public Transform port1R;public Transform port2R;public Transform port3R;public Transform port4R;public Transform port5R;
	public Transform port1L;public Transform port2L;public Transform port3L;public Transform port4L;public Transform port5L;
	private float chosentime1;private float chosentime2;private float chosentime3;private float chosentime4;
	private float chosentime5;  public GameObject smoke; public GameObject smoke2; public GameObject smoke3;
	// Use this for initialization
	void Start () {
		reticule=GameObject.Find("targetbeam");
	}

	void OnEnable(){
		if(Unitcontrol.crosshairs!=null)
			Unitcontrol.crosshairs.SetActive(true);
		reticule=GameObject.Find("targetbeam");
		if(reticule==null){
			reticule=Instantiate(targetbeam,Vector3.zero-Vector3.up*1000,Quaternion.identity) as GameObject; reticule.name="targetbeam";}
	}
	void OnDisable(){
		targeting=false;if(reticule!=null)reticule.transform.position=Vector3.zero-Vector3.up*1000;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0.0f || Unitcontrol.dead)return;
		var water=GameObject.Find("water");
		if(Input.GetAxis("Horizontal")!=0 )//move object, rtte with arrows animate men
			Rotate();
		 if(Input.GetAxis("Vertical")!=0  && Unitcontrol.Unit!="cannon"){
			rigidbody.AddForce(transform.forward*Input.GetAxis("Vertical")*speed*6000*Time.deltaTime);
		}
		if(Unitcontrol.Unit=="sub"){
		if(Input.GetKey(KeyCode.LeftShift) && water!=null){
			if(transform.position.y<water.transform.position.y)
			rigidbody.MovePosition(transform.position+Vector3.up*0.05f);}
		if(Input.GetKey(KeyCode.LeftControl) && water!=null){
			if(transform.position.y>water.transform.position.y-10.4f)
					rigidbody.MovePosition(transform.position-Vector3.up*0.05f);}  }
		if(Input.GetMouseButtonDown(1)){
			if(targeting)
			{targeting=false;if(reticule!=null)reticule.transform.position=Vector3.zero-Vector3.up*1000;
				GameObject.Find("CamControl").GetComponent<CameraController>().enabled=false;
				Camera.main.gameObject.GetComponent<MouseOrbit>().enabled=true;return;}
			else
			{targeting=true;GameObject.Find("CamControl").GetComponent<CameraController>().enabled=true;}
		}
		if(targeting && reticule!=null){
			reticule.transform.position=MousePoint()+Vector3.up*0.1f;
			Camera.main.gameObject.GetComponent<MouseOrbit>().enabled=false;}
		//turn wheels
		//shoot & reload  shoot graeshot if right click
		if(state==shooting)
			Shoot();
		if(Input.GetButtonDown("Fire") && !acting && state!=shooting)
			Shoot();
		//die if health low put men flat
		//animate man to reload
		//if(timeout<3)timeout++; if(timeout==1 || timeout==2)Unitcontrol.crosshairs.SetActive(true);
	}

	void Rotate(){
		//transform.Rotate(0,Input.GetAxis("Horizontal")*turn,0);
		//Vector3 eulerAngleVelocity = new Vector3(0,Input.GetAxis("Horizontal")*turn, 0);
		//Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
		rigidbody.AddTorque(Vector3.up*Input.GetAxis("Horizontal")*turn*200);
	}

	void Shoot(){
		if(timeline==0){chosentime1=Random.Range(0.0f,1.5f);chosentime2=Random.Range(0.0f,1.5f);chosentime3=Random.Range(0.0f,1.5f);
			chosentime4=Random.Range(0.0f,1.5f);chosentime5=Random.Range(0.0f,1.5f);acting=true;state=shooting;}
		if(SignedAngle(transform.forward,Camera.main.transform.position-transform.position)<0 && Unitcontrol.Unit=="ship"){
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
		if(SignedAngle(transform.forward,Camera.main.transform.position-transform.position)>0 && Unitcontrol.Unit=="ship"){
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
		if(timeline>=0.05 && timeline<=0.05+Time.deltaTime*1.1){ //ship or sub fire projectile
			if(Unitcontrol.Unit=="battleship"){
				var missile=Instantiate(cannonball,transform.position+transform.up*24.6f+transform.forward*3.5f,Quaternion.LookRotation(Vector3.up)) as GameObject;
				if(targeting){
					missile.GetComponent<missile>().coords=MousePoint();targeting=false;
					Camera.main.gameObject.GetComponent<MouseOrbit>().enabled=true;
					GameObject.Find("CamControl").GetComponent<CameraController>().enabled=false;
					reticule.transform.position=Vector3.zero-Vector3.up*1000;}}
			if(Unitcontrol.Unit=="sub"){
				var water=GameObject.Find("water");
				if(water!=null){
					var missile=Instantiate(cannonball,transform.position+transform.forward*34.4f,Quaternion.LookRotation(transform.forward)) as GameObject;
					missile.transform.position=new Vector3(missile.transform.position.x,water.transform.position.y-1.7f,missile.transform.position.z);} }
		}
		if(timeline>1.0f)
			rigidbody.constraints=RigidbodyConstraints.None;
		if(timeline>=7.0f){
			acting=false;timeline=0;state=idle;return;
		}
		timeline+=Time.deltaTime;
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

	Vector3 MousePoint(){
		RaycastHit hit;
		//ray shooting out of the camera from where the mouse is
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out hit)){
			if(hit.collider.tag=="Untagged" || hit.collider.tag=="Player" || hit.collider.tag=="unit")
				return hit.point;
			else
				return Vector3.zero;
		}
		else
			return Vector3.zero;
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.name=="cannonball" && other.rigidbody.velocity.magnitude>1.0f)
			hit.Play();
		if(other.gameObject.name=="cannonball" && Unitcontrol.health<100 && !Unitcontrol.dead)
			smoke.GetComponent<ParticleEmitter>().emit=true;
		if(other.gameObject.name=="cannonball" && Unitcontrol.health<50 && !Unitcontrol.dead)
			smoke2.GetComponent<ParticleEmitter>().emit=true;
		if(other.gameObject.name=="cannonball" && Unitcontrol.health<25 && !Unitcontrol.dead)
			smoke3.GetComponent<ParticleEmitter>().emit=true;
	}

}//






