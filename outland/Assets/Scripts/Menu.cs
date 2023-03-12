using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    private const int StartScreenIndex = 0, GameIndex = 1;
    
    public void QuitGame() => Application.Quit();
    public void LoadStartMenu() => SceneManager.LoadSceneAsync(StartScreenIndex);
    public void LoadGame() => SceneManager.LoadSceneAsync(GameIndex);
}
