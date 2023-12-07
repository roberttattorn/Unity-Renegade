using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {
	public GameObject gunsmoke;
	public int time=7;
	public ParticleEmitter emitter1;
	public ParticleEmitter emitter2;
	public ParticleEmitter spark;
	private int timeout=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	  if(time>0)
			time-=1;
		if(time==1)
		{if(emitter1!=null){emitter1.emit=false;emitter2.emit=false;spark.emit=false;}timeout+=1;
			Instantiate(gunsmoke,transform.position,Quaternion.identity);}
		if(timeout>0)
			timeout++;
		if(timeout>=60)
			Destroy(gameObject);
	}
}
