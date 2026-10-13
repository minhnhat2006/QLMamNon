using ACG.Core.WinForm.Util;
using QLMamNon.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace QLMamNon
{
    /// <summary>
    /// Caches bangthutiengenhistory rows per (LopId, NgayTinh) so that evaluating a whole
    /// grid of viewbangthutien rows does not issue one query per row.
    /// The qlmamnonEntities instance is borrowed, not owned: this class never disposes it.
    /// Misses are cached as well, so a lop/month without a gen history is queried only once.
    /// </summary>
    public class BangThuTienGenHistoryCache
    {
        private readonly qlmamnonEntities entities;
        private readonly Dictionary<string, bangthutiengenhistory> genHistoryMap = new Dictionary<string, bangthutiengenhistory>();

        public BangThuTienGenHistoryCache(qlmamnonEntities entities)
        {
            this.entities = entities;
        }

        public bangthutiengenhistory Get(int lopId, DateTime ngayTinh)
        {
            string key = string.Format("{0}|{1}", lopId, ngayTinh.Ticks);

            if (this.genHistoryMap.ContainsKey(key))
            {
                return this.genHistoryMap[key];
            }

            bangthutiengenhistory genHistoryRow = null;

            if (this.entities != null)
            {
                List<bangthutiengenhistory> genHistoryDataTable = this.entities
                    .getBangThuTienGenHistoryByLopAndNgay(lopId, ngayTinh, MergeOption.NoTracking).ToList();

                if (!ListUtil.IsEmpty(genHistoryDataTable))
                {
                    genHistoryRow = genHistoryDataTable[0];
                }
            }

            this.genHistoryMap.Add(key, genHistoryRow);

            return genHistoryRow;
        }
    }
}
