using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{

    //Animators
    [SerializeField]
    private Animator fadeAnimator, logoAnimator;

    //Menus and backend for making things smooth
    [SerializeField]
    private GameObject mainMenu, optionsMenu, creditsMenu, splashMenu, transitionObj, prevObj;

    //Smart Scene Transitions
    private string nextScene, invProInit;

    //On the start, sets timescale back to 1 for animations to work
    //Plays the fade in from black animation and sets up future transitions
    void Start()
    {
        Time.timeScale = 1.0f;
        fadeAnimator.SetTrigger("FadeStart");
        prevObj = splashMenu;
        transitionObj = mainMenu;
        StartCoroutine(SplashLoad());
    }

    //Plays animations to transition from the Splash Menu to the Main Menu and relocate the logo
    IEnumerator SplashLoad()
    {
        yield return new WaitForSeconds(2.5f);
        prevObj.SetActive(false);
        logoAnimator.SetTrigger("ShrinkStart");
        yield return new WaitForSeconds(1f);
        transitionObj.SetActive(true);
        prevObj = transitionObj;

    }

    //Transitions between menus in the scene disabling the previous menu and enabling the target
    IEnumerator Transition(GameObject transition)
    {
        transitionObj = transition;
        fadeAnimator.SetBool("FadeSwitch", true);
        yield return new WaitForSeconds(1f);
        prevObj.SetActive(false);
        fadeAnimator.SetBool("FadeSwitch", false);
        yield return new WaitForSeconds(0.75f);
        transitionObj.SetActive(true);
        prevObj = transitionObj;
    }

    //Transitions to the level
    IEnumerator LoadLevel()
    {
        fadeAnimator.SetBool("FadeSwitch", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(invProInit, LoadSceneMode.Additive);
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(nextScene);
    }

    #region Button Controls
    //Marks target and starts Transition
    public void CallTransition(GameObject obj)
    {
        StartCoroutine(Transition(obj));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    //DEV MODE STUFF
    //Sets the next scene and calls the transition
    public void NextLevel()
    {
        nextScene = "Test Forest Level";
        invProInit = "InventoryProLoad";
        StartCoroutine(LoadLevel());
    }

}