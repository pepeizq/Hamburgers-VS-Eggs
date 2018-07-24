using UnityEngine;

public class EnemigoComportamiento : MonoBehaviour {

    public int vida = 1;

    public Transform explosion;

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.name.Contains("Laser"))
        {
            LaserComportamiento laser = colision.gameObject.GetComponent("LaserComportamiento") as LaserComportamiento;
            vida -= laser.daño;
            Destroy(colision.gameObject);

            if (vida <= 1)
            {
                Destroy(this.gameObject);

                JuegoControlador controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<JuegoControlador>();
                controlador.EnemigoMuerto();
                controlador.IncrementarPuntuacion(10);

                if (explosion)
                {
                    GameObject explotar = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                    Destroy(explotar, 2.0f);
                }
            }
        }       
    }
}
