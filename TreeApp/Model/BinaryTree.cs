using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Swift;
using Microsoft.AspNetCore.Mvc.Infrastructure;
namespace TreeApp.Model;

public class BinaryTree
{
	public Node? root { get; private set; }

	public BinaryTree()
	{

	}

	public void Insert(int value)
	{
		if (root == null)
		{
			root = new Node(value, null, null, 0);

		}
		else
		{
			root.InsertValue(value);
			root = RebalanceTree(root);
		}
	}

	string InOrder()
	{
		return " ";
	}

	int Height()
	{
		return CalculateHeight(root);
	}

	private int CalculateHeight(Node? node) 
	{
		if (node == null) 
		{
			return -1;
		}

		node.Height = 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
        return node.Height;

	}

	public string ToMermaid() 
	{
        int links = 0;
		int height = CalculateHeight(root);

		if (root == null)
		{
			return "graph TD;\n";
		}
		else if (root.Left == null && root.Right == null)
		{
			return $"graph TD;\n {root.Value}";
		}
		else 
		{
			return $"graph TD\n {ToMermaid(root, ref links)}";
		}
	}

	private string ToMermaid(Node? node, ref int links)
	{

		if (node == null || (node.Right == null && node.Left == null)) 
		{
			return string.Empty;
		}

		string result = string.Empty;

		if (node.Left != null)
		{
			result += $"{node.Value} --> {node.Left.Value}[{node.Left.Value} h:{node.Left.Height} d:{node.Left.Depth}]\n";
			links++;
			result += ToMermaid(node.Left, ref links);
			
		}
		else 
		{
            result += $"{node.Value} --> _phl{node.Value}[ ] \n";
			result += $"linkStyle {links} stroke:none, stroke-width:0, fill:none\n";
			result += $"style _phl{node.Value} fill:none, stroke:none, color:none\n";
			links++;
        }

        
		if (node.Right != null)
        {
            result += $"{node.Value} --> {node.Right.Value}[{node.Right.Value} h:{node.Right.Height} d:{node.Right.Depth}]\n";
            links++;
			result += ToMermaid(node.Right, ref links);
        }
        else
        {
            result += $"{node.Value} --> _phr{node.Value}[ ] \n";
			result += $"linkStyle {links} stroke:none, stroke-width:0, fill:none\n";
            result += $"style _phr{node.Value} fill:none, stroke:none, color:none\n";
			links++;
        }

		return result;
	}

	Node RebalanceTree(Node node)
	{
		if (node == null)
		{
			return null;
		}

		if (node.Left == null && node.Right == null) 
		{
			return node;
		}
		int leftHeight;

		if (node.Left != null)
		{
			node.Left = RebalanceTree(node.Left);
			leftHeight = node.Left.Height;
		}
		else 
		{
			leftHeight = -1;
		}

		int rightHeight;
		if (node.Right != null)
		{
			node.Right = RebalanceTree(node.Right);
			rightHeight = node.Right.Height;
		}
		else
		{
			rightHeight = -1;
		}

        if (leftHeight - rightHeight > 1) 
		{
			int leftRightHeight;
			int leftLeftHeight;

			if (node.Left.Left != null)
			{
				leftLeftHeight = node.Left.Left.Height;
			}
			else 
			{
				leftLeftHeight = -1;
			}

			if (node.Left.Right != null)
			{
				leftRightHeight = node.Left.Right.Height;
			}
			else 
			{
				leftRightHeight = -1;
			}

			if (leftRightHeight > leftLeftHeight)
			{
				node.Left = RotateLeft(node.Left);
			}

			return RotateRight(node);
		}

		if (rightHeight - leftHeight > 1) 
		{
            int rightLeftHeight;
            int rightRightHeight;

            if (node.Right.Left != null)
            {
                rightLeftHeight = node.Right.Left.Height;
            }
            else
            {
                rightLeftHeight = -1;
            }

            if (node.Right.Right != null)
            {
                rightRightHeight = node.Right.Right.Height;
            }
            else
            {
                rightRightHeight = -1;
            }

            if (rightLeftHeight > rightRightHeight)
            {
                node.Right = RotateRight(node.Right);
            }

            return RotateLeft(node);
		}

		return node;
	}

    Node RotateRight(Node node)
    {
        Node l = node.Left;
        Node r = l.Right;
        l.Right = node;
        node.Left = r;
        return l;
    }

    Node RotateLeft(Node node)
    {
        Node r = node.Right;
        Node l = r.Left;
        r.Left = node;
        node.Right = l;
        return r;
    }
}
