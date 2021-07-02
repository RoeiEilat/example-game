using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisably;
    [SerializeField]
    string RemoteLayerName = "RemotePlayer";

    Camera secenCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponent();
            AssignRemoteLayer();
        }
        else
        {
            secenCamera = Camera.main;
            if(secenCamera != null)
            {
                secenCamera.gameObject.SetActive(false);
            }
        }

        RegisterPlayer();
    }

    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }


    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(RemoteLayerName);
    }


    void DisableComponent()
    {
        for (int i = 0; i < componentsToDisably.Length; i++)
        {
            componentsToDisably[i].enabled = false;
        }
    }

    void OnDisable()
    {
        if(secenCamera != null)
        {
            secenCamera.gameObject.SetActive(true);
        }
    }
}
