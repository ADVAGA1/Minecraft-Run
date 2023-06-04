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
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        SceneManager.LoadScene("Juego");
    }

    public void HowToPlayClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        currentScene = MenuScene.HOWTOPLAY;
        Menu.SetActive(false);
        HowToPlay.SetActive(true);
        
    }

    public void CreditsClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        currentScene = MenuScene.CREDITS;
        Credits.SetActive(true);
        Menu.SetActive(false);
    }

    public void ExitClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPressed");
        Application.Quit();
    }

    public void GoBack()
    {
        if(currentScene == MenuScene.HOWTOPLAY)
        {
            FindObjectOfType<AudioManager>().Play("ButtonPressed");
            currentScene = MenuScene.MENU;
            HowToPlay.SetActive(false);
            Menu.SetActive(true);
        }
        
        if(currentScene == MenuScene.CREDITS)
        {
            FindObjectOfType<AudioManager>().Play("ButtonPressed");
            currentScene = MenuScene.MENU;
            Credits.SetActive(false);
            Menu.SetActive(true);
        }
    }

}
