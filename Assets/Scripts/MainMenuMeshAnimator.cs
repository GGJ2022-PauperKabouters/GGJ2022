using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMeshAnimator : MonoBehaviour
{
    private Transform rotateTrans;

    public float rotateSpeedPerSec = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateTrans = transform.GetChild(0);
    }

    void Update()
    {
        Vector3 angles = rotateTrans.eulerAngles;
        angles.y += rotateSpeedPerSec * Time.deltaTime;
        rotateTrans.eulerAngles = angles;
    }
}
