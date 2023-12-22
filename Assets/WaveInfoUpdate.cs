using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveInfoUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI text;


    public void Update()
    {
        text.text = "Wave n. " + (WaveGenerator.Instance.TotalWavesCompleted + 1).ToString();
    }
}
