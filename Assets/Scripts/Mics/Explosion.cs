using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  public float radius = 5.0F;
  public float power = 10.0F;
  // Start is called before the first frame update
  void Awake()
  {
    StartCoroutine(ExampleCoroutine());
  }
  void Update()
  {

  }

  void explosion()
  {
    Vector3 explosionPos = transform.position;
    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    foreach (Collider hit in colliders)
    {
      Rigidbody rb = hit.GetComponent<Rigidbody>();

      if (rb != null)
        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
    }
  }
  IEnumerator ExampleCoroutine()
  {
    explosion();
    yield return new WaitForSeconds(.1f);
    StartCoroutine(ExampleCoroutine());
  }
}
