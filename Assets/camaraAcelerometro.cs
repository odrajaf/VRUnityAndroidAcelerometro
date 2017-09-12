using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraAcelerometro : MonoBehaviour {

		float hRotation = 0.0f;    //Horizontal angle
		float vRotation = 0.0f;   //Vertical rotation angle of the camera
		float cameraMovementSpeed = 0.50f;
		float valorAnterior = 0;
		

		// Use this for initialization
		void Start () {

		}

		void OnGUI() { 
			
			GUI.Label (new Rect (10, 10, 150, 100), "Valor X: " + Input.acceleration.x);
			GUI.Label (new Rect (10, 30, 150, 100), "Valor Y: " + Input.acceleration.y);
			GUI.Label (new Rect (10, 50, 150, 100), "Valor Z: " + Input.acceleration.z);
			/*GUI.Label (new Rect (10, 10, 150, 100), "Valor X: " + Input.GetAxis("Mouse X"));
			GUI.Label (new Rect (10, 30, 150, 100), "Valor Y: " + Input.GetAxis("Mouse Y"));
			GUI.Label (new Rect (10, 50, 150, 100), "Valor Z: " + Input.GetAxis("Mouse Z"));*/

		}

		void Update() {
			float valorZ = -(Input.acceleration.z)* 45;
			/*hRotation -= Input.acceleration.y * cameraMovementSpeed;
			vRotation += Input.acceleration.z * cameraMovementSpeed;
			transform.rotation = Quaternion.Euler(hRotation, vRotation, 0.0f);*/

			//hRotation -= Input.GetAxis("Mouse Y") * cameraMovementSpeed;
			//vRotation += Input.GetAxis("Mouse X") * cameraMovementSpeed;
			
			transform.rotation = Quaternion.Euler((valorZ+ valorAnterior)/2, vRotation, 0.0f);
			valorAnterior = valorZ;
		}
	}
