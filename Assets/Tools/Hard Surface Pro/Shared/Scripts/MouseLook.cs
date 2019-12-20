using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	public float sensitivityX = 5F;
	public float sensitivityY = 5F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -90F;
	public float maximumY = 90F;
	
	public float smoothSpeed = 20F;
	
	float rotationX = 0F;
	float smoothRotationX = 0F;
	float rotationY = 0F;
	float smoothRotationY = 0F;
	Vector3 vMousePos;
	
	bool bActive = false;
	
	void Start()
	{
		vMousePos = Input.mousePosition;
		if (Screen.fullScreen)
			Cursor.visible = false;
	}

	void Update()
	{
		if (Input.GetMouseButton(1))
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			Cursor.visible = false;
			//Screen.lockCursor = true;
			bActive = true;
		}
		else if (bActive || vMousePos != Input.mousePosition)
		{
			Cursor.visible = true;
			//Screen.lockCursor = false;
		}
		
		vMousePos = Input.mousePosition;
		
		// smooth mouse look
		smoothRotationX += (rotationX - smoothRotationX) * smoothSpeed * Time.smoothDeltaTime;
		smoothRotationY += (rotationY - smoothRotationY) * smoothSpeed * Time.smoothDeltaTime;
		
		// transform camera to new direction
		transform.localEulerAngles = new Vector3(-smoothRotationY, smoothRotationX, 0);
		
		// handle camera movement via controller
		Vector3 inputMag = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		Vector3 inputMoveDirection = transform.rotation * inputMag;
		transform.position += inputMoveDirection * 25.0f * Time.smoothDeltaTime;
		
		//transform.position += Vector3.up * (Input.GetAxis("VerticalOffset") * 10.0f * Time.smoothDeltaTime);
		transform.position += (transform.rotation * Vector3.forward) * Input.GetAxis("Mouse ScrollWheel") * 200.0f;
	}
}
