using Godot;
using System;

public class Piece : Node2D {

    [Export]
    public int value;
    [Export]
    public PackedScene nextValue;

    public Tween moveTween;
    public Tween sizeTween;
    public Tween modulateTween;
    
    public override void _Ready() {
        moveTween = GetNode<Tween>("MoveTween");
        sizeTween = GetNode<Tween>("SizeTween");
        modulateTween = GetNode<Tween>("ModulateTween");
        popIn();
    }

    public void popIn() {
        sizeTween.InterpolateProperty(this, "scale", new Vector2(0.1f, 0.1f), new Vector2(1f, 1f), 0.2f, Tween.TransitionType.Sine, Tween.EaseType.Out);
        sizeTween.Start();
    }

    public void move(Vector2 newPosition) {
        moveTween.InterpolateProperty(this, "position", Position, newPosition, 0.3f, Tween.TransitionType.Bounce, Tween.EaseType.Out);
        moveTween.Start();
    }

    public void remove() {
        sizeTween.InterpolateProperty(this, "scale", Scale, new Vector2(1.5f, 1.5f), 0.2f, Tween.TransitionType.Sine, Tween.EaseType.Out);
        modulateTween.InterpolateProperty(this, "modulate", Modulate, new Color(0,0,0,0), 0.2f, Tween.TransitionType.Sine, Tween.EaseType.Out);
        sizeTween.Start();
        modulateTween.Start();
        QueueFree();
    }

    public void _on_ModulateTween_tween_completed(object obj, NodePath key) {
        if(Scale == new Vector2(1.5f, 1.5f)) {
            QueueFree();
        }
    }
}
