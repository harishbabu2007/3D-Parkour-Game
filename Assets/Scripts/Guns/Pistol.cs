using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
  private AudioSource source;
  public AudioClip clip;

  private void Awake()
  {
    source = GetComponent<AudioSource>();
  }
  public override void BeforeRaycast()
  {
    source.PlayOneShot(clip);
  }
}
