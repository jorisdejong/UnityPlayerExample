using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

using System.Reflection;
using System.Xml;

using System.IO;

public class ResoLinkSetup : MonoBehaviour
{
    int port;
    bool sent = false;

    extOSC.OSCReceiver receiver;
    extOSC.OSCTransmitter sender;

    Camera cam;
    RenderTexture spoutTexture;
    Klak.Spout.SpoutSender spoutSender;
    
    float pTime;

    [System.Serializable]
    public struct ShowInfo
    {
        public string name;
    }

    void Start()
    {
        WriteParamInfo();
        SetupOscReceiver();
        SetupOscSender();
        SetupCamera();

        pTime = Time.realtimeSinceStartup;
        StartCoroutine( Render() );
    }

    private void SetupCamera()
    {
        Application.targetFrameRate = 60;
        cam = Camera.main;
        if ( cam == null )
        {
            var cams = FindObjectsOfType<Camera>();
            if ( cams.Length > 0 )
                cam = cams[ 0 ];
            else
                UnityEngine.Debug.LogError( "Cannot build a scene without a camera" );
        }
        Process currentProcess = Process.GetCurrentProcess();
        int processId = currentProcess.Id;
        
        spoutSender = gameObject.AddComponent<Klak.Spout.SpoutSender>();
        spoutSender.alphaSupport = true;
        gameObject.name = "Hybrid/" + Application.productName + "/" + processId.ToString();
    }

    private void SetupOscSender()
    {
        //this sender pings the shared port 23171
        //this is where the running instance of HybridLedger will be listening
        sender = gameObject.AddComponent<extOSC.OSCTransmitter>();
        sender.RemoteHost = "127.0.0.1";
        sender.RemotePort = 23171;
    }

    private void SetupOscReceiver()
    {
        //this receiver opens up a random free port
        receiver = gameObject.AddComponent<extOSC.OSCReceiver>();
        IPEndPoint ip = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 0 );
        UdpClient client = new UdpClient( ip );
        port = ( (IPEndPoint) client.Client.LocalEndPoint ).Port;
        receiver.LocalPort = port;
        receiver.Bind( "/hybrid/liveloop/resolution", SetResolution );
        receiver.Bind( "/hybrid/liveloop/param", SetParam );
    }

    private static void WriteParamInfo()
    {
        XmlDocument doc = new XmlDocument();
        ShowInfo showInfo;
        showInfo.name = Application.productName;
        Serialization.ToXml<ShowInfo>( ref doc, showInfo );

        //find all the params
        Dictionary<GameObject, List<Param>> orphanFixtures = new Dictionary<GameObject, List<Param>>();
        foreach ( MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>() )
        {
            foreach ( FieldInfo info in mono.GetType().GetFields() )
            {
                if ( info.FieldType.IsSubclassOf( typeof( Param ) ) )
                {
                    if ( !orphanFixtures.ContainsKey( mono.gameObject ) )
                        orphanFixtures[ mono.gameObject ] = new List<Param>();
                    var obj = info.GetValue( mono );
                    var param = obj as Param;
                    orphanFixtures[ mono.gameObject ].Add( param );
                    ParamManager.GetInstance().AddParam( param );
                }
            }
        }
        foreach ( GameObject fixture in orphanFixtures.Keys )
        {
            XmlElement child = doc.CreateElement( "fixture" );
            child.SetAttribute( "name", fixture.name );
            doc.DocumentElement.AppendChild( child );
            foreach ( Param param in orphanFixtures[ fixture ] )
            {
                Serialization.AddXml<Param>( ref child, param );
            }
        }

        string dataPath = Application.dataPath;
        dataPath += "/..";
        dataPath = Path.Combine( dataPath, "fixturedata.xml" );
        FileStream fileStream = new FileStream( dataPath, FileMode.Create );
        doc.Save( fileStream );
    }

    //render loop
    //because we're rendering offscreen, we can't use Unity's internal fps throttling
    //so this corourtine tries to maintain fps but will throttle if it gets too heavy
    IEnumerator Render()
    {
        while ( true )
        {
            float dTime = pTime - Time.realtimeSinceStartup;
            Application.targetFrameRate = Mathf.Min( 60, Mathf.CeilToInt( 1.0f / dTime ) / 2 );

            cam.Render();
            pTime = Time.realtimeSinceStartup;

            yield return new WaitForSeconds( 1.0f / 60.0f );
        }
    }

    void Update()
    {
        //register ourselves in the ledger
        //the ledger is responsible for starting instances 
        //so it is unlikely that the ledger is not running when we send this meessage
        //but there is a possible point of failure where running instances do not re-register themselves when the ledger is restarted
        if ( !sent )
        {
            extOSC.OSCValue[] values = { extOSC.OSCValue.String( Application.productName ), extOSC.OSCValue.Int( port ) };
            sender.Send( new extOSC.OSCMessage( "/hybrid/ledger/setport", values ) );
            sent = true;
        }
    }

    void SetResolution( extOSC.OSCMessage received )
    {
        Destroy( spoutTexture );
        spoutTexture = new RenderTexture( received.Values[ 0 ].IntValue, received.Values[ 1 ].IntValue, 1 );
        spoutTexture.depth = 0;
        spoutTexture.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_UNorm;

        cam.targetTexture = spoutTexture;
        spoutSender.sourceTexture = spoutTexture;
    }

    void SetParam( extOSC.OSCMessage received )
    {
        List<float> values = new List<float>();
        for ( int i = 1; i < received.Values.Count; i++ )
            values.Add( received.Values[ i ].FloatValue );
        ParamManager.GetInstance().SetParam( received.Values[ 0 ].StringValue, values );
    }
}
