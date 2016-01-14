using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General.AStar
{
    public class Node
    {
        public Point Location { get; private set; }
        public bool IsWalkable { get; set; }
        public double LengthFromStart { get; private set; }
        public double LengthToEnd { get; private set; }
        public double LengthFromStartToEnd { get { return this.LengthFromStart + this.LengthToEnd; } }
        public NodeState State { get; set; }

        private Node _parentNode;
        public Node ParentNode
        {
            get { return _parentNode; }
            set
            {
                _parentNode = value;
            }
        }

        internal virtual double GetTraversalCostsFromParent()
        {
            return Location.DistanceTo(ParentNode.Location);
        }

        internal IEnumerable<Node> GetAdjacentLocations(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
