using System;

namespace pokenae.WebSystem.API.DTOs
{
    /// <summary>
    /// �y�[�W��DTO
    /// </summary>
    public class PageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Route { get; set; }
    }
}

