namespace SIEG_API.DTO
{
    public class NewsInfoDTO
    {
        public int Id { get; set; }
        public string? 圖檔 { get; set; }
        public string? 標題 { get; set; }
        public string? 內文 { get; set; }
        public string? 分類 { get; set; }
        public DateTime? 建立時間 { get; set; }
    }
}
