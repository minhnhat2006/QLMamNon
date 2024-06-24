namespace QLMamNon.Entity.Form
{
    public class BangKeCacLoaiChiItem
    {
        public int STT { get; set; }

        public string PhanLoaiChi { get; set; }

        public long Week1 { get; set; }

        public long Week2 { get; set; }

        public long Week3 { get; set; }

        public long Week4 { get; set; }

        public long Week5 { get; set; }

        public long Week6 { get; set; }

        public long Total
        {
            get
            {
                return Week1 + Week2 + Week3 + Week4 + Week5 + Week6;
            }
        }
    }
}
