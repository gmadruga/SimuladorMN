using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adiciona forças de atrito em cada uma das direções separadamente

/* 
 * Ao retirarmos o atrito das direções horizontais, possibilitamos uma locomoção
 * mas natural do barco pela água. Ele não deve mais freiar bruscamente quando sua
 * aceleração for interrompida, mas perderá velocidade paulatinamente até parar.
 * 
 * o atrito maior na direção vertical impedirá o barco de "pular" sempre que encostar 
 * na superfície da água 
 * 
 */

public class ExtraDrag : MonoBehaviour
{
	public float x = 0;
	public float y = 10;
	public float z = 0;
	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float waveYPos = WaterController.current.GetWaveYPos(transform.position, Time.time);

		if ( transform.position.y < waveYPos + 0.3f )
    	{

			DirectionalDrag(x,y,z);
		}
	}

	void DirectionalDrag(float x_drag ,float y_drag ,float z_drag)
	{

		Vector3 dragforce;

		dragforce = new Vector3 ( -rb.velocity.x * x_drag,
			-rb.velocity.y * y_drag,
			-rb.velocity.z * z_drag);
		
		rb.AddRelativeForce( dragforce * 1000 );
	}
}