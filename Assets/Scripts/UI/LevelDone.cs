using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDone : MonoBehaviour
{
  public GameObject lvlLoading;
  public Slider slider;
  public void SameLevel()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void GoToLevelOne()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(0);
  }

  public void ExitGame()
  {
    Application.Quit();
  }

  public void LoadLevel()
  {
    Time.timeScale = 1f;
    StartCoroutine(LoadAsync());
  }

  IEnumerator LoadAsync()
  {
    lvlLoading.SetActive(true);
    AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    while (operation.isDone == false)
    {
      float progress = Mathf.Clamp01(operation.progress / 0.9f);
      slider.value = progress;
      yield return null;
    }
  }
}
