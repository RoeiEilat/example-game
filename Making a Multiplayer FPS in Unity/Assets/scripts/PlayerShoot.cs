using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{

    private const string PLAYER_TAG = "Player";


    public PlayerWeapen weapen;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: no cam was found");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapen.range, mask))
        {
            if(_hit.collider.tag == PLAYER_TAG)
            {
                cmdPlayerShot(_hit.collider.name);
            }
        }
    }


    [Command]
    void cmdPlayerShot(string _PlayerID)
    {
        Debug.Log(_PlayerID + " HAS BEEN SHOT");
    }
}
