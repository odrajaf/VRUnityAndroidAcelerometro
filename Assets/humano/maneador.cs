using UnityEngine;
using System.Collections;

public class maneador : MonoBehaviour
{

	private Animator animacion;
	private bool inicioSalto;
	private bool bajandoSalto;
	public float alturaDeSalto = 0.05f;

	private float incSaltando;
	private bool andando;

	// Use this for initialization
	void Start()
	{
		animacion = GetComponent<Animator>();
		inicioSalto = false;
		bajandoSalto = false;
		incSaltando = 0;

		andando = false;

	}

	void paraSaltar(){
		animacion.SetFloat ("saltaRun",0f);
	}

	// Update is called once per frame
	void Update()
	{
		
		if (Input.GetAxis("Vertical") > 0.0f) { //hacia delante (w)

			animacion.SetFloat ("anda",1.0f);
			andando = true;

			if (Input.GetAxis ("Vertical") > 0.9) {
				transform.Translate (0, 0, 2.5f* Time.deltaTime);
			} else {
				transform.Translate (0, 0, 1f* Time.deltaTime);
			}


		}

		if (Input.GetAxis("Vertical") < 0.1 &&  Input.GetAxis("Vertical") > -0.1) { //Deja de andar hacia delante (w)

			animacion.SetFloat ("anda",0f);
			andando = false;


		}

		if (Input.GetButtonDown("Jump"))
		{

			if (!andando) {
				animacion.SetBool ("salta", true);
				inicioSalto = true;
				incSaltando = 0;
			} else {
				animacion.SetFloat ("saltaRun",1f);
				Invoke ("paraSaltar", 0.3f);
			}

		}

		if (incSaltando < 0.265f && inicioSalto)
		{
			incSaltando = incSaltando + alturaDeSalto;
			transform.Translate(0, incSaltando, 0);

		}

		if (incSaltando >= 0.265f && inicioSalto)
		{
			animacion.SetBool("salta", false);
			incSaltando = -1f;
			inicioSalto = false;
			bajandoSalto = true;
		}

		if (bajandoSalto)
		{
			incSaltando = incSaltando + alturaDeSalto;
			transform.Translate(0, -0.9f * alturaDeSalto, 0);

		}
		if (incSaltando >= 0.0f && bajandoSalto)
		{
			bajandoSalto = false;
		}
	}
}
