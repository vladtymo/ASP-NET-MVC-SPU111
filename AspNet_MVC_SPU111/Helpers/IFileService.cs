namespace AspNet_MVC_SPU111.Helpers
{
    public interface IFileService
    {
        Task<string> SaveProductImage(IFormFile file);
        Task DeleteProductImage(string path);
    }
}
