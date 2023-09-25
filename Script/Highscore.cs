using Godot;
using System;
using System.Xml.Schema;

public class Highscore : Node
{
    private int highscore;

    public int PlayerHighscore
    {
        set {highscore = value;}
        get {return highscore;}
    }
}
