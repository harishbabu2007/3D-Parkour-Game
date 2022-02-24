using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject Player;
    public float MoveSpeed = 4;
    public float MaxDist = 6;
    public float MinDist = 5;

    public float enemyCooldown = 1;
    public float damage = 2;

    private bool playerInRange = false;
    private bool canAttack = true;
    public float speed = 1;

    public GameObject player;

    private void Update()
    {

        transform.LookAt(Player.transform);

        if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        

        if (playerInRange && canAttack)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().health -= damage;
            StartCoroutine(AttackCooldown());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ;
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ;
        {
            playerInRange = false;
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }
}
