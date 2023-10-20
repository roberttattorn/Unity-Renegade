using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {
	public int Health;
	public GameObject healthbar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 if(transform.parent!=null){
			Health=transform.parent.GetComponent<unitcontrol>().health;
			healthbar.GetComponent<MeshRenderer>().material.color=new Color(100/Health,Health/100,8f);
			transform.position=transform.parent.transform.position;
		}
		else
			transform.position=Vector3.zero-Vector3.up*100;
	}
}
