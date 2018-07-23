using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MenuComportamiento : MonoBehaviour {

    public Text puntuacionTexto;

    public void Start()
    {
        if (PlayerPrefs.HasKey("puntuacion"))
        {
            int puntuacion = PlayerPrefs.GetInt("puntuacion");
            puntuacionTexto.text = puntuacion.ToString();
        }

        //Advertisement.Show();
    }

    public void LoadLevel(string nombreNivel)
    {     
        SceneManager.LoadScene(nombreNivel);
    }

    public void QuitGame()
    { 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;        
        #else
        Application.Quit();
        #endif
    }

    public void WebPepe()
    {
        Application.OpenURL("https://pepeizqapps.com/");
    }

    public void WebLucia()
    {
        Application.OpenURL("https://luciacubel.com/");
    }
}
