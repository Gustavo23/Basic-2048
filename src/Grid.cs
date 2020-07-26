using Godot;
using System;

public class Grid : Node2D {

    [Export]
    public PackedScene twoPiece;
    [Export]
    public PackedScene fourPiece;
    [Export]
    public PackedScene backgroundPiece;

    public int width = 4;
    public int height = 4;
    public float xStart = 96;
    public float yStart = 912;
    public float offset = 128;
    public object[,] board;

    private Random _random = new Random();

    public override void _Ready() {
        board = new object[width, height];
        generateBackground();
        generateNewPiece();
    }

    public void generateBackground() {
        for(var i=0; i<width; i++) {
            for(var j=0; j<height; j++) {
                Node2D aux = (Node2D)backgroundPiece.Instance();
                AddChild(aux);
                aux.Position = gridToPixel(new Vector2(i,j));
            }
        }
    }

    public Vector2 gridToPixel(Vector2 gridPosition) {
        float newX = xStart + offset * gridPosition.x;
        float newY = yStart + -offset * gridPosition.y;
        return new Vector2(newX, newY);      
    }

    public Vector2 pixelToGrid(Vector2 pixelPosition) {
        float newX = Mathf.Round(pixelPosition.x - xStart) / offset;
        float newY = Mathf.Round(pixelPosition.y - yStart) / -offset;
        return new Vector2(newX, newY);      
    }

    public bool isInGrid(Vector2 gridPosition) {
        if(gridPosition.x >= 0 && gridPosition.x < width
        && gridPosition.y >=0 && gridPosition.y < height) {
            return true;
        }
        return false;
    }

    public bool isBlankSpace() {
        for(var i=0; i<width; i++) {
            for(var j=0; j<height; j++) {
                if (board[i, j] == null) {
                    return true;
                }
            }
        }
        return false;
    }

    public void moveAllPieces(Vector2 direction) {
        if(direction == Vector2.Up) {

        } else if(direction == Vector2.Right) {
            for(int i=0; i<width-1; i++) {
                for(int j=0; j<height; j++) {
                    if(board[i, j] != null) {
                        movePiece(new Vector2(i,j), Vector2.Right);
                    }
                }
            }
        } else if(direction == Vector2.Down) {
            
        } else if(direction == Vector2.Left) {
            for(int i=width-1; i>0; i--) {
                for(int j=0; j<height; j++) {
                    if(board[i, j] != null) {
                        GD.Print("i e j:"+ i + ' ' + j);
                        movePiece(new Vector2(i,j), Vector2.Left);
                    }
                }
            }
        } else {
            // TODO
        }
    }

    public void moveAndSetBoardValue(Vector2 currentPosition, Vector2 newPosition) {
        Piece temp = board[(int)currentPosition.x, (int)currentPosition.y] as Piece;
        temp.move(gridToPixel(new Vector2(newPosition.x, newPosition.y)));
        board[(int)currentPosition.x, (int)currentPosition.y] = null;
        board[(int)newPosition.x, (int)newPosition.y] = temp;
    }

    public void movePiece(Vector2 piece, Vector2 direction) {
        Piece thisPiece = board[(int)piece.x, (int)piece.y] as Piece;
        int thisValue = thisPiece.value;
        Vector2 nextSpace = new Vector2((int)piece.x + direction.x, (int)piece.y + direction.y);
        PackedScene nextValue = (board[(int)nextSpace.x, (int)nextSpace.y] as PackedScene);

        if(direction == Vector2.Right) {
            for(int i=(int)nextSpace.x; i<width; i++) {
                // If it's the end of the board, and that spot is null
                if(i == width - 1 && board[i, (int)piece.y] == null) {
                    moveAndSetBoardValue(piece, new Vector2(width-1, piece.y));
                }
                // If this spot is full, and the value is not the same, then move to one before it
                else if(board[i, (int)piece.y] != null && (board[i, (int)piece.y] as Piece).value != thisValue) {
                    // Move to one before it
                    moveAndSetBoardValue(piece, new Vector2(i-1, piece.y));
                }
                // Otherwise, if it's the same:
                else if(board[i, (int)piece.y] != null && (board[i, (int)piece.y] as Piece).value == thisValue) {
                    PackedScene tempValue = (board[i, (int)piece.y] as Piece).nextValue;
                    removeAndClear(piece);
                    removeAndClear(new Vector2(i, piece.y));
                    Piece newPiece = tempValue.Instance() as Piece;
                    AddChild(newPiece);
                    board[i, (int)piece.y] = newPiece;
                    newPiece.Position = gridToPixel(new Vector2(i, (int)piece.y));
                }
            }
        }
        if(direction == Vector2.Left) {
            for(int i=(int)nextSpace.x; i>-1; i--) {
                // If it's the end of the board, and that spot is null
                if(i == 0 && board[i, (int)piece.y] == null) {
                    moveAndSetBoardValue(piece, new Vector2(0, piece.y));
                }
                // If this spot is full, and the value is not the same, then move to one before it
                else if(board[i, (int)piece.y] != null && (board[i, (int)piece.y] as Piece).value != thisValue) {
                    // Move to one before it
                    moveAndSetBoardValue(piece, new Vector2(i+1, piece.y));
                }
                // Otherwise, if it's the same value:
                else if(board[i, (int)piece.y] != null && (board[i, (int)piece.y] as Piece).value == thisValue) {
                    PackedScene tempValue = (board[i, (int)piece.y] as Piece).nextValue;
                    removeAndClear(piece);
                    removeAndClear(new Vector2(i, piece.y));
                    Piece newPiece = tempValue.Instance() as Piece;
                    AddChild(newPiece);
                    board[i, (int)piece.y] = newPiece;
                    newPiece.Position = gridToPixel(new Vector2(i, (int)piece.y));
                }
            }
        }
        if(direction == Vector2.Up) {
            // todo
        }
        if(direction == Vector2.Down) {
            // todo
        }
    }

    public void removeAndClear(Vector2 piece) {
        if(board[(int)piece.x, (int)piece.y] != null) {
            (board[(int)piece.x, (int)piece.y] as Piece).remove();
        }
        board[(int)piece.x, (int)piece.y] = null;

    }

    public void generateNewPiece() {
        if (isBlankSpace()) {
            int piecesMade = 0;
            while (piecesMade < 2) {
                int xPosition = (int)(Math.Floor(RandRange(0,4)));
                int yPosition = (int)(Math.Floor(RandRange(0,4)));
                if(board[xPosition, yPosition] == null) {
                    Node2D temp = (Node2D)generateTwoOrFour().Instance();
                    AddChild(temp);
                    board[xPosition, yPosition] = temp;
                    temp.Position = gridToPixel(new Vector2(xPosition, yPosition));
                    piecesMade += 1;
                }
            }
        } else {
            GD.Print("No more room!");
        }
    }

    public PackedScene generateTwoOrFour() {
        var temp = RandRange(0,100);
        if(temp <= 75) {
            return twoPiece;
        } else {
            return fourPiece;
        }
    }

    public void _on_TouchControl_Move(Vector2 direction) {
        moveAllPieces(direction);
    }

    public void _on_KeyboardControl_Move(Vector2 direction) {
        moveAllPieces(direction);
    }

    private float RandRange(float min, float max) {
        return (float)_random.NextDouble() * (max-min) + min;
    }

}
