using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    Text masterVal, bgmVal, sfxVal;

    [SerializeField]
    AudioMixerGroup masterGroup, bgmGroup, sfxGroup;

    [SerializeField]
    GameObject selectionMenu, volumeMenu;

    public void SetMasterVolume(float volume)
    {
        masterGroup.audioMixer.SetFloat("MasterVolume", volume);
        int val = (int)(100 + (volume * 1.25));
        masterVal.text = val + "%";
    }

    public void SetBGMVolume(float volume)
    {
        bgmGroup.audioMixer.SetFloat("BGMVolume", volume);
        int val = (int)(100 + (volume * 1.25));
        bgmVal.text = val + "%";
    }

    public void SetSFXVolume(float volume)
    {
        sfxGroup.audioMixer.SetFloat("SFXVolume", volume);
        int val = (int)(100 + (volume * 1.25));
        sfxVal.text = val + "%";
    }

    public void BackToSelections()
    {
        StartCoroutine(ReturnAudio());
    }

    IEnumerator ReturnAudio()
    {
        yield return new WaitForSeconds(.5f);
        volumeMenu.SetActive(false);
        selectionMenu.SetActive(true);
    }
}