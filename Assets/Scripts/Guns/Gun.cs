using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  private Animator animator;
  public Transform cam, firePoint;
  public GameObject muzzleFlash, impactEffect;
  public float range;
  public SlowMotion slowMotion;
  public TrailRenderer bulletTrail;
  void Start()
  {
    animator = GetComponent<Animator>();
    cam = Camera.main.GetComponent<Transform>();
    slowMotion = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMotion>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Shoot();
    }
  }

  public virtual void Shoot()
  {
    animator.SetTrigger("Fire");
    ShowFlash();

    RaycastHit hit;
    if (Physics.Raycast(cam.position, cam.forward, out hit, range))
    {
      TrailRenderer trail = Instantiate(bulletTrail, firePoint.position, Quaternion.identity);
      StartCoroutine(SpawnTrail(trail, hit));

      BeforeRaycast();

      if (hit.collider.gameObject.tag == "Enemy")
      {
        slowMotion.StartSlowMotion();
      }

      StartCoroutine(waiter());
    }
  }

  private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
  {
    float time = 0;
    Vector3 startposition = trail.transform.position;

    while (time < 1)
    {
      trail.transform.position = Vector3.Lerp(startposition, hit.point, time);
      time += Time.deltaTime / trail.time;

      yield return null;
    }
    trail.transform.position = hit.point;
    GameObject bulletEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    AfterRaycast(hit, bulletEffect);

    if (hit.collider.gameObject.tag == "Enemy")
    {
      if (hit.collider.gameObject.GetComponent<Enemy>() != null)
      {
        hit.collider.gameObject.GetComponent<Enemy>().Die();
      }
    }

    Destroy(trail.gameObject, trail.time);
  }

  IEnumerator waiter()
  {
    yield return new WaitForSeconds(.1f);
    slowMotion.StopSlowMotion();
  }

  public void ShowFlash()
  {
    muzzleFlash.GetComponent<Transform>().position = firePoint.position;
    muzzleFlash.GetComponent<ParticleSystem>().Play();
  }

  public virtual void BeforeRaycast() { }

  public virtual void AfterRaycast(RaycastHit hit, GameObject bulletEffect) { }
}
