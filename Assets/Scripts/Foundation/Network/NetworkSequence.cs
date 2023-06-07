﻿using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkSequence
{
    //このクラスはstatic的に機能する
    static NetworkSequence _instance = new NetworkSequence();
    static public NetworkSequence Instance => _instance;
    private NetworkSequence() { }


    Dictionary<string, UniTask> _sequenceDeck = new Dictionary<string, UniTask>();

    static public bool RegisterSequence(string key, UniTask task)
    {
        if (_instance._sequenceDeck.ContainsKey(key))
        {
            Debug.Log("重複して登録している");
            return false;
        }

        _instance._sequenceDeck.Add(key, task);
        return true;
    }

    static public bool DeleteSequence(string key, UniTask task)
    {
        if (!_instance._sequenceDeck.ContainsKey(key))
        {
            Debug.Log("登録されていない");
            return false;
        }

        _instance._sequenceDeck.Remove(key);
        return true;
    }
}