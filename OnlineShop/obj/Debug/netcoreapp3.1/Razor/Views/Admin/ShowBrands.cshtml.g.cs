#pragma checksum "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "811d350fb6234804f273668d1e43423ae68c4b55"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ShowBrands), @"mvc.1.0.view", @"/Views/Admin/ShowBrands.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"811d350fb6234804f273668d1e43423ae68c4b55", @"/Views/Admin/ShowBrands.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b853c2693020103458b52d8246f923ca3fd89014", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ShowBrands : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OnlineShop.DataAccessLayer.Entities.Brand>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
      
        ViewData["Title"] = "برند ها";
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
#line 29 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
                       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 32 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
                       Write(Html.DisplayNameFor(model => model.NotShow));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            عملیات\r\n                        </th>\r\n\r\n                    </tr>\r\n\r\n                </thead>\r\n\r\n                <tbody>\r\n\r\n");
#nullable restore
#line 44 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
#nullable restore
#line 48 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 51 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
                           Write(Html.CheckBoxFor(modelItem => item.NotShow));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1747, "\"", 1776, 3);
            WriteAttributeValue("", 1757, "MyDetails(", 1757, 10, true);
#nullable restore
#line 54 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
WriteAttributeValue("", 1767, item.Id, 1767, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1775, ")", 1775, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-xs btn-info\">جزئیات</a> |\r\n                                <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\'", 1859, "\'", 1885, 3);
            WriteAttributeValue("", 1869, "MyEdit(", 1869, 7, true);
#nullable restore
#line 55 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
WriteAttributeValue("", 1876, item.Id, 1876, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1884, ")", 1884, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-xs btn-warning\">اصلاح</a> |\r\n                                <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\'", 1970, "\'", 1998, 3);
            WriteAttributeValue("", 1980, "MyDelete(", 1980, 9, true);
#nullable restore
#line 56 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
WriteAttributeValue("", 1989, item.Id, 1989, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1997, ")", 1997, 1, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn-xs btn-danger\">حذف</a>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 59 "D:\Project\OnlineShop\OnlineShop\OnlineShop\Views\Admin\ShowBrands.cshtml"
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
                    url: ""/Admin/AddBrand");
                WriteLiteral(@"/"",
                    type: ""Get"",
                    data: {}
                }).done(function (result) {
                    $('#myModal').modal('show');
                    $('#bodyModal').html(result);
                });
            }
        </script>

        <script>
            function MyEdit(id) {
                $.ajax({
                    url: ""/Admin/EditBrand/"" + id,
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
                    url: ""/Admin/DeleteBrand/"" + id,
                    type: ""Get"",
                    data: {}
                }).done(function (result) {
                    $('#myModal').modal('show');
                    $('#bodyModal').html(result);
         ");
                WriteLiteral(@"       });
            }
        </script>

        <script>
            function MyDetails(id) {
                $.ajax({
                    url: ""/Admin/DetailsBrand/"" + id,
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
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OnlineShop.DataAccessLayer.Entities.Brand>> Html { get; private set; }
    }
}
#pragma warning restore 1591