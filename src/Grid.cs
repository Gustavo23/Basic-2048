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

    public override void _Ready() {
        board = new object[width, height];
        generateBackground();
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

    public Vector2 moveAllPieces(Vector2 direction) {
        return new Vector2(0,0);
    }

    public void generateNewPiece() {
        if (isBlankSpace()) {
            // TODO
        } else {
            GD.Print("No more room!");
        }
    }

    public void _on_TouchControl_Move(Vector2 direction) {
        moveAllPieces(direction);
    }

    public void _on_KeyboardControl_Move(Vector2 direction) {
        moveAllPieces(direction);
    }

}
