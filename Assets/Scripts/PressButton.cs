using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("Car");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    public void AgainPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
