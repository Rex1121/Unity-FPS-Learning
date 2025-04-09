using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    public static SpecialEffectsManager Instance;

    public ParticleSystem hitEffect;
    public ParticleSystem magicEffect;
    public Camera mainCamera;

    private Vector3 originalCamPos;

    void Awake()
    {
        if (Instance == null) Instance = this;
        originalCamPos = mainCamera.transform.position;
    }

    public void PlayHitEffect(Vector3 position)
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, position, Quaternion.identity).Play();
        }

        StartCoroutine(CameraShake(0.1f, 0.2f));
    }

    public void PlayMagicEffect(Vector3 position)
    {
        if (magicEffect != null)
        {
            Instantiate(magicEffect, position, Quaternion.identity).Play();
        }
    }

    private System.Collections.IEnumerator CameraShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.position = originalCamPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalCamPos;
    }
}