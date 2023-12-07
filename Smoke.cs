using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour {

	public float time=3.5f;
	private float timer=0.0f;
	public bool dontDestroy=false;
	// Use this for initialization
	void Start () {
	  if(Application.loadedLevelName=="armada")
			time=59.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		if(timer>=3.5)
			GetComponent<ParticleEmitter>().emit=false;
		if(timer>=time && !dontDestroy)
			Destroy(gameObject);
	}
}
