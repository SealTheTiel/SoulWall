using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Logger : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint serverEndPoint;
    private bool serverFound = false;
    private const int listenPort = 9001;  // Port for server discovery
    private const int sendPort = 9000;    // Port for sending data
    private Thread listenThread;
    void DetectServer()
    {
        if (!serverFound)
        {
            Debug.Log("Looking for server...");
            SendBroadcast();
        }
    }

    void SendBroadcast()
    {
        UdpClient broadcastClient = new UdpClient();
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, listenPort);
        byte[] message = Encoding.UTF8.GetBytes("DISCOVER_SERVER");
        broadcastClient.Send(message, message.Length, endPoint);
        broadcastClient.Close();
    }

    void ListenForServer()
    {
        while (!serverFound)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, listenPort);
                byte[] data = udpClient.Receive(ref anyIP);
                string message = Encoding.UTF8.GetString(data);
                
                if (message.StartsWith("SERVER_IP:"))
                {
                    string ip = message.Split(':')[1];
                    serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), sendPort);
                    serverFound = true;
                    Debug.Log($"Server found at {ip}");
                    Log("START", "START");
                }
            }
            catch (SocketException ex)
            {
                Debug.LogError("Socket Exception: " + ex.Message);
            }
        }
    }
    public void Log(string filename, string id, params object[] data) {
        string logData = $"{filename},{System.DateTime.UtcNow.ToString("o")},{id}";
        for (int i = 0; i < data.Length; i++) {
            logData += $",{data[i]}";
        }
        byte[] sendBytes = Encoding.UTF8.GetBytes(logData);
        udpClient.Send(sendBytes, sendBytes.Length, serverEndPoint);
    }
    void Start()
    {
        udpClient = new UdpClient(listenPort);
        serverEndPoint = null;

        listenThread = new Thread(ListenForServer);
        listenThread.IsBackground = true;
        listenThread.Start();

        InvokeRepeating(nameof(DetectServer), 0, 5f);
    }

    void Update()
    {
    }


    void OnApplicationQuit()
    {
        listenThread?.Abort();
        udpClient?.Close();
    }
}
