﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace SharpFileDB.Blocks
{
    /// <summary>
    /// 存储到数据库文件中的一块数据。由于一页只有4KB，所以一个对象可能需要多页存储。所以我们用<see cref="DataBlock"/>来一部分一部分地存储。
    /// </summary>
    [Serializable]
    public class DataBlock : AllocBlock, ILinkedNode<DataBlock>
    {
        /// <summary>
        /// 一个或多个数据块代表的对象序列化后所占用的字节总数。用此值便于<code>byte[] bytes = new byte[DataBlock.ObjectLength];</code>。
        /// </summary>
        public Int32 ObjectLength { get; set; }// RULE: 一个Table序列化后的最大长度为Int32.MaxValue个字节。

        /// <summary>
        /// 数据块。
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 安排所有文件指针。如果全部安排完毕，返回true，否则返回false。
        /// </summary>
        /// <returns></returns>
        public override bool ArrangePos()
        {
            bool allArranged = true;

            if (NextObj != null)
            {
                if (NextObj.ThisPos != 0)
                { this.NextPos = NextObj.ThisPos; }
                else
                { allArranged = false; }
            }

            return allArranged;
        }

        /// <summary>
        /// 存储到数据库文件中的一块数据。由于一页只有4KB，所以一个对象可能需要多页存储。所以我们用<see cref="DataBlock"/>来一部分一部分地存储。
        /// </summary>
        public DataBlock() { }

        const string strObjectLength = "A";
        const string strData = "B";

        const string strNextPos = "C";

        /// <summary>
        /// 序列化时系统会调用此方法。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(strObjectLength, this.ObjectLength);
            info.AddValue(strData, this.Data);

            info.AddValue(strNextPos, this.NextPos);
        }

        /// <summary>
        /// BinaryFormatter会通过调用此方法来反序列化此块。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DataBlock(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            this.ObjectLength = info.GetInt32(strObjectLength);
            this.Data = (byte[])info.GetValue(strData, typeof(byte[]));

            this.NextPos = info.GetInt64(strNextPos);
        }

        /// <summary>
        /// 显示此块的信息，便于调试。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, ObjectLength: {1}, Data: {2} bytes, next pos: {3}",
                base.ToString(),
                this.ObjectLength, this.Data == null ? 0 : this.Data.Length, this.NextPos);
        }

        #region ILinkedNode<DataBlock> 成员

        /// <summary>
        /// 下一个数据块的位置。
        /// <para>如果此值为0，则说明没有下一块。</para>
        /// </summary>
        public long NextPos { get; set; }

        /// <summary>
        /// 数据块链表的下一个结点。
        /// </summary>
        public DataBlock NextObj { get; set; }

        #endregion
    }
}
