using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour {

    public GameObject HeepControls;
    public GameObject OverlayImage;
    public GameObject FindingChestSound;

	private IEnumerator GoHeepyTime()
	{
		OverlayImage.SetActive(true);
		FindingChestSound.SetActive(true);

		yield return new WaitForSeconds (5);

		HeepControls.GetComponent<IslandHeep>().ChestTriggered();
	}

    private void OnTriggerEnter(Collider other)
    {
		Debug.Log("Hello Trigger");

		StartCoroutine (GoHeepyTime ());
    }
}
