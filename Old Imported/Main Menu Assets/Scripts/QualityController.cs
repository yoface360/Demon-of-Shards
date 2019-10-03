using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityController : MonoBehaviour
{

    [SerializeField]
    GameObject qualityMenu, selectionMenu;

    public void BackToSelections()
    {
        StartCoroutine(ReturnAudio());
    }

    IEnumerator ReturnAudio()
    {
        yield return new WaitForSeconds(.5f);
        qualityMenu.SetActive(false);
        selectionMenu.SetActive(true);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
