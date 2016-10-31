using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fizzler;
using System.IO;
using System.Web;
namespace Adayroi
{
    class Program
    {
        static void Main(string[] args)
        {
            eventCrawler();
        }
        static void eventCrawler()
        {
            try
            {
                //Khai báo đường dẫn URL web cần lấy nội dung HTML
                for (int i = 0; i < 100; i++)
                {
                    string _url = "https://www.adayroi.com/thuc-pham-r591?p=";

                    HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();

                    htmlWeb.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:31.0) Gecko/20100101 Firefox/31.0";

                    
                    HtmlAgilityPack.HtmlDocument htmlDoc = htmlWeb.Load(_url + i);
                    string _html = htmlDoc.DocumentNode.InnerHtml;
                    _html = HttpUtility.HtmlDecode(_html);
                    HtmlAgilityPack.HtmlNode _nodThreads = htmlDoc.DocumentNode.SelectSingleNode(@"//div[@class='row body-list-item']");                
                    HtmlAgilityPack.HtmlNodeCollection nodChuDe = _nodThreads.SelectNodes(@"div");

                    //Khai báo biến tạm để hiện thị kết quả
                    string ketqua = "";

                    //Duyet qua các nod nodChuDe vừa select được

                    foreach (var n in nodChuDe)
                    {//data-brand-id data-category-id  data-product-item-id  data-merchant-id
                        string brand_id = n.SelectSingleNode("div").Attributes["data-brand-id"].Value.ToString().Trim();
                        string category_id = n.SelectSingleNode("div").Attributes["data-category-id"].Value.ToString().Trim();
                        string product_item_id = n.SelectSingleNode("div").Attributes["data-product-item-id"].Value.ToString().Trim();
                        string merchant_id = n.SelectSingleNode("div").Attributes["data-merchant-id"].Value.ToString().Trim();
                        string urlImage = n.SelectSingleNode("div/div[1]/span/a/img").Attributes["data-other-src"].Value.ToString().Trim();
                        string name = n.SelectSingleNode("div/div[2]/div/h4").InnerText.Trim();
                        name = HttpUtility.HtmlDecode(name);
                        string gia = n.SelectSingleNode("div/div[2]/div/div/span").InnerText.Trim();
                        string urlSanPham = n.SelectSingleNode("div/div[1]/span/a").Attributes["href"].Value.ToString();
                        if (string.IsNullOrEmpty(urlImage) || string.IsNullOrEmpty(urlImage)|| string.IsNullOrEmpty(urlImage) || string.IsNullOrEmpty(urlImage))
                        {
                            break;
                        }

                        //HtmlAgilityPack.HtmlDocument htmlDocSanpham = htmlWeb.Load("https://www.adayroi.com" + urlSanPham);
                        //string _htmlSP = htmlDocSanpham.DocumentNode.InnerHtml;
                        //string _ItemInFoBlock = htmlDocSanpham.DocumentNode.SelectSingleNode(@"//div[@id='product_excerpt']").InnerText; //table table-bordered
                        //string _Product_description = htmlDocSanpham.DocumentNode.SelectSingleNode(@"//div[@id='product_description']").InnerHtml;

                        ketqua += brand_id + " $ " + category_id + " $ " + product_item_id + " $ " + merchant_id + " $ " + name + " $ " + gia + " $ " + urlImage + "\n";
                    }
                    using (FileStream fs = new FileStream(@"F:\DATA.txt", FileMode.Append, FileAccess.Write))
                    using (StreamWriter w =  new StreamWriter(fs, Encoding.UTF8))
                    {
                        ketqua = HttpUtility.HtmlDecode(ketqua);
                        w.WriteLine(ketqua+ "\n");
                        Console.WriteLine("Trang " + i + " OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}

