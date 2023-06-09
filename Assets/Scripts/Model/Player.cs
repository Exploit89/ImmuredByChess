﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player
{
    private List<GameObject> _pieces;
    private List<GameObject> _capturedPieces;
    private List<GameObject> _items;
    private Wallet _wallet;

    public string Name { get; private set; }
    public int Forward { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }

    public event UnityAction ItemsChanged;

    public Player(string name, bool positiveZMovement)
    {
        Name = name;
        Forward = positiveZMovement ? 1 : -1;
        _pieces = new List<GameObject>();
        _capturedPieces = new List<GameObject>();
        _items = new List<GameObject>();
        _wallet = new Wallet();
        Level = 1;
        Experience = 0;
    }

    public void AddPiece(GameObject pieceObject)
    {
        _pieces.Add(pieceObject);
    }

    public void AddCapturedPiece(GameObject pieceToCapture)
    {
        _capturedPieces.Add(pieceToCapture);
    }

    public bool ContainsPiece(GameObject piece)
    {
        return _pieces.Contains(piece);
    }

    public void IncreaseLevel()
    {
        Level++;
    }

    public void IncreaseExperience(int value)
    {
        Experience += value;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void AddMoney(int amount)
    {
        _wallet.IncreaseMoney(amount);
    }

    public void SpentMoney(int amount)
    {
        _wallet.DecreaseMoney(amount);
    }

    public int GetMoneyAmount()
    {
        return _wallet.MoneyAmount;
    }

    public void AddExperienceToPiece(GameObject piece, int value)
    {
        int pieceIndex = _pieces.IndexOf(piece);
        _pieces[pieceIndex].GetComponent<Unit>().IncreaseExperience(value);
    }

    public void AddItem(GameObject item)
    {
        _items.Add(item);
        ItemsChanged?.Invoke();
    }

    public List<GameObject> GetItems()
    {
        List<GameObject> items = _items;
        return items;
    }

    public void RemoveItem(int number)
    {
        _items.Remove(_items[number]);
        ItemsChanged?.Invoke();
    }
}
