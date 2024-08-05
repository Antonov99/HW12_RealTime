using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    [UsedImplicitly]
    public class ServerTimeManager
    {
        public bool isActualTimeReceived;

        private DateTime _serverBaseTime;
        private DateTime _localBaseTime;

        private const string _SERVER_TIME_URL = "http://worldtimeapi.org/api/timezone/Etc/UTC";
        
        public async UniTask Initialize()
        {
            await FetchServerTime();
        }

        public DateTime GetCurrentTime()
        {
            if (!isActualTimeReceived)
                throw new Exception("time not received");

            TimeSpan elapsed = DateTime.Now - _localBaseTime;
            return _serverBaseTime.Add(elapsed);
        }

        private async UniTask FetchServerTime()
        {
            var webRequest = UnityWebRequest.Get(_SERVER_TIME_URL);
            await webRequest.SendWebRequest().ToUniTask();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                ServerTimeResponse timeInfo = JsonUtility.FromJson<ServerTimeResponse>(webRequest.downloadHandler.text);
                Debug.Log(timeInfo.datetime);
                _serverBaseTime = DateTime.Parse(timeInfo.datetime);
                _localBaseTime = DateTime.Now;
                isActualTimeReceived = true;
            }
            else
            {
                {
                    Debug.LogError(webRequest.error);
                }
            }
        }

    }
}

[Serializable]
public class ServerTimeResponse
{
    public string datetime;
}