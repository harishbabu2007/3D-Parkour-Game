using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
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
  public override void AfterRaycast(RaycastHit hit, GameObject bulletEffect)
  {
    base.AfterRaycast(hit, bulletEffect);
  }
}
