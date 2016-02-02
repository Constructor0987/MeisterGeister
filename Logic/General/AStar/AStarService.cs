using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace MeisterGeister.Logic.General.AStar
{
    public class AStarService
    {
        SearchParameters searchParameters;
        Size boundaries;
        Node endNode;
        Node startNode;
        public ICollection<Node> Nodes
        {
            get; set;
        }

        public AStarService(SearchParameters searchParameters)
        {
            this.Nodes = new List<Node>();
            this.searchParameters = searchParameters;
            this.endNode = searchParameters.EndNode;
            this.startNode = searchParameters.StartNode;
            this.boundaries = searchParameters.Boundaries;
        }                                                                                                                                                                                                                       

        public List<Node> FindPath()
        {
            var findPathStopWatch = Stopwatch.StartNew();
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
            findPathStopWatch.Stop();
            Debug.WriteLine("findPathStopWatch: " + findPathStopWatch.Elapsed.TotalSeconds);
            return path;
        }

        private bool Search()
        {
            Node currentNode = startNode;
            Nodes.Add(currentNode);
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
            Node result = Nodes.First();
            double minDistance = Double.MaxValue;
            foreach(var node in Nodes.Where(n => n.State == NodeState.Open))
            {
                if (node.LengthFromStartToEnd < minDistance)
                {
                    result = node;
                    minDistance = node.LengthFromStartToEnd;
                }
            }
            return result;
        }

        private bool AnyOpenNodes()
        {
            bool anyOpenNodes = Nodes.Any(n => n.State == NodeState.Open || n.State == NodeState.Untested);
            return anyOpenNodes;
        }

        private void CheckAdjacentWalkableNodes(Node fromNode)
        {
            //var checkAdjacentWalkableNodesStopWatch = Stopwatch.StartNew();
            // Debug.WriteLine("From Node: " + fromNode);
            IEnumerable<Node> nextNodes = fromNode.GetAdjacentNodes();
            // Debug.WriteLine("Adjacent Nodes: " + String.Join(", ", nextNodes));

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
            //checkAdjacentWalkableNodesStopWatch.Stop();
            //Debug.WriteLine("checkAdjacentWalkableNodesStopWatch: " + checkAdjacentWalkableNodesStopWatch.Elapsed.TotalMilliseconds);
        }

        private void AddNode(Node node)
        {
            if(!Nodes.Contains(node))
            {
                Nodes.Add(node);
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
