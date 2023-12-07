using UnityEngine;
using System.Collections;

public class missile : MonoBehaviour {
	public GameObject target;
	public float speed=1.2f;
	public float turn=2.0f;
	public GameObject explosion;
	private int time=900;
	[HideInInspector]
	public Vector3 coords;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {    if(Time.timeScale==0.0f)return;

		if(target!=null)
		{ RotateTo(target.transform);  // first rotate them move rigidbody otherwise object wont move
			if(Vector3.Angle(transform.forward,target.transform.position-transform.position)>45)
				rigidbody.MovePosition(transform.position+transform.forward*speed/3);
			else
			rigidbody.MovePosition(transform.position+transform.forward*speed);
			if(Vector3.Angle(transform.forward,target.transform.position-transform.position)>90) //give up chase
				target=null;
		}
		else if(coords!=Vector3.zero){ 
			RotateToPoint(coords);
			if(Vector3.Angle(transform.forward,coords-transform.position)>45)
				rigidbody.MovePosition(transform.position+transform.forward*speed/3);
			else
				rigidbody.MovePosition(transform.position+transform.forward*speed);
		}
		else 
			rigidbody.MovePosition(transform.position+transform.forward*speed);
		if(time>0)
			time--;
		if(time<=0)
			Destroy(gameObject);
	}

	void RotateTo(Transform enemy){
		var pos = enemy.position;//y pos same as player's
		var dir = pos - transform.position; float singleStep;
		singleStep = turn * Time.deltaTime;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
	}
	void RotateToPoint(Vector3 enemy){
		//var pos = enemy.transform.position;//y pos same as player's
		var dir = enemy - transform.position; float singleStep;
		singleStep = turn * Time.deltaTime;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Player" || other.gameObject.tag=="Untagged")
		{Instantiate(explosion,transform.position,Quaternion.identity);
			if(other.gameObject.tag=="Player")
				other.gameObject.GetComponent<unitcontrol>().health-=100;
			Destroy(gameObject);}
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name=="water")
			speed=0.3f;
	}

}
