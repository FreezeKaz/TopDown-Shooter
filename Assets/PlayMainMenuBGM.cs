using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMainMenuBGM : MonoBehaviour
{
    void Start()
    {
        SoundFXManager.instance.StopBGM();
        SoundFXManager.instance.PlayBGM(0);
    }


}
