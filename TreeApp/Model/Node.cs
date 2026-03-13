using System;
namespace TreeApp.Model;

public class Node
{
    public int Value { get; private set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }
    public int Depth;
    public int Height;


    public Node(int value, Node left, Node right, int depth)
    {
        Value = value;
        Left = left;
        Right = right;
        Depth = depth;
    }

    public void InsertValue(int newValue) 
    {
        if (newValue < Value)
        {
            if (Left == null)
            {
                Left = new Node(newValue, null, null, Depth+1);
            }
            else
            {
                Left.InsertValue(newValue);
            }
        }

        else if (newValue > Value)
        {
            if (Right == null)
            {
                Right = new Node(newValue, null, null, Depth+1);
            }
            else
            {
                Right.InsertValue(newValue);
            }
        }
    }
}