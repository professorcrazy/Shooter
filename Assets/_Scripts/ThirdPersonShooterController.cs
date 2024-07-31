using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSelsitivity = 1.0f;
    [SerializeField] private float aimSensitivty = 0.5f;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform aimHitPos;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] GameObject bulletPrefab;
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    private Animator anim;

    Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

    bool canShoot = true;
    [SerializeField] float rps = 1f / 5f;
    float shotDelay;
    float lastShot;

    //Runs as the first before start.
    private void Awake() {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
        updateShotDelay(rps);
    }

    public void updateShotDelay(float newRPS) {
        shotDelay = 1 / newRPS;
    }

    // Update is called once per frame
    void Update() {

        Vector3 mouseWorldPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask)) {
            aimHitPos.position = hit.point;
            mouseWorldPos = hit.point;
        }
        if (starterAssetsInputs.aim) {
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime*10f));
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivty);
            thirdPersonController.SetRotateOnMove(false);
            Vector3 worldAimTarget = mouseWorldPos;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget-transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else {
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSelsitivity);
            thirdPersonController.SetRotateOnMove(true);
        }

        canShoot = (Time.time > (lastShot + shotDelay)) ? true : false;
        if (starterAssetsInputs.shoot && canShoot) {
            canShoot = false;
            lastShot = Time.time;
            Vector3 aimDir = (mouseWorldPos - bulletSpawnPos.position).normalized;
            Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.LookRotation(aimDir,Vector3.up));
            //starterAssetsInputs.shoot = false;
        }
    }
}
