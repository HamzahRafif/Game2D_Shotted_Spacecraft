using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OpenLevel()
    {
        SceneManager.LoadScene("PickLevel1");
    }
    public void OpenStage1()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ToLevel2()
    {
        SceneManager.LoadScene("PickLevel2");
    }

    public void OpenStage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}