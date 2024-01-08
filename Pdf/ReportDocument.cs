using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bislerium_cafe_pos.Models;
using Microsoft.Maui.Controls;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;


namespace bislerium_cafe_pos.Pdf
{

    public class ReportDocument : IDocument
    {
        public Report ReportObj;

        public ReportDocument(Report model)
        {
            ReportObj = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {

            container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
            });
        }

        // This method composes the header of the PDF document.
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            string pdfTitle = $"Bislerium Cafe {ReportObj.ReportType} Sales Transaction Report - ({ReportObj.ReportDate})";


            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{pdfTitle}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").Medium();
                        text.Span($"{DateTime.Now}").Medium();
                    });
                });
            });
        }

        // This method composes the content of the PDF document.
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                // Sales Transactions
                column.Item().Element(ComposeSalesTransactionsHeader);
                column.Item().PaddingTop(10).Element(ComposeSalesTransactionsTable);

                // Top 5 Most Purchased Coffee 
                column.Item().PaddingTop(30).Element(ComposeHeadingForTopCoffees);
                column.Item().PaddingTop(10).Element(ComposeMostPurchasedCoffesTable);

                // Top 5 Most Added Add-In Items
                column.Item().PaddingTop(30).Element(ComposeHeadingForTopAddInsItems);
                column.Item().PaddingTop(10).Element(ComposeMostPurchasedAddInsItemTable);
            });
        }

        // This method composes the header of the Sales Transactions table.
        void ComposeSalesTransactionsHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(16).SemiBold();

            string title = $"{ReportObj.ReportType} Sales Transaction Report - ({ReportObj.ReportDate})";

            container.Row(row =>
            {
               
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(titleStyle);
                  
                    column.Item().PaddingTop(2).Text(text =>
                    {
                        text.Span("Total Revenue: ").FontSize(14);
                        text.Span($"Rs. {ReportObj.TotalRevenue}").FontSize(14);
                    });
                });
            });
          
        }

        // This method generates the Sales Transactions table.
        void ComposeSalesTransactionsTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(130);
                    columns.ConstantColumn(90);
                    columns.ConstantColumn(80);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Customer Name");
                    header.Cell().Element(CellStyle).Text("Phone Number");
                    header.Cell().Element(CellStyle).Text("Employee");
                    header.Cell().Element(CellStyle).Text("Total Amount");
                    header.Cell().Element(CellStyle).Text("Discount Amount");
                    header.Cell().Element(CellStyle).Text("Grand Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var order in ReportObj.Orders)
                {
                    table.Cell().Element(CellStyle).Text((ReportObj.Orders.IndexOf(order) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(order.CustomerName);
                    table.Cell().Element(CellStyle).Text(order.CustomerPhoneNum);
                    table.Cell().Element(CellStyle).Text(order.EmployeeUserName);
                    table.Cell().Element(CellStyle).Text($"Rs.{order.OrderTotalAmount}");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.DiscountAmount}");
                    table.Cell().Element(CellStyle).Text($"Rs.{order.OrderTotalAmount - order.DiscountAmount}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        // This method composes the header of the Top 5 Most Purchased Coffees.
        void ComposeHeadingForTopCoffees(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(16).SemiBold();

            string title = $"Top 5 Most Purchased Coffee - ({ReportObj.ReportDate})";

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(titleStyle);

                });
            });


        }

        // This method generates the Top 5 Most Purchased Coffees table.
        void ComposeMostPurchasedCoffesTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Coffee Type");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var coffee in ReportObj.CoffeeList)
                {
                    table.Cell().Element(CellStyle).Text((ReportObj.CoffeeList.IndexOf(coffee) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(coffee.ItemName);
                    table.Cell().Element(CellStyle).Text(coffee.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        // This method composes the header of the Top 5 Most Added Add-In Items.
        void ComposeHeadingForTopAddInsItems(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(16).SemiBold();

            string title = $"Top 5 Most Added Add-In Items - ({ReportObj.ReportDate})";

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{title}").Style(titleStyle);

                });
            });


        }

        // This method generates the Top 5 Most Added Add-In Items table.
        void ComposeMostPurchasedAddInsItemTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(20);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(70);

                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Add-In Item Name");
                    header.Cell().Element(CellStyle).Text("Quantity");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var addInItem in ReportObj.AddInsList)
                {
                    table.Cell().Element(CellStyle).Text((ReportObj.AddInsList.IndexOf(addInItem) + 1).ToString());
                    table.Cell().Element(CellStyle).Text(addInItem.ItemName);
                    table.Cell().Element(CellStyle).Text(addInItem.Quantity.ToString());

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

    }
}
