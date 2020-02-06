using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            backToMainMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();
        }
    }

    public void reloadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void setActiveObject(GameObject _gameObject)
    {
        if (!_gameObject.activeSelf)
            _gameObject.SetActive(true);
        else
        {
            _gameObject.SetActive(false);
        }
    }
}
