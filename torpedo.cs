using UnityEngine;
using System.Collections;

public class torpedo : MonoBehaviour {
	public float speed=0.25f;
	public GameObject explosion;
	public GameObject watersplash;
	private int time=2800;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {   if(Time.timeScale==0.0f)return;

		rigidbody.MovePosition(transform.position+transform.forward*speed);
		if(time>0)
			time--;
		if(time<=0)
			Destroy(gameObject);
	
	}
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Player" || other.gameObject.tag=="Untagged")
		{Instantiate(explosion,transform.position,Quaternion.identity); Instantiate(watersplash,transform.position,Quaternion.identity);
			if(other.gameObject.tag=="Player")
				other.gameObject.GetComponent<unitcontrol>().health-=200;
			Destroy(gameObject);}
	}
}
