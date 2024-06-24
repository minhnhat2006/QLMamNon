using QLMamNon.Dao;
using System;
using System.Collections.Generic;

namespace QLMamNon
{
    public static class ThongTinHocSinhUtil
    {
        public static void EvaluateLopInfoForHocSinhTable(qlmamnonEntities entities, List<hocsinh> hocSinhTable)
        {
            List<int> hocSinhIds = new List<int>(hocSinhTable.Count);
            foreach (hocsinh row in hocSinhTable)
            {
                hocSinhIds.Add(row.HocSinhId);
            }

            Dictionary<int, hocsinh_lop> hocSinhIdsToHocSinhLops = StaticDataUtil.GetHocSinhLopsByHocSinhIds(entities, hocSinhIds, DateTime.Now);
            Dictionary<int, lop> hocSinhIdsToLops = StaticDataUtil.GetLopsByHocSinhIds(entities, hocSinhIds, DateTime.Now);

            foreach (hocsinh row in hocSinhTable)
            {
                if (hocSinhIdsToLops.ContainsKey(row.HocSinhId))
                {
                    lop lop = hocSinhIdsToLops[row.HocSinhId];
                    row.LopDangHoc = lop.Name;
                }

                if (hocSinhIdsToHocSinhLops.ContainsKey(row.HocSinhId))
                {
                    hocsinh_lop hocSinhLop = hocSinhIdsToHocSinhLops[row.HocSinhId];
                    row.NgayVaoLop = hocSinhLop.DateJoin;
                }
            }
        }
    }
}
