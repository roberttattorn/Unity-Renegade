using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour {
	private bool stop=false;
	public float speed=1.0f;
	private float time;
	private int damage=50;
	private int timeout=0;
	private int guarding=2;
	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	 if(!stop){
			transform.Translate(transform.forward*speed-Vector3.up*0.02f,Space.World);}
		time+=Time.deltaTime;
		if(time>60.0f)
			Destroy(gameObject);

		if(timeout<2)
			timeout++;
		if(timeout==1 && speed==1.2f)
			damage=100;
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Player" && !stop){ 
			if(other.gameObject.GetComponent<ai>()!=null){
if(other.gameObject.GetComponent<ai>().state==guarding && 
			   Vector3.Angle(other.transform.forward,transform.position-other.transform.position)<60)
			{}
			else{
				int dice=2; 
			if(damage==50) dice=3;
			if(Random.Range(0,dice)==1)
			other.gameObject.GetComponent<unitcontrol>().health-=50;
		else
					other.gameObject.GetComponent<unitcontrol>().health-=100;}   
			}
			else if(other.gameObject.GetComponent<vehicleai>()!=null && other.gameObject.GetComponent<unitcontrol>().Unit=="cannon"){

					int dice=2; 
					if(damage==50) dice=3;
					if(Random.Range(0,dice)==1)
						other.gameObject.GetComponent<unitcontrol>().health-=50;
					else
						other.gameObject.GetComponent<unitcontrol>().health-=100;
			}
		}
		if(other.gameObject.tag=="Player" || other.gameObject.tag=="Untagged" )
		{stop=true;transform.parent=other.transform;}
	}
}
