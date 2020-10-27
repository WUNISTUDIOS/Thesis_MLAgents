/*
 * 
 * Unity 2 M4L (Basic)
 * By Cocell
 * Date: 4/15/2020
 * cocell@yahoo.com
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;


[Serializable]
public class Unity2M4L : MonoBehaviour
{
    public string IP = "127.0.0.1";
    public int port = 8001;

    IPEndPoint remoteEndPoint;
    UdpClient client;

    /// <summary>
    ///
    /// </summary>


    void Start()
    {
        init();
    }

    public void init()
    {
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        client.EnableBroadcast = true;
    }

    public static string ByteArrayToString(byte[] ba)
    {
        return BitConverter.ToString(ba).Replace("-", "");
    }

    public void NoteOn(int midiNote)
    {
        try
        {
            var msg = string.Format("{0}{1}{2}", "/noteon\0", ",i\0\0", string.Empty);
            var data = new List<byte>(ASCIIEncoding.ASCII.GetBytes(msg));
            data.AddRange(BitConverter.GetBytes(midiNote).Reverse()); //AddRange() Adds the elements to the end of list
            client.Send(data.ToArray(), data.ToArray().Length, remoteEndPoint);
        }

        catch (Exception err)
        {
            //
        }

    }

    public void NoteOff(int midiNote)
    {
        try
        {
            var msg = string.Format("{0}{1}{2}", "/noteoff\0\0\0\0", ",i\0\0", string.Empty);
            var data = new List<byte>(ASCIIEncoding.ASCII.GetBytes(msg));
            data.AddRange(BitConverter.GetBytes(midiNote).Reverse()); //AddRange() Adds the elements to the end of list
          //Debug.Log("Bytes " + data2.ToArray().Length + "\n string " + ByteArrayToString(data2.ToArray()));
            client.Send(data.ToArray(), data.ToArray().Length, remoteEndPoint);
        }

        catch (Exception err)
        {
            //
        }

    }

}
