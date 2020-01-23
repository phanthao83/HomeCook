using HC.Ultility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCook.Areas.Extension
{
    public static class PathConfiguration
    {
        /*
         * Return the directory path that Product Images upload from client to temparory folder
         */
        public static string GetProductImgUploadFolder(IWebHostEnvironment hostEnviroment, string folderName)
        {

            var webRootPath = hostEnviroment.WebRootPath;
            var uploadToFolder = Path.Combine(webRootPath, @"resource\\tmp", folderName);

            return uploadToFolder;

        }

        public static string GetProductImgStoreFolder(IWebHostEnvironment hostEnviroment = null )
        {
            var webRootPath =  @"\"; 
                if (hostEnviroment !=  null ) webRootPath =  hostEnviroment.WebRootPath ;
            return Path.Combine(webRootPath, @"resource\\product");
        }

        public static string GetAvatarUploadFolder(IWebHostEnvironment hostEnviroment, string folderName)
        {

            var webRootPath = hostEnviroment.WebRootPath;
            var uploadToFolder = Path.Combine(webRootPath, @"resource\\tmp", folderName);

            return uploadToFolder;

        }

        public static string GetAvatarStoreFolder(IWebHostEnvironment hostEnviroment = null)
        {
            var webRootPath = @"\";
            if (hostEnviroment != null) webRootPath = hostEnviroment.WebRootPath;
            return Path.Combine(webRootPath, @"resource\\avatar");
        }

    }
}
