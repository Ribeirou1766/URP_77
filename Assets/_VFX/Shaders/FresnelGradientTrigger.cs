using System.Collections;
using UnityEngine;

public class FresnelGradientTrigger : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Renderer targetRenderer;

    [Header("Shader Properties")]
    [SerializeField] private string progressProperty = "_TriggerProgress";
    [SerializeField] private string enabledProperty = "_EffectEnabled";

    [Header("Timing")]
    [SerializeField] private float duration = 3f;

    private Material materialInstance;
    private Coroutine playRoutine;

    private void Awake()
    {
        materialInstance = targetRenderer.material;

        materialInstance.SetFloat(progressProperty, 0f);
        materialInstance.SetFloat(enabledProperty, 0f);
    }

    private void Update()
    {
        // 🔥 Press T to trigger the effect
        if (Input.GetKeyDown(KeyCode.T))
        {
            TriggerEffect();
        }
    }

    public void TriggerEffect()
    {
        // Restart if already playing
        if (playRoutine != null)
            StopCoroutine(playRoutine);

        playRoutine = StartCoroutine(PlayEffect());
    }

    private IEnumerator PlayEffect()
    {
        materialInstance.SetFloat(enabledProperty, 1f);

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float progress = Mathf.Clamp01(time / duration);
            materialInstance.SetFloat(progressProperty, progress);

            yield return null;
        }

        // Ensure it finishes exactly at 1
        materialInstance.SetFloat(progressProperty, 1f);

        // Optional: turn off after finishing
        materialInstance.SetFloat(enabledProperty, 0f);
    }
}