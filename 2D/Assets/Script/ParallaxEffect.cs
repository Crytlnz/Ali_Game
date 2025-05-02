using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    public float parallaxStrength = 0.5f; // Bisa diatur di Inspector untuk menyesuaikan efek

    private Vector2 startingPosition;
    private float startingZ;

    private Vector2 camMoveSinceStart => new Vector2(cam.transform.position.x, cam.transform.position.y) - startingPosition;
    private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
    private float parallaxFactor => Mathf.Clamp01(Mathf.Abs(zDistanceFromTarget) / cam.farClipPlane) * parallaxStrength;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main; // Auto-detect camera jika tidak diassign
        }

        if (followTarget == null)
        {
            Debug.LogError("Follow target belum diassign!", this);
            return;
        }

        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (followTarget == null) return;

        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
