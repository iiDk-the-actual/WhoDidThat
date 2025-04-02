using BepInEx;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace WhoDidThat
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Start()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        public static void OnEvent(EventData data)
        {
            string playerName = "[Unknown player]";
            Player plr = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(data.Sender, false);
            if (plr != null)
            {
                playerName = plr.NickName;
            }
            if (data.Code != 201 && data.Code != 205 && data.Code != 206 && data.Code != 208)
            {
                try
                {
                    if (data.Code != 200)
                    {
                        object[] array = (object[])((Hashtable)data.CustomData)[(byte)4];
                        string lmfao = "";
                        foreach (object element in array)
                        {
                            try
                            {
                                lmfao += element.ToString() + ", ";
                            } catch { lmfao += "[Could not find value]" + ", "; }
                        }
                        UnityEngine.Debug.Log("Event by " + playerName + " sent " + data.Code.ToString() + " // " + lmfao);
                    }
                    else
                    {
                        object[] array = (object[])((Hashtable)data.CustomData)[(byte)4];
                        string lmfao = "";
                        foreach (object element in array)
                        {
                            try
                            {
                                lmfao += element.ToString() + ", ";
                            }
                            catch { lmfao += "[Could not find value]" + ", "; }
                        }
                        UnityEngine.Debug.Log("RPC by " + playerName + " sent " + PhotonNetwork.PhotonServerSettings.RpcList[int.Parse(((Hashtable)data.CustomData)[(byte)5].ToString())] + " // " + lmfao);
                    }
                } catch
                {
                    UnityEngine.Debug.Log("Event by " + playerName + " sent " + data.Code.ToString());
                }
            }
        }
    }
}
