using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;

namespace Udemy.BankApp.Web.TagHelpers
{

    [HtmlTargetElement("getAccountCount")]
    public class GetAccountCount:TagHelper
    {
        private readonly BankContext _context;
        public GetAccountCount(BankContext context)
        {
            _context = context;
        }
        public int ApplicationUserId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var accountCount = _context.Accounts.Count(x=>x.ApplicationUserId==ApplicationUserId);
            var html = $"<span class='badge bg-danger'>{accountCount}</span>";
            output.Content.SetHtmlContent(html);
        }
    }
}
