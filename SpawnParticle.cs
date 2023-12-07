using UnityEngine;
using System.Collections;

public class SpawnParticle : MonoBehaviour {

	public GameObject particle;
	// Use this for initialization
	void Start () {
		Instantiate(particle,transform.position,transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
