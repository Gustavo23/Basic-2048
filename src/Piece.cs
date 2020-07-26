using Godot;
using System;

public class Piece : Node2D {

    [Export]
    public int value;
    [Export]
    public PackedScene nextValue;
    
    public override void _Ready() {
        
    }

}
