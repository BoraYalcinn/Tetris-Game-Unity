using UnityEditor.Media;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Windows.WebCam;

public class Board : MonoBehaviour
{
        public Tilemap tilemap { get; private set; }
        
        public Piece activePiece { get; private set; }
        public TetrominoData[] tetrominoData;
        public  Vector3Int spawnPosition;

        public Vector2Int boardSize = new Vector2Int(10, 20);
        
        private float stepDelay = 1f;
        private float stepTime;

        public RectInt Bounds
        {
                get
                {
                        Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
                        return new RectInt(position,this.boardSize);
                }
        }
        
        private void Awake()
        {       
                
                this.tilemap = GetComponentInChildren<Tilemap>();
                this.activePiece = GetComponentInChildren<Piece>();
                
                for (int i = 0; i < this.tetrominoData.Length; i++)
                {
                        this.tetrominoData[i].Initalize();
                }
                
        }
        private void Update()
        {
                if (activePiece == null)
                        return;

                if (Time.time >= stepTime)
                {
                        stepTime = Time.time + stepDelay;
                        activePiece.Fall();
                }
        }
        
        public void Clear(Piece piece)
        {
                for (int i = 0; i < piece.cells.Length; i++)
                {
                        Vector3Int tilePosition = piece.cells[i] + piece.position;
                        tilemap.SetTile(tilePosition, null);
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
        public bool IsValidPosition(Piece piece, Vector3Int position)
        {
                RectInt bounds = this.Bounds;

                foreach (Vector3Int cell in piece.cells)
                {
                        Vector3Int tilePosition = position + cell; 

                        if (!bounds.Contains((Vector2Int)tilePosition))
                        {
                                return false;
                        }

                        if (this.tilemap.HasTile(tilePosition))
                        {
                                return false;
                        }
                }

                return true;
        }


        public void Lock(Piece piece)
        {
                for (int i = 0; i < piece.cells.Length; i++)
                {
                        Vector3Int tilePosition = piece.cells[i] + piece.position;
                        tilemap.SetTile(tilePosition, piece.data.tile);
                }
                
                Destroy(piece.gameObject);
                activePiece = null;
                SpawnPiece();
        }
}
