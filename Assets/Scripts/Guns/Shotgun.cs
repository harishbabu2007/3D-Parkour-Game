using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
  public override void Shoot()
  {
    base.Shoot();
    ShowFlash();
  }
}
