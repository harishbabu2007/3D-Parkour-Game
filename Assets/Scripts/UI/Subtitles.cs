using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles : MonoBehaviour
{
  TextMeshProUGUI textG;
  public void startSubtitles(string text, float time)
  {
    textG = GetComponent<TextMeshProUGUI>();
    StartCoroutine(StartSound(text, time));
  }

  IEnumerator StartSound(string text, float time)
  {
    textG.text = text;
    yield return new WaitForSeconds(time);
    textG.text = "";
  }
}
