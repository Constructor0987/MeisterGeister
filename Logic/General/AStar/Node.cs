using MeisterGeister.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterGeister.Logic.General.AStar
{
    public abstract class Node
    {
        public virtual Point Location { get; protected set; }
        public bool IsWalkable { get; set; }
        public virtual double LengthFromStart { get; protected set; }
        public virtual double LengthToEnd { get; protected set; }
        public double LengthFromStartToEnd { get { return this.LengthFromStart + this.LengthToEnd; } }
        public NodeState State { get; set; }
        protected Point EndLocation { get; set; }
        protected SearchParameters SearchParameters { get; set; }
        
        private Node _parentNode;
        public Node ParentNode
        {
            get { return this._parentNode; }
            set
            {
                this._parentNode = value;
                if(_parentNode != null)
                    this.LengthFromStart = this._parentNode.LengthFromStart + GetTraversalCost(this._parentNode);
            }
        }

        public Node(double x, double y, bool isWalkable, Node endLocation, SearchParameters searchParameters)
        {
            Init(x, y, isWalkable, endLocation, searchParameters);
        }

        public void Init(double x, double y, bool isWalkable, Node endLocation, SearchParameters searchParameters)
        {
            this.SearchParameters = searchParameters;
            this.Location = new Point(x, y);
            this.State = NodeState.Untested;
            this.IsWalkable = isWalkable;
            SetEndLocation(endLocation);
            this.LengthFromStart = 0;
            this.ParentNode = null;
        }

        public virtual void SetEndLocation(Node endLocation)
        {
            if (endLocation != null)
            {
                this.EndLocation = endLocation.Location;
                this.LengthToEnd = GetTraversalCost(endLocation);
            }
        }

        public virtual double GetTraversalCost(Node target)
        {
            if (target == null || target == this)
                return 0.0;
            else
                return this.Location.DistanceTo(target.Location);
        }

        public abstract void EnrichData(List<Node> path);

        public abstract IEnumerable<Node> GetAdjacentNodes();
    }
}
