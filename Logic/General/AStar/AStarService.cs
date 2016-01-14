using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General.AStar
{
    public class AStarService
    {
        Size boundaries;
        ICollection<Node> nodes;
        Node endNode;

        public AStarService(ICollection<Node> allNodes, Size boundaries)
        {
            this.nodes = allNodes;
            this.boundaries = boundaries;
        }                                                                                                                                                                                                                       

        public List<Point> FindPath(Node startNode, Node endNode)
        {
            this.endNode = endNode;
            List<Point> path = new List<Point>();
            bool success = Search(startNode);
            if (success)
            {
                Node node = this.endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                path.Reverse();
            }
            return path;
        }

        private bool Search(Node currentNode)
        {
            currentNode.State = NodeState.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
            nextNodes.Sort((node1, node2) => node1.LengthFromStartToEnd.CompareTo(node2.LengthFromStartToEnd));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == this.endNode.Location)
                {
                    return true;
                }
                else
                {
                    if (Search(nextNode)) // Note: Recurses back into Search(Node)
                        return true;
                }
            }
            return false;
        }

        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Node> nextNodes = GetAdjacentNodes(fromNode);

            foreach (var node in nextNodes)
            {
                double x = node.Location.X;
                double y = node.Location.Y;

                // Stay within the grid's boundaries
                if (IsWithinBoundaries(x, y))
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
                    double traversalCost = node.GetTraversalCostsFromParent();
                    double gTemp = fromNode.LengthFromStart + traversalCost;
                    if (gTemp < node.LengthFromStart)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        private bool IsWithinBoundaries(Point location)
        {
            double x = location.X;
            double y = location.Y;

            return IsWithinBoundaries(x, y);
        }

        private bool IsWithinBoundaries(double x, double y)
        {
            return x < 0 || x >= this.boundaries.Width || y < 0 || y >= this.boundaries.Height;
        }

        private IEnumerable<Node> GetAdjacentNodes(Node node)
        {
            return node.GetAdjacentLocations(node);
        }
    }
}
