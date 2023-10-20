#pragma strict
private var cam:Transform;

function Start () {
cam=GameObject.Find("Main Camera").transform;
}

function Update () {
//var vector=cam.transform.position-transform.position;
transform.LookAt(cam,transform.forward);
}