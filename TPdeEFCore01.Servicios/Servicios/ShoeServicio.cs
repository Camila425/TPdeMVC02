using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
	public class ShoeServicio : IShoeServicio
	{
		private readonly IShoesRepositorio _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISizeRepositorio _sizeRepositorio;
        private readonly IShoeImagesRepositorio _shoeImagesRepositorio;

        public ShoeServicio(IShoesRepositorio repository, IUnitOfWork unitOfWork, ISizeRepositorio sizeRepositorio
            , IShoeImagesRepositorio shoeImagesRepositorio)
		{
			_repository = repository ?? throw new ArgumentException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
			_sizeRepositorio = sizeRepositorio;
			_shoeImagesRepositorio = shoeImagesRepositorio;
		}

		public void Delete(Shoe shoe)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(shoe);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Shoe shoe)
		{
			return _repository!.Exist(shoe);
		}

        public Shoe? Get(Expression<Func<Shoe, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _repository!.Get(filter, propertiesNames, tracked);
        }

        public IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null,
            Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

        public List<int> GetAssignedColorsForShoe(Shoe shoe)
        {
            return _repository!.GetAssignedColorsForShoe(shoe);
        }

        public List<int> GetAssignedSizeForShoe(Shoe shoe)
		{
			return _repository!.GetAssignedSizeForShoe(shoe);
		}

		public Shoe? GetShoeId(int shoeId)
		{
			return _repository!.GetShoeId(shoeId);
		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}
        public Shoe SaveShoeWithImages(Shoe shoe, List<IFormFile>? imageFiles, string wwwWebRoot)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                if (shoe.ShoeId == 0)
                {
                    _repository?.Add(shoe);
                    _unitOfWork.SaveChanges(); 
                }
                else
                {
                    _repository?.Update(shoe);
                }

                if (shoe.ShoeId == 0)
                {
                    _unitOfWork.Rollback();
                    throw new InvalidOperationException("Failed to generate ShoeId");
                }

                if (imageFiles != null && imageFiles.Any())
                {
                    var permittedExtensions = new[] { ".png", ".jpg", ".jpeg", ".gif" };

                    if (shoe.ShoeId != 0)
                    {
                        var existingImages = _shoeImagesRepositorio.GetShoeByShoeImageId(shoe.ShoeId);
                        foreach (var existingImage in existingImages)
                        {
                            string oldImagePath = Path.Combine(wwwWebRoot, existingImage.ImageUrl!.TrimStart('/'));
                            if (File.Exists(oldImagePath))
                            {
                                File.Delete(oldImagePath);
                            }

                            _shoeImagesRepositorio.Delete(existingImage);
                        }
                    }

                    foreach (var imageFile in imageFiles)
                    {
                        var fileExtension = Path.GetExtension(imageFile.FileName).ToLower(); 

                        if (!permittedExtensions.Contains(fileExtension))
                        {
                            _unitOfWork.Rollback();
                            throw new InvalidOperationException("invalid extension");
                        }

                        string fileName = $"{Guid.NewGuid()}{fileExtension}";
                        string pathName = Path.Combine(wwwWebRoot, "images", fileName);

                        using (var fileStream = new FileStream(pathName, FileMode.Create))
                        {
                            imageFile.CopyTo(fileStream);
                        }
                        var shoeImage = new ShoeImage
                        {
                            ShoeId = shoe.ShoeId, 
                            ImageUrl = $"/images/{fileName}"
                        };
                        _shoeImagesRepositorio.Add(shoeImage);
                    }
                }

                _unitOfWork.Commit();
                return shoe;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
