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

    private Game.PLAYER player;

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
            case "blackQueen":  sprite = blackQueen; player = Game.PLAYER.BLACK; break;
            case "blackKnight": sprite = blackKnight; player = Game.PLAYER.BLACK; break;
            case "blackBishop": sprite = blackBishop; player = Game.PLAYER.BLACK; break;
            case "blackKing": sprite = blackKing; player = Game.PLAYER.BLACK; break;
            case "blackRook": sprite = blackRook; player = Game.PLAYER.BLACK; break;
            case "blackPawn": sprite = blackPawn; player = Game.PLAYER.BLACK; break;
            case "whiteQueen": sprite = whiteQueen; player = Game.PLAYER.WHITE; break;
            case "whiteKnight": sprite = whiteKnight; player = Game.PLAYER.WHITE; break;
            case "whiteBishop": sprite = whiteBishop; player = Game.PLAYER.WHITE; break;
            case "whiteKing": sprite = whiteKing; player = Game.PLAYER.WHITE; break;
            case "whiteRook": sprite = whiteRook; player = Game.PLAYER.WHITE; break;
            case "whitePawn": sprite = whitePawn; player = Game.PLAYER.WHITE; break;
        }

        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetCoords()
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

    private void OnMouseUp()
    {
        if(!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }
    }

    internal void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    private void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "whiteQueen":
            case "blackQueen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;

            case "whiteKnight":
            case "blackKnight":
                LMovePlate();
                break;
            case "whiteBishop":
            case "blackBishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "whiteKing":
            case "blackKing":
                SurroundMovePlate();
                break;
            case "whiteRook":
            case "blackRook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "blackPawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
        }
    }

    private void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game game = controller.GetComponent<Game>();
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while(game.PositionOnBoard(x, y) && game.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (game.PositionOnBoard(x, y) && game.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateSpawn(x, y, true);
        }
    }


    private void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    private void SurroundMovePlate()
    {
        PointMovePlate(xBoard - 0, yBoard + 1);
        PointMovePlate(xBoard - 0, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    private void PointMovePlate(int x, int y)
    {
        Game game = controller.GetComponent<Game>();
        if (game.PositionOnBoard(x, y))
        {
            GameObject chessPiece = game.GetPosition(x, y);
            if(chessPiece == null)
            {
                MovePlateSpawn(x, y);
            } else if (chessPiece.GetComponent<Chessman>().player != player)
            {
                MovePlateSpawn(x, y, true);
            }
        }
    }

    private void PawnMovePlate(int x, int y)
    {
        Game game = controller.GetComponent<Game>();
        if (game.PositionOnBoard(x, y))
        {
            if(game.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if(game.PositionOnBoard(x + 1, y) 
                && game.GetPosition(x + 1, y) != null 
                && game.GetPosition(x+1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateSpawn(x + 1, y, true);
            }

            if (game.PositionOnBoard(x - 1, y)
                && game.GetPosition(x - 1, y) != null
                && game.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateSpawn(x - 1, y, true);
            }
        }
    }

    private void MovePlateSpawn(int matrixX, int matrixY, bool isAttacking = false)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject renderedMovePlate = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate movePlateScript = renderedMovePlate.GetComponent<MovePlate>();
        movePlateScript.SetReference(gameObject);
        movePlateScript.isAttacking = isAttacking;
        movePlateScript.SetCoords(matrixX, matrixY);
    }
}
