using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    private const int StartScreenIndex = 0, GameIndex = 1, InstructionsIndex = 3;

    public void PlayGame(){
        SceneManager.LoadSceneAsync(InstructionsIndex);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void EndScreen(){
        SceneManager.LoadSceneAsync(StartScreenIndex);
    }
    public void Instructions(){
        SceneManager.LoadSceneAsync(GameIndex);
    }
}
