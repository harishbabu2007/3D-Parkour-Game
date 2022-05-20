using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct guns
{
  public string name;
  public GameObject gunObj;
  public GameObject ThrowObject;
}

public class GunManager : MonoBehaviour
{
  public List<guns> gunsList;
  private string currentGunName = null;
  public Transform gunThrowpoint, cam;
  public float upwardThrowForce, forwardThrowForce;

  private void Start()
  {
    foreach (var item in gunsList)
    {
      item.gunObj.SetActive(false);
    }

    cam = Camera.main.GetComponent<Transform>();
  }

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.Q) && currentGunName != null)
    {
      ThrowGun();
    }
  }

  private void ThrowGun()
  {
    guns ThrowGunItem = gunsList.Find(item => item.name == currentGunName);
    ThrowGunItem.gunObj.SetActive(false);

    currentGunName = null;

    GameObject ThrownGun = Instantiate(ThrowGunItem.ThrowObject, gunThrowpoint.position, Quaternion.identity);

    ThrownGun.GetComponent<Rigidbody>().AddForce(cam.forward * forwardThrowForce, ForceMode.Impulse);
    ThrownGun.GetComponent<Rigidbody>().AddForce(cam.forward * upwardThrowForce, ForceMode.Impulse);
  }

  public GameObject getCurrentGun()
  {
    guns ThrowGunItem = gunsList.Find(item => item.name == currentGunName);
    if (ThrowGunItem.gunObj != null)
    {
      return ThrowGunItem.gunObj;
    }
    return null;
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    foreach (var item in gunsList)
    {
      if (hit.gameObject.CompareTag(item.name) && currentGunName == null)
      {
        currentGunName = item.name;
        item.gunObj.SetActive(true);
        Destroy(hit.gameObject);
      }
    }
  }
}
