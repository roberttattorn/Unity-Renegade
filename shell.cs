using UnityEngine;
using System.Collections;

public class shell : MonoBehaviour {
	public int force=20;
	public int damage=200;
	public GameObject explosion;
	public GameObject muzzleflash;
	public GameObject smoke;
	// Use this for initialization
	void Start () {
		rigidbody.AddForce(transform.forward*force,ForceMode.Impulse);
		Instantiate(muzzleflash,transform.position,Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Player")
		{other.gameObject.GetComponent<unitcontrol>().health-=damage;Instantiate(explosion,transform.position,Quaternion.identity);
			if(other.gameObject.GetComponent<unitcontrol>().health<=0)
			Instantiate(smoke,transform.position,Quaternion.identity);
			Destroy(gameObject);}
		if(other.gameObject.tag=="Untagged" || other.gameObject.tag=="unit")
		{Instantiate(explosion,transform.position,Quaternion.identity);Destroy(gameObject);}
	}
}
