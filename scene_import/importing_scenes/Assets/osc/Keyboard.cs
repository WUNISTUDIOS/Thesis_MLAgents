using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    Unity2M4L unity2M4L;
    public int keyAMidi = 36; //C2 
    public int keySMidi = 38; //D2
    public int keyDMidi = 40; //E2

    private void Start()
    {
       unity2M4L = GetComponent<Unity2M4L>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //C2
            unity2M4L.NoteOn(keyAMidi);
            Debug.Log("Note On");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            unity2M4L.NoteOff(keyAMidi);
            Debug.Log("Note Off");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //D2
            unity2M4L.NoteOn(keySMidi);
            Debug.Log("Note On");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {     
            unity2M4L.NoteOff(keySMidi);
            Debug.Log("Note Off");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //E2
            unity2M4L.NoteOn(keyDMidi);
            Debug.Log("Note On");
        }

        if (Input.GetKeyUp(KeyCode.D))
        {        
            unity2M4L.NoteOff(keyDMidi);
            Debug.Log("Note Off");
        }
    }
}
