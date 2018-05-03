using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

    public GameObject target;
    public GameObject indicator;
    public float indicatorOffset;
    public float detectingOffset;
    private Camera cam;
    private Vector3 targetPosOnScreen;
    private Vector3 displayPos;
    private Vector3 center;
 

    void Start () {
        cam = Camera.main;
    }

    void Update () {

        targetPosOnScreen = cam.WorldToViewportPoint(target.transform.position);

        if (targetPosOnScreen.x >= 0f - detectingOffset &&
            targetPosOnScreen.x <= 1f + detectingOffset &&
            targetPosOnScreen.y >= 0f - detectingOffset &&
            targetPosOnScreen.y <= 1f + detectingOffset) {
            indicator.SetActive(false);
        } else {
            if (!indicator.activeSelf) {
                indicator.SetActive(true);
            }
            center = cam.WorldToViewportPoint( transform.position);            
            float angle = Mathf.Atan2(targetPosOnScreen.y - center.y, targetPosOnScreen.x - center.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle)); //Quaternion.AngleAxis(angle, Vector3.forward);
            displayPos = new Vector3( Mathf.Clamp( targetPosOnScreen.x, indicatorOffset, 1f-indicatorOffset), Mathf.Clamp(targetPosOnScreen.y, indicatorOffset, 1f- indicatorOffset), 0f);
            indicator.transform.position = cam.ViewportToScreenPoint(displayPos);
        }
    }
}
