using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonNetworkingLauncher))]
public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    private PhotonNetworkingLauncher _networkingLauncher;
    
    void Start()
    {
        _networkingLauncher = GetComponent<PhotonNetworkingLauncher>();
        playButton.onClick.AddListener(PlayButtonPressed);
    }

    private void PlayButtonPressed()
    {
        _networkingLauncher.Connect();
    }
}
