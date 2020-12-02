using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollector : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField]
    private GameObject gascanCollectVFX;

    private void Start()
    {
        cc = GetComponentInParent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GasCan")
        {
            cc.fuelAmount += 0.1f;
            Instantiate(gascanCollectVFX, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
    }

}
