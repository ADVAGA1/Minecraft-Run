using UnityEngine;
using UnityEngine.SceneManagement;

enum MenuScene
{
    MENU,HOWTOPLAY,CREDITS
}

public class MenuScript : MonoBehaviour
{
    private MenuScene currentScene;
    public GameObject Menu, HowToPlay, Credits;

    private void Start()
    {
        currentScene = MenuScene.MENU;
    }

    public void PlayClick()
    {
        SceneManager.LoadScene("Juego");
    }

    public void HowToPlayClick()
    {
        currentScene = MenuScene.HOWTOPLAY;
        Menu.SetActive(false);
        HowToPlay.SetActive(true);
        
    }

    public void CreditsClick()
    {
        currentScene = MenuScene.CREDITS;
        Credits.SetActive(true);
        Menu.SetActive(false);
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        if(currentScene == MenuScene.HOWTOPLAY)
        {
            currentScene = MenuScene.MENU;
            HowToPlay.SetActive(false);
            Menu.SetActive(true);
        }
        
        if(currentScene == MenuScene.CREDITS)
        {
            currentScene = MenuScene.MENU;
            Credits.SetActive(false);
            Menu.SetActive(true);
        }
    }

}
