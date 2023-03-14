using UnityEngine;

public class RaycastShooting : MonoBehaviour
{
    public float range = 100f;
    public GameObject impactEffect;
    public GameObject GunTip;
    public ParticleSystem MuzzleFlash;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        GunTip.transform.position = MuzzleFlash.transform.position;
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("Hit " + hit.transform.name);
            MuzzleFlash.Play();

            // Spawn impact effect at hit point
            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}