using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardEmptyScript : MonoBehaviour
{

    public GameObject tilePrefab, pawnPrefab, officerPrefab, rookPrefab, knightPrefab, queenPrefab, kingPrefab; // Declare the Prefabs
    public Material lightSquareMaterial, darkSquareMaterial, lightPieceMaterial, darkPieceMaterial;

    private void Start()
    {
        CreateGraphicalBoard();
        CreatePawns();
        CreateOfficers();
        CreateRooks();
        CreateKnights();
        CreateQueens();
        CreateKings();
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

    //public void StartANewGame()
    //{
    //    ChessPieceBase[] allPieces = FindObjectsOfType<ChessPieceBase>();
    //    if (allPieces.Length != 0)
    //    {
    //        foreach (ChessPieceBase piece in allPieces)
    //        {
    //            Destroy(piece.gameObject);
    //        }
    //    }
    //    CreatePawns();
    //    CreateOfficers();
    //    CreateRooks();
    //    CreateKnights();
    //    CreateQueens();
    //    CreateKings();
    //}

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
        for (int file = 0; file < 8; file++)
        {
            GameObject pawn = Instantiate(pawnPrefab, new Vector3(-3.5f + file, 1, -2.5f), Quaternion.identity);
            pawn.tag = "ChessPiece";
            Renderer renderer = pawn.GetComponent<Renderer>();
            renderer.material = lightPieceMaterial;
            ChessPawnMoveScript pawnScript = pawn.GetComponent<ChessPawnMoveScript>();
            pawnScript.SetTeam("White");
        }
        // Black
        for (int file = 0; file < 8; file++)
        {
            GameObject pawnBlack = Instantiate(pawnPrefab, new Vector3(-3.5f + file, 1, 2.5f), Quaternion.identity);
            pawnBlack.tag = "ChessPiece";
            Renderer rendererBlack = pawnBlack.GetComponent<Renderer>();
            rendererBlack.material = darkPieceMaterial;
            ChessPawnMoveScript pawnScriptBlack = pawnBlack.GetComponent<ChessPawnMoveScript>();
            pawnScriptBlack.SetTeam("Black");
            pawnBlack.transform.Rotate(Vector3.up, 180f);
        }
    }

    private void CreateOfficers()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject officer = Instantiate(officerPrefab, new Vector3(-1.5f + i * 3f, 1, -3.5f), Quaternion.identity);
            officer.tag = "ChessPiece";
            Renderer renderer = officer.GetComponent<Renderer>();
            renderer.material = lightPieceMaterial;
            ChessOfficerMoveScript officerScript = officer.GetComponent<ChessOfficerMoveScript>();
            officerScript.SetTeam("White");
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject officer = Instantiate(officerPrefab, new Vector3(-1.5f + i * 3f, 1, 3.5f), Quaternion.identity);
            officer.tag = "ChessPiece";
            Renderer renderer = officer.GetComponent<Renderer>();
            renderer.material = darkPieceMaterial;
            ChessOfficerMoveScript officerScript = officer.GetComponent<ChessOfficerMoveScript>();
            officerScript.SetTeam("Black");
            officer.transform.Rotate(Vector3.up, 180f);
        }

    }

    private void CreateRooks()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject rook = Instantiate(rookPrefab, new Vector3(-3.5f + 7 * i, 1, -3.5f), Quaternion.identity);
            rook.tag = "ChessPiece";
            Renderer renderer = rook.GetComponent<Renderer>();
            renderer.material = lightPieceMaterial;
            ChessCastleMoveScript rookScript = rook.GetComponent<ChessCastleMoveScript>();
            rookScript.SetTeam("White");
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject rook = Instantiate(rookPrefab, new Vector3(-3.5f + 7 * i, 1, 3.5f), Quaternion.identity);
            rook.tag = "ChessPiece";
            Renderer renderer = rook.GetComponent<Renderer>();
            renderer.material = darkPieceMaterial;
            ChessCastleMoveScript rookScript = rook.GetComponent<ChessCastleMoveScript>();
            rookScript.SetTeam("Black");
            rook.transform.Rotate(Vector3.up, 180f);
        }
    }

    private void CreateKnights()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject knight = Instantiate(knightPrefab, new Vector3(-2.5f + i * 5, 1, -3.5f), Quaternion.identity);
            knight.tag = "ChessPiece";
            Renderer renderer = knight.GetComponent<Renderer>();
            renderer.material = lightPieceMaterial;
            ChessKnightMoveScript knightScript = knight.GetComponent<ChessKnightMoveScript>();
            knightScript.SetTeam("White");
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject knight = Instantiate(knightPrefab, new Vector3(-2.5f + i * 5, 1, 3.5f), Quaternion.identity);
            knight.tag = "ChessPiece";
            Renderer renderer = knight.GetComponent<Renderer>();
            renderer.material = darkPieceMaterial;
            ChessKnightMoveScript knightScript = knight.GetComponent<ChessKnightMoveScript>();
            knightScript.SetTeam("Black");
            knight.transform.Rotate(Vector3.up, 180f);
        }
    }

    private void CreateQueens()
    {
        //White
        GameObject queen = Instantiate(queenPrefab, new Vector3(-0.5f, 1, -3.5f), Quaternion.identity);
        queen.tag = "ChessPiece";
        Renderer renderer = queen.GetComponent<Renderer>();
        renderer.material = lightPieceMaterial;
        ChessQueenMoveScript queenScript = queen.GetComponent<ChessQueenMoveScript>();
        queenScript.SetTeam("White");
        //Black
        queen = Instantiate(queenPrefab, new Vector3(-0.5f, 1, 3.5f), Quaternion.identity);
        queen.tag = "ChessPiece";
        renderer = queen.GetComponent<Renderer>();
        renderer.material = darkPieceMaterial;
        queenScript = queen.GetComponent<ChessQueenMoveScript>();
        queenScript.SetTeam("Black");
        queen.transform.Rotate(Vector3.up, 180f);
    }

    private void CreateKings()
    {
        //White
        GameObject king = Instantiate(kingPrefab, new Vector3(0.5f, 1, -3.5f), Quaternion.identity);
        king.tag = "ChessPiece";
        Renderer renderer = king.GetComponent<Renderer>();
        renderer.material = lightPieceMaterial;
        ChessKingMoveScript kingScript = king.GetComponent<ChessKingMoveScript>();
        kingScript.SetTeam("White");
        //Black
        king = Instantiate(kingPrefab, new Vector3(0.5f, 1, 3.5f), Quaternion.identity);
        king.tag = "ChessPiece";
        renderer = king.GetComponent<Renderer>();
        renderer.material = darkPieceMaterial;
        kingScript = king.GetComponent<ChessKingMoveScript>();
        kingScript.SetTeam("Black");
        king.transform.Rotate(Vector3.up, 180f);
    }
}