using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Node
    {
        public int idParent { get; set; }
        public int idNode { get; set; }
        public List<int> idsChild { get; set; }
        public List<int> stateOfField { get; set; }
        public Node(int idnode, int idparent, List<int> idschild, List<int> stateoffield)
        {
            this.idNode = idnode;
            this.idParent = idparent;
            this.idsChild = idschild;
            this.stateOfField = stateoffield;
        }
    }
}
