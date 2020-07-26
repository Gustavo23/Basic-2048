using Godot;
using System;

public class KeyboardControl : Node2D {

    [Signal]
    delegate void Move();

    public override void _Ready() {
        
    }

}
