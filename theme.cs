using UnityEngine;
using System.Collections;

public class theme : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnLevelWasLoaded(){
		 if(Application.loadedLevelName=="skirmish")
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
