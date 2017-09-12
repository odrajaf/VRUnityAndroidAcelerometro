using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SimpleMouseRotator : MonoBehaviour
	{
		// A mouselook behaviour with constraints which operate relative to
		// this gameobject's initial rotation.
		// Only rotates around local X and Y.
		// Works in local coordinates, so if this object is parented
		// to another moving gameobject, its local constraints will
		// operate correctly
		// (Think: looking out the side window of a car, or a gun turret
		// on a moving spaceship with a limited angular range)
		// to have no constraints on an axis, set the rotationRange to 360 or greater.
		public Vector2 rotationRange = new Vector3(70, 70);
		public float rotationSpeed = 10;
		public float dampingTime = 0.2f;
		public bool autoZeroVerticalOnMobile = true;
		public bool autoZeroHorizontalOnMobile = false;
		public bool relative = true;


		private Vector3 m_TargetAngles;
		private Vector3 m_FollowAngles;
		private Vector3 m_FollowVelocity;
		private Quaternion m_OriginalRotation;

		private GameObject PersonajePrincipal;
		private GameObject CamaraPrincipal;
		private GameObject soporteCamara;
		private GameObject limiteCamara;
		private float distanciaPersCamara;
		public float limiteFrontScroll = 0.29f;
		public float limiteBackScroll = 2.9f;

		private Animator animacion;

		public float debug = 0;
		public float debug2 = 0;
		public float anteriorAngulo; //valor inicial pasando del limite de 180 para que se 
											//actualice en la primera ejecucion

		private void Start()
		{
			distanciaPersCamara = 0;
			PersonajePrincipal = GameObject.Find("Koreano");
			CamaraPrincipal = GameObject.Find ("Main Camera");
			soporteCamara =  GameObject.Find ("SoporteCamara");
			limiteCamara = GameObject.Find ("LimiteCamara");

			m_OriginalRotation = transform.localRotation;
			Cursor.visible = false;

			animacion = GetComponent<Animator>();
			anteriorAngulo = 365;

		}

		private void dejarDeGirar(){
			animacion.SetFloat ("rotandoDer", -1);
		}


		private void Update()
		{
			if(Input.GetButtonDown("Cancel")){
				Cursor.visible = true;
			}
			// we make initial calculations from the original local rotation
			transform.localRotation = m_OriginalRotation;

			// read input from mouse or mobile controls
			float inputH;
			float inputV;
			if (relative)
			{
				inputH = Input.GetAxis("Mouse X");
				inputV = Input.GetAxis("Mouse Y");

				// wrap values to avoid springing quickly the wrong way from positive to negative
				if (m_TargetAngles.y > 180)
				{
					m_TargetAngles.y -= 360;
					m_FollowAngles.y -= 360;
				}
				if (m_TargetAngles.x > 180)
				{
					m_TargetAngles.x -= 360;
					m_FollowAngles.x -= 360;
				}
				if (m_TargetAngles.y < -180)
				{
					m_TargetAngles.y += 360;
					m_FollowAngles.y += 360;
				}
				if (m_TargetAngles.x < -180)
				{
					m_TargetAngles.x += 360;
					m_FollowAngles.x += 360;
				}

				#if MOBILE_INPUT
				// on mobile, sometimes we want input mapped directly to tilt value,
				// so it springs back automatically when the look input is released.
				if (autoZeroHorizontalOnMobile) {
				m_TargetAngles.y = Mathf.Lerp (-rotationRange.y * 0.5f, rotationRange.y * 0.5f, inputH * .5f + .5f);
				} else {
				m_TargetAngles.y += inputH * rotationSpeed;
				}
				if (autoZeroVerticalOnMobile) {
				m_TargetAngles.x = Mathf.Lerp (-rotationRange.x * 0.5f, rotationRange.x * 0.5f, inputV * .5f + .5f);
				} else {
				m_TargetAngles.x += inputV * rotationSpeed;
				}
				#else
				// with mouse input, we have direct control with no springback required.
				m_TargetAngles.y += inputH*rotationSpeed;
				m_TargetAngles.x += inputV*rotationSpeed;
				#endif

				// clamp values to allowed range
				m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -rotationRange.y*0.5f, rotationRange.y*0.5f);
				m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -rotationRange.x*0.5f, rotationRange.x*0.5f);
			}
			else
			{
				inputH = Input.mousePosition.x;
				inputV = Input.mousePosition.y;

				// set values to allowed range
				m_TargetAngles.y = Mathf.Lerp(-rotationRange.y*0.5f, rotationRange.y*0.5f, inputH/Screen.width);
				m_TargetAngles.x = Mathf.Lerp(-rotationRange.x*0.5f, rotationRange.x*0.5f, inputV/Screen.height);
			}

			// smoothly interpolate current values to target angles
			m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, dampingTime);

			// update the actual gameobject's rotation
			//transform.localRotation = m_OriginalRotation*Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);

			if (anteriorAngulo > 360) {
				anteriorAngulo = m_FollowAngles.y + 180 + 25;
			}
			debug = m_FollowAngles.y+ 180;
			debug2 = anteriorAngulo +25 ;


			if (m_FollowAngles.y + 180  > anteriorAngulo) {
				anteriorAngulo = m_FollowAngles.y + 180 + 25;
				animacion.SetFloat ("rotandoDer", 1);
				Invoke ("dejarDeGirar", 0.5f);
			
			}
		
			float diferenciaAng = PersonajePrincipal.transform.localEulerAngles.y;

			PersonajePrincipal.transform.Rotate (0, m_FollowAngles.y, 0);
			CamaraPrincipal.transform.localRotation= m_OriginalRotation*Quaternion.Euler(-m_FollowAngles.x,-diferenciaAng, 0);


			float x1 = soporteCamara.transform.position.x;
			float x2 = limiteCamara.transform.position.x;

			float z1 = soporteCamara.transform.position.z;
			float z2 = limiteCamara.transform.position.z;

			distanciaPersCamara = Mathf.Sqrt( Mathf.Pow (x1 - x2, 2) +   Mathf.Pow (z1 - z2, 2));

			float ruletaRaton = Input.GetAxis ("Mouse ScrollWheel");

			if ( (ruletaRaton > 0 && distanciaPersCamara > limiteFrontScroll) || (distanciaPersCamara < limiteBackScroll && ruletaRaton < 0)) {
				soporteCamara.transform.Translate (Vector3.forward * Input.GetAxis ("Mouse ScrollWheel"));
			}
		}
	}
}