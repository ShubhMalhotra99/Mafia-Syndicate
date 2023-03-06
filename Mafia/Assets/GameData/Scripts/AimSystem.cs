using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    public GameObject FreeLookCamera;
    public GameObject AimCamera;
    private void FixedUpdate()
    {
        if(Input.GetMouseButton(1))
        {
            FreeLookCamera.SetActive(false);
            AimCamera.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            FreeLookCamera.SetActive(true);
            AimCamera.SetActive(false);
        }
    }
}
