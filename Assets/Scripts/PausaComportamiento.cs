using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaComportamiento : MenuComportamiento {

    public static bool estaPausado;
    public GameObject pausaMenu;

    void Start ()
    {
        estaPausado = false;
        pausaMenu.SetActive(false);
    }

	void Update ()
    {
        if (Input.GetKeyUp("escape"))
        {
            estaPausado = !estaPausado;
            Time.timeScale = (estaPausado) ? 0 : 1;
            pausaMenu.SetActive(estaPausado);
        }
    }

    public void ReanudarJuego()
    {
        estaPausado = false;
        pausaMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ReanudarJuego();
    }
}
