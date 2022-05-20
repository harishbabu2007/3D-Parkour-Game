using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
  public int hp;
  private SlowMotion slowMotion;
  public TextMeshProUGUI text;

  private void Start()
  {
    slowMotion = GetComponent<SlowMotion>();
    text = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<TextMeshProUGUI>();
  }

  public void EnemyHit()
  {
    hp -= 10;
  }

  // Update is called once per frame
  void Update()
  {
    if (hp <= 0)
    {
      Die();
    }

    text.text = hp.ToString();
  }

  void Die()
  {
    if (slowMotion != null)
    {
      slowMotion.StopSlowMotion();
    }

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
