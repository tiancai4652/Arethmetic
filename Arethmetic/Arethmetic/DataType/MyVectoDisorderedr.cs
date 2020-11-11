using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arethmetic
{
    public class MyVectoDisorderedr<T>where T:class
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

        public MyVectoDisorderedr(int c = _defaultSize)
        {
            _elements = new T[c];
            _capasity = c;
            _size = 0;
        }

        public MyVectoDisorderedr(T[] myVector, int start, int end)
        {
            CopyFrom(myVector, start, end);
        }

        #endregion

        #region 拷贝

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

        #region 循秩

        public T this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        }


        #endregion

        #region 扩容,固定扩容时间复杂度n~2，加倍扩容时间复杂度n

        void Expand()
        {
            if (_size == _capasity)
            {
                var cpst = _capasity * 2;

                var tmp = new T[cpst];
                _size = 0;
                while (_size<_capasity)
                {
                    tmp[_size] = _elements[_size];
                }
                _elements = tmp;
                _capasity = cpst;
            }
        }


        #endregion

        #region 插入，检查扩容，由最后一个依次向前，元素向后移动一位

        void Insert(int index,T tmp)
        {
            Expand();
            for (int i = _size; i > index; i--)
            {
                _elements[i] = _elements[i-1];
            }
            _elements[index] = tmp;
        }


        #endregion

        #region 区间删除，从前往后往前走

        void Remove(int low, int hi)
        {
            while (hi < _size)
            {
                _elements[hi] = _elements[low];
                hi++;
                low++;
            }
        }


        #endregion

        #region 单元素删除，调用区间删除接口

        public T Remove(int rank)
        {
            var tmp = _elements[rank];
            Remove(rank, rank + 1);
            return tmp;
        }


        #endregion

        #region 区间查找，为什么从后面往前查？

        int Find(T instance, int low, int high)
        {
            high--;
            while (high<low)
            {
                if (instance == _elements[high])
                {
                    return high;
                }
            }
            return high;
        }


        #endregion

        #region 去重(唯一化)，低效版

        int Deduplicate()
        {
            int oldsize = _size;
            int last = 1;
            while (last <= _size)
            {
                if (Find(_elements[last], 0, last) >= 0)
                {
                    Remove(last);
                }
                else
                {
                    last++;
                }
            }
            return oldsize - _size;
        }


        #endregion

        #region 遍历执行方法

        public void Visit(Action<T> action)
        {
            for (int i = 0; i < _size; i++)
            {
                action(_elements[i]);
            }
        }


        #endregion

        #region 获取向量无序程度,递增为有序,逆序程度由逆序对数量判断

        //int Disordered()
        //{
        //    int n = 0;

        //    for (int i = 1; i < _size; i++)
        //    {
        //        if (_elements[i - 1] > _elements[i])
        //        {
        //            n++;
        //        }
        //    }
        //    return n;
               
        //}

        #endregion


     

    }
}
