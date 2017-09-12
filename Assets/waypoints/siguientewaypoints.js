#pragma strict
var velocidad : float;
var objetivo : Transform;
var vel_rotacion : float;
function Start () {
//objetivo = GameObject.Find ("punto1").transform;
}

function Update () {
	gameObject.transform.Translate (Vector3.forward * Time.deltaTime * velocidad);
	
	var rotation = Quaternion.LookRotation(objetivo.transform.position - gameObject.transform.position);
	gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * vel_rotacion);
}
