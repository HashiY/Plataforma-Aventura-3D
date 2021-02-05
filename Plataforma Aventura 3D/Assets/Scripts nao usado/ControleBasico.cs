﻿using UnityEngine;
using System.Collections;
//nao usado
public class ControleBasico : MonoBehaviour {

	private float velocidade = 1.0f;
	private float giro = 180.0f;
	private float gravidade = 3.5f;
	private float pulo = 5.0f;
	private CharacterController objetoCharControler;
	private Vector3 vetorDirecao = new Vector3(0,0,0); //direçao da gravidade para baixo

	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); //pega
	}

	void Update (){ 
		if (Input.GetKey(KeyCode.LeftShift)) { velocidade = 2.5f; } else{velocidade = 5;}

		Vector3 forward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade; // propria direçao * v
		transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * giro *Time.deltaTime,0));// muda de direçao no y

		objetoCharControler.Move(forward * Time.deltaTime); // para nao ficar muito rapido
		objetoCharControler.SimpleMove(Physics.gravity); // para ter a gravidade

		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
		} 
		vetorDirecao.y -= gravidade * Time.deltaTime;    // para cair depois do pulo
		objetoCharControler.Move(vetorDirecao * Time.deltaTime); // mover com o vetor de direçao
	}
}
