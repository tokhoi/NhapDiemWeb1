namespace NhapDiemWeb1
{
    public class SubjectConfig
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TxColumns { get; set; } // Số cột điểm thường xuyên
    }

    public class Student
    {
        public int Stt { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<double?> TxScores { get; set; } = new List<double?>(); // Điểm thường xuyên (cho phép null)
        public double? GkScore { get; set; }
        public double? CkScore { get; set; }

        // Điểm giả lập HK1 (để tính cho HK2)
        public double TbHk1 { get; set; } = 7.5;

        // Tính toán điểm TB (Logic giả lập)
        public double? GetTbHk()
        {
            // Logic đơn giản: (TX + GK*2 + CK*3) / Tổng hệ số
            // Đây chỉ là mô phỏng
            double sum = 0;
            int count = 0;
            foreach (var s in TxScores) { if (s.HasValue) { sum += s.Value; count++; } }
            if (GkScore.HasValue) { sum += GkScore.Value * 2; count += 2; }
            if (CkScore.HasValue) { sum += CkScore.Value * 3; count += 3; }

            return count > 0 ? Math.Round(sum / count, 1) : (double?)null;
        }

        public double? GetTbCn()
        {
            var hk2 = GetTbHk();
            if (hk2.HasValue) return Math.Round((TbHk1 + hk2.Value * 2) / 3, 1);
            return null;
        }
    }
}