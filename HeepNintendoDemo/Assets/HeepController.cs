using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heep;

public class HeepController : MonoBehaviour {

    HeepDevice myDevice;

    public GameObject RightLight;
    public GameObject LeftLight;

    private void OnApplicationQuit()
    {
        Debug.Log("Quitting!");
        myDevice.CloseDevice();
    }

    void CreateDevice()
    {
        List<byte> ID = new List<byte>();
        for(byte i = 0; i < 4; i++)
        {
            byte multiplier = 20;
            ID.Add((byte)(i * multiplier));
        }

        DeviceID myID = new DeviceID(ID);
        myDevice = new HeepDevice(myID);

        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Left Light", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Right Light", false));

        myDevice.SetDeviceNameStartup("Room Game");
        myDevice.StartListening();
    }

	// Use this for initialization
	void Start () {
        CreateDevice();
	}
	
	// Update is called once per frame
	void Update () {
		if(myDevice.GetControlValueByID(0) == 0)
        {
            LeftLight.GetComponent<Light>().enabled = false;
        }
        else
        {
            LeftLight.GetComponent<Light>().enabled = true;
        }

        if(myDevice.GetControlValueByID(1) == 0)
        {
            RightLight.GetComponent<Light>().enabled = false;
        }
        else
        {
            RightLight.GetComponent<Light>().enabled = true;
        }
	}
}
