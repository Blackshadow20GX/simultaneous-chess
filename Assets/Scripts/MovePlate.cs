using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Board positions, not world positions
    int matrixX;
    int matrixY;

    public bool isAttacking = false;

    public void Start()
    {
        if (isAttacking)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(214, 60, 60);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (isAttacking)
        {
            GameObject chessPiece = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            Destroy(chessPiece);
        }

        controller.GetComponent<Game>().SetPositionEmpty(
            reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard()
        );

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
