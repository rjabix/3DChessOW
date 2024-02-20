using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardEmptyScript : MonoBehaviour
{

    public GameObject tilePrefab, pawnPrefab, officerPrefab; // Declare the Prefabs
    public Material lightSquareMaterial;
    public Material darkSquareMaterial;

    private void Start()
    {
        CreateGraphicalBoard();
        CreatePawns();
        CreateOfficers();
    }

    private void Update()
    {

    }

    private void CreateGraphicalBoard()
    {
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                bool isLightSquare = (file + rank) % 2 != 0;

                var squareColour = (isLightSquare) ? lightSquareMaterial : darkSquareMaterial;
                var position = new Vector3(-3.5f + file, 0, -3.5f + rank);

                DrawSquare(squareColour, position);
            }
        }
    }




    private void DrawSquare(Material squareColour, Vector3 position)
    {

        GameObject tile = Instantiate(tilePrefab, new Vector3(position.x, 0, position.z), Quaternion.identity);
        tile.tag = "ChessCube";
        tile.name = "" + ChessCoordinates.PositionToFile(position) + ChessCoordinates.PositionToRow(position).ToString();
        Renderer renderer = tile.GetComponent<Renderer>();
        renderer.material = squareColour;
    }

    private void CreatePawns()
    {
        //White
        for(int file = 0; file < 8; file++)
        {
            GameObject pawn = Instantiate(pawnPrefab, new Vector3(-3.5f + file, 1, -2.5f), Quaternion.identity);
            pawn.tag = "ChessPiece";
            Renderer renderer = pawn.GetComponent<Renderer>();
            ChessPawnMoveScript pawnScript = pawn.GetComponent<ChessPawnMoveScript>();
            pawnScript.SetTeam("White");
        }
        //Black ...{}
    }

    private void CreateOfficers()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject officer = Instantiate(officerPrefab, new Vector3(-1.5f + i * 3f, 1, -3.5f), Quaternion.identity);
            officer.tag = "ChessPiece";
            Renderer renderer = officer.GetComponent<Renderer>();
            ChessOfficerMoveScript officerScript = officer.GetComponent<ChessOfficerMoveScript>();
            officerScript.SetTeam("White");
        }
        
    }
}