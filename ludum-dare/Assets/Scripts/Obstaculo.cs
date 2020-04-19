using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    [SerializeField]
    private GameObject smoke;
    void OnDestroy()
    {
        Instantiate(smoke, transform.position, Quaternion.identity);
    }
}
