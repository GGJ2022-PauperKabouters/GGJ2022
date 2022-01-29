using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LocalPlayerInstance;


    public void setLocalPlayerCanMove(bool value)
    {
        LocalPlayerInstance.GetComponent<PlayerController>().setCanMove(value);
    }

}
