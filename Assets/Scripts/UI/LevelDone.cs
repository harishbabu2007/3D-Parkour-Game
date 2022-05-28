using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDone : MonoBehaviour
{
  public GameObject lvlLoadingPanel;
  public Slider slider;
  public AudioClip clip;
  private AudioSource source;
  public void Start()
  {
    source = gameObject.AddComponent<AudioSource>();
  }
  public void SameLevel()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void GoToLevelOne()
  {
    GameObject bgm = GameObject.FindGameObjectWithTag("BGM");
    Destroy(bgm);

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
    StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex + 1));
  }

  IEnumerator LoadAsync(int index)
  {
    lvlLoadingPanel.SetActive(true);
    AsyncOperation operation = SceneManager.LoadSceneAsync(index);

    while (operation.isDone == false)
    {
      float progress = Mathf.Clamp01(operation.progress / 0.9f);
      slider.value = progress;
      yield return null;
    }
  }

  public void StartTraining()
  {
    StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex + 1));
  }

  public void Click()
  {
    source.PlayOneShot(clip);
  }
}

