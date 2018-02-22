using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AI_LAB1
{
    public class Node: IEquatable<Node>
    {
        public int State { get; set; } = 1;
        public List<int> Field { get; set; }
        public Node Parent { get; set; } = null;
        public int CountOfChild { get; set; } = 0;
        public Node(List<int> field, int state = 0)
        {
            Field = new List<int>(field);
            State = state;
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

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return this.Field.SequenceEqual(other.Field);
        }

        public bool rotate(int number, out List<int> output)
        {
            Dictionary<int, int> values = new Dictionary<int, int>();
            List<int> tempField = new List<int>(Field);
            
            switch(number)
            {
                default:
                    {
                        output = null;
                        return false;
                    }
                case 1:
                    {
                        values.Add(0, tempField[0]);
                        values.Add(1, tempField[1]);
                        values.Add(3, tempField[3]);
                        values.Add(4, tempField[4]);

                        tempField[0] = values[1];
                        tempField[3] = values[0];
                        tempField[4] = values[3];
                        tempField[1] = values[4];
                    }
                    break;
                case 2:
                    {
                        values.Add(1, tempField[1]);
                        values.Add(2, tempField[2]);
                        values.Add(4, tempField[4]);
                        values.Add(5, tempField[5]);

                        tempField[1] = values[2];
                        tempField[4] = values[1];
                        tempField[5] = values[4];
                        tempField[2] = values[5];
                    }
                    break;
                case 3:
                    {
                        values.Add(3, tempField[3]);
                        values.Add(4, tempField[4]);
                        values.Add(6, tempField[6]);
                        values.Add(7, tempField[7]);

                        tempField[3] = values[4];
                        tempField[6] = values[3];
                        tempField[7] = values[6];
                        tempField[4] = values[7];
                    }
                    break;
                case 4:
                    {
                        values.Add(4, tempField[4]);
                        values.Add(5, tempField[5]);
                        values.Add(7, tempField[7]);
                        values.Add(8, tempField[8]);

                        tempField[4] = values[5];
                        tempField[7] = values[4];
                        tempField[8] = values[7];
                        tempField[5] = values[8];
                    }
                    break;
            }
            output = new List<int>(tempField);
            return true;

        }
    }
}