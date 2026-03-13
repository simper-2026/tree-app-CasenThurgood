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
}
