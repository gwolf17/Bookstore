using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically creating the page links

        private IUrlHelperFactory uhf;

        //Constructor
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;  //Create an instance of UrlHelperFactory and save to the variable 'uhf'
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageInfo { get; set; }  //Instance of the PageInfo class

        public string PageAction { get; set; }

        //Variables to hold class elements from Index page
        public string PageClass { get; set; }
        public bool ClassesEnabled { get; set; }
        public string ClassNormal { get; set; }
        public string ClassSelected { get; set; }


        //Override Process class
        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);  //Pass in ViewContext variable

            TagBuilder final = new TagBuilder("div");

            //For each page, dynamically build an 'a' tag with correct link
            for (int i = 1; i <= PageInfo.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");

                //Build each 'a' tag
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                //Add css to the a tags
                if (ClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    //Use different css for the current page tag
                    tb.AddCssClass(i == PageInfo.CurrentPage ? ClassSelected : ClassNormal);
                }

                tb.AddCssClass(PageClass);

                tb.InnerHtml.Append(i.ToString());  //Set the page# to be shown to the user

                final.InnerHtml.AppendHtml(tb);  //Apend the results to the final results variable
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
