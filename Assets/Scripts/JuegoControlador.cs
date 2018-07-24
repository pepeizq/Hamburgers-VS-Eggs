using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JuegoControlador : MonoBehaviour {

    public Transform enemigo;

    public int enemigosPorOleada = 10;
    private int actualNumeroEnemigos = 0;

    private int puntuacion = 0;
    private int numeroOleada = 0;
    private int vidasJugador = 3;

    public Text puntuacionTexto;
    public Text oleadaTexto;
    public Text vidasJugadorTexto;

    public GameObject pausaMenu;

    void Start ()
    {
        StartCoroutine(GenerarEnemigos());
    }

    IEnumerator GenerarEnemigos()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            if (actualNumeroEnemigos <= 0)
            {
                numeroOleada++;
                oleadaTexto.text = "Wave: " + numeroOleada;
                enemigosPorOleada += 2;
                yield return new WaitForSeconds(5);

                for (int i = 0; i < enemigosPorOleada; i++)
                {
                    float aleatoriaDistancia = Random.Range(20, 35);
                    Vector2 aleatoriaDireccion = Random.insideUnitCircle;
                    Vector3 enemigoPosicion = this.transform.position;

                    enemigoPosicion.x += aleatoriaDireccion.x * aleatoriaDistancia;
                    enemigoPosicion.y += aleatoriaDireccion.y * aleatoriaDistancia;

                    Instantiate(enemigo, enemigoPosicion, this.transform.rotation);
                    actualNumeroEnemigos++;
                }
            }
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
            int puntuacionPrevia = PlayerPrefs.GetInt("puntuacion");

            if (puntuacion > puntuacionPrevia)
            {
                PlayerPrefs.SetInt("puntuacion", puntuacion);
            }

            SceneManager.LoadScene("Menu");
        }
    }
}
