using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Animator animator;
  public Transform player;
  public bool isDead = false;
  public float attackDist;
  public Transform firePoint;
  public float range;
  public TrailRenderer bulletTrail;
  public GameObject muzzleFlash;
  private AudioSource source;
  public AudioClip ShootClip;
  public Collider[] col;
  public Rigidbody[] rb;
  public Collider mainCollider;
  private Ray sight;
  private bool isThere = false;

  void Start()
  {
    animator = GetComponent<Animator>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    source = GetComponent<AudioSource>();

    isRagdoll(isDead);
  }

  public void isRagdoll(bool isragdoll)
  {
    if (isragdoll == false)
    {
      for (int i = 0; i < col.Length; i++)
      {
        col[i].enabled = false;
        rb[i].isKinematic = true;
      }

      mainCollider.enabled = true;
      animator.enabled = true;
      GetComponent<Rigidbody>().isKinematic = false;
    }
    else
    {
      animator.enabled = false;
      mainCollider.enabled = false;
      GetComponent<Rigidbody>().isKinematic = true;

      for (int i = 0; i < col.Length; i++)
      {
        rb[i].isKinematic = false;
        col[i].enabled = true;
      }
    }
  }

  void Update()
  {
    if (!isDead)
    {
      float dist = Vector3.Distance(player.position, transform.position);

      if (attackDist >= dist)
      {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        sight.origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        sight.direction = transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(sight, out hit, range) || isThere)
        {
          if (hit.collider.gameObject.CompareTag("Player") || isThere)
          {
            animator.SetTrigger("Shoot");
            isThere = true;
          }
        }
      }
    }
  }

  public void Shoot()
  {
    sight.origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    sight.direction = transform.forward;
    source.PlayOneShot(ShootClip);
    ShowFlash();
    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    RaycastHit hit;
    if (Physics.Raycast(sight, out hit, range))
    {
      TrailRenderer trail = Instantiate(bulletTrail, firePoint.position, Quaternion.identity);

      if (hit.collider.gameObject.tag == "Player")
      {
        PlayerHealth playerHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
        playerHealth.EnemyHit();
      }

      StartCoroutine(SpawnTrail(trail, hit));
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
    Destroy(trail.gameObject, trail.time);
  }

  public void ShowFlash()
  {
    muzzleFlash.GetComponent<Transform>().position = firePoint.position;
    muzzleFlash.GetComponent<ParticleSystem>().Play();
  }

  public void Die()
  {
    isDead = true;
    isRagdoll(isDead);
  }
}