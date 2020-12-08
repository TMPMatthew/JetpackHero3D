using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField]
    private GameObject gascanCollectVFX;
    [SerializeField]
    private GameObject coinCollectVFX;


    public bool characterEnteredMultiplier;

    private void Start()
    {
        cc = GetComponentInParent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GasCan":

                cc.fuelAmount += 0.1f;
                Instantiate(gascanCollectVFX, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);

                break;
            case "KillZone":

                cc.OnGameEnded();

                break;
            case "EndgameTrigger":

                cc.enteredEndgame = true;

                break;
            case "Coin":

                cc.coinsCollected += 1;
                Instantiate(coinCollectVFX, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);

                break;
            case "StopTrigger":

                cc.dollyCart.m_Speed = 0;
                cc.coinsCollected *= other.GetComponent<StairMultiplier>().multiplierFactor;

                break;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            cc.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cc.grounded = false;
    }

}
