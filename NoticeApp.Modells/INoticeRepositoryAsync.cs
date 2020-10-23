using System;
using System.Threading.Tasks;

namespace NoticeApp.Modells
{
    /// <summary>
    /// [4] Repository Interface
    /// </summary>
    public interface INoticeRepositoryAsync : ICRUDRepositoryAsync<Notice> // 공지사항만 피료?
    {
        Task<Tuple<int, int>> GetStatus(int parentId);

        Task<bool> DeleteAllByParentId(int parentId);
        
    }
}
