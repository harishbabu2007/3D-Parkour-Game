using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  private Animator animator;
  public Transform cam, firePoint;
  public GameObject muzzleFlash, impactEffect;
  public float range;
  void Start()
  {
    animator = GetComponent<Animator>();
    cam = Camera.main.GetComponent<Transform>();
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
      GameObject bulletEffect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
      AfterRaycast(hit, bulletEffect);
    }
  }

  public void ShowFlash()
  {
    muzzleFlash.GetComponent<Transform>().position = firePoint.position;
    muzzleFlash.GetComponent<ParticleSystem>().Play();
  }

  public virtual void AfterRaycast(RaycastHit hit, GameObject bulletEffect) { }
}
