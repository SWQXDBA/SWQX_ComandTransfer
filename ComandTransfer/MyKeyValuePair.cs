using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace test
{

    [Serializable]
    //用于Serializable 类库中的KeyValuePair序列化时 key value为只读的 无法记录信息
        public class MyKeyValuePair<TKey, TValue>
        {
            
            public  TKey Key;
         
            public  TValue Value;

            public MyKeyValuePair()
            {
            }

            public MyKeyValuePair(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
            }

            public override string ToString()
            {
                return Key+ " " + Value;
            }
        }
    
}