using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class BattleFade : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public Color fadeColor = Color.black;
    private float alpha = 1f; // Start fully black
    private Texture2D fadeTexture;

    [SerializeField] CinemachineVirtualCamera camera2;

    void Start()
    {
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        fadeTexture.Apply();
    }

    void Update()
    {
        
        StartCoroutine(CameraTransition());
        // Fade in (reduce alpha over time)
        if (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            alpha = Mathf.Clamp01(alpha);
            fadeTexture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            fadeTexture.Apply();
        }
        //camera2.Priority = 1;
        //StartCoroutine(CameraTransition());

    }

    void OnGUI()
    {
        if (alpha > 0f)
        {
            GUI.depth = -1000; // Draw on top of everything
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }

    IEnumerator CameraTransition()
    {
        yield return new WaitForSeconds(0);
        camera2.Priority = 1;
        // Change of virtual camera

    }
}
