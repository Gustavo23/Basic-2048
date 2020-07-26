using Godot;
using System;

public class TouchControl : Node2D {

    [Signal]
    delegate void Move();

    public override void _Ready() {
        
    }

}
