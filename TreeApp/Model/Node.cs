using System;

public class Node
{
    int Value { public get; private set; }
    Node? Left { public get; public set; }
    Node? Right { public get; public set; }


    public void Node(int Value, Node Left, Node Right)
    {
        this.Value = Value;
        this.Left = Left;
        this.Right = Right;
    }

    void InsertValue(int newValue) 
    {
        if newValue < this.Value
        {
            if (this.Left == null)
            {
                this.Left = new Node();
                this.Left.Node(newValue, null, null);
            }
            else
            {
                this.Left.InsertValue(newValue);
            }
        }
        else if newValue > this.Value
        {
            if (this.Right == null)
            {
                this.Right = new Node();
                this.Right.Node(newValue, null, null);
            }
            else
            {
                this.Right.InsertValue(newValue);
            }
        }
    }
}