using UnityEngine;
using System.Collections;

public class KeepOrientation : MonoBehaviour {
	public float x=90f;
	public float y=0f;
	public float z=0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles=new Vector3(x,y,z);

	}
}
