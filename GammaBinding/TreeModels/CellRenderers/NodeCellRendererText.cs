﻿using System;
using System.Collections.Generic;
using Gtk;
using System.Reflection;
using Gamma.Binding;
using System.Text.RegularExpressions;

namespace Gamma.GtkWidgets.Cells
{
	public class NodeCellRendererText<TNode> : CellRendererText, INodeCellRendererHighlighter
	{
		public List<Action<NodeCellRendererText<TNode>, TNode>> LambdaSetters = new List<Action<NodeCellRendererText<TNode>, TNode>>();

		public string DataPropertyName { get{ return DataPropertyInfo.Name;
			}}

		public bool SearchHighlight { get; set;}

		public PropertyInfo DataPropertyInfo { get; set;}

		public IValueConverter EditingValueConverter { get; set;}

		public NodeCellRendererText ()
		{
		}

		public void RenderNode(object node)
		{
			if(node is TNode)
			{
				var typpedNode = (TNode)node;
				LambdaSetters.ForEach (a => a.Invoke (this, typpedNode));
			}
		}

		public void RenderNode(object node, string searchHighlightText)
		{
			RenderNode(node);
			if (SearchHighlight && !String.IsNullOrEmpty(searchHighlightText))
			{
				string pattern = String.Format("{0}", Regex.Escape(searchHighlightText.ToLower()));
				if(!String.IsNullOrEmpty(Text))
					Markup = Regex.Replace(Text, pattern, (match) => String.Format("<b>{0}</b>", match.Value), RegexOptions.IgnoreCase);
			}
		}
	}
}

