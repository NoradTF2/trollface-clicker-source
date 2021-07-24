using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPresets : MonoBehaviour
{
    public Clicker clicker;
    public void HandleInputData(int val)
    {
        clicker.soundPreset = val;

        FindObjectOfType<SoundManager>().Stop("normalBGM");
        FindObjectOfType<SoundManager>().Stop("2000BGM");
        FindObjectOfType<SoundManager>().Stop("ironyBGM");
        switch (val)
        {
            case 0:
                {
                    FindObjectOfType<SoundManager>().Play("normalBGM");
                    break;
                }
            case 1:
                {
                    FindObjectOfType<SoundManager>().Play("2000BGM");
                    break;
                }
            case 2:
                {
                    FindObjectOfType<SoundManager>().Play("ironyBGM");
                    break;
                }
        }
    }
}
