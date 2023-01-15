﻿namespace SIEG_API.DTO
{
    public class G_ForumArticlesDTO
    {
        public int? ForumArticleId { get; set; }
        public int MemberId { get; set; }
        public string? Category { get; set; }
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? ArticleContent { get; set; }
        public int LikeCount { get; set; }
        public int? ViewsCount { get; set; }
        public DateTime? AddTime { get; set; }
        public string? Img { get; set; }
        public bool? ValIdity { get; set; }
    }
}
