using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPawnMoveScript : ChessPieceBase
{
    public string team;
    private bool isPieceSelected = false, hasStarted = false;
    private Vector3 targetPosition;
    private Material originalMaterial;
    public Material highlightMaterial; 

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }

    private void OnMouseDown()
    {
        if (MoveController.Instance.GetCurrentMove() == team)
        {
            DeselectAllPieces();
            isPieceSelected = true;
            HighlightPiece();
        }
    }

    private void Update()
    {
        if (isPieceSelected && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ChessCube") && IsLegalMove(transform.position, hit.collider.bounds.center))
                {
                    targetPosition = hit.collider.bounds.center; 
                    TeleportPiece();
                    isPieceSelected = false;
                    hasStarted = true;
                    RestoreMaterial();
                    MoveController.Instance.ChangeMove();
                }

            }
        }
    }

    private void TeleportPiece()
    {
        transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
    }

    private void HighlightPiece()
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = highlightMaterial;
        }
    }

    
    private bool IsPositionOccupied(Vector3 position)
    {

        Collider[] colliders = Physics.OverlapSphere(position, 0.7f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("ChessPiece"))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsLegalMove(Vector3 Pawn_position, Vector3 Cube_position) //Ïåðåâ³ðêà ëåãàëüíîñò³ ðóõó
    {
        int Pawn_Row = ChessCoordinates.PositionToRow(Pawn_position), Cube_Row = ChessCoordinates.PositionToRow(Cube_position);
        char Pawn_File = ChessCoordinates.PositionToFile(Pawn_position), Cube_File = ChessCoordinates.PositionToFile(Cube_position);
        if (team == "White")
        {
            if (!hasStarted && Pawn_File == Cube_File && Cube_Row - Pawn_Row < 3 && Cube_Row - Pawn_Row > 0)
                return true;

            if (IsPositionOccupied(Cube_position) && (Pawn_File == Cube_File + 1 || Pawn_File == Cube_File - 1) && Cube_Row - Pawn_Row == 1)
            {

                Debug.Log("Position Occupied, getting trying to find it....");
                ChessPieceBase[] allPieces = FindObjectsOfType<ChessPieceBase>();
                foreach (ChessPieceBase piece in allPieces)
                {
                    if (ChessCoordinates.PositionToFile(piece.transform.position) == Cube_File && ChessCoordinates.PositionToRow(piece.transform.position) == Cube_Row)
                    {
                        if (piece.GetTeam() == this.team) return false;
                        Destroy(piece.gameObject);
                        return true;
                    }
                }

            }
            if (Pawn_File == Cube_File && Cube_Row - Pawn_Row == 1) return true;
            Debug.Log("Invalid Pawn Move");
            return false;
        }
        else
        {
            if (!hasStarted && Pawn_File == Cube_File && Pawn_Row - Cube_Row < 3 && Pawn_Row - Cube_Row > 0)
                return true;

            if (IsPositionOccupied(Cube_position) && (Pawn_File == Cube_File + 1 || Pawn_File == Cube_File - 1) && Pawn_Row - Cube_Row == 1)
            {

                Debug.Log("Position Occupied, getting trying to find it....");
                ChessPieceBase[] allPieces = FindObjectsOfType<ChessPieceBase>();
                foreach (ChessPieceBase piece in allPieces)
                {
                    if (ChessCoordinates.PositionToFile(piece.transform.position) == Cube_File && ChessCoordinates.PositionToRow(piece.transform.position) == Cube_Row)
                    {
                        if (piece.GetTeam() == this.team) return false;
                        Destroy(piece.gameObject);
                        return true;
                    }
                }

            }
            if (Pawn_File == Cube_File && Pawn_Row - Cube_Row == 1) return true;
            Debug.Log("Invalid Pawn Move");
            return false;
        }
    }


    public override string GetTeam()
    {
        return team;
    }
    public override void SetTeam(string Team)
    {
       team = Team;
    }
    public override void RestoreMaterial()
    {
        isPieceSelected = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }
}
