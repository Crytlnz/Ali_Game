using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSistem : MonoBehaviour
{
    private Transform mario;
    public bool debug;

    private Vector3 initialCameraPosition; // Simpan posisi awal kamera

    [System.Obsolete]
    void Start()
    {
        Screen.SetResolution(256, 240, true, 60);

        GameObject marioObject = GameObject.FindWithTag("Player");
        if (marioObject != null)
        {
            mario = marioObject.transform;
        }
        else
        {
            Debug.LogError("Mario object not found! Ensure it's in the scene and has the 'Player' tag.");
        }

        // Simpan posisi awal kamera
        initialCameraPosition = transform.position;
    }

    void Update()
    {
        if (mario == null)
        {
            GameObject marioObject = GameObject.FindWithTag("Player");
            if (marioObject != null)
            {
                mario = marioObject.transform;
            }
        }

        if (mario != null)
        {
            // Kamera mengikuti posisi pemain di sepanjang sumbu X, tanpa batasan hanya maju ke depan
            transform.position = new Vector3(mario.position.x, transform.position.y, -10);
        }
    }

    // Metode untuk mereset kamera ke posisi awal
    public void ResetCamera(Vector3 playerStartPos)
    {
        transform.position = new Vector3(playerStartPos.x, initialCameraPosition.y, initialCameraPosition.z);
    }
}