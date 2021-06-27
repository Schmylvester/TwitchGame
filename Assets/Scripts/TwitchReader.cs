using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public struct TwitchUser
{
    public string userName;
    public string password;
    public string channelName;
}

public class TwitchReader : MonoBehaviour
{
    TcpClient m_twitchClient;
    StreamReader m_twitchReader;
    StreamWriter m_twitchWriter;

    [SerializeField] string m_filePath;
    StringReader m_userStream;
    TwitchUser m_user;

    List<string> m_validCommands = new List<string>
    {
        "!JOIN",
        "!STANCE",
        "!WALK",
        "!RUN"
    };
    public delegate void chatCommandReceived(string user, string[] commands);
    public chatCommandReceived m_chatEvent;

    void Start()
    {
        string filePath = Application.persistentDataPath + '/' + m_filePath;
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            m_user = (TwitchUser)formatter.Deserialize(stream);
            stream.Close();

            ConnectToTwitch();
        }
        else
        {
            Debug.LogError("You have not set your credentials correctly.");
        }
    }

    void Update()
    {
        if (!m_twitchClient.Connected)
        {
            Debug.Log("Connection lost - Reconnecting");
            ConnectToTwitch();
        }

        ReadChat();
    }

    private void ReadChat()
    {
        if (m_twitchClient.Available > 0)
        {
            string message = m_twitchReader.ReadLine(); //Read in the current message
            if (message.Contains("PRIVMSG"))
            {
                //Get the users name by splitting it from the string
                int splitPoint = message.IndexOf("!", 1);
                string chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                parseMessage(chatName, message.ToUpper());
            }
        }
    }

    void parseMessage(string chatName, string message)
    {
        if (message[0] == '!')
        {
            string[] messageParts = message.Split(' ');
            if (m_validCommands.Contains(messageParts[0]))
            {
                m_chatEvent.Invoke(chatName, messageParts);
            }
        }
    }

    public Color getUsernameColour(string username)
    {
        UnityEngine.Random.State seed = UnityEngine.Random.state;
        int userID = 0;
        foreach (char n in username)
        {
            userID += (int)n;
        }
        UnityEngine.Random.InitState(userID);
        Color nameColour = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        UnityEngine.Random.state = seed;
        return nameColour;
    }

    void ConnectToTwitch()
    {
        Debug.Log("Connecting");
        m_twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        m_twitchReader = new StreamReader(m_twitchClient.GetStream());
        m_twitchWriter = new StreamWriter(m_twitchClient.GetStream());

        m_twitchWriter.WriteLine("PASS " + m_user.password);
        m_twitchWriter.WriteLine("NICK " + m_user.userName);
        m_twitchWriter.WriteLine("USER " + m_user.userName + " 8 * :" + m_user.userName);
        m_twitchWriter.WriteLine("JOIN #" + m_user.channelName);
        m_twitchWriter.Flush();
    }
}
