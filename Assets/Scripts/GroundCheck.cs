using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterController cc;

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
