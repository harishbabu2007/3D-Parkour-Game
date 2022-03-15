using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
  public override void AfterRaycast(RaycastHit hit, GameObject bulletEffect)
  {
    base.AfterRaycast(hit, bulletEffect);
    firePoint.GetComponent<Explosion>().Explode();
  }
}
