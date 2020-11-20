using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FaceRecognitionDotNet;
using FaceRecognitionApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaceRecognitionApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class FaceSearchController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
		private readonly WantedTestPrivateContext _context;
        private readonly FaceRecognition _fr;

        private const string UPLOADED_FILE_PATH = "file";
        private const string PHOTO_FILES_FOLDER = "photos";
        private const string USER_NAME = "userName";
		
		public FaceSearchController(WantedTestPrivateContext context, IWebHostEnvironment env)
		{
            _appEnvironment = env;
            _fr = FaceRecognition.Create(
                string.Concat(_appEnvironment.WebRootPath, "\\MachineLearningFiles"));
			_context = context;
		}

        [HttpGet]
        [Route("test")]
        public string GetTestString()
        {
            return "SUCCESS!";
        }

        [HttpPost]
        [Route("createDescriptor")]
        public async Task<string> GetNewPhotoDescriptror([FromForm] IFormCollection form)
        {
            string uploadedFilePath = await GetUploadedFacePhotoPathAsync(form);
            // загружаем изображение из файла
            FaceEncoding encoding = CreateImageDescriptor(uploadedFilePath);

            string descriptor = encoding != null 
                ? JsonConvert.SerializeObject(encoding)
                : "";

            DeleteUploadedFacePhoto(uploadedFilePath);

            return descriptor;
        }

        [HttpGet]
        [Route("populate")]
        public async Task PopulateDescriptors()
        {
            var wantedPhotosFolderPath = String.Concat(
                _appEnvironment.WebRootPath, "\\WantedFacePhotos\\", "photoCollection");
            
            if(!Directory.Exists(wantedPhotosFolderPath)) 
            {
                Directory.CreateDirectory(wantedPhotosFolderPath);
            }

            var wantedFacePhotos = _context.LoadPhotoFilesTest();
            string filePath = "";

            foreach(var photo in wantedFacePhotos)
            {
                string photoName = string.Concat("\\photo.", photo.Mime);
                filePath = string.Concat(wantedPhotosFolderPath, photoName);
                await System.IO.File.WriteAllBytesAsync(filePath, photo.Photo);
                Image photoImage = FaceRecognition.LoadImageFile(filePath);                
                IEnumerable<Location> faceLocations = _fr.FaceLocations(photoImage);
                FaceEncoding faceEncoding = CreateImageDescriptor(filePath);
                string encodingSerialized = faceEncoding != null 
                    ? JsonConvert.SerializeObject(faceEncoding)
                    : "";
                PhotoFaceDescriptor desc;

                if(!string.IsNullOrEmpty(encodingSerialized))
                {
                    desc = new PhotoFaceDescriptor()
                    {
                        Id = Guid.NewGuid(), 
                        PhotoFaceId = photo.PhotoId, 
                        Descriptor = encodingSerialized
                    };

                    _context.PhotoFaceDescriptors.Add(desc);
                    _context.SaveChanges();
                }                

                System.IO.File.Delete(filePath);       
            }
        }

        [HttpPost]
        [Route("postPhotoToCompare")]
        public async Task<JsonResult> GetSerializedFacePhotoIdsAsync([FromForm] IFormCollection form)
        {
            string filePath = await GetUploadedFacePhotoPathAsync(form);
            var photoIdList = await GetEqualFacePhotosAsync(filePath);

            DeleteUploadedFacePhoto(filePath);

            return Json(photoIdList);
        }

        private async Task<IEnumerable<Guid>> RecognizeTheFacePhotoAsync(Dictionary<string, string> fileRoutes)
        {
            var photoIdList = new List<Guid>();
            var facePhotos = _context.LoadPhotoFilesTest();

            // загружаем изображение из файла
            Image uploadedImage = FaceRecognition.LoadImageFile(fileRoutes[UPLOADED_FILE_PATH]);
            // находим лицо на фотографии
            IEnumerable<Location> uploadedLocations = _fr.FaceLocations(uploadedImage);
            // кодируем изображение найденного лица
            var uploadImageEncoding = 
                _fr.FaceEncodings(uploadedImage, uploadedLocations).First();
            // определяем порог допустимой точности
            const double precision = 0.6d;
            bool hasMatch = false; 

            Image wantedImage; 
            IEnumerable<Location> wantedLocations; 
            FaceEncoding wantedImageEncoding; 
            int count = 0;

            foreach(var photo in facePhotos)
            {
                string photoName = string.Concat("\\", count.ToString(), ".", photo.Mime);
                string filePath = string.Concat(fileRoutes[PHOTO_FILES_FOLDER], photoName);

                await System.IO.File.WriteAllBytesAsync(filePath, photo.Photo);
                
                try
                {
                    wantedImage = FaceRecognition.LoadImageFile(filePath);
                    wantedLocations = _fr.FaceLocations(wantedImage);
                    wantedImageEncoding = _fr.FaceEncodings(wantedImage, wantedLocations).First();
                    hasMatch = FaceRecognition.CompareFace(uploadImageEncoding, wantedImageEncoding, precision);

                    if(hasMatch)
                    {
                        photoIdList.Add(photo.PhotoId);
                    }
                }
                catch(Exception)
                {
                    count++;
                    continue;
                }

                count++;
            }

            return photoIdList;
        }

        private async Task<IEnumerable<Guid>> GetEqualFacePhotosAsync(string uploadedFilePath)
        {
            FaceEncoding uploadedImageEncoding = CreateImageDescriptor(uploadedFilePath);

            var photoIdList = new List<Guid>();
            var photoFaceDescriptors = await _context.LoadPhotoDescriptorsTest();

            foreach(var desc in photoFaceDescriptors)
            {
                if(IsEqualFaceDescriptros(uploadedImageEncoding, desc))
                {
                    photoIdList.Add(desc.PhotoFaceId);
                }
            }

            return photoIdList;
        }

        private bool IsEqualFaceDescriptros(FaceEncoding uploadedImageEncoding, PhotoFaceDescriptor databasePhoto)
        {
            bool result = false;
            // определяем порог допустимой точности
            const double precision = 0.6d;

            if(databasePhoto != null)
            {
                FaceEncoding imageEncodingToCompare = 
                    JsonConvert.DeserializeObject<FaceEncoding>(databasePhoto.Descriptor);
                result = FaceRecognition.CompareFace(
                    uploadedImageEncoding, imageEncodingToCompare, precision);
            }

            return result;
        }

        private FaceEncoding CreateImageDescriptor(string filePath)
        {
            FaceEncoding uploadedImageEncoding = null;

            try
            {
                // создаём образ фотографии
                Image uploadedImage = FaceRecognition.LoadImageFile(filePath);
                // находим лицо на фотографии
                IEnumerable<Location> uploadedLocations = _fr.FaceLocations(uploadedImage);
                // кодируем изображение найденного лица
                uploadedImageEncoding = _fr.FaceEncodings(uploadedImage, uploadedLocations).First();
            }
            catch(Exception)
            {
                return null;
            }

            return uploadedImageEncoding;
        }

        private async Task<string> GetUploadedFacePhotoPathAsync(IFormCollection form)
        {
            var uploadedFile = form.Files.FirstOrDefault();
            var uploadedFileFolderPath = String.Concat(
                _appEnvironment.WebRootPath, "\\PhotoToRecognize\\", form[USER_NAME]); 

            if(!Directory.Exists(uploadedFileFolderPath)) 
            {
                Directory.CreateDirectory(uploadedFileFolderPath);
            }

            var uploadedFilePath = String.Concat(
                uploadedFileFolderPath, "\\", uploadedFile.FileName);
            
            using(var fileStream = new FileStream(uploadedFilePath, FileMode.Create))
            {   
                await uploadedFile.CopyToAsync(fileStream);
            }

            return uploadedFilePath;
        }

        private void DeleteUploadedFacePhoto(string filePath)
        {
            DirectoryInfo directoryInfo = null;

            if(System.IO.File.Exists(filePath))
            {
                FileStream file = System.IO.File.Open(filePath, FileMode.Open);
                FileInfo fileInfo = new System.IO.FileInfo(file.Name);
                directoryInfo = fileInfo.Directory;
                file.Close();
            }

            if(directoryInfo != null && directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
        }
    }
}