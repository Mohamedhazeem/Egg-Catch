using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Virtual Cameras")]
    public CameraProperties[] camProperties;
    private Vector2 lastScreenResolution;
    private Dictionary<string, float> aspectProfileToSize;
    void Start()
    {
        aspectProfileToSize = new Dictionary<string, float>
        {
            { "MobileSmall", 0f },
            { "MobileMid", 0f },
            { "Tablet", 0f },
            { "PC_16_9", 0f },
            { "PC_16_10", 0f },
            { "PC_FullHD", 0f },
            { "PC_UltraWide", 0f },
        };

        // UpdateAspectProfiles();
        ChangeCameraSize();
        lastScreenResolution = new Vector2(Screen.width, Screen.height);

    }

    void Update()
    {
        // UpdateAspectProfiles();
        ChangeCameraSize();
        if (Screen.width != lastScreenResolution.x || Screen.height != lastScreenResolution.y)
        {
            lastScreenResolution = new Vector2(Screen.width, Screen.height);
        }
    }

    private void UpdateAspectProfiles()
    {
        foreach (var camProps in camProperties)
        {
            if (camProps.camera != null)
            {
                aspectProfileToSize["MobileSmall"] = camProps.iPhoneX;
                aspectProfileToSize["MobileMid"] = camProps.iPhone6;
                aspectProfileToSize["Tablet"] = camProps.iPad;
                aspectProfileToSize["PC_FullHD"] = camProps.FullHD;
                aspectProfileToSize["PC_16_9"] = camProps.PC_16_9;
                aspectProfileToSize["PC_16_10"] = camProps.PC_16_10;
                aspectProfileToSize["PC_UltraWide"] = camProps.PC_UltraWide;
            }
        }
    }

    public void ChangeCameraSize()
    {
        foreach (var camProps in camProperties)
        {
            if (camProps.camera == null)
            {
                Debug.LogWarning($"Camera '{camProps.cameraName}' is not assigned!");
                continue;
            }

            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            float aspect = camProps.camera.aspect;
            float targetSize;

            if (screenWidth == 1920 && screenHeight == 1080)
            {
                targetSize = camProps.FullHD;
            }
            else if (aspect <= 0.5f)
            {
                targetSize = camProps.iPhoneX;
            }
            else if (aspect <= 0.6f)
            {
                targetSize = camProps.iPhone6;
            }
            else if (aspect <= 1.4f)
            {
                targetSize = camProps.iPad;
            }
            else if (aspect <= 1.7f)
            {
                targetSize = camProps.PC_16_10;
            }
            else if (aspect <= 2.0f)
            {
                targetSize = camProps.PC_16_9;
            }
            else
            {
                targetSize = camProps.PC_UltraWide;
            }

            if (camProps.isOrthographic)
            {
                camProps.camera.orthographic = true;
                camProps.camera.orthographicSize = targetSize;
            }
            else
            {
                camProps.camera.orthographic = false;
                camProps.camera.fieldOfView = targetSize;
            }
        }
    }


}
[System.Serializable]
public class CameraProperties
{
    public string cameraName;
    public Camera camera;
    public bool isOrthographic;

    [Header("Mobile Sizes")]
    public float iPhoneX;
    public float iPhone6;
    public float iPad;

    [Header("PC Sizes")]
    public float PC_16_9;
    public float PC_16_10;

    public float PC_UltraWide;
    public float FullHD;
}

