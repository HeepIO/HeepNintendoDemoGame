using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour {

    public GameObject HeepControls;

    private void OnTriggerEnter(Collider other)
    {
        HeepControls.GetComponent<IslandHeep>().ChestTriggered();
        Debug.Log("Hello Trigger");
    }
}
