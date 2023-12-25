﻿using MongoDB.Driver;
using MongodbApp.Contracts;
using MongodbApp.Dtos;
using MongodbApp.Models;

namespace MongodbApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        IMongoCollection<Product> productCollection;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongodbConnection");
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            productCollection = database.GetCollection<Product>("product");
        }

       
        public async Task<ResponseModel<bool>> Insert(ProductDto productDto)
        {
            var response = new ResponseModel<bool>();
            if (productDto == null)
            {
                response.Code = "99";
                response.Message = "Invalid request";
                response.Data = false;
                return response;
            }
            var product = new Product
            {
                ProductCode = productDto.ProductCode,
                Price = productDto.Price,
                ProductName = productDto.ProductName,
            };

            await productCollection.InsertOneAsync(product);

            response.Message = "Operation Successful";
            response.Code = "00";
            response.Data = true;
            return response;
        }

        public async Task<ResponseModel<bool>> Update(ProductDto productDto)
        {
            var response = new ResponseModel<bool>();

            var filterDefinition = Builders<Product>.Filter.Eq(a => a.ProductCode, productDto.ProductCode);
            if (filterDefinition == null)
            {
                response.Code = "99";
                response.Message = $"Record with product code {productDto.ProductCode} was not found";
                response.Data = false;
                return response;
            }
            var updateDefinition = Builders<Product>.Update
                .Set(a => a.ProductName, productDto.ProductName)
                .Set(a => a.Price, productDto.Price);

            await productCollection.UpdateOneAsync(filterDefinition, updateDefinition);

            response.Code = "00";
            response.Data = true;
            response.Message = "Operation Successful";
            return response;

        }

        public async Task<ResponseModel<bool>> Delete(string productCode)
        {
            var response = new ResponseModel<bool>();

            var filterDefinition = Builders<Product>.Filter.Eq(a=>a.ProductCode, productCode);
            if (!filterDefinition == null) 
            {
                response.Code = "99";
                response.Message = $"Record with product code {productCode } was not found";
                response.Data = false;
                return response;
            }

            await productCollection.DeleteOneAsync(filterDefinition);

            response.Code = "00";
            response.Message = $"Operation Successful";
            response.Data = true;
            return response;
        }

        public async Task<ResponseModel<List<ProductResponseDto>>> GetAll()
        {
            var response = new ResponseModel<List<ProductResponseDto>>();

            var filterDefiniton = Builders<Product>.Filter.Empty;
            var products = await productCollection.Find(filterDefiniton).ToListAsync();
            if (products.Count == 0)
            {
                response.Code = "99";
                response.Message = $"No Record Found";
                return response;
            }

            var productsList = new List<ProductResponseDto>();
            foreach (var item in products)
            {
                productsList.Add(new ProductResponseDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    ProductCode = item.ProductCode,
                });
            }

            response.Code = "00";
            response.Message = $"Operation Successful";
            response.Data = productsList;
            return response;
        }
    }
}