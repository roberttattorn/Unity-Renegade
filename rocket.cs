using UnityEngine;
using System.Collections;

public class rocket : MonoBehaviour {
	public string type="nlaw";
	[HideInInspector]
	public GameObject target;
	private float speed=2.2f;
	public GameObject explosion;
	private float time=0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(type=="nlaw" && target!=null){
			if(Vector3.Angle(transform.forward,target.transform.position-transform.position)<80)
			RotateTo(target);
		}
		rigidbody.MovePosition(transform.position+transform.forward*speed);
		time+=Time.deltaTime;
		if(time>=6.0f)
			Destroy(gameObject);
	}

	void RotateTo(GameObject taget){
		var pos = taget.transform.position;//y pos same as player's
		var dir = pos - transform.position;
		var singleStep = 4.5f * Time.deltaTime;
		var Direction = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
		transform.rotation = Quaternion.LookRotation(Direction);
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Untagged" || other.gameObject.tag=="Player" || other.gameObject.tag=="unit")
		{Instantiate(explosion,transform.position,Quaternion.identity);
			if(other.gameObject.tag=="Player")
				other.gameObject.GetComponent<unitcontrol>().health-=100;
			Destroy(gameObject);}
	}

}




