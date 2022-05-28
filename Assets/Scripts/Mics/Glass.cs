using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
  public GameObject glassBroken;

  public void GlassBreak()
  {
    GameObject ins = Instantiate(glassBroken, transform.position, Quaternion.identity);
    // ins.transform.localScale = transform.localScale;

    Destroy(this.gameObject);
  }
}
