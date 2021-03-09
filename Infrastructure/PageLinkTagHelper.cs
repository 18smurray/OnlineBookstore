using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineBookstore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Building our own tag helper - similar to the tag helpers that enable asp-action, asp-validation, etc.

namespace OnlineBookstore.Infrastructure
{
    //Specify target element (div) and the attribute to express (page-model)
    [HtmlTargetElement("div", Attributes ="page-model")]


    //Inherit from TagHelper class
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        //Constructor
        public PageLinkTagHelper(IUrlHelperFactory hp)
        {
            //Set urlHelperFactory equal to the IUrlHelperFactory type hp variable passed in parameter
            urlHelperFactory = hp;
        }

        //Class attributes

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        //Uses the PagingInfo class that holds all the page information attributes
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }

        //Attribute for tracking category filter
        //Anytime someone enters "page-url-(whatever)", adds it to dictionary
        //ASP.NET groups properties with a common prefix
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        //Attributes to manage styling
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }


        //Override the Process method inherited from the TagHelper class
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Defines contract- how classes and their information will interact (?)
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            //Creating the tags dynamically for each page
            TagBuilder result = new TagBuilder("div");

            //Create <a> tag for each page needed (TotalPages from PagingInfo) and the href using the page parameter
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");

                //track page currently on in dictionary
                PageUrlValues["pageNum"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                //Manage styling
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    //If the page is the current page, style it as selected, otherwise leave it as normal
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                tag.InnerHtml.Append(i.ToString());

                //Appends the new <a href> element to the result div
                result.InnerHtml.AppendHtml(tag);
            }

            //Appends the result InnerHtml
            output.Content.AppendHtml(result.InnerHtml); 
        }
    }
}
