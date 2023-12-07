#pragma strict
public var time=2;
private var timer=0;

function Start () {

}

function Update () {

timer+=1;
if(timer>=time)
 Destroy(gameObject);

}