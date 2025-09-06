namespace MiMenu_Back.Data.DTOs
{
    public class CategoryAddDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Visibility { get; set; }
    }
    public class CategoryGetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Visibility { get; set; }
    }
    public class CategoryQueryDto
    {
        public string TypeCategory { get; set; }
        public string? SortName { get; set; }
        public bool? Visibility { get; set; }
    }
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
    }
}
