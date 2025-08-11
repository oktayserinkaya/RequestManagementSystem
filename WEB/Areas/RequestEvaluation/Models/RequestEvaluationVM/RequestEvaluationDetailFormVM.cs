namespace WEB.Areas.RequestEvaluation.Models.RequestEvaluationVM
{
    public class RequestEvaluationDetailFormVM
    {
        public Guid Id { get; set; }

        // Request.RequestDate büyük olasılıkla nullable; hata almamak için nullable tuttuk
        public DateTime? RequestDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public string FullName { get; set; } = "";
        public string DepartmentName { get; set; } = "";

        // Şimdilik placeholder; entity’de yoksa sabit "-" basıyoruz
        public string CategoryName { get; set; } = "-";
        public string SubCategoryName { get; set; } = "-";
        public string ProductName { get; set; } = "-";

        // Entity’de olmayabilir; boş bırakılıyor
        public string? SpecialProductName { get; set; }
        public decimal? Amount { get; set; }             // int yerine decimal? yaptık (uyumsuzluk hatasını çözer)
        public string? Description { get; set; }         // entity’de yoksa boş
        public string? ExistingProductFeaturesFilePath { get; set; }

        public string StatusText { get; set; } = "";
    }
}
