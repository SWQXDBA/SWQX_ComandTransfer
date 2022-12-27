using System;
using Torch;

namespace ComandTransfer
{
    public class Convertor:ViewModel,IComparable<Convertor>
    {
        private string from_;
        private string to_;

        public string From
        {
            get { return from_; }
            set
            {
                SetValue(ref from_,value);
            }
        }
        public string To
        {
            get { return to_; }
            set
            {
                SetValue(ref to_,value);
            }
        }

        public int CompareTo(Convertor other)
        {

            return String.Compare(this.from_, other.from_, StringComparison.Ordinal) + String.Compare(this.To, other.to_, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (obj is  Convertor other)
            {
                return this.from_==other.from_&&this.To==other.To;
            }

            return false;
        }
    }
}