﻿using UnityEngine;
using UnityEngine.Assertions;

public class Player {
    public int id;
    public Material material;
}

public class PlayerManager : MonoBehaviour {
    private static PlayerManager _instance;

    public Material[] baseMaterials;

    private Color32[] _colorPalette = new Color32[] {
        new Color32(0x3f, 0x51, 0xb5, 0xff),
        new Color32(0x9c, 0x27, 0xb0, 0xff),
        new Color32(0x25, 0x9b, 0x24, 0xff),
        new Color32(0xff, 0xeb, 0x3b, 0xff)
    };

    private Player[] _players;
    private int _selfId;

    public static PlayerManager instance {
        get {
            if (!_instance && !(_instance = FindObjectOfType<PlayerManager>())) {
                Debug.LogError("There needs to be one active PlayerManger script on an object in your scene.");
            }
            return _instance;
        }
    }

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        Reset();
    }

    public void Reset() {
        _players = null;
        _selfId = -1;
    }

    public void SetPlayerAmount(int amount) {
        int material_index;
        int color_index;
        _players = new Player[amount];
        for (int i = 0; i < amount; i++) {
            material_index = i % baseMaterials.Length;
            color_index = i % _colorPalette.Length;
            _players[i] = new Player();
            _players[i].id = i;
            _players[i].material = new Material(baseMaterials[material_index]);
            _players[i].material.color = _colorPalette[color_index];
        }
    }

    public int PlayerAmount() {
        if (_players == null) return -1;
        return _players.Length;
    }

    public void SetSelfId(int id) {
        Debug.Log("Set self ID to " + id);
        _selfId = id;
    }

    public Player Self() {
        Assert.AreNotEqual(_selfId, -1);
        return GetPlayerFromId(_selfId);
    }

    public Player GetPlayerFromId(int id) {
        Assert.IsTrue(id >= 0);
        Assert.IsFalse(_players.Length == 0);
        return _players[id];
    }
}