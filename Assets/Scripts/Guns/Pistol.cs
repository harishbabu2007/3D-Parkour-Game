using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
  // Start is called before the first frame update
  public override void Shoot()
  {
    base.Shoot();
    ShowFlash();
  }
}
