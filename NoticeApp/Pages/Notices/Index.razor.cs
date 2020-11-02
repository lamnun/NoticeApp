using Microsoft.AspNetCore.Components;
using NoticeApp.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace NoticeApp.Pages.Notices
{
    public partial class Index
    {
        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        protected List<Notice> models;

        protected DulPager.DulPagerBase pager = new DulPager.DulPagerBase() //페이저
        {
            PageNumber = 1,
            PageIndex = 0,
            PageSize = 2,
            PagerButtonCount = 5
        };


        protected override async Task OnInitializedAsync()
        {
            await DisplayData();
        }

        private async Task DisplayData()
        {
            //await Task.Delay(3000); // 프로세스바 테스트용
            var resultSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize); //데이터 추울력
            pager.RecordCount = resultSet.TotalRecords; //카운트
            models = resultSet.Records.ToList(); //전체레코드 출력

        }

        protected void NameClick(int id)
        {
            NavigationManagerReference.NavigateTo($"/Notices/Details/{id}");
        }

        protected async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            await DisplayData();

            StateHasChanged(); 
        }
    }
}
