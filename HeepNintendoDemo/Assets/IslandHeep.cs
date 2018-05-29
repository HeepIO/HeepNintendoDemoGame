using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heep;

public class IslandHeep : MonoBehaviour {

    HeepDevice myDevice;

    public Material noonSkybox;
    public GameObject ControllerCamera;
    public GameObject noonLight;
    public GameObject nightLight;

    public GameObject waterFallRock;

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

        myDevice.SetDeviceNameStartup("Island Game");
        myDevice.StartListening();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();

        HandleTimeOfDay();
        HandleWaterFall();
    }

    public void ChestTriggered()
    {
        myDevice.SetControlByID(2, 1, false);
    }

    private void HandleWaterFall()
    {
        if(myDevice.GetControlValueByID(1) == 1)
        {
            waterFallRock.SetActive(false);
        }
    }

    private void HandleTimeOfDay()
    {
        if(myDevice.GetControlValueByID(0) == 1)
        {
            ControllerCamera.GetComponent<Skybox>().material = noonSkybox;
            noonLight.SetActive(true);
            nightLight.SetActive(false);
        }
    }
}
