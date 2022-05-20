using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using System.Collections;

public class SlowMotion : MonoBehaviour
{
  public float slowMotionTimeScale;
  private float startTimeScale;
  private float startFixedDeltaTime;
  public Volume volume;
  LensDistortion lensDistortion;
  Vignette vignette;
  ChromaticAberration chromaticAberration;
  float timeEffect = 0.8f;

  private void Start()
  {
    startTimeScale = Time.timeScale;
    startFixedDeltaTime = Time.fixedDeltaTime;
    LensDistortion tmp;
    Vignette tmp_v;
    ChromaticAberration tmp_c;

    if (volume.profile.TryGet<LensDistortion>(out tmp))
    {
      lensDistortion = tmp;
    }
    if (volume.profile.TryGet<Vignette>(out tmp_v))
    {
      vignette = tmp_v;
    }
    if (volume.profile.TryGet<ChromaticAberration>(out tmp_c))
    {
      chromaticAberration = tmp_c;
    }

  }

  public void StartSlowMotion()
  {
    vignette.intensity.value = Mathf.Lerp(0.44f, 0.45f, timeEffect);
    vignette.smoothness.value = Mathf.Lerp(0.219f, 0.35f, timeEffect);
    chromaticAberration.intensity.value = Mathf.Lerp(0, 0.8f, timeEffect);
    lensDistortion.intensity.value = Mathf.Lerp(0, -0.44f, timeEffect);

    Time.timeScale = slowMotionTimeScale;
    Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
  }
  public void StopSlowMotion()
  {
    Time.timeScale = startTimeScale;
    Time.fixedDeltaTime = startFixedDeltaTime;

    vignette.intensity.value = Mathf.Lerp(0.45f, 0.44f, timeEffect);
    vignette.smoothness.value = Mathf.Lerp(0.35f, 0.219f, timeEffect);
    chromaticAberration.intensity.value = Mathf.Lerp(0.8f, 0, timeEffect);
    lensDistortion.intensity.value = Mathf.Lerp(-0.44f, 0, timeEffect);
  }
}
