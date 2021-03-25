using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private bool isMute;
    private AudioSource _audioSource;
    [SerializeField] private Image on;
    [SerializeField] private Image off;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void pressSp()
    {
        if (isMute)
        {
            isMute = false;
            _audioSource.mute = false;
            off.gameObject.SetActive(false);
            on.gameObject.SetActive(true);
        }
        else
        {
            isMute = true;
            _audioSource.mute = true;
            off.gameObject.SetActive(true);
            on.gameObject.SetActive(false);
        }
    }
}
