using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoHaciaJugador : MonoBehaviour {

    private Transform jugador;
    public float velocidad = 2.0f;

    void Start ()
    {
        jugador = GameObject.Find("NaveJugador").transform;
    }

	void Update ()
    {
        Vector3 delta = jugador.position - transform.position;
        delta.Normalize();

        float movimientoVelocidad = velocidad * Time.deltaTime;
        transform.position = transform.position + (delta * movimientoVelocidad);
    }
}
