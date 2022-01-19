using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business
{
    public interface IFileBusiness
    {
        //Método ler aquivo em disco
        public byte[] GetFile(string fileName);

        //Método salvar aquivo em disco
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);

        //Método salvar vários aquivos em disco
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
