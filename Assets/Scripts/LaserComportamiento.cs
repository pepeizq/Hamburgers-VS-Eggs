using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserComportamiento : MonoBehaviour {

    public float vidaTiempo = 3.0f;
    public float velocidad = 50.0f;
    public int daño = 1;

    void Start () {
        Destroy(gameObject, vidaTiempo);
    }

	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * velocidad);
    }
}
