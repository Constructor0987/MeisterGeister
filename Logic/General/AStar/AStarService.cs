using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MeisterGeister.Logic.General.AStar
{
    public class AStarService
    {
        SearchParameters searchParameters;
        Size boundaries;
        ICollection<Node> nodes;
        Node endNode;
        Node startNode;

        public AStarService(SearchParameters searchParameters)
        {
            this.nodes = new List<Node>();
            this.searchParameters = searchParameters;
            this.endNode = searchParameters.EndNode;
            this.startNode = searchParameters.StartNode;
            this.boundaries = searchParameters.Boundaries;
        }                                                                                                                                                                                                                       

        public List<Node> FindPath()
        {
            List<Node> path = new List<Node>();
            bool success = Search();
            if (success)
            {
                Node node = this.endNode;
                while (node != null)
                {
                    path.Add(node);
                    node = node.ParentNode;
                }
                path.Reverse();
                foreach (var item in path)
                {
                    item.EnrichData(path);
                }
            }
            return path;
        }

        private bool Search()
        {
            Node currentNode = startNode;
            nodes.Add(currentNode);
            while (AnyOpenNodes())
            {
                if (currentNode == endNode)
                    return true;

                currentNode.State = NodeState.Closed;
                CheckAdjacentWalkableNodes(currentNode);
                currentNode = GetLowestOverallLength();
            }
            return false;
        }

        private Node GetLowestOverallLength()
        {
            var orderedNodes = nodes
                .Where(n => n.State == NodeState.Open)
                .OrderBy(n => n.LengthFromStartToEnd);
            return orderedNodes.FirstOrDefault();
        }

        private bool AnyOpenNodes()
        {
            return nodes.Any(n => n.State == NodeState.Open || n.State == NodeState.Untested);
        }

        private void CheckAdjacentWalkableNodes(Node fromNode)
        {
            Debug.WriteLine("From Node: " + fromNode);
            IEnumerable<Node> nextNodes = fromNode.GetAdjacentNodes();
            Debug.WriteLine("Adjacent Nodes: " + String.Join(", ", nextNodes));

            foreach (var node in nextNodes)
            {
                double x = node.Location.X;
                double y = node.Location.Y;

                // Stay within the grid's boundaries
                if (IsNotWithinBoundaries(x, y))
                    continue;

                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;

                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    double traversalCost = fromNode.GetTraversalCost(node);
                    double gTemp = fromNode.LengthFromStart + traversalCost;
                    if (gTemp < node.LengthFromStart)
                    {
                        node.ParentNode = fromNode;
                        AddNode(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    AddNode(node);
                }
            }
        }

        private void AddNode(Node node)
        {
            if(!nodes.Contains(node))
            {
                nodes.Add(node);
            }
        }

        private bool IsNotWithinBoundaries(Point location)
        {
            double x = location.X;
            double y = location.Y;

            return IsNotWithinBoundaries(x, y);
        }

        private bool IsNotWithinBoundaries(double x, double y)
        {
            return x < 0 || x >= this.boundaries.Width || y < 0 || y >= this.boundaries.Height;
        }
    }
}
