using UnityEngine;

public class HitPointBarCreator : MonoBehaviour
{
    [SerializeField] private GameObject _unitHitPointBarPrefab;
    [SerializeField] private InputName _inputName;

    private int _maxPiecesOnBoard = 32;
    private int _maxPiecesBySide = 16;

    private void AddHitPontBar(GameObject piece)
    {
        GameObject unitHitPointBar = Instantiate(_unitHitPointBarPrefab, gameObject.transform);
        unitHitPointBar.name = piece.name;
        unitHitPointBar.GetComponent<PieceHitPointView>().SetPiece(piece);
        unitHitPointBar.GetComponent<PieceHitPointView>().AddListener();
    }

    public void CreateHitPointBars()
    {
        GameObject playerPieces = GameObject.FindGameObjectWithTag("PlayerPieces");
        Transform[] playerPiecesTransform = playerPieces.GetComponentsInChildren<Transform>(true);
        GameObject enemyPieces = GameObject.FindGameObjectWithTag("EnemyPieces");
        Transform[] enemyPiecesTransform = enemyPieces.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < _maxPiecesOnBoard; i++)
        {
            if (i < _maxPiecesBySide)
            {
                AddHitPontBar(playerPiecesTransform[i + 1].gameObject);
            }
            else
            {
                AddHitPontBar(enemyPiecesTransform[i + 1 - _maxPiecesBySide].gameObject);
            }
        }
    }
}
