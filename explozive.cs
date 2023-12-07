using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class explozive : MonoBehaviour {
	public int damageradius=4;
	public int maxdamage=400;
	// Use this for initialization
	void Start () {

		GameObject[] items = GameObject.FindGameObjectsWithTag("Player");
		foreach( GameObject item in items){  //add filter to make sure unit is in player's side
			if(item.GetComponent<unitcontrol>()!=null){
		   if(Vector3.Distance(transform.position,item.transform.position)<=damageradius && item.GetComponent<unitcontrol>().dead==false 
				   && !item.GetComponent<unitcontrol>().armoured)
				{ int damage=damageradius*80/200;  damage=Mathf.Clamp(damage,1,maxdamage);
					item.GetComponent<unitcontrol>().health-=damage;}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
