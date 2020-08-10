using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models.ECom
{
    public static partial class Extension
    {
        private static Database_MainEntities1 db = new Database_MainEntities1();

        /// <summary>
        /// Check max OrderBy attribute for EComCategoryMaps.
        /// </summary>
        /// <param name="eComCategoryId">Number of category</param>
        /// <returns>Max of OrderBy attribute</returns>
        private static int MaxValueOfOrderBy(int eComCategoryId)
        {
            List<EComCategoryMap> eComCategoryMaps = db.EComCategoryMaps.Where(c => c.EComCategoryId.Equals(eComCategoryId)).ToList();

            int max = eComCategoryMaps.Count == 0 ? 0 : eComCategoryMaps.Select(p => System.Convert.ToInt32(p.OrderBy)).Max();
            return max;
        }

        /// <summary>
        /// Sort change for order by
        /// </summary>
        /// <param name="eComCategoryMap">CategoryMap will chage order by</param>
        /// <param name="sortChange">sort change is set diff order by</param>
        public static void SortChange(this EComCategoryMap eComCategoryMap, int sortChange)
        {
            if (System.Math.Abs(sortChange) == 1)
            {
                EComCategoryMap Original = db.EComCategoryMaps.Find(eComCategoryMap.Id);
                int currentOrder = System.Convert.ToInt32(Original.OrderBy);

                if (currentOrder != db.EComCategoryMaps.Where(c=>c.EComCategoryId.Equals(Original.EComCategoryId)).Min(c => c.OrderBy) && sortChange.Equals(-1))
                {
                    Original.SetSortChange(-1);
                }
                else if (currentOrder != MaxValueOfOrderBy(Original.EComCategoryId) && sortChange.Equals(1))
                {
                    Original.SetSortChange(1);
                }
            }
        }

        /// <summary>
        /// Set Order by on categoryMap
        /// </summary>
        /// <param name="eComCategoryMap">current categoryMap</param>
        /// <param name="sortChange">set 1 or -1</param>
        private static void SetSortChange(this EComCategoryMap eComCategoryMap, int sortChange)
        {
            EComCategoryMap Original = eComCategoryMap;
            int currentOrder = System.Convert.ToInt32(Original.OrderBy);

            List<EComCategoryMap> eComCategoryMapOthers = db.EComCategoryMaps.Where(c => c.EComCategoryId.Equals(eComCategoryMap.EComCategoryId)).ToList();

            if (sortChange == 1)
            {
                Original.SwitchOrder(eComCategoryMapOthers.Where(c => c.OrderBy > currentOrder).OrderBy(c => c.OrderBy).FirstOrDefault());
            }
            else if (sortChange == -1)
            {
                Original.SwitchOrder(eComCategoryMapOthers.Where(c => c.OrderBy < currentOrder).OrderByDescending(c => c.OrderBy).FirstOrDefault());
            }
        }

        /// <summary>
        /// Switch order by in CategoryMap
        /// </summary>
        /// <param name="main">CategoryMap will switch</param>
        /// <param name="other">CategoryMap will switch</param>
        private static void SwitchOrder(this EComCategoryMap main, EComCategoryMap other)
        {
            int orderByOfMain = main.OrderBy;
            int orderByOfOther = other.OrderBy;

            EComCategoryMap main_Re_Order = main;
            main_Re_Order.OrderBy = orderByOfOther;
            db.Entry(main).CurrentValues.SetValues(main_Re_Order);
            db.SaveChanges();

            EComCategoryMap other_Re_Order = other;
            other_Re_Order.OrderBy = orderByOfMain;
            db.Entry(other).CurrentValues.SetValues(other_Re_Order);
            db.SaveChanges();
        }

        /// <summary>
        /// ทำการเพิ่ม EComCategoryMap ตามที่กำหนด
        /// </summary>
        /// <param name="eComProduct">EComProduct ที่จะทำการเพิ่ม Category</param>
        /// <param name="EComCategoryMapsValue">CategoryId ที่จะทำการเพิ่ม</param>
        public static void InsertCategoryMap(this EComProduct eComProduct, int[] EComCategoryMapsValue)
        {
            if (eComProduct.EComCategoryMaps != null)
                foreach (int EComCategoryMapsId in EComCategoryMapsValue)
                {
                    EComCategoryMap eComCategoryMap = new EComCategoryMap();
                    eComCategoryMap.EComCategoryId = EComCategoryMapsId;
                    eComCategoryMap.EComProductId = eComProduct.Id;
                    eComCategoryMap.OrderBy = MaxValueOfOrderBy(EComCategoryMapsId) + 1;
                    db.EComCategoryMaps.Add(eComCategoryMap);
                    db.SaveChanges();
                }
        }

        /// <summary>
        /// ทำการอัพเดท EComCategoryMap ตามที่กำนหด
        /// </summary>
        /// <param name="eComProduct">EComProduct ที่จะทำการแก้ไข Category</param>
        /// <param name="EComCategoryMapsValue">CategoryId ที่จะทำการแก้ไข</param>
        public static void UpdateCategoryMap(this EComProduct eComProduct, int[] EComCategoryMapsValue)
        {
            if (eComProduct.EComCategoryMaps != null)
            {
                int[] oldEComCategoryMaps = eComProduct.EComCategoryMaps.Select(c => c.EComCategoryId).ToArray();
                int[] arrayInsert = EComCategoryMapsValue.Except(oldEComCategoryMaps).ToArray();
                int[] arrayDelete = oldEComCategoryMaps.Except(EComCategoryMapsValue).ToArray();

                DeleteCategoryMap(eComProduct, arrayDelete);
                InsertCategoryMap(eComProduct, arrayInsert);
            }
        }

        /// <summary>
        /// ทำการลบ EComCategoryMap ตามที่กำหนด
        /// </summary>
        /// <param name="eComProduct">EComProduct ที่จะทำการลบ Category</param>
        /// <param name="EComCategoryMapsValue">CategoryId ที่จะทำการลบ</param>
        public static void DeleteCategoryMap(this EComProduct eComProduct, int[] EComCategoryMapsValue)
        {
            if (eComProduct.EComCategoryMaps != null)
                foreach (int EComCategoryMapsId in EComCategoryMapsValue)
                {
                    EComCategoryMap eComCategoryMap = new EComCategoryMap();
                    eComCategoryMap = eComProduct.EComCategoryMaps.Where(cm => cm.EComCategoryId == EComCategoryMapsId).FirstOrDefault();
                    db.EComCategoryMaps.Remove(db.EComCategoryMaps.Find(eComCategoryMap.Id));
                    db.SaveChanges();
                }
        }
    }
}