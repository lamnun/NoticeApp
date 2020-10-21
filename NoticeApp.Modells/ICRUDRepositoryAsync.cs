using Dul.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoticeApp.Modells
{
    /// <summary>
    ///  [3.1] Generic Repository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICRUDRepositoryAsync<T>
    {
        Task<T> Addsync(T model); //입력
        Task<List<T>> GetAllAsync(); //출력
        Task<T> GetByIdAsync(int id); //상세
        Task<bool> EditAsync(T model); //수정
        Task<bool> DeleteAsync(int id); //삭제
        Task<PagingResult<T>> GetAllAsync(int pageIndex, int tapageSizeke); //페이징
        Task<PagingResult<T>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId); //부모
    }
}
