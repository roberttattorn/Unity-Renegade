using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public GameObject sparks;
	private int time=200;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () { if(Time.timeScale==0.0f)return;
		transform.Translate(transform.forward*3.5f,Space.World);
		time-=1;
		if(time<=0)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Player" || other.gameObject.tag=="Untagged")
		{if(other.gameObject.tag=="Player" && other.gameObject.GetComponent<unitcontrol>()!=null)
			other.gameObject.GetComponent<unitcontrol>().health-=10;
			Instantiate(sparks,transform.position,Quaternion.identity);Destroy(gameObject);}
	}

}
