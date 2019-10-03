using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    [SerializeField]
    GameObject resolutionMenu, selectionMenu;

    Resolution[] resolutions;

    [SerializeField]
    Dropdown resolutionDropdown;

    public void BackToSelections()
    {
        StartCoroutine(ReturnAudio());
    }

    IEnumerator ReturnAudio()
    {
        yield return new WaitForSeconds(.5f);
        resolutionMenu.SetActive(false);
        selectionMenu.SetActive(true);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
