using UnityEditor.Media;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Windows.WebCam;

public class Board : MonoBehaviour
{
        public Tilemap tilemap { get; private set; }
        public Piece activePiece { get; private set; }
        public TetrominoData[] tetrominoData;
        public Vector3Int spawnPosition;

        private void Awake()
        {
                this.tilemap = GetComponentInChildren<Tilemap>();
                this.activePiece = GetComponentInChildren<Piece>();
                
                for (int i = 0; i < this.tetrominoData.Length; i++)
                {
                        this.tetrominoData[i].Initalize();
                }
        }

        private void Start()
        {
                SpawnPiece();
        }

        public void SpawnPiece()
        {
                int random = Random.Range(0,this.tetrominoData.Length);
                TetrominoData data = this.tetrominoData[random];
                
                this.activePiece.Initialize(this,this.spawnPosition,data);
                Set(this.activePiece);
        }

        public void Set(Piece piece)
        {
                for (int i = 0; i < piece.cells.Length; i++)
                {
                        Vector3Int tilePosition = piece.cells[i] + piece.position;
                        this.tilemap.SetTile(tilePosition, piece.data.tile);
                }
        }
        
}
