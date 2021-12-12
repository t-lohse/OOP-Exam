using System.Collections.Generic;
using System.Linq;
using Xunit;
using Stregsystem.Exceptions;

namespace Stregsystem.Tests.StregsystemTests
{
    public class CatalogTests
    {
        [Fact]
        public void SearchNonExistingProduct()
        {
            var sts = new Stregsystem(productPath: "../../../products_test.csv");
            Assert.Throws<NonExistingProductException>(() => sts.GetProductById(190101));
        }
        
        [Fact]
        public void SearchExistingProduct()
        {
            var sts = new Stregsystem(productPath: "../../../products_test.csv");
            Assert.Equal(sts.GetProductById(10), new Product(10, "Cocio",1600));
        }
        
        [Fact]
        public void Products()
        {
            List<Product> expected = new List<Product>()
            {
                new Product(1, "Diverse",100,false),
                new Product(2, "½L Letmælk",450,false),
                new Product(3, "¼L Letmælk",250,false),
                new Product(4, "¼L Skummetmælk",225,false),
                new Product(5, "¼L Kærnemælk",225,false),
                new Product(6, "½L vand & kildevand excl. pant",1200),
                new Product(7, "Øl(f.eks. Thy, Guld, 47, T-FF)",1000),
                new Product(8, "Øl(Tuborg/Classic/Carlsberg/Rød)",650),
                new Product(9, "Øl(Staropramen, Corona, Budweis)",1500),
                new Product(10, "Cocio",1600),

            };
            var sts = new Stregsystem(productPath: "../../../products_test.csv");
            
            Assert.True(CompareProductList(expected, sts.Products));
        }
        
        [Fact]
        public void ActiveProducts()
        {
            List<Product> expected = new List<Product>()
            {
                //new Product(1, "Diverse",100,false),
                //new Product(2, "½L Letmælk",450,false),
                //new Product(3, "¼L Letmælk",250,false),
                // Product(4, "¼L Skummetmælk",225,false),
                //new Product(5, "¼L Kærnemælk",225,false),
                new Product(6, "½L vand & kildevand excl. pant",1200),
                new Product(7, "Øl(f.eks. Thy, Guld, 47, T-FF)",1000),
                new Product(8, "Øl(Tuborg/Classic/Carlsberg/Rød)",650),
                new Product(9, "Øl(Staropramen, Corona, Budweis)",1500),
                new Product(10, "Cocio",1600),

            };
            var sts = new Stregsystem(productPath: "../../../products_test.csv");
            
            Assert.True(CompareProductList(expected, sts.ActiveProducts));
        }

        private bool CompareProductList(List<Product> expected, List<Product> actual) =>
            expected.All(x => actual.Any(y => y.Id == x.Id)) &&
            actual.All(x => expected.Any(y => y.Id == x.Id));
    }
}