using UnityEngine;
using System.Collections;

public class global : MonoBehaviour {
	private static int easy=0;
	private static int  hard=1;
	private static int free=0;
	private static int controlling=1;

	public static float musicVolume=0.5f;
	public static int difficulty=easy;
	public static bool autosave=true;
	public static bool isPlaying=false;
	public static int mode=free;
	public static GameObject controlled;
	public static int team=1;  //team player is on
	public static int chosenteam=1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
