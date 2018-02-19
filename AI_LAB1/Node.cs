using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_LAB1
{
    class Node
    {
        private List<int> ideal = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};
        public int State { get; set; } = 1;
        public List<int> Field { get; set; }
        public Node(List<int> field)
        {
            Field = new List<int>(field);
        }
        public bool isVictory()
        {
            return this.Field.SequenceEqual(ideal);
        }
        public String FieldToString()
        {
            String result = "";
            foreach(int f in Field)
            {
                result += f.ToString()+" ";
            }
            return result;
        }
    }
}
