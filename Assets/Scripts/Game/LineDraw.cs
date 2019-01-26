﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    public ConnectionManager connectionManager;

    private GameObject _player1;
    private GameObject _player2;

    public void StoreReferences(GameObject p1, GameObject p2)
    {
        _player1 = p1;
        _player2 = p2;
    }

    //Cycles through and draws all the necessary lines per frame update for whatever lines need to be drawn
    public void DrawLines(List<GameObject> people, bool people1)
    {
        GameObject refPlayer;
        if (people1)
        {
            refPlayer = _player1;
        }
        else
        {
            refPlayer = _player2;
        }

        foreach (GameObject person in people)
        {
            LineRenderer lineRenderer = person.GetComponent<LineRenderer>();

            //Only do an update if the line renderer is enabled
            if (lineRenderer.enabled)
            {
                var points = new Vector3[2];
                points[0] = person.transform.position;
                points[1] = refPlayer.transform.position;
                lineRenderer.SetPositions(points);
            }
        }

        //With all that done, check to see if we need to draw a point to the mouse pointer
        DrawLineToMousePointer();
    }

    private void DrawLineToMousePointer()
    {
        if (connectionManager.playerSelected)
        {
            //Get the mouse location relative to the screen
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);

            //Get the current player object and reactivate its line renderer
            GameObject curPlayer = connectionManager.currentPlayer;
            LineRenderer lineRenderer = curPlayer.GetComponent<LineRenderer>();
            lineRenderer.enabled = true;

            //Draw the line
            var points = new Vector3[2];
            points[0] = curPlayer.transform.position;
            points[1] = mouseWorld;
            lineRenderer.SetPositions(points);
        }
    }
}