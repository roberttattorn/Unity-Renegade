using UnityEngine;
using System.Collections;

public class watersplash : MonoBehaviour {
	public float time=0.8f;
		private int timer=200;
	private float timing=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer-=1;
		timing+=Time.deltaTime;
		if(timing>=time)
			GetComponent<ParticleEmitter>().emit=false;
		if(timer<=0)
			Destroy(gameObject);
	}
}
