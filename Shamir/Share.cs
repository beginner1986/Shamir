using System;
using System.Collections.Generic;
using System.Text;

namespace Shamir
{
    public class Share
    {
        private string name;
        private int value;

        public Share(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string ToString()
        {
            StringBuilder sb = new StringBuilder(name);
            sb.Append(", ").Append(value.ToString());
            return sb.ToString();
        }

        public string GetName()
        {
            return name;
        }

        public int GetValue()
        {
            return value;
        }
    }
}
