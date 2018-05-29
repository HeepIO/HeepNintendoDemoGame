using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour {

    public GameObject HeepControls;
    public GameObject OverlayImage;
    public GameObject FindingChestSound;

    private void OnTriggerEnter(Collider other)
    {
        HeepControls.GetComponent<IslandHeep>().ChestTriggered();
        OverlayImage.SetActive(true);
        FindingChestSound.SetActive(true);
        Debug.Log("Hello Trigger");
    }
}
