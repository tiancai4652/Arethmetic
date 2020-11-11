using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arethmetic
{
   public class MyVerctorOrdered:MyVectoDisorderedr<double>
    {

        #region 获取向量无序程度,递增为有序,逆序程度由逆序对数量判断

        int Disordered()
        {
            int n = 0;

            for (int i = 1; i < _size; i++)
            {
                if (_elements[i - 1] > _elements[i])
                {
                    n++;
                }
            }
            return n;
        }

        #endregion


        #region 有序向量去重(低效版)，默认有一个递增有序向量,只需要比较相邻两个元素是否相等，去除相等的，因为是有序的。

        int Uniquify()
        {
            int oldsize = _size;
            for (int i = 1; i < _size; i++)
            {
                if (_elements[i - 1] == _elements[i])
                {
                    Remove(i - 1);
                }
            }
            return oldsize - _size;


        }

        #endregion

        #region 有序向量去重(高效版)，一个元素多次向前移动一个单位改为一次向前移动多个单位。

        int Uniquify_High()
        {
            int oldsize = _size;
            int i = 0;
            int j = 1;
            while (j < _size)
            {
                if (_elements[i] != _elements[j])
                {
                    _elements[i + 1] = _elements[j];
                    i += 1;
                    j += 1;
                }
                else
                {
                    j += 1;
                }
            }
            _size = i;

            return oldsize - _size;
        }

        #endregion

        #region 有序向量二分查找，每做一次，需要判断的数组的长度减少一半

        int Search(int instance, int low, int high)
        {
            int middle = (high - low) / 2;

            while (low < high)
            {

                if (instance < _elements[middle])
                {
                    high = middle;
                    middle = (high - low) / 2;
                }
                else if (_elements[middle] < instance)
                {
                    low = middle+1;
                    middle = (high - low) / 2;
                }
                else
                {
                    return middle;
                }
            }
            return -1;

        }

        #endregion



    }
}
