using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arethmetic
{
    public class MyVector<T>
    {
        #region Properties

        /// <summary>
        /// 默认大小
        /// </summary>
        const int _defaultSize = 3;

        /// <summary>
        /// 规模,有效规模
        /// </summary>
        int _size;

        /// <summary>
        /// 容量/物理空间大小
        /// </summary>
        int _capasity;

        /// <summary>
        /// 数据区
        /// </summary>
        T[] _elements;

        #endregion

        #region Constructure

        public MyVector(int c = _defaultSize)
        {
            _elements = new T[c];
            _capasity = c;
            _size = 0;
        }

        public MyVector(T[] myVector, int start, int end)
        {
            CopyFrom(myVector, start, end);
        }

        #endregion

        #region Private

        void CopyFrom(T[] myVector, int start, int end)
        {

            if (end <= start)
            {
                throw new Exception("end <= start");
            }
            //预留了一些空间之后，就会使在之后的较长的时间之内，不会由于要扩容而打断计算过程
            var length = (end - start) * 2;
            _elements = new T[length];
            _size = 0;

            int i = start;
            while (i < end + 1)
            {
                _elements[_size] = myVector[i];
                i++;
                _size++;
            }


        }


        #endregion


    }
}
