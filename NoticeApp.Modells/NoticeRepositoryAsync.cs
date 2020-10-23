using Dul.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Modells
{


    /// <summary>
    /// [6] Repository Class : ADO.NET
    /// </summary>
    public class NoticeRepositoryAsync : INoticeRepositoryAsync
    {
        private readonly NoticeAppDbContext _context;
        private readonly ILogger _logger;

        public NoticeRepositoryAsync(NoticeAppDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger(nameof(NoticeRepositoryAsync));
        }


        //입력
        public async Task<Notice> Addsync(Notice model)
        {
            _context.Notices.Add(model);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {

                _logger.LogError($"에러 발생({nameof(Addsync)}): {e.Message}");
            }

            return model;
        }

        //출력
        public async Task<List<Notice>> GetAllAsync()
        {
            return await _context.Notices.OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .ToListAsync();
        }

        //상세
        public async Task<Notice> GetByIdAsync(int id)
        {
           return await _context.Notices
                //.Include(m => m.NoticesComments)
                .SingleOrDefaultAsync(m => m.Id == id);

        }

        //수정
        public async Task<bool> EditAsync(Notice model)
        {
            _context.Notices.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false); // 반환값이 0 보다 크면 true, 아니면 false
            }
            catch (Exception e)
            {
                _logger.LogError($"에러 발생({nameof(EditAsync)}): {e.Message}");
            }

            return false;
        }


        //삭제
        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _context.Notices.SingleOrDefaultAsync(m => m.Id == id);
            _context.Remove(model);

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger.LogError($"에러 발생({nameof(DeleteAsync)}): {e.Message}");
            }

            return false;
        }

        //페이징
        public async Task<PagingResult<Notice>> GetAllAsync(int pageIndex, int pageSize)
        {
            var totalRecords = await _context.Notices.CountAsync();
            var models = await _context.Notices
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Notice>(models, totalRecords);
        }

        //부모
        public async Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
        {
            var totalRecords = await _context.Notices.Where( m => m.ParentId == parentId).CountAsync();
            var models = await _context.Notices
                .Where(m => m.ParentId == parentId)
                .OrderByDescending(m => m.Id)
                //.Include(m => m.NoticesComments)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagingResult<Notice>(models, totalRecords);
        }

        //상태 (공지 고정 상태?)
        public async Task<Tuple<int, int>> GetStatus(int parentId)
        {
            var totalRecords = await _context.Notices.Where(m => m.ParentId == parentId).CountAsync();
            var pinnedRecode = await _context.Notices.Where(m => m.ParentId == parentId && m.IsPinned == true).CountAsync();

            return new Tuple<int, int>(pinnedRecode, totalRecords );  // (2, 10) 10개중 2개가 고정된 값이다...ex
        }


        //삭제 by Parent 
        public async Task<bool> DeleteAllByParentId(int parentId)
        {
            try
            {
                var models = await _context.Notices.Where(m => m.ParentId == parentId).ToListAsync();

                foreach (var model in models)
                {
                    _context.Notices.Remove(model);
                }
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {

                _logger.LogError($"에러 발생({nameof(DeleteAllByParentId)}): {e.Message}");

            }

            return false;
        }
    }
}
