using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JuegoControlador : MonoBehaviour {

    public Transform enemigo;

    public float tiempoAntesGeneracionEnemigos = 1.5f;
    public float tiempoEntreEnemigos = .25f;
    public float tiempoEntreOleadas = 2.0f;

    public int enemigosPorOleada = 10;
    private int actualNumeroEnemigos = 0;

    private int puntuacion = 0;
    private int numeroOleada = 0;
    private int vidasJugador = 3;

    public Text puntuacionTexto;
    public Text oleadaTexto;
    public Text vidasJugadorTexto;

    void Start ()
    {
        StartCoroutine(GenerarEnemigos());
    }

    IEnumerator GenerarEnemigos()
    {
        yield return new WaitForSeconds(tiempoAntesGeneracionEnemigos);

        while (true)
        {
            if (actualNumeroEnemigos <= 0)
            {
                numeroOleada++;
                oleadaTexto.text = "Wave: " + numeroOleada;
                enemigosPorOleada += 2;

                for (int i = 0; i < enemigosPorOleada; i++)
                {
                    float aleatoriaDistancia = Random.Range(15, 35);
                    Vector2 aleatoriaDireccion = Random.insideUnitCircle;
                    Vector3 enemigoPosicion = this.transform.position;

                    enemigoPosicion.x += aleatoriaDireccion.x * aleatoriaDistancia;
                    enemigoPosicion.y += aleatoriaDireccion.y * aleatoriaDistancia;

                    Instantiate(enemigo, enemigoPosicion, this.transform.rotation);
                    actualNumeroEnemigos++;
                    yield return new WaitForSeconds(tiempoEntreEnemigos);
                }
            }
            yield return new WaitForSeconds(tiempoEntreOleadas);
        }
    }

    public void EnemigoMuerto()
    {
        actualNumeroEnemigos--;
    }

    public void IncrementarPuntuacion(int incremento)
    {
        puntuacion += incremento;
        puntuacionTexto.text = "Score: " + puntuacion;
    }

    public void BajarVida(int quitar)
    {
        vidasJugador -= quitar;
        vidasJugadorTexto.text = "Lifes: " + vidasJugador;

        if (vidasJugador <= 0)
        {
            PlayerPrefs.SetInt("puntuacion", puntuacion);
            SceneManager.LoadScene("Menu");
        }
    }
}
