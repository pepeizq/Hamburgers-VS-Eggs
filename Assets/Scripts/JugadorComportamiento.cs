using System.Collections.Generic;
using UnityEngine;

public class JugadorComportamiento : MonoBehaviour {

    public float jugadorVelocidad = 4.0f;
    private float actualVelocidad = 0.0f;
    private Vector3 ultimoMovimiento = new Vector3();

    public Transform laser;
    public float laserDistancia = .2f;
    public float tiempoEntreDisparos = .3f;
    private float tiempoHastaSiguienteDisparo = 0.0f;

    public List<KeyCode> botonesDisparo;

    void Update()
    {
        if (!PausaComportamiento.estaPausado)
        {
            Rotacion();

            Movimiento();

            foreach (KeyCode boton in botonesDisparo)
            {
                if (Input.GetKey(boton) && tiempoHastaSiguienteDisparo < 0)
                {
                    tiempoHastaSiguienteDisparo = tiempoEntreDisparos;
                    DispararLaser();
                    break;
                }
            }

            tiempoHastaSiguienteDisparo -= Time.deltaTime;
        }         
    }

    void Rotacion()
    {
        Vector3 posicionMundo = Input.mousePosition;
        posicionMundo = Camera.main.ScreenToWorldPoint(posicionMundo);

        float dx = this.transform.position.x - posicionMundo.x;
        float dy = this.transform.position.y - posicionMundo.y;

        float angulo = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Quaternion rotacion = Quaternion.Euler(new Vector3(0, 0, angulo + 90));

        this.transform.rotation = rotacion;
    }

    void Movimiento()
    {
        Vector3 movimiento = new Vector3();

        movimiento.x += Input.GetAxis("Horizontal");
        movimiento.y += Input.GetAxis("Vertical");

        movimiento.Normalize();

        if (movimiento.magnitude > 0)
        {
            actualVelocidad = jugadorVelocidad;
            this.transform.Translate(movimiento * Time.deltaTime * jugadorVelocidad, Space.World);
            ultimoMovimiento = movimiento;
        }
        else
        {
            this.transform.Translate(ultimoMovimiento * Time.deltaTime * actualVelocidad, Space.World);
            actualVelocidad *= .9f;
        }
    }

    void DispararLaser()
    {
        Vector3 laserPosicion = this.transform.position;
        float rotacionAngulo = transform.localEulerAngles.z - 90;

        laserPosicion.x += (Mathf.Cos((rotacionAngulo) * Mathf.Deg2Rad) * -laserDistancia);
        laserPosicion.y += (Mathf.Sin((rotacionAngulo) * Mathf.Deg2Rad) * -laserDistancia);

        Instantiate(laser, laserPosicion, this.transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.name.Contains("Enemigo"))
        {
            JuegoControlador controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<JuegoControlador>();
            controlador.BajarVida(1);
            Destroy(colision.gameObject);
        }
    }
}
