using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManagerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI waveTimerText;

    public void UpdateWaveText(string waveText)
    {
        this.waveText.text = waveText;
    }

    public void UpdateWaveTimerText(string waveTimerText)
    {
        this.waveTimerText.text = waveTimerText;
    }
}
