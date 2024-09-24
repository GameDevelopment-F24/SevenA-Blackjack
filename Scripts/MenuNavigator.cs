using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour
{
    public void changeScene(string sceneName) {
        if (sceneName == "Menu") {
            SceneManager.LoadScene(0);
        }
        else if (sceneName == "Game") {
            SceneManager.LoadScene(1);
        }
        else if (sceneName == "Skin") {
            SceneManager.LoadScene(2);
        }
        else if (sceneName == "Details") {
            SceneManager.LoadScene(3);
        }
    }
    
}
