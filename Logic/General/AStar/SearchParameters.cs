using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeisterGeister.Logic.General.AStar
{
    public class SearchParameters
    {
        public Size Boundaries { get; private set; }
        public Node StartNode { get; private set; }
        public Node EndNode { get; private set; }

        public SearchParameters(Size boundaries, Node startNode, Node endNode)
        {
            this.Boundaries = boundaries;
            this.EndNode = endNode;
            this.StartNode = startNode;
        }
    }
}
