using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heep;

public class HeepController : MonoBehaviour {

    HeepDevice myDevice;

    public GameObject RightLight;
    public GameObject LeftLight;
    public GameObject FireBall;
    public GameObject CoffeeTable;

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
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Fire Ball", false));
        myDevice.AddControl(Control.CreateControl(Control.CtrlInputOutput.input, Control.CtrlType.OnOff, "Flip Table", false));

        myDevice.SetDeviceNameStartup("Room Game");
        myDevice.StartListening();
    }

	// Use this for initialization
	void Start () {
        CreateDevice();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("escape"))
            Application.Quit();

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

        if (myDevice.GetControlValueByID(2) == 0)
        {
            FireBall.SetActive(false);
        }
        else
        {
            FireBall.SetActive(true);
        }

        if (myDevice.GetControlValueByID(3) == 1)
        {
            myDevice.SetControlByID(3, 0, false);
            CoffeeTable.GetComponent<Rigidbody>().AddForce(new Vector3(100, 1000, 0));
        }
    }
}
