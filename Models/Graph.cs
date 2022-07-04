using System;
using System.Text;

namespace Models{

    #region Graph: Search Type Enum        
    enum SearchType
    {
        DFS,
        BFS
    }
    #endregion

    #region Graph: Create Undirected  Structure, Build, Search and Get the path  

    public class Graph<T>
    {
        // HttpContext _httpContext
        //     => new HttpContextAccessor().HttpContext;
        #region Graph: Internal Variable - list of nodes        
        List<GraphNode<T>> nodes = new List<GraphNode<T>>();
        #endregion

        #region Graph: constructor
        public Graph()
        {

        }
        #endregion        

        #region Graph: Readonly Properties - Count, Nodes        
        public int Count
        {
            get
            {
                return nodes.Count;
            }
        }
        public IList<GraphNode<T>> Nodes
        {
            get
            {
                return nodes.AsReadOnly();
            }
        }
        #endregion

        #region Graph: Basic operations - AddNode, AddEdge, RemoveNode, RemoveEdge, Clear, ToString, Find                 
        public bool AddNode(T value)
        {
            if (Find(value)!=null)
            {
                //duplicate value
                return false;
            }
            else
            {
                nodes.Add(new GraphNode<T>(value));
                return true;
            }
        }        
        public bool AddEdge(T value1, T value2)
        {
            GraphNode<T> node1 = Find(value1);
            GraphNode<T> node2 = Find(value2);
            if (node1==null||node2==null)
            {
                return false;
            }
            else if (node1.Neighbors.Contains(node2))
            {
                return false;
            }
            else
            {
                //for directed graph only below 1st line is required  node1->node2
                node1.AddNeighbors(node2);
                //for undirected graph need below line as well
                node2.AddNeighbors(node1);
                return true;
            }
        }
        public GraphNode<T> Find(T value)
        {
            foreach(GraphNode<T> node in nodes)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }
            return null;
        }
        public bool RemoveNode(T value)
        {
            GraphNode<T> removeNode= Find(value);
            if (removeNode==null)
            {
                return false;
            }
            else
            {
                nodes.Remove(removeNode);
                foreach (GraphNode<T> node in nodes)
                {
                    node.RemoveNeighbors(removeNode);
                }
                return true;
            }
        }
        public bool RemoveEdge(T value1, T value2)
        {
            GraphNode<T> node1 = Find(value1);
            GraphNode<T> node2 = Find(value2);
            if (node1 == null || node2 == null)
            {
                return false;
            }
            else if (!node1.Neighbors.Contains(node2))
            {
                return false;
            }
            else
            {
                //for directed graph only below 1st line is required  node1->node2
                node1.RemoveNeighbors(node2);
                //for undirected graph need below line as well
                node2.RemoveNeighbors(node1);
                return true;
            }
        }
        public void Clear()
        {
            foreach (GraphNode<T> node in nodes)
            {
                node.RemoveAllNeighbors();
            }
            for (int i = nodes.Count-1; i >=0; i--)
            {
                nodes.RemoveAt(i);
            }
        }
        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();            
            for (int i = 0; i < Count; i++)
            {
                nodeString.Append(nodes[i].ToString());
                if (i<Count-1)
                {
                    nodeString.Append("\n");
                }
            }
            return nodeString.ToString();
        }
        #endregion
    }

    #endregion
}