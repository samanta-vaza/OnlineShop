#pragma checksum "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c0c9b120c1db11d6ee138f7fd55bda48220e13c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ShowFields), @"mvc.1.0.view", @"/Views/Admin/ShowFields.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c0c9b120c1db11d6ee138f7fd55bda48220e13c4", @"/Views/Admin/ShowFields.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b853c2693020103458b52d8246f923ca3fd89014", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ShowFields : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OnlineShop.DataAccessLayer.Entities.Field>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
  
    ViewData["Title"] = "مشخصه ها";
    Layout = "~/Views/Shared/_Panel.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""col-lg-offset-3 col-lg-8 col-md-offset-3 col-md-8 col-sm-10 col-sm-offset-1 col-xs-10 col-xs-offset-1 panel-back"">

    <div class=""margin-top-50"">

        <h3 class=""pull-right"">
            <input type=""text"" id=""myInput"" onkeyup=""myfunction()"" placeholder=""جستجو ..."" />
        </h3>
        <h3 class=""pull-left"">
            <a href=""#"" onclick=""MyCreate()"" class=""btn btn-primary"">جدید</a>
        </h3>

    </div>

    <div class=""margin-top-30"">

        <table class=""table table-bordered table-hover"" id=""myTable"">

            <thead>

                <tr>

                    <th>
                        ");
#nullable restore
#line 30 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
                   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        عملیات\r\n                    </th>\r\n\r\n                </tr>\r\n\r\n            </thead>\r\n\r\n            <tbody>\r\n\r\n");
#nullable restore
#line 42 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 46 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\'", 1332, "\'", 1358, 3);
            WriteAttributeValue("", 1342, "MyEdit(", 1342, 7, true);
#nullable restore
#line 49 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
WriteAttributeValue("", 1349, item.Id, 1349, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1357, ")", 1357, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-xs btn-warning\">اصلاح</a> |\r\n                            <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\'", 1439, "\'", 1467, 3);
            WriteAttributeValue("", 1449, "MyDelete(", 1449, 9, true);
#nullable restore
#line 50 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
WriteAttributeValue("", 1458, item.Id, 1458, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1466, ")", 1466, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-xs btn-danger\">حذف</a>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 53 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowFields.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </tbody>

        </table>

    </div>

</div>

<div id=""myModal"" class=""modal fade"" tabindex=""-1"" role=""dialog"">

    <div class=""modal-dialog"" role=""document"">

        <div class=""modal-content"">

            <div class=""modal-header"">
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>

            <div id=""bodyModal"" class=""modal-body"">



            </div>

        </div>

    </div>

</div>


");
            DefineSection("mySection", async() => {
                WriteLiteral(@"
    <script>
        function myfunction() {
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById(""myInput"");
            filter = input.value.toUpperCase();
            table = document.getElementById(""myTable"");
            tr = table.getElementsByTagName(""tr"");

            for (var i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName(""td"")[0];

                if (td) {
                    txtValue = td.textContent || td.innerText;

                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = """";
                    } else {
                        tr[i].style.display = ""none"";
                    }
                }
            }
        }
    </script>

    <script>
        function MyCreate() {
            $.ajax({
                url: ""/Admin/CreateField/"",
                type: ""Get"",
                data: {}
            }).done(function (re");
                WriteLiteral(@"sult) {
                $('#myModal').modal('show');
                $('#bodyModal').html(result);
            });
        }
    </script>

    <script>
        function MyEdit(id) {
            $.ajax({
                url: ""/Admin/EditField/"" + id,
                type: ""Get"",
                data: {}
            }).done(function (result) {
                $('#myModal').modal('show');
                $('#bodyModal').html(result);
            });
        }
    </script>

    <script>
        function MyDelete(id) {
            $.ajax({
                url: ""/Admin/DeleteField/"" + id,
                type: ""Get"",
                data: {}
            }).done(function (result) {
                $('#myModal').modal('show');
                $('#bodyModal').html(result);
            });
        }
    </script>

");
            }
            );
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OnlineShop.DataAccessLayer.Entities.Field>> Html { get; private set; }
    }
}
#pragma warning restore 1591
