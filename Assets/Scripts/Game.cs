using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;

    // Positions and team for each chess piece
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    public enum PLAYER
    {
        WHITE,
        BLACK
    };

    private PLAYER currentPlayer = PLAYER.WHITE;
    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("whiteRook", 0, 0), Create("whiteKnight", 1, 0), Create("whiteBishop", 2, 0), 
            Create("whiteQueen", 3, 0), Create("whiteKing", 4, 0), Create("whiteBishop", 5, 0),
            Create("whiteKnight", 6, 0), Create("whiteRook", 7, 0),
            Create("whitePawn", 0, 1), Create("whitePawn", 1, 1), Create("whitePawn", 2, 1),
            Create("whitePawn", 3, 1), Create("whitePawn", 4, 1), Create("whitePawn", 5, 1),
            Create("whitePawn", 6, 1), Create("whitePawn", 7, 1)
        };

        playerBlack = new GameObject[]
        {
            Create("blackRook", 0, 7), Create("blackKnight", 1, 7), Create("blackBishop", 2, 7),
            Create("blackQueen", 3, 7), Create("blackKing", 4, 7), Create("blackBishop", 5, 7),
            Create("blackKnight", 6, 7), Create("blackRook", 7, 7),
            Create("blackPawn", 0, 6), Create("blackPawn", 1, 6), Create("blackPawn", 2, 6),
            Create("blackPawn", 3, 6), Create("blackPawn", 4, 6), Create("blackPawn", 5, 6),
            Create("blackPawn", 6, 6), Create("blackPawn", 7, 6)
        };

        // Set all place positions on the position board
        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        }

        
    }

    public void SetPosition(GameObject obj)
    {
        Chessman chessman = obj.GetComponent<Chessman>();
        positions[chessman.GetXBoard(), chessman.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        return !(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1));
    }

    private GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman chessman = obj.GetComponent<Chessman>();
        chessman.name = name;
        chessman.SetXBoard(x);
        chessman.SetYBoard(y);
        chessman.Activate();
        return obj;
    }
}
