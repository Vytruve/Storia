using Storia.Application.DTOs;
using Storia.Application.Interfaces;
using Storia.Core.Entities;
using Storia.Core.Enums;
using Storia.Core.Interfaces; // <-- Mude para a interface do UnitOfWork
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork; // <-- Injetar IUnitOfWork

        public ProductService(IUnitOfWork unitOfWork) // <-- Modificar construtor
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var existingProduct = await _unitOfWork.Products.GetBySkuAsync(productDto.Sku);
            if (existingProduct != null)
            {
                throw new InvalidOperationException($"Um produto com o SKU '{productDto.Sku}' já existe.");
            }

            var product = MapToEntity(productDto);

            // CORREÇÃO: Chamar o método síncrono 'Add'
            _unitOfWork.Products.Add(product);
            // CORREÇÃO: Salvar as alterações através do Unit of Work
            await _unitOfWork.CompleteAsync();

            return MapToDto(product);
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productDto.Id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID '{productDto.Id}' não encontrado.");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Sku = productDto.Sku;
            product.Barcode = productDto.Barcode;
            product.Price = productDto.Price;
            product.UnitOfMeasure = Enum.Parse<UnitOfMeasure>(productDto.UnitOfMeasure, true);
            product.UpdatedAt = DateTimeOffset.UtcNow;

            // CORREÇÃO: Chamar o método síncrono 'Update'
            _unitOfWork.Products.Update(product);
            // CORREÇÃO: Salvar as alterações através do Unit of Work
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID '{id}' não encontrado.");
            }

            // CORREÇÃO: Chamar o método síncrono 'Delete'
            _unitOfWork.Products.Delete(product);
            // CORREÇÃO: Salvar as alterações através do Unit of Work
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddStockAsync(Guid productId, decimal quantity, decimal purchasePrice, string? lotNumber, DateTimeOffset? expirationDate)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID '{productId}' não encontrado para adicionar estoque.");
            }

            var newLot = new Lot(productId, quantity, purchasePrice, DateTimeOffset.UtcNow)
            {
                LotNumber = lotNumber,
                ExpirationDate = expirationDate
            };

            product.Lots.Add(newLot);
            product.UpdatedAt = DateTimeOffset.UtcNow;

            // CORREÇÃO: Chamar o método síncrono 'Update'
            _unitOfWork.Products.Update(product);
            // CORREÇÃO: Salvar as alterações através do Unit of Work
            await _unitOfWork.CompleteAsync();
        }

        private ProductDto MapToDto(Product product)
        {
            // ... (código existente sem alterações)
            return new ProductDto { /* ... */ };
        }

        private Product MapToEntity(ProductDto dto)
        {
            // CORREÇÃO: Não tentar atribuir o Id aqui, pois ele é gerado no construtor.
            var product = new Product(
                dto.Name,
                dto.Sku,
                dto.Price,
                Enum.Parse<UnitOfMeasure>(dto.UnitOfMeasure, true)
            )
            {
                // O Id já foi gerado pelo construtor. Se for uma atualização, o Id do DTO será usado
                // para buscar a entidade, não para criar uma nova.
                Description = dto.Description,
                Barcode = dto.Barcode
            };

            // Se for uma atualização, o Id do DTO será usado para buscar a entidade,
            // não para criar uma nova. A lógica de atualização já lida com isso.
            // A lógica de criação gera um novo Guid automaticamente.
            // Portanto, a linha 'Id = ...' foi removida.

            return product;
        }
    }
}