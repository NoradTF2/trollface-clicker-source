using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public int animationTimer;
    public int nextScene;
    private bool alreadyClicked = false;
    public void ButtonAction()
    {
        if (!alreadyClicked)
        {
            Invoke("NextScene", animationTimer);
            alreadyClicked = true;
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
