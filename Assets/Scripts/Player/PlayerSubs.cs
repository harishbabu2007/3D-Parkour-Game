using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubs : MonoBehaviour
{
  // Start is called before the first frame update
  private AudioSource source;
  public AudioClip clip;
  public string subtitles;

  void Start()
  {
    source = GetComponent<AudioSource>();
    StartCoroutine(PlaySound());
  }

  IEnumerator PlaySound()
  {
    yield return new WaitForSeconds(2);
    GameObject.FindGameObjectWithTag("Subtitles").GetComponent<Subtitles>().startSubtitles(subtitles, clip.length);
    source.PlayOneShot(clip);
  }

}
