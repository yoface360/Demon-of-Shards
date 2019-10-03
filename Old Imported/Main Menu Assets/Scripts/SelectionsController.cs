using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionsController : MonoBehaviour
{
    [SerializeField]
    GameObject volumeMenu, resolutionMenu, qualityMenu, selectionMenu;

    public void VolumeSelection()
    {
        StartCoroutine(GoToVolume());
    }

    public void ResolutionSelection()
    {
        StartCoroutine(GoToResolution());
    }

    public void QualitySelection()
    {
        StartCoroutine(GoToQuality());
    }

    IEnumerator GoToQuality()
    {
        yield return new WaitForSeconds(.5f);
        selectionMenu.SetActive(false);
        qualityMenu.SetActive(true);
    }

    IEnumerator GoToResolution()
    {
        yield return new WaitForSeconds(.5f);
        selectionMenu.SetActive(false);
        resolutionMenu.SetActive(true);
    }

    IEnumerator GoToVolume()
    {
        yield return new WaitForSeconds(.5f);
        selectionMenu.SetActive(false);
        volumeMenu.SetActive(true);
    }
}
