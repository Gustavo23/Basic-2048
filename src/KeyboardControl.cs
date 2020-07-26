using Godot;
using System;

public class KeyboardControl : Node2D {

    [Signal]
    delegate void Move();

    public override void _Ready() {
        
    }

    public override void _Process(float delta) {
        if(Input.IsActionJustPressed("down")) {
            EmitSignal("Move", Vector2.Down);
        }
        if(Input.IsActionJustPressed("left")) {
            EmitSignal("Move", Vector2.Left);
        }
        if(Input.IsActionJustPressed("up")) {
            EmitSignal("Move", Vector2.Up);
        }
        if(Input.IsActionJustPressed("right")) {
            EmitSignal("Move", Vector2.Right);
        }
    }

}
