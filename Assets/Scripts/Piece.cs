using UnityEngine;

public class Piece : MonoBehaviour
{
        public Board board;
        public TetrominoData data { get; private set; }
        public Vector3Int[] cells { get; private set; }
        public Vector3Int position { get; private set; }
        
        public void Initialize(Board board,Vector3Int position, TetrominoData data)
        {
                this.board = board;
                this.position = position;
                this.data = data;

                if (this.cells == null)
                {
                        this.cells = new Vector3Int[data.cells.Length];
                }

                for (int i = 0; i < data.cells.Length; i++)
                {
                        this.cells[i] = (Vector3Int)data.cells[i];
                }
        }
        
        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.A))
                {
                        Move(Vector2Int.left);
                }else if (Input.GetKeyDown(KeyCode.D))
                {
                        Move(Vector2Int.right);
                }
        }

        private bool Move(Vector2Int translation)
        {
                Vector3Int newPosition = this.position;
                newPosition.x += translation.x;
                newPosition.y += translation.y;

                bool valid = this.board.IsValidPosition(this,newPosition);

                if (valid)
                {
                        this.position = newPosition;
                }

                return valid;
        }
        
        
        public void Fall()
        {
                Vector3Int nextPosition = position + Vector3Int.down;

                board.Clear(this);

                if (board.IsValidPosition(this, nextPosition))
                {
                        position = nextPosition;
                }
                else
                {
                        board.Lock(this);
                        return;
                }

                board.Set(this);
        }
        
}
