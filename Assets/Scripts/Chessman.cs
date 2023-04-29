using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // References
    public GameObject controller;
    public GameObject movePlate;

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;
    
    // Sprites
    public Sprite blackQueen, blackKnight, blackBishop, blackKing, blackRook, blackPawn;
    public Sprite whiteQueen, whiteKnight, whiteBishop, whiteKing, whiteRook, whitePawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Take the instantiated location and adjust the transform
        SetCoords();

        Sprite sprite = null;

        switch (this.name)
        {
            case "blackQueen":  sprite = blackQueen; break;
            case "blackKnight": sprite = blackKnight; break;
            case "blackBishop": sprite = blackBishop; break;
            case "blackKing": sprite = blackKing; break;
            case "blackRook": sprite = blackRook; break;
            case "blackPawn": sprite = blackPawn; break;
            case "whiteQueen": sprite = whiteQueen; break;
            case "whiteKnight": sprite = whiteKnight; break;
            case "whiteBishop": sprite = whiteBishop; break;
            case "whiteKing": sprite = whiteKing; break;
            case "whiteRook": sprite = whiteRook; break;
            case "whitePawn": sprite = whitePawn; break;
        }

        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        // Magic numbers
        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }
}
