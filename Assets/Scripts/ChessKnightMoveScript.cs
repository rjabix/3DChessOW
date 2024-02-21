using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessKnightMoveScript : ChessPieceBase
{

    public string team;
    private bool isPieceSelected = false;
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

        Collider[] colliders = Physics.OverlapSphere(position, 0.55f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("ChessPiece"))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsLegalMove(Vector3 Knight_position, Vector3 Cube_position)
    {
        if (!IsKnightMove(Knight_position, Cube_position))
        {
            Debug.Log("Not a valid knight move");
            return false;
        }

        if (IsPositionOccupied(Cube_position))
        {
            Debug.Log("Position Occupied, trying to find it...");
            ChessPieceBase[] allPieces = FindObjectsOfType<ChessPieceBase>();
            foreach (ChessPieceBase piece in allPieces)
            {
                if (ChessCoordinates.PositionToFile(piece.transform.position) == ChessCoordinates.PositionToFile(Cube_position) && ChessCoordinates.PositionToRow(piece.transform.position) == ChessCoordinates.PositionToRow(Cube_position))
                {
                    if (piece.GetTeam() == this.team) return false;
                    Destroy(piece.gameObject);
                    return true;
                }
            }
        }

        return true;
    }

    private bool IsKnightMove(Vector3 knightPosition, Vector3 targetPosition)
    {
        int rowDiff = Mathf.Abs(ChessCoordinates.PositionToRow(targetPosition) - ChessCoordinates.PositionToRow(knightPosition));
        int fileDiff = Mathf.Abs(ChessCoordinates.PositionToFile(targetPosition) - ChessCoordinates.PositionToFile(knightPosition));

        // Хід коня складається з двох кроків по одному напрямку та одного по іншому, тому:
        // 1. Різниця в рядках повинна бути 2, а в стовпцях 1 або наоборот
        // 2. Різниця в рядках повинна бути 1, а в стовпцях 2 або наоборот
        return (rowDiff == 2 && fileDiff == 1) || (rowDiff == 1 && fileDiff == 2);
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
