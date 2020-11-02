using Microsoft.AspNetCore.Components;
using NoticeApp.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoticeApp.Pages.Notices
{
    public partial class Create  //partial 추가 같은거라고 인식위해
    {
        [Inject]
        public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        protected Notice model = new Notice();

        public string ParentId { get; set; }
        protected int[] parentIds = { 1, 2, 3 };

        protected async void FormSubmit()
        {
            await NoticeRepositoryAsyncReference.AddAsync(model);
            NavigationManagerReference.NavigateTo("/Notices");
        }



        protected override void OnInitialized()
        {

        }
    }
}
