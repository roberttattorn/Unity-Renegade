using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

	public GameObject muzzleflash;
	public GameObject dirt;
	public int force=1;
	[HideInInspector]
	public int dangerous=0;
	public int time=150;
	public bool isGrapeshot;
	public AudioSource ground;
	public AudioSource hit;
	public string Name;
	// Use this for initialization
	void Start () {
		name=Name;
		Instantiate(muzzleflash,transform.position,transform.rotation);
		if(!isGrapeshot)
		rigidbody.AddForce(transform.forward*force+Vector3.up*0.5f,ForceMode.Impulse);
		else
		{	var vert=Random.Range(-0.2f,0.2f); var horz=Random.Range(-0.3f,0.3f);
			rigidbody.AddForce((transform.forward+transform.right*horz+transform.up*vert)*force,ForceMode.Impulse);}

	}
	
	// Update is called once per frame
	void Update () {
	   if(dangerous>0)
			dangerous-=1;
		time-=1;
		if(time<=0)
			Destroy(gameObject);
		if(time==149)
			dangerous=90;
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Player" && rigidbody.velocity.magnitude>0.5f){
			if(!isGrapeshot){
				if(other.gameObject.GetComponent<unitcontrol>().Unit=="ship")
					other.gameObject.GetComponent<unitcontrol>().health-=10;
					else
				other.gameObject.GetComponent<unitcontrol>().health-=300;}
		else
				other.gameObject.GetComponent<unitcontrol>().health-=100;hit.Play();dangerous=0;
			if(other.gameObject.GetComponent<unitcontrol>().Unit=="ship")
			{time=60;Instantiate(dirt,transform.position,Quaternion.identity);}}
		if(other.gameObject.name=="Terrain" || other.gameObject.name=="Building")
		{time=60;Instantiate(dirt,transform.position,Quaternion.identity);if(!isGrapeshot)ground.Play();}
	}//

}










