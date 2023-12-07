#pragma strict
/*private var momentum=0.0;
public var ramp:GameObject;
public var acceleration=50;*/
public var buoyancy=49;
public var sealevel=20;
public var dip=3.5;
//public var torque=6000;

static var controlled=false;

function Start () {

}

function OnEnable(){

}

function OnDisable(){

}

function FixedUpdate () { 


if(transform.position.y<sealevel-dip)
 rigidbody.AddForce(Vector3.up*buoyancy);
 
//transform.eulerAngles.x=0;
//transform.eulerAngles.z=0;
}

function OnTriggerEnter(other:Collider){
 if(other.gameObject.name=="water")
    rigidbody.useGravity=false;
}

function OnTriggerExit(other:Collider){
 if(other.gameObject.name=="water" && transform.position.y>sealevel)
    rigidbody.useGravity=true;
}






