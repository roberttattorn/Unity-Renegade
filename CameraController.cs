using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float moveSpeed;
	public float zoomSpeed;
	public float minZoomDist;
	public float maxZoomDist;
	public Camera cam;
	public GameObject maincamera;
	private Vector3 prev;
	private Transform follow=null;
	private float heightDamping = 2.0f;
	private float rotationDamping = 3.0f;
	private float x,y;
	public static float dist;
	public static Vector3 pos;
	// Use this for initialization

	void Awake ()
	{
		//cam = Camera.main;
	}
	void Start () {
		x=transform.eulerAngles.y;
		y=transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Zoom();

		/*if (Input.GetMouseButtonDown(2) && follow==null)  //middle click plane to follow it left click to unfollow
		{
			//empty RaycastHit object which raycast puts the hit details into
			  RaycastHit hit;
			//ray shooting out of the camera from where the mouse is
			 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray,out hit))
			{
				//print out the name if the raycast hits something
				
				if(hit.collider.tag=="plane")
					follow=hit.collider.transform;
				   }

	}*///mousebutton(1)
		//if (Input.GetMouseButtonDown(0) && follow!=null)  //stop following
		//	follow=null;
		pos=transform.position;
		if(follow!=null)
		{//transform.LookAt(follow);
			//transform.position=follow.position+Vector3.Normalize(transform.position-follow.position)*12;
		}

	}
	void Move ()
	{  if(Input.GetMouseButton(1) )  //right click
		return;
		float xInput = Input.GetAxisRaw("Horizontal");
		float zInput = Input.GetAxisRaw("Vertical"); 
		Vector3 dir = transform.forward * zInput + transform.right * xInput;
		if(Time.timeScale==0.0f)
		transform.position += dir * moveSpeed * 1; //camera must move even in stopped time
			else
		rigidbody.position += dir * moveSpeed * 1;
		if(Input.mousePosition.x < Screen.width*1/24){
			if(Time.timeScale==1.0f)
			rigidbody.MovePosition(transform.position-transform.right*1.5f);
			else  
				transform.position += -transform.right*1.5f; }
		if(Input.mousePosition.x > Screen.width*23/24){
			if(Time.timeScale==1.0f)
			rigidbody.MovePosition(transform.position+transform.right*1.5f);
			else
				transform.position += transform.right*1.5f;}
		if(Input.mousePosition.y < Screen.height*1/24){
			if(Time.timeScale==1.0f)
			rigidbody.MovePosition(transform.position-transform.forward*1.5f);
				else
				transform.position += -transform.forward*1.5f;}
		if(Input.mousePosition.y > Screen.height*23/24){
			if(Time.timeScale==1.0f)
			rigidbody.MovePosition(transform.position+transform.forward*1.5f);
					else
						transform.position += transform.forward*1.5f;}



	}
	void Zoom ()
	{   if(transform.position.y<3.0f || transform.eulerAngles.x>0)
		{if(transform.eulerAngles.x>0)transform.eulerAngles=new Vector3(0,transform.eulerAngles.y,transform.eulerAngles.z);return;}
		float scrollInput = Input.GetAxis("Mouse ScrollWheel"); 
		float dist = Vector3.Distance(transform.position, cam.transform.position); 
		if(dist < minZoomDist && scrollInput > 0.0f)
			return;
		else if(dist > maxZoomDist && scrollInput < 0.0f)
			return;
		cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
	}

	void LateUpdate(){
		prev=transform.position;
		if(Input.GetMouseButton(1)){ // right mouse look around
			float distance=Vector3.Distance(transform.position,cam.transform.position);
			y=cam.transform.eulerAngles.x;
			x += Input.GetAxis("Mouse X") * 250 * 0.02f;
			var rotation = Quaternion.Euler(y, x, 0);
			var position = rotation * new Vector3(0.0f, 0.0f, -distance) + transform.position;
			
			cam.transform.rotation = rotation;
			cam.transform.position = position;
			transform.eulerAngles=new Vector3(transform.eulerAngles.x,cam.transform.eulerAngles.y,transform.eulerAngles.z);
		}//

		if(Input.GetMouseButtonUp(1)){
			Vector3 vector=cam.transform.position-transform.position;
			/*cam.transform.position=transform.position+Vector3.Normalize(vector)*dist;*/}//
		// Early out if we don't have a target
		if (!follow)
			return;
		
		// Calculate the current rotation angles
		 float wantedRotationAngle = follow.eulerAngles.y+110;
		 float wantedHeight = follow.position.y + 0.1f;//<= height
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = follow.position;
		transform.position -= (currentRotation * Vector3.forward * 16);//<=dist
		
		// Set the height of the camera
		//transform.position = transform.position+Vector3.up*currentHeight;
		
		// Always look at the target
		transform.LookAt (follow);


		//if(transform.rotation.x>0 || transform.rotation.x<0)
		//	transform.rotation=Quaternion.Euler(0,transform.eulerAngles.y,transform.eulerAngles.z);;


	}

	public void FocusOnPosition (Vector3 pos)
	{
		transform.position = pos;
	}
}



