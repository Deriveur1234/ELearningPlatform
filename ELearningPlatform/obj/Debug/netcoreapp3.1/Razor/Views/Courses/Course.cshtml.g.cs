#pragma checksum "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f8a2a531af6c666d5ed8e0e4ced938d706ca730"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Courses_Course), @"mvc.1.0.view", @"/Views/Courses/Course.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\_ViewImports.cshtml"
using ELearningPlatform;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\_ViewImports.cshtml"
using ELearningPlatform.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml"
using static ELearningPlatform.Data.CoursesData;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f8a2a531af6c666d5ed8e0e4ced938d706ca730", @"/Views/Courses/Course.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e3081f39eae504873096168130ab2136a2bf59d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Courses_Course : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ELearningPlatform.Models.Course>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"card mb-4 Course\" >\r\n    <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 147, "\"", 183, 1);
#nullable restore
#line 4 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml"
WriteAttributeValue("", 155, ViewData[ViewDataIdSubject], 155, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <div class=\"view overlay\">\r\n        <img class=\"img-fluid\"");
            BeginWriteAttribute("src", " src=\"", 251, "\"", 279, 1);
#nullable restore
#line 6 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml"
WriteAttributeValue("", 257, ViewData[ViewDataIco], 257, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Card image cap\">\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 314, "\"", 359, 2);
            WriteAttributeValue("", 321, "CourseDetails?id=", 321, 17, true);
#nullable restore
#line 7 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml"
WriteAttributeValue("", 338, ViewData[ViewDataId], 338, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"mask rgba-white-slight\"></div>\r\n        </a>\r\n    </div>\r\n    <div class=\"card-body\">\r\n        <h4 class=\"card-title\">");
#nullable restore
#line 12 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml"
                          Write(ViewData[ViewDataName]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n        <p class=\"card-text\">");
#nullable restore
#line 13 "C:\Users\Loic\Documents\GitHub\ELearningPlatform\ELearningPlatform\Views\Courses\Course.cshtml"
                        Write(ViewData[ViewDataDesc]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ELearningPlatform.Models.Course> Html { get; private set; }
    }
}
#pragma warning restore 1591
