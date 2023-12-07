#pragma strict
private var xrot;
private var zrot;

function Start () {
 xrot=transform.rotation.x; zrot=transform.rotation.z;
}

function Update () {
  transform.eulerAngles.x=xrot;transform.eulerAngles.z=zrot;
}