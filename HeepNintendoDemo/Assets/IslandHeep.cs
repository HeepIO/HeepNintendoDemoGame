using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heep;
using UnityStandardAssets.Characters.FirstPerson;

public class IslandHeep : MonoBehaviour {

    HeepDevice myDevice;

    public Material noonSkybox;
    public GameObject ControllerCamera;
    public GameObject noonLight;
    public GameObject nightLight;

    public GameObject waterFallRock;
    public GameObject waterFallRockExplosion;

    public GameObject SunTurnOnSound;

    public GameObject MainCharacter;

	public List<GameObject> Boulders;

	public GameObject VolcanoFire;
	public GameObject VolcanoOutExplosion;

    private void OnApplicationQuit()
    {
        Debug.Log("Quitting!");
        myDevice.CloseDevice();
    }

    // Use this for initialization
    void Start () {

        List<byte> ID = new List<byte>();
        for (byte i = 0; i < 4; i++)
        {
            byte multiplier = 21;
            ID.Add((byte)(i * multiplier));
        }

        DeviceID myID = new DeviceID(ID);
        myDevice = new HeepDevice(myID);

        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Set Noon", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Waterfall", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.output, Control.CtrlType.OnOff, "Treasure", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Forward", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Backward", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Left", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Right", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Jump", false));

        myDevice.SetDeviceNameStartup("Island Game");
        myDevice.StartListening();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();

        HandleTimeOfDay();
        HandleWaterFall();
        HandleMovementInput();
    }

    public void HandleMovementInput()
    {
        int forwardValue = myDevice.GetControlValueByID(3);
        int backwardValue = myDevice.GetControlValueByID(4);
        int leftValue = myDevice.GetControlValueByID(5);
        int rightValue = myDevice.GetControlValueByID(6);
        int jumpValue = myDevice.GetControlValueByID(7);
        MainCharacter.GetComponent<FirstPersonController>().HeepWalkControl(forwardValue, backwardValue, leftValue, rightValue, jumpValue);
    }

    public void ChestTriggered()
    {
        myDevice.SetControlByID(2, 1, false);
    }

	private bool WaterFallHandled = false;
    private void HandleWaterFall()
    {
        if(myDevice.GetControlValueByID(1) == 1)
        {
            //waterFallRock.SetActive(false);
            waterFallRockExplosion.SetActive(true);
			VolcanoOutExplosion.SetActive (true);
			VolcanoFire.SetActive (false);
			if (!WaterFallHandled) {
				
				for (int i = 0; i < Boulders.Count; i++) {
					Boulders [i].AddComponent<Rigidbody> ();
					Boulders [i].GetComponent<Rigidbody> ().mass = 10;
				}

				WaterFallHandled = true;
			}

        }
    }

    private void HandleTimeOfDay()
    {
        if(myDevice.GetControlValueByID(0) == 1)
        {
            ControllerCamera.GetComponent<Skybox>().material = noonSkybox;
            noonLight.SetActive(true);
            nightLight.SetActive(false);
            SunTurnOnSound.SetActive(true);
        }
    }
}
