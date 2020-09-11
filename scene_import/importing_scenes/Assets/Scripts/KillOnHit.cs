using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnHit : MonoBehaviour
{
    //basically a brick wall make out of black holes...made out of ones and zeros
    private void OnCollisionEnter(Collision Other)
    {
        Destroy(Other.gameObject);
    }
}
