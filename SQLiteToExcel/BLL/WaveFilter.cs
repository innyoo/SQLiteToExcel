using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteToExcel.BLL
{
    internal class WaveFilter
    {
        internal static Hashtable FILTER_LIST_SPEED = new Hashtable();
        internal static Hashtable SLIDE_LIST_P2 = new Hashtable();
        internal static Hashtable SLIDE_LIST_P3 = new Hashtable();
        internal static double TimeFilter(Hashtable timelist, long datatime, double currentV, int threshold, int times)
        {
            double returnV = currentV;
            //int lasttime = 0
            ArrayList key = new ArrayList();
            foreach (long k in timelist.Keys)
            {
                key.Add(k);
            }
            foreach (long k in key)
            {
                if ((datatime - k) > times || datatime - k < 0)     //时间范围超出
                {
                    timelist.Remove(k);
                }
            }

            bool needFilter = false;
            double threshold_max = currentV + Math.Abs(threshold);
            double threshold_min = currentV - Math.Abs(threshold);
            ArrayList value = new ArrayList();
            foreach (double v in timelist.Values)
            {
                value.Add(v);
                if (v > threshold_max || v < threshold_min)      //超出阈值，直接使用当前值
                {
                    timelist.Clear();
                    break;
                }
                else
                {
                    if (v != currentV)
                    {
                        needFilter = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (needFilter)
            {
                double max = (double)value[0];
                foreach (double val in value)
                {
                    if ((double)val > max)
                    {
                        max = (double)val;
                    }
                }
                returnV = max;
            }
            timelist[datatime] = currentV;
            return returnV;
        }

        internal static double MovingAverageFilter(Hashtable slidelist, long datatime, double currentV)
        {
            slidelist.Add(datatime, currentV);
            ArrayList key = new ArrayList();
            foreach (long k in slidelist.Keys)
            {
                key.Add(k);
            }

            double slidetotal = 0;
            double slidecount = 0;
            foreach (long k in key)
            {
                if ((datatime - k) > 4 || (datatime - k) < 0)
                {
                    //时间范围超出
                    slidelist.Remove(k);
                }
                else
                {
                    slidecount++;
                    slidetotal += (double)slidelist[k];
                }
            }

            double slideaverage;
            if (slidecount > 0)
            {
                slideaverage = slidetotal / slidecount;
            }
            else
            {
                slideaverage = currentV;
            }
            return slideaverage;
        }

    }
}
