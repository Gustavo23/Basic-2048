using Godot;
using System;

public class Piece : Node2D {

    [Export]
    public int value;
    [Export]
    public PackedScene nextValue;

    public Tween moveTween;
    
    public override void _Ready() {
        moveTween = GetNode<Tween>("MoveTween");
    }

    public void move(Vector2 newPosition) {
        moveTween.InterpolateProperty(this, "position", Position, newPosition, 0.3f, Tween.TransitionType.Bounce, Tween.EaseType.Out);
        moveTween.Start();
    }

    public void remove() {
        QueueFree();
    }
}
