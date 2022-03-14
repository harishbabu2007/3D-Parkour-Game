using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
  public GameObject FinishUI;

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.gameObject.CompareTag("Bottle"))
    {
      GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

      FinishUI.SetActive(true);
      Cursor.lockState = CursorLockMode.None;

      Time.timeScale = 0.4f;
    }
  }
}
